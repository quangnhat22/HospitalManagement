using HospitalManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    public class LoginWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            LoginWindow loginWindow = parameter as LoginWindow;
            var mainWindow = new MainWindow();
            if(checkPassword(loginWindow.txtPassword.Text))
            {
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                loginWindow.Close();
            }
            else
            {
                NotifyWindow notifyWindow = new NotifyWindow();
                notifyWindow.Show();
            }
        }


        private bool checkPassword(string plainText)
        {
            string hashedPasswordInput = Hash(plainText);
            string databasePassword = "cdd96d3cc73d1dbdaffa03cc6cd7339b";
            return string.Compare(databasePassword, hashedPasswordInput, true) == 0;
        }

        private string Hash(string plainText)
        {
            return CreateMD5(Base64Encode(plainText));
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
