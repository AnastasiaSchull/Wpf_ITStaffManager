using Wpf_ITStaffManager.Views;

namespace Wpf_ITStaffManager.Services
{
    public class WindowService : IWindowService
    {
        public bool ShowEmployeeDialog(ref string firstName, ref string lastName, ref string position, ref string department)
        {
            var window = new InputEmployeeWindow();
            window.FirstNameTextBox.Text = firstName;
            window.LastNameTextBox.Text = lastName;
            window.PositionTextBox.Text = position;
            window.DepartmentTextBox.Text = department;

            bool? result = window.ShowDialog();
            if (result == true)
            {
                firstName = window.FirstNameTextBox.Text;
                lastName = window.LastNameTextBox.Text;
                position = window.PositionTextBox.Text;
                department = window.DepartmentTextBox.Text;
                return true;
            }
            return false;
        }
    }
}
