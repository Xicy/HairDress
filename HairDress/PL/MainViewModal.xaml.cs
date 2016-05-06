using System.Windows;
using System.Windows.Controls;

namespace HairDress.PL
{
    /// <summary>
    ///     Interaction logic for Main.xaml
    /// </summary>
    public partial class MainViewModal : Page
    {
        public MainViewModal()
        {
            InitializeComponent();
        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {
            var a = new UsersViewModal();
            NavigationService?.Navigate(a);
        }
    }
}