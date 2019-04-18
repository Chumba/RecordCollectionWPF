using Prism.Commands;
using Prism.Mvvm;
using RecordLibrary.Models;
using RecordLibrary.Views;
using System.Windows.Input;

namespace RecordLibrary.ViewModels
{
    /// <summary>
    /// Interaction logic for the new record form
    /// </summary>
    public class AddRecordFormViewModel : BindableBase
    {
        private RecordCollectionViewmModel _RecordCollection;

        public AddRecordFormViewModel(RecordCollectionViewmModel recordCollection)
        {
            _RecordCollection = recordCollection;
        }

        #region Commands

        #region Add Record Command

        private ICommand _OkCommand;

        public ICommand OkCommand
        {
            get
            {
                if (_OkCommand == null)
                {
                    _OkCommand = new DelegateCommand<AddRecordForm>((AddRecordForm) => Ok(AddRecordForm),
                                                     (AddRecordForm) => true);
                }
                return _OkCommand;
            }
        }

        private void Ok(AddRecordForm window)
        {
            _RecordCollection.AddRecord(new Record()
            {
                Artist = window.Artist.Text,
                ReleaseName = window.ReleaseName.Text,
                ReleaseYear = window.ReleaseYear.Text
            });
            window.Close();
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
                    _CancelCommand = new DelegateCommand<AddRecordForm>((AddRecordForm) => Cancel(AddRecordForm),
                                                            (AddRecordForm) => true);
                }
                return _CancelCommand;
            }
        }

        private void Cancel(AddRecordForm window)
        {
            window.Close();
        }

        #endregion Remove Record Command

        #endregion Commands
    }
}