using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecordCollection.ViewModels
{
    public class AddRecordFormViewModel : BindableBase
    {
        public AddRecordFormViewModel() { }

        #region Commands

        #region Add Record Command

        private ICommand _OkCommand;

        public ICommand OkCommand
        {
            get
            {
                if (_OkCommand == null)
                {
                    _OkCommand = new DelegateCommand(() => Ok(),
                                                            () => CanOk());
                }
                return _OkCommand;
            }
        }

        private void Ok()
        {
           
        }

        private bool CanOk()
        {
            return true;
        }

        #endregion Add Record Command

        #region Remove Record Command

        private ICommand _CancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new DelegateCommand(() => Cancel(),
                                                            () => CanCancel());
                }
                return _CancelCommand;
            }
        }

        private void Cancel()
        {
            
        }

        private bool CanCancel()
        {
            return true;
        }

        #endregion Remove Record Command

        #endregion Commands

    }
}
