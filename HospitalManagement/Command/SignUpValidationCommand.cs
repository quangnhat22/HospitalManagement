using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.ViewModel;
using HospitalManagement.View;
using System.Collections;
using System.ComponentModel;
using System.Windows;

namespace HospitalManagement.Command
{
    class SignUpValidationCommand : ICommand, INotifyDataErrorInfo
    {
        private string _firstName;
        public string FirstName { get { return _firstName; } set { _firstName = value; OnPropertyChanged("FirstName"); } }
        private string _lastName;
        public string LastName { get { return _lastName; } set { _lastName = value; OnPropertyChanged("LastName"); } }
        private string _userName;
        public string UserName { get { return _userName; } set { _userName = value; OnPropertyChanged("UserName"); } }
        private string _email;
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged("Email"); } }
        private string _password;
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged("Password"); } }
        private string _confirmPassword;
        public string ConfirmPassword { get { return _confirmPassword; } set { _confirmPassword = value; OnPropertyChanged("ConfirmPassword"); } }
        private string _birthdate;
        public string Birthdate { get { return _birthdate; } set { _birthdate = value; OnPropertyChanged("Birthdate"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
            Validate(name);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return HasErrors ? false : true;
        }

        public void Execute(object parameter)
        {
            Validate(nameof(FirstName));
            Validate(nameof(LastName));
            Validate(nameof(UserName));
            Validate(nameof(Email));
            Validate(nameof(Password));
            Validate(nameof(ConfirmPassword));
            Validate(nameof(Birthdate));
            if (!HasErrors) MessageBox.Show("Gọi command thành công");
        }

        private void Validate(string changedPropertyName)
        {
            switch (changedPropertyName)
            {
                case nameof(FirstName):
                    if (string.IsNullOrWhiteSpace(FirstName))
                    {
                        _ValidationErrorsByProperty[nameof(FirstName)] = new List<object> { "Vui lòng nhập họ" };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(FirstName)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(FirstName)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(FirstName)));
                    }
                    break;
                case nameof(LastName):
                    if (string.IsNullOrWhiteSpace(LastName))
                    {
                        _ValidationErrorsByProperty[nameof(LastName)] = new List<object> { "Vui lòng nhập tên" };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(LastName)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(LastName)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(LastName)));
                    }
                    break;
                case nameof(UserName):
                    if (string.IsNullOrWhiteSpace(UserName))
                    {
                        _ValidationErrorsByProperty[nameof(UserName)] = new List<object> { "Vui lòng nhập tên đăng nhập" };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(UserName)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(UserName)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(UserName)));
                    }
                    break;
                case nameof(Email):
                    if (string.IsNullOrWhiteSpace(Email))
                    {
                        _ValidationErrorsByProperty[nameof(Email)] = new List<object> { "Vui lòng nhập email" };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Email)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(Email)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Email)));
                    }
                    break;
                case nameof(Password):
                    if (string.IsNullOrWhiteSpace(Password))
                    {
                        _ValidationErrorsByProperty[nameof(Password)] = new List<object> { "Vui lòng nhập mật khẩu" };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Password)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(Password)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Password)));
                    }
                    break;
                case nameof(ConfirmPassword):
                    if (string.IsNullOrWhiteSpace(ConfirmPassword))
                    {
                        _ValidationErrorsByProperty[nameof(ConfirmPassword)] = new List<object> { "Vui lòng nhập lại mật khẩu" };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(ConfirmPassword)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(ConfirmPassword)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(ConfirmPassword)));
                    }
                    break;
                case nameof(Birthdate):
                    if (string.IsNullOrWhiteSpace(Birthdate))
                    {
                        _ValidationErrorsByProperty[nameof(Birthdate)] = new List<object> { "Vui lòng nhập ngày sinh" };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Birthdate)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(Birthdate)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Birthdate)));
                    }
                    break;
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (_ValidationErrorsByProperty.TryGetValue(propertyName, out List<object> errors))
            {
                return errors;
            }
            return Array.Empty<object>();
        }

        private readonly Dictionary<string, List<object>> _ValidationErrorsByProperty = new Dictionary<string, List<object>>();

        public bool HasErrors => _ValidationErrorsByProperty.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
