using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;

namespace RecordLibrary.ViewModels
{
    /// <summary>
    /// Interaction logic for the Random Record form view
    /// </summary>
    public class RandomRecordFormViewModel : BindableBase
    {
        private RecordCollectionViewmModel _RecordCollection;

        public RandomRecordFormViewModel(RecordCollectionViewmModel recordCollection)
        {
            _RecordCollection = recordCollection;
            Shuffle();
        }

        private void Shuffle()
        {
            var record = _RecordCollection.Shuffle();
            Artist = record.Artist;
            ReleaseName = record.ReleaseName;
        }

        private string _Artist;

        public string Artist
        {
            get => _Artist;
            set
            {
                SetProperty(ref _Artist, value);
            }
        }

        private string _ReleaseName;

        public string ReleaseName
        {
            get => _ReleaseName;
            set
            {
                SetProperty(ref _ReleaseName, value);
            }
        }

        private ICommand _ShuffleCommand;

        public ICommand ShuffleCommand
        {
            get
            {
                if (_ShuffleCommand == null)
                {
                    _ShuffleCommand = new DelegateCommand(() => Shuffle(), () => true);
                }

                return _ShuffleCommand;
            }
        }
    }
}