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
using System.Collections.ObjectModel;
using userManager.View.controls;

namespace userManager.ViewModel
{
    public class userManagerModel
    {
        public ICommand AddUser { get; private set; }
        public ObservableCollection<Users> dataUsers { get; private set; }
        public userManagerModel()
        {
            try { 

            this.AddUser = new RelayCommand(AddingUser);
                using (var databaseConfig = new DatabaseConfig())
                {
                    Users item = new Users();
                    databaseConfig.createTableIfNotExist<Users>();
                    List<Users> users = databaseConfig.getValues<Users>(item);
                    dataUsers = new ObservableCollection<Users>();

                    foreach (var user in users) dataUsers.Add(user);
                }

            }catch(Exception ex)
            {

            }
        }

        public void updateDataGrid()
        {
            try
            {
                dataUsers.Clear();
                using (var databaseConfig = new DatabaseConfig())
                {
                    Users item = new Users();
                    List<Users> users = databaseConfig.getValues<Users>(item);
                    foreach (var user in users) dataUsers.Add(user);
                }
            }catch(Exception ex) { }
        }
        public void AddingUser()
        {
            addUserPopPup poppup = new addUserPopPup();
            poppup.ShowDialog();
            updateDataGrid();
        }
    }
}
