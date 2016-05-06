using System.Windows.Controls;
using HairDress.VOL;

namespace HairDress.PL
{
    /// <summary>
    ///     Interaction logic for UserViewModal.xaml
    /// </summary>
    public partial class UsersViewModal : Page
    {
        public UsersViewModal()
        {
            InitializeComponent();
            this.DataContext = DatabaseConnection.Instance.Person.FindAll();
        }

        public static explicit operator Person[](UsersViewModal x)
        {
            return (Person[])x.DataContext;
        }
    }
}