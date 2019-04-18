using RecordLibrary.ViewModels;
using System.Windows;

namespace RecordLibrary.Views
{
    /// <summary>
    /// Interaction logic for RandomRecordForm.xaml
    /// </summary>
    public partial class RandomRecordForm : Window
    {
        public RandomRecordForm(RecordCollectionViewmModel recordCollection)
        {
            InitializeComponent();
            DataContext = new RandomRecordFormViewModel(recordCollection);
        }
    }
}