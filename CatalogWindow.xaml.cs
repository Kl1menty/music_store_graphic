using System.Windows;

namespace MusicStore
{
    public partial class CatalogWindow : Window
    {
        private Window prev;

        public CatalogWindow(Window previous)
        {
            InitializeComponent();
            prev = previous;
            UpdateCatalogText();
        }

        private void UpdateCatalogText()
        {
            CatalogTextBox.Clear();
            int i = 1;
            foreach (var p in MainWindow.Catalog)
            {
                CatalogTextBox.AppendText($"{i}. {p}\n");
                i++;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            prev.Show();
            this.Close();
        }
    }
}