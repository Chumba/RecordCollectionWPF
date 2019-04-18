using LiteDB;
using Prism.Mvvm;
using RecordLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecordLibrary.ViewModels
{
    public class RecordCollectionViewmModel : BindableBase
    {
        #region Private Properties

        private LiteDatabase _LiteDatabase;
        private LiteCollection<Record> _Records;
        private Random _Random;

        #endregion Private Properties

        #region Constructor and Public Members

        public RecordCollectionViewmModel()
        {
            _LiteDatabase = new LiteDatabase(@"MyData.db");
            _Records = _LiteDatabase.GetCollection<Record>("Records");
            _Random = new Random((int)DateTime.Now.Ticks);
        }

        public List<Record> Records
        {
            get => _Records.FindAll().ToList();
        }

        public void AddRecord(Record record)
        {
            Task.Run(() =>
            {
                _Records.Insert(record);
                RaisePropertyChanged(nameof(Records));
            });
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

        public Record Shuffle()
        {
            int random = _Random.Next(0, Records.Count());
            return Records.ElementAt(random);
        }

        public void Import(string path)
        {
            Task.Run(() =>
            {
                var newEntries = new List<Record>();
                using (var reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        newEntries.Add(new Record
                        {
                            Artist = values[0],
                            ReleaseName = values[1],
                            ReleaseYear = values[2]
                        });
                    }
                }
                _Records.InsertBulk(newEntries);
                RaisePropertyChanged(nameof(Records));
            });
        }

        #endregion Constructor and Public Members
    }
}