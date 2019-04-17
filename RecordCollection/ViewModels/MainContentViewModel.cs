using Prism.Commands;
using Prism.Mvvm;
using RecordLibrary.Models;
using RecordLibrary.Views;
using System.Windows.Input;

namespace RecordLibrary.ViewModels
{
    public class MainContentViewModel : BindableBase
    {
        #region Public Properties

        private RecordCollectionViewmModel _RecordCollection;

        public RecordCollectionViewmModel RecordCollection
        {
            get => _RecordCollection;
            set
            {
                SetProperty(ref _RecordCollection, value);
            }
        }

        private string _SearchText;

        public string SearchText
        {
            get
            {
                return _SearchText;
            }
            set
            {
                SetProperty(ref _SearchText, value);
                FilterOn(value);
            }
        }

        private Record _SelectedItemt;

        public Record SelectedItem
        {
            get
            {
                return _SelectedItemt;
            }
            set
            {
                SetProperty(ref _SelectedItemt, value);
            }
        }

        #endregion Public Properties

        #region Constructor

        public MainContentViewModel()
        {
            RecordCollection = new RecordCollectionViewmModel();
        }

        #endregion Constructor

        #region Commands

        #region Add Record Command

        private ICommand _AddRecordCommand;

        public ICommand AddRecordCommand
        {
            get
            {
                if (_AddRecordCommand == null)
                {
                    _AddRecordCommand = new DelegateCommand(() => AddRecord(),
                                                            () => true);
                }
                return _AddRecordCommand;
            }
        }

        private void AddRecord()
        {
            var newRecordForm = new AddRecordForm(RecordCollection);
            newRecordForm.Show();
        }

        #endregion Add Record Command

        #region Remove Record Command

        private ICommand _RemoveRecordCommand;

        public ICommand RemoveRecordCommand
        {
            get
            {
                if (_RemoveRecordCommand == null)
                {
                    _RemoveRecordCommand = new DelegateCommand<Record>((Record) => RemoveRecord(Record),
                                                            (Record) => CanRemoveRecord(Record)).ObservesProperty(() => SelectedItem); ;
                }
                return _RemoveRecordCommand;
            }
        }

        private void RemoveRecord(Record record)
        {
            _RecordCollection.RemoveRecord(record);
        }

        private bool CanRemoveRecord(Record record)
        {
            return record != null;
        }

        #endregion Remove Record Command

        #endregion Commands

        #region Methods

        private void FilterOn(string value)
        {
        }

        #endregion Methods
    }
}