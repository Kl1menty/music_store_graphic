using System.Windows;
using System.Windows.Input;

namespace MusicStore
{
    public partial class PriceWindow : Window
    {
        private Window prev;

        public PriceWindow(Window previous)
        {
            InitializeComponent();
            prev = previous;
            UpdateCatalog();
        }

        private void UpdateCatalog()
        {
            CatalogTextBox.Clear();
            int i = 1;
            foreach (var p in MainWindow.Catalog)
            {
                CatalogTextBox.AppendText($"{i}. {p}\n");
                i++;
            }
        }

        private void SetPrice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(ProductIndexTextBox.Text, out int index) || !double.TryParse(PriceTextBox.Text, out double price))
                {
                    MessageBox.Show("Введите корректные числа.");
                    return;
                }

                if (index < 1 || index > MainWindow.Catalog.Count)
                {
                    MessageBox.Show("Неверный номер товара.");
                    return;
                }

                if (price <= 0)
                {
                    MessageBox.Show("Цена должна быть больше 0.");
                    return;
                }

                MainWindow.Catalog[index - 1].Price = price;
                FileManager.SaveCatalog(MainWindow.Catalog);
                MessageBox.Show("Цена обновлена.");
                UpdateCatalog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            prev.Show();
            this.Close();
        }

        private void NumberOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(e.Text, out _) && e.Text != "\b";
        }
    }
}
