﻿using LiteDB;
using Prism.Mvvm;
using RecordLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecordLibrary.ViewModels
{
    public class RecordCollectionViewmModel : BindableBase
    {
        private LiteDatabase _LiteDatabase;
        private LiteCollection<Record> _Records;

        public RecordCollectionViewmModel()
        {
            _LiteDatabase = new LiteDatabase(@"MyData.db");
            _Records = _LiteDatabase.GetCollection<Record>("Records");
        }

        public List<Record> Records
        {
            get => _Records.FindAll().ToList();
        }

        public void AddRecord(Record record)
        {
            _Records.Insert(record);
            RaisePropertyChanged(nameof(Records));
        }

        public void RemoveRecord(Record record)
        {
            Task.Run(() =>
            {
                _Records.Delete(record.Id);
                RaisePropertyChanged(nameof(Records));
            });
        }

        public bool ContainsRecord(Record record)
        {
            return Records.Any(i => i == record);
        }
    }
}