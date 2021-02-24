using HomeOffice.classes.Users;
using System;
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
using System.Windows.Shapes;
using HomeOffice.classes.Passwords;
using HomeOffice.Views;
using System.Globalization;
using HomeOffice.ViewModels;

namespace HomeOffice
{
    public partial class UserPanel : Window
    {
        User user;
        public UserPanel()
        {
            user = null;
            InitializeComponent();
        }

        public UserPanel(User u)
        {
            InitializeComponent();
            user = u;
            if ((UserRoles)user.UserGroup == UserRoles.Administrator)
            {
                var viewModel = new AdminViewModel();
                this.DataContext = viewModel;
            }
            else if ((UserRoles)user.UserGroup == UserRoles.Employee)
            {
                var viewModel = new EmployeeViewModel();
                this.DataContext = viewModel;
            }
            else if ((UserRoles)user.UserGroup==UserRoles.Manager)
            {
                var viewModel = new ManagerViewModel();
                this.DataContext = viewModel;
            }
            
        }

        private void UserPanel_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Windows[0].Show();
        }
    }
}
