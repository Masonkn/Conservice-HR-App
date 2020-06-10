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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Conservice_HR_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext DataConnection = new DataClasses1DataContext(Properties.Settings.Default.EmployeesConnectionString);

        // Instantiates the datagrid with the databases data.

        public MainWindow()
        {
            InitializeComponent();
            if (DataConnection.DatabaseExists())
            {
                EmployeeDataGrid.ItemsSource = DataConnection.Employees;
                EmployeeDataGrid.AutoGenerateColumns = false;
            }
            else
            {
                throw new Exception("No database connected.");
            }
        }
        
        //When changes are made to the grid, they can be saved to the database by pressing the "Update Button."

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DataConnection.SubmitChanges();
        }

        //When SearchBox's Text is changed this method is called. 

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(SearchBox.Text == null)
            {
                EmployeeDataGrid.ItemsSource = DataConnection.Employees;
                return;
            }

            var SelectQuery= 
                from LastName in DataConnection.GetTable<Employee>()
                select LastName;

            List<Employee> people = new List<Employee>();

            foreach (var item in SelectQuery)
            {
                string FullName = item.FirstName + item.LastName;
                bool contains = FullName.IndexOf(SearchBox.Text, StringComparison.OrdinalIgnoreCase) != -1;
                if(contains)
                {
                people.Add(item);
                }

            }

            EmployeeDataGrid.ItemsSource = people;


        }
    }
}
