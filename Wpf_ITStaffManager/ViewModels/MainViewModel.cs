using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Wpf_ITStaffManager.Models;
using Wpf_ITStaffManager.Services;

namespace Wpf_ITStaffManager.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly CompanyContext _context = new();
        private Employee? _selectedEmployee;
        private readonly IWindowService _windowService;

        public ObservableCollection<Employee> Employees { get; set; }

        public Employee? SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            NotifyPropertyChanged(propertyName);
        }

        public ICommand AddEmployeeCommand { get; }
        public ICommand RemoveEmployeeCommand { get; }
        public ICommand EditEmployeeCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ExitCommand { get; }
        public MainViewModel(IWindowService windowService)
        {
            _windowService = windowService;
            Employees = new ObservableCollection<Employee>(_context.Employees.ToList());

            AddEmployeeCommand = new RelayCommand(AddEmployee);
            RemoveEmployeeCommand = new RelayCommand(RemoveEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee);
            SaveCommand = new RelayCommand(SaveChanges);
            ExitCommand = new RelayCommand(ExitApplication);
        }
        private void AddEmployee()
        {
            string firstName = string.Empty;
            string lastName = string.Empty;
            string position = string.Empty;
            string department = string.Empty;

            if (_windowService.ShowEmployeeDialog(ref firstName, ref lastName, ref position, ref department))
            {
                var newEmployee = new Employee
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Position = position,
                    Department = department
                };
                _context.Employees.Add(newEmployee);
                _context.SaveChanges();
                Employees.Add(newEmployee);
            }
        }

        private void RemoveEmployee()
        {
            if (SelectedEmployee != null)
            {
                _context.Employees.Remove(SelectedEmployee);
                Employees.Remove(SelectedEmployee);
                _context.SaveChanges();
            }
        }

        private void EditEmployee()
        {
            if (SelectedEmployee != null)
            {
                string? firstName = SelectedEmployee.FirstName;
                string? lastName = SelectedEmployee.LastName;
                string? position = SelectedEmployee.Position;
                string? department = SelectedEmployee.Department;

                // вызываем окно и передаем данные
                if (_windowService.ShowEmployeeDialog(ref firstName, ref lastName, ref position, ref department))
                {
                    // обновление свойств 
                    SelectedEmployee.FirstName = firstName;
                    SelectedEmployee.LastName = lastName;
                    SelectedEmployee.Position = position;
                    SelectedEmployee.Department = department;

                    _context.SaveChanges();
                    RefreshEmployees();

                    // обновление интерфейса
                    OnPropertyChanged(nameof(SelectedEmployee));
                    OnPropertyChanged(nameof(Employees));
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для редактирования.");
            }
        }

        private void RefreshEmployees()
        {
            Employees.Clear();
            foreach (var employee in _context.Employees.ToList())
            {
                Employees.Add(employee);
            }
            OnPropertyChanged(nameof(Employees));
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
            MessageBox.Show("Changes have been saved!");
        }

        private void ExitApplication()
        {
            Application.Current.Shutdown(); 
        }
    }
}