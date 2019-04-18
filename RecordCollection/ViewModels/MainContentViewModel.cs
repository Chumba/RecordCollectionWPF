using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using RecordLibrary.Models;
using RecordLibrary.Views;
using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace RecordLibrary.ViewModels
{
    /// <summary>
    /// Interaction logic for the main view of the library manager including the data grid and the commands for the buttons
    /// </summary>
    public class MainContentViewModel : BindableBase
    {
        #region Public Properties

        /// <summary>
        /// This connects our view to the data service RecordCollection
        /// </summary>
        private RecordCollectionViewmModel _RecordCollection;

        public RecordCollectionViewmModel RecordCollection
        {
            get => _RecordCollection;
            set
            {
                SetProperty(ref _RecordCollection, value);
            }
        }

        /// <summary>
        /// This CollectionViewSource/CollectionView is what the datagrid binds to, it filters based on the search text box, and any other future filters
        /// </summary>
        public CollectionViewSource ViewSource { get; set; }

        private Record _SelectedItem;

        private string _SearchText = "";

        public string SearchText
        {
            get
            {
                return _SearchText;
            }
            set
            {
                SetProperty(ref _SearchText, value);
                ViewSource.View.Refresh();
            }
        }

        public Record SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                SetProperty(ref _SelectedItem, value);
            }
        }

        #endregion Public Properties

        #region Constructor

        public MainContentViewModel()
        {
            RecordCollection = new RecordCollectionViewmModel();
            ViewSource = new CollectionViewSource();
            ViewSource.Source = _RecordCollection.Records;
            ViewSource.SortDescriptions.Add(new SortDescription(nameof(Record.Artist), ListSortDirection.Ascending));
            ViewSource.SortDescriptions.Add(new SortDescription(nameof(Record.ReleaseYear), ListSortDirection.Ascending));
            ViewSource.SortDescriptions.Add(new SortDescription(nameof(Record.ReleaseName), ListSortDirection.Ascending));
            ViewSource.Filter += Filter;
            ViewSource.View.Refresh();
            SelectedItem = null;
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
                    _RemoveRecordCommand = new DelegateCommand(() => RemoveSelectedRecord(),
                                                            () => CanRemoveRecord()).ObservesProperty(() => SelectedItem); ;
                }
                return _RemoveRecordCommand;
            }
        }

        private void RemoveSelectedRecord()
        {
            _RecordCollection.RemoveRecord(SelectedItem);
            SelectedItem = null;
        }

        private bool CanRemoveRecord()
        {
            return SelectedItem != null;
        }

        #endregion Remove Record Command

        #region Pick A Record Command

        private ICommand _PickARecordCommand;

        public ICommand PickARecordCommand
        {
            get
            {
                if (_PickARecordCommand == null)
                {
                    _PickARecordCommand = new DelegateCommand(() => PickARecord(),
                                                            () => true);
                }
                return _PickARecordCommand;
            }
        }

        private void PickARecord()
        {
            var randomRecordForm = new RandomRecordForm(RecordCollection);
            randomRecordForm.Show();
        }

        #endregion Pick A Record Command

        #region ImportCommand

        private ICommand _ImportCommand;

        public ICommand ImportCommand
        {
            get
            {
                if (_ImportCommand == null)
                {
                    _ImportCommand = new DelegateCommand(() => Import(),
                                                            () => true);
                }
                return _ImportCommand;
            }
        }

        private void Import()
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "CSV documents (.csv)|*.csv";
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                _RecordCollection.Import(openFileDlg.FileName);
            }
        }

        #endregion ImportCommand

        #endregion Commands

        #region Methods

        /// <summary>
        /// Returns wether or not the record should be displayed based on current filter settings
        /// </summary>
        /// <param name="record">The record to be tested</param>
        /// <returns>Boolean true if the record should be shown in the datagrid</returns>
        private void Filter(object sender, FilterEventArgs e)
        {
            Record t = e.Item as Record;
            var searchText = _SearchText.ToUpper();
            if (t != null)
            {
                e.Accepted = string.IsNullOrEmpty(SearchText) ||
                (t.Artist ?? "").ToUpper().Contains(searchText) ||
                (t.ReleaseName ?? "").ToUpper().Contains(searchText) ||
                (t.ReleaseYear ?? "").ToUpper().Contains(searchText);
            }
        }

        #endregion Methods
    }
}