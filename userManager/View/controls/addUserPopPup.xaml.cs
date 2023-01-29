using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using userManager.ViewModel.controls;

namespace userManager.View.controls
{
    /// <summary>
    /// Interaction logic for addUserPopPup.xaml
    /// </summary>
    public partial class addUserPopPup : Window
    {
        public addUserPopPup()
        {
            this.DataContext = new addUserModel();
            InitializeComponent();
        }
        

        private void TelephoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
    }
}
