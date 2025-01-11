using System.Windows;

namespace Wpf_ITStaffManager.Views
{
    public partial class InputEmployeeWindow : Window
    {
        public string FirstName => FirstNameTextBox.Text;
        public string LastName => LastNameTextBox.Text;
        public string Position => PositionTextBox.Text;
        public string Department => DepartmentTextBox.Text;

        public InputEmployeeWindow()
        {
            InitializeComponent();
        }     

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
