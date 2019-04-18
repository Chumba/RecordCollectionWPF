using RecordLibrary.ViewModels;
using System.Windows;

namespace RecordLibrary.Views
{
    /// <summary>
    /// Interaction logic for AddRecordForm.xaml
    /// </summary>
    public partial class AddRecordForm : Window
    {
        public AddRecordForm(RecordCollectionViewmModel recordCollection)
        {
            InitializeComponent();
            DataContext = new AddRecordFormViewModel(recordCollection);
        }
    }
}