using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using HairDress.PL;
using HairDress.VOL;

namespace HairDress
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /*
            var ops = new List<Operation> { new Operation { Description = "1", Price = 1 }, new Operation { Description = "2", Price = 2 }, new Operation { Description = "3", Price = 3 } };
            DatabaseConnection.Instance.InsertOrUpdateOperation(ops.ToArray());
            var per = new Person
            {
                FirstName = "Umut",
                LastName = "Akkaya",
                BirthDay = new DateTime(1996, 12, 5),
                OperationsList =
                    new List<Operation2Person> {new Operation2Person {Date = DateTime.Now, Operations = ops}},
                Phones = new List<Phone> {new Phone {PhoneData = "5366927997"}, new Phone {PhoneData = "2322598686"}},
                Picture = new FileStream(@"D:\Downloads\p-2.jpg", FileMode.Open)
            };
            DatabaseConnection.Instance.InsertOrUpdatePerson(per);
             */

            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            MainFrame.Navigate(new MainViewModal());
        }
    }
}