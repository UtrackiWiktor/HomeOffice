﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace HomeOffice.Views
{
    /// <summary>
    /// Logika interakcji dla klasy EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        User user;
        public EmployeeView()
        {
            user = ((MainWindow)Application.Current.MainWindow).GetUser();
            InitializeComponent();
        }
        void LoadTasks(object sender, RoutedEventArgs e)
        {

        }

        private void DoneUndone_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
