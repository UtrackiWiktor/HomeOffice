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
using HomeOffice.classes.Tasks;
using HomeOffice.classes.Users;

namespace HomeOffice
{
    /// <summary>
    /// Logika interakcji dla klasy AddToDictionary.xaml
    /// </summary>
    public partial class AddToDictionary : Window
    {
        User AuthorizedUser;
        public Delegate AddTaskDel;
        public AddToDictionary(User user)
        {
            if((UserRoles)user.UserGroup==UserRoles.Administrator)
            {
                AuthorizedUser = new Administrator(user);
            }
            else if ((UserRoles)user.UserGroup == UserRoles.Manager)
            {
                AuthorizedUser = new Manager(user);
            }
            InitializeComponent();
        }


        private void AddTask_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int unit;
                if (!String.IsNullOrWhiteSpace(TaskDesc.Text) && !String.IsNullOrWhiteSpace(TaskTitle.Text) && int.TryParse(Unit.Text, out unit))
                {

                    AuthorizedUser.AddToTaskDictionary(new TaskDictionary(TaskTitle.Text, TaskDesc.Text, unit));
                    MessageBox.Show("Task was added successfully.");
                    AddTaskDel.DynamicInvoke();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You probably provided incorrect data. Please correct it");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("You probably provided incorrect data. Please correct it");
            }
        }
    

        private void TaskDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            CharLimit.Content = TaskDesc.Text.Length.ToString() + "/225";
            if (TaskDesc.Text.Length>225)
            {
                TaskDesc.Text = TaskDesc.Text.Substring(0, 225);
                TaskDesc.CaretIndex = TaskDesc.Text.Length;
            }
        }
    }
}
