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
    /// @author Mason Nielson
    /// Creates a simple front end for managing employees and some information about them. 
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext DataConnection = new DataClasses1DataContext(Properties.Settings.Default.EmployeesConnectionString);

        // Instantiates the datagrid with the databases data.

        public MainWindow()
        {
            InitializeComponent();
            InitializeRecruiting();
            InitializePermissions();

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

        //Returns a list that represents all employees currently in the database.

        public List<Employee> GetList()
        {
            var SelectQuery =
                from LastName in DataConnection.GetTable<Employee>()
                select LastName;
            List<Employee> people = new List<Employee>();
            foreach(var item in SelectQuery)
            {
                people.Add(item);
            }
            return people;
        }

        //When changes are made to the grid, they can be saved to the database by pressing the "Update Button."

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DataConnection.SubmitChanges();
            InitializeRecruiting();
        }

        //When SearchBox's Text is changed this method is called. 

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(SearchBox.Text == null)
            {
                EmployeeDataGrid.ItemsSource = DataConnection.Employees;
                return;
            }


            List<Employee> people = GetList();
            List<Employee> PeopleInSearch = new List<Employee>();

            foreach (Employee item in people)
            {
                string FullName = item.FirstName + item.LastName;
                bool contains = FullName.IndexOf(SearchBox.Text, StringComparison.OrdinalIgnoreCase) != -1;
                if(contains)
                {
                    PeopleInSearch.Add(item);
                }

            }
            EmployeeDataGrid.ItemsSource = PeopleInSearch;
        }

        //Runs code to establish how many emloyees have been hired and how many have been fired (terminated).

        private void InitializeRecruiting()
        {
            Terminated.Text = HowManyTerminated();
            Hired.Text = HowManyHired();
        }

        //Counts how many people in the database have an EndDate. Currently does not adjust for the year.
        //Returns a string that is how many people are terminated.

        private string HowManyTerminated()
        {
            int TerminatedThisWeek = 0;

            foreach (var item in GetList())
            {
                if (item.EndDate != (null))
                {
                    TerminatedThisWeek++;
                }
            }
                return TerminatedThisWeek.ToString();
        }

        //An overloaded method with an int in its parameters that will return an int instead of a string when called upon.
        
        private int HowManyTerminated(int i)
        {
            int TerminatedThisWeek = 0;

            foreach (var item in GetList())
            {
                if (item.EndDate != (null))
                {
                    TerminatedThisWeek++;
                }
            }
            return TerminatedThisWeek;
        }

        //Returns a string representing how many new recruits exist this week.

        private string HowManyHired()
        {
            int HiredPeople = GetList().Count() - HowManyTerminated(0);
            return HiredPeople.ToString();
        }

        private void InitializePermissions()
        {
            
        }
    }
}
