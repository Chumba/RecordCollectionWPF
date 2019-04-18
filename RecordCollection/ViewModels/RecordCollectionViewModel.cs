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
    /// <summary>
    /// This class contains the data connection to the LiteDB data file and handles all create/delete/querying of the database.    ///
    /// LiteDB works off of a RecordCollection.db file which will either be read from the working directory, or created if not present.
    /// </summary>
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
            _LiteDatabase = new LiteDatabase(@"RecordCollection.db");
            _Records = _LiteDatabase.GetCollection<Record>("Records");
            _Random = new Random((int)DateTime.Now.Ticks);
        }

        /// <summary>
        /// The access point for outside clients to the data records
        /// </summary>
        public List<Record> Records
        {
            get => _Records.FindAll().ToList();
        }

        /// <summary>
        /// Adds a record to the db
        /// </summary>
        /// <param name="record">The record to be added</param>
        public void AddRecord(Record record)
        {
            Task.Run(() =>
            {
                _Records.Insert(record);
                RaisePropertyChanged(nameof(Records));
            });
        }

        /// <summary>
        /// Removes a record from the db
        /// </summary>
        /// <param name="record">The record to be removed</param>
        public void RemoveRecord(Record record)
        {
            Task.Run(() =>
            {
                _Records.Delete(record.Id);
                RaisePropertyChanged(nameof(Records));
            });
        }

        /// <summary>
        /// Tests whether a record is contained in the db
        /// </summary>
        /// <param name="record">The record to search for</param>
        /// <returns>Boolean true if the record is present</returns>
        public bool ContainsRecord(Record record)
        {
            return Records.Any(i => i == record);
        }

        /// <summary>
        /// Returns a randomized record from the db
        /// </summary>
        /// <returns>A random Record object from the db.</returns>
        public Record Shuffle()
        {
            int random = _Random.Next(0, Records.Count());
            return Records.ElementAt(random);
        }

        /// <summary>
        /// Imports a CSV file into the DB, the CSV file should be formatted as
        ///
        /// Artist, ReleaseName, ReleaseYear
        ///
        /// </summary>
        /// <param name="path">The path to a .csv file containing appropriate data.</param>
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