using LiteDB;
using Prism.Commands;
using Prism.Mvvm;
using RecordCollection.Views;
using RecordLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace RecordLibrary.ViewModels
{
    public class MainContentViewModel : BindableBase
    {
        #region Public Properties

        private List<Record> _RecordCollection;

        public List<Record> RecordCollection
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

        #endregion Public Properties

        #region Constructor

        public MainContentViewModel()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var Records = db.GetCollection<Record>("Records");
                _RecordCollection = Records.FindAll().ToList();
            }
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
                                                            () => CanAddRecord());
                }
                return _AddRecordCommand;
            }
        }

        private void AddRecord()
        {
            var win = new AddRecordForm();
            win.Show();
            win.Closed += Win_Closed;
        }

        private void Win_Closed(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private bool CanAddRecord()
        {
            return true;
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
                    _RemoveRecordCommand = new DelegateCommand(() => RemoveRecord(),
                                                            () => CanRemoveRecord());
                }
                return _RemoveRecordCommand;
            }
        }

        private void RemoveRecord()
        {
        }

        private bool CanRemoveRecord()
        {
            return false;
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