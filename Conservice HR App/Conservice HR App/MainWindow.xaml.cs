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
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.EmployeesConnectionString);

        // Instantiates the datagrid as the databases data allows.
        public MainWindow()
        {
            InitializeComponent();
            if (dc.DatabaseExists())
            {
                EmployeeDataGrid.ItemsSource = dc.Employees;
            }
        }
        
        //When changes are made to the grid, they can be saved to the database by pressing the "Update Button."

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            dc.SubmitChanges();
        }

        //When SearchBox's Text is changed this method is called. 

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Text Changed.");
            List<Employee> people = new List<Employee>();
            //people = (from FirstName in dc.Employees select new Employee() FirstName)
            
            EmployeeDataGrid.ItemsSource = people;
        }
    }
}
