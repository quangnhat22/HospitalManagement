using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class SaveChangeAccountCommand : ICommand
    {
        private ChangeAccountViewModel changeAccountViewModel;
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public SaveChangeAccountCommand()
        {
            changeAccountViewModel = new ChangeAccountViewModel();
        }
        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Check()
        {

        }
    }
}
