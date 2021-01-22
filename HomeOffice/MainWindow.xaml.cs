using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HomeOffice.classes.Users;
using MySql.Data.MySqlClient;
using HomeOffice.Data;
using System.Xaml;
using Microsoft.EntityFrameworkCore;
using HomeOffice.classes.Tasks;
using HomeOffice.classes.Units;

namespace HomeOffice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            

            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {

            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
        }
    }
}
