using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using userManager.Model;
using userManager.Model.Database;

namespace userManager.ViewModel.controls
{
    public class addUserModel
    {
        public ICommand InsertUser {get;  set; }
        public string Name { get;  set; } = "";
        public string Address { get;  set; } = "";
        public string Telephone { get;  set; } = "";
        public addUserModel()
        {
            InsertUser = new RelayCommand(insertUserInDatabase);
        }

        public void insertUserInDatabase()
        {
            try
            {
                if (Name == "" || Address == "" || Telephone == "")
                {
                    MessageBox.Show("Os dados estão incompletos. Verifique e tente novamente");
                    return;

                }
                else
                {
                    using (var databaseConfig = new DatabaseConfig())
                    {
                        Users user = new Users(Name, Address, Convert.ToInt32(Telephone));
                        databaseConfig.publishValue<Users>(user);
                    }
                    MessageBox.Show("Usuario adicionado com sucesso");
                    CloseWindow();

                }
            }catch(Exception Ex)
            {
                MessageBox.Show($"Error in send message with error: {Ex.Message}");
            }
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
