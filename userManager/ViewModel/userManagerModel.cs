using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using userManager.Model.Database;
using userManager.Model;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace userManager.ViewModel
{
    public class userManagerModel
    {
        public ICommand AddUser { get; private set; }
        public userManagerModel()
        {

            this.AddUser = new RelayCommand(AddingUser);
            using(var databaseConfig = new DatabaseConfig())
            {
                Users item = new Users(1, "erikson", "erere", DateTime.Now, 5);
                databaseConfig.createTableIfNotExist<Users>();
                List<Users> users = databaseConfig.getValues<Users>(item);

                databaseConfig.publishValue<Users>(item);

            }
        }

        public void AddingUser()
        {
            MessageBox.Show("ADICIONAR USUARIO");
        }
    }
}
