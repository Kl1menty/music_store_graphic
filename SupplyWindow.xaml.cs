using System.Windows;
using System.Windows.Input;

namespace MusicStore
{
    public partial class SupplyWindow : Window
    {
        private Window prev;

        public SupplyWindow(Window previous)
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

        private void PlaceSupply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(ProductIndexTextBox.Text, out int index) || !int.TryParse(QuantityTextBox.Text, out int qty))
                {
                    MessageBox.Show("Введите корректные числа.");
                    return;
                }

                if (index < 1 || index > MainWindow.Catalog.Count)
                {
                    MessageBox.Show("Неверный номер товара.");
                    return;
                }

                if (qty < 1 || qty > 100)
                {
                    MessageBox.Show("Количество должно быть от 1 до 100.");
                    return;
                }

                var product = MainWindow.Catalog[index - 1];
                product.Quantity += qty;

                MainWindow.lastSupplyId++;
                FileManager.AppendSupply(new Supply(MainWindow.lastSupplyId, product, qty));

                MessageBox.Show("Поставка оформлена.");
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
            e.Handled = !int.TryParse(e.Text, out _) && e.Text != "\b";
        }
    }
}
