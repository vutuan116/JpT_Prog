using JpT.Logic;
using JpT.Model;
using JpT.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace JpT
{
    public partial class MainWindow : Window
    {
        private MainScreenLogic _logic = new MainScreenLogic();
        public MainModel _model = new MainModel();

        public MainWindow()
        {
            InitializeComponent();
            _logic.InitData(_model);
            this.DataContext = _model;
            ShowKanjiFlashcard();
        }

        private void ListViewMenu_SelectionChanged(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem selectedItem = (ListViewItem)((ListView)sender).SelectedItem;
            if (selectedItem == null) return;

            string name = selectedItem.Name;

            switch (name)
            {
                case "Item_Kanji_Flashcard":
                    ShowKanjiFlashcard();
                    break;
                default:
                    break;
            }
        }

        private void ShowKanjiFlashcard()
        {
            GridChildren.Children.Clear();
            GridChildren.Children.Add(new View_Kanji_Flashcard(this));
        }

        #region Common method

        private void close_Window(object sender, RoutedEventArgs e)
        {
            if (Notification.ShowConfirm("Bạn muốn thoát chương trình?") == System.Windows.Forms.DialogResult.Yes)
            {
                _logic.SaveData(_model);
                Application.Current.Shutdown();
            }
        }
        #endregion
    }
}
