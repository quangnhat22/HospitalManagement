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
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Windows.Threading;
using HospitalManagement.View.Login;
using HospitalManagement.Utils;

namespace HospitalManagement.Command
{
    class ForgotPasswordValidationCommand : ICommand
    {
        
        //save email and verified code
        public int VerifiedCode { get; set; }
        public string EmailToAddress { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            ForgotPasswordWindow mw = parameter as ForgotPasswordWindow;
            if (Check(mw))
            {
               
                
                Window window = parameter as Window;
                var recoverWindow = new RecoverAccountWindow();
                Application.Current.MainWindow = recoverWindow;

                //process email and save address, code
                Random rd = new Random();
                VerifiedCode = rd.Next(0, 999999);
                EmailToAddress = mw.tbMailAddress.Text;
                RecoverAccountViewModel recoverAccountViewModel = recoverWindow.DataContext as RecoverAccountViewModel;
                recoverAccountViewModel.emailToAddress = EmailToAddress;
                recoverAccountViewModel.verifiedCode = VerifiedCode;
                sendEmail(mw);
                window.Close();
                Thread windowThread = new Thread(new ThreadStart(() =>
                {
                    window.Closed += (s, e) =>
                    Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
                    System.Windows.Threading.Dispatcher.Run();
                }));
                windowThread.SetApartmentState(ApartmentState.STA);
                windowThread.IsBackground = true;
                windowThread.Start();
                recoverWindow.Show();
            }
        }

        public bool Check(ForgotPasswordWindow mw)
        {
            if (mw == null) return false;
            if (string.IsNullOrWhiteSpace(mw.tbMailAddress.Text) || mw.tbMailAddress.Text.Contains('@') == false)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ email");
                notifyWindow.ShowDialog();
                mw.tbMailAddress.Focus();
                return false;
            }
            return true;
        }
        public async void sendEmail(ForgotPasswordWindow mw)
        {
            try
            {
                string emailSubject = "Mã xác thực FHMS";

                string emailBody = "Mã xác thực của bạn là: " + VerifiedCode;
                EmailProcessing emailProcessing = new EmailProcessing(mw.tbMailAddress.Text, "hotrofhms@gmail.com", "supportfhms719",emailSubject,emailBody);
                emailProcessing.sendEmail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
