using System.Windows;
using System.Windows.Controls;
using HomeOffice.classes.Users;
using HomeOffice.classes.Tasks;
using System.Collections.Generic;

namespace HomeOffice.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ManagerView.xaml
    /// </summary>
    public partial class ManagerView : UserControl
    {
        Manager manager;
        Task t;
        TaskDictionary task;
        public delegate void RefreshList();
        public event RefreshList RefreshListEvent;
        public ManagerView()
        {
            InitializeComponent();
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //tutaj
        private void reportButton_Click(object sender, RoutedEventArgs e)
        {
          //  List<String> list = manager.task;
          //  System.Windows.Forms.MessageBox.Show(manager.PrintTheReport(list));
        }

        private void tasksGrid_Loaded(object sender, RoutedEventArgs e)
        {
           // tasksGrid.ItemsSource = t.AllTasksToList();
        }

        private void empGrid_Loaded(object sender, RoutedEventArgs e)
        {
          //  empGrid.ItemsSource = Query z pracowenikami z unitu managera
        }

        private void taskDicGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //taskDicGrid.ItemsSource = task.AllTaskDictionaryToList();
        }

        private void filterButton2nd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void refreshButton2nd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void assignButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
