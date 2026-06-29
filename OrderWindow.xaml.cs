using System.Windows;
using System.Windows.Input;

namespace MusicStore
{
    public partial class OrderWindow : Window
    {
        private Window prev;

        public OrderWindow(Window previous)
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

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
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

                var product = MainWindow.Catalog[index - 1];

                if (qty < 1 || qty > product.Quantity)
                {
                    MessageBox.Show("Недопустимое количество.");
                    return;
                }

                product.Quantity -= qty;
                MainWindow.lastSaleId++;
                FileManager.AppendSale(new Sale(MainWindow.lastSaleId, product, qty));
                MessageBox.Show("Заказ оформлен.");
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
