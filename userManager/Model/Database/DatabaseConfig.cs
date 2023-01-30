
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace userManager.Model.Database
{
    public class DatabaseConfig : IDisposable
    {
        public MySqlConnection connection;
        public DatabaseConfig()
        {
            try
            {
                var connectionConfig = "server=localhost;Database=users;Uid=root;Pwd=1234567";
                this.connection = new MySqlConnection(connectionConfig);

            }
            catch (Exception ex)
            {

            }

        }

        public void createTableIfNotExist<T>()
        {
            try
            {
                this.connection.Open();
                MySqlCommand mySqlCommand = this.connection.CreateCommand();
                var type = typeof(T);

                List<string> properties = new List<string>();
                List<string> types = new List<string>();

                foreach (var prop in type.GetProperties())
                {
                    properties.Add(prop.Name);
                    types.Add(prop.PropertyType.Name);
                }

                string query = $"CREATE TABLE IF NOT EXISTS {type.Name} (";
                for (var i = 0; i < types.Count; i++)
                {
                    switch (types[i].ToLower())
                    {
                        case "int32":

                            if (properties[i].ToLower() == "id")
                                query = (i == 0) ? $"{query} {properties[i]} INT NOT NULL PRIMARY KEY AUTO_INCREMENT" :
                                    $"{query}, {properties[i]} INT";
                            else
                                query = (i == 0) ? $"{query} {properties[i]} INT" :
                                         $"{query}, {properties[i]} INT";
                            break;

                        case "string":
                            query = (i == 0) ? $"{query} {properties[i]} VARCHAR(50)" :
                                   $"{query}, {properties[i]} VARCHAR(50)";

                            break;

                        case "double":
                            query = (i == 0) ? $"{query} {properties[i]} DOUBLE" :
                                   $"{query}, {properties[i]} DOUBLE";

                            break;

                    }
                }
                query = $"{query})";
                mySqlCommand.CommandText = query;
                mySqlCommand.ExecuteReader();
                this.connection.Close();
            }catch(Exception ex) { }
        }


        public void publishValue<T>(T obj)
        {
            this.connection.Open();
            MySqlCommand mySqlCommand = this.connection.CreateCommand();
            var type = typeof(T);

            string query = $"INSERT INTO {obj.GetType().Name} (";
            string column = "";
            int i = 0;
            foreach(var prop in type.GetProperties())
            {
                var aux = (prop.Name.ToLower() != "id") ? $"{prop.Name}" : "";
                column = (i ==0)? $"{aux}": $"{column},{aux}";
                i = (column=="")?i=0:i+1;

            }
            query = $"{query}{column}) VALUES (";
            i = 0;
            column = "";
            foreach (var prop in type.GetProperties())
            {
                var aux = (prop.Name.ToLower() != "id") ? $"'{prop.GetValue(obj)}'" : "";
                column = (i == 0) ? $"{aux}" : $"{column},{aux}";
                i = (column == "") ? i = 0 : i + 1;

            }
            query = $"{query}{column})";
            mySqlCommand.CommandText = query;
            mySqlCommand.ExecuteReader();
            this.connection.Close();

        }


        public List<T> getValues<T>(T item)
        {
            this.connection.Open();
            MySqlCommand mySqlCommand = this.connection.CreateCommand();
            var type = item.GetType();
            mySqlCommand.CommandText = "SELECT * from " + type.Name.ToLower();
            List<T> values = new List<T>();

            using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    T obj = (T)Activator.CreateInstance(type);
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var prop = type.GetProperty(reader.GetName(i));
                        if (prop != null && prop.CanWrite)
                        {
                            prop.SetValue(obj, reader.GetValue(i));
                        }
                    }
                    values.Add(obj);
                }
            }
            this.connection.Close();
            return values;
        }

        public void Dispose() => this.connection.Close();

    }
}