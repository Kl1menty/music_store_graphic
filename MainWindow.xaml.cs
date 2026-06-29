using System.Windows;
using System.Collections.Generic;

namespace MusicStore
{
    public partial class MainWindow : Window
    {
        public static int lastSaleId = 0;
        public static int lastSupplyId = 0;
        public static List<Product> Catalog = new List<Product>();
        

        public MainWindow()
        {
            InitializeComponent();
            FileManager.LoadCatalog(Catalog);
        }

        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            CatalogWindow window = new CatalogWindow(this);
            window.Show();
            this.Hide();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow window = new OrderWindow(this);
            window.Show();
            this.Hide();
        }

        private void SupplyButton_Click(object sender, RoutedEventArgs e)
        {
            SupplyWindow window = new SupplyWindow(this);
            window.Show();
            this.Hide();
        }

        private void PriceButton_Click(object sender, RoutedEventArgs e)
        {
            PriceWindow window = new PriceWindow(this);
            window.Show();
            this.Hide();
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileManager.SaveCatalog(Catalog);
                MessageBox.Show("Отчет успешно сформирован (файлы сохранены в папке data).");
            }
            catch { MessageBox.Show("Ошибка при формировании отчета."); }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}