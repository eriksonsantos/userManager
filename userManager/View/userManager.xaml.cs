using MahApps.Metro.Controls;
using userManager.ViewModel;

namespace userManager.View
{
    /// <summary>
    /// Interaction logic for userManager.xaml
    /// </summary>
    public partial class userManager : MetroWindow
    {
        public userManager()
        {
            userManagerModel vm = new userManagerModel();
            this.DataContext = vm;

            InitializeComponent();

        }
    }
}
