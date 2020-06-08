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

        public MainWindow()
        {
            InitializeComponent();

            if (dc.DatabaseExists()) EmployeeDataGrid.ItemsSource = dc.Employees;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            dc.SubmitChanges();
        }
    }
}
