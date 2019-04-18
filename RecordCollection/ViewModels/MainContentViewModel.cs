﻿using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using RecordLibrary.Models;
using RecordLibrary.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        /// This list is what the datagrid binds to, it filters based on the search text box, and any other future filters
        /// </summary>
        public List<Record> DisplayRecords
        {
            get
            {
                return _RecordCollection.Records.Where(i => Filter(i)).ToList();
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
                RaisePropertyChanged(nameof(DisplayRecords));
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
            SearchText = "";
            RecordCollection.PropertyChanged += RecordCollection_PropertyChanged;
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
        private bool Filter(Record record)
        {
            var searchText = SearchText.ToUpper();
            return (
                string.IsNullOrEmpty(SearchText) ||
                record.Artist.ToUpper().Contains(searchText) ||
                record.ReleaseName.ToUpper().Contains(searchText) ||
                record.ReleaseYear.ToUpper().Contains(searchText)
            );
        }

        /// <summary>
        /// Allows us to monitor the RecordLibrary records and update based on new/removed items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void RecordCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RecordCollection.Records))
            {
                RaisePropertyChanged(nameof(DisplayRecords));
            }
        }

        #endregion Methods
    }
}