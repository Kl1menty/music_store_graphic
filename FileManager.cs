using System;
using System.Collections.Generic;
using System.IO;

namespace MusicStore
{
    public static class FileManager
    {
        private static string dataFolder = "data";

        public static void EnsureDataFolder()
        {
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);
        }

        public static void SaveCatalog(List<Product> catalog, string path = "data/catalog.txt")
        {
            try
            {
                EnsureDataFolder();
                using (var writer = new StreamWriter(path, false))
                {
                    foreach (var p in catalog)
                    {
                        writer.WriteLine($"{p.Name};{p.Category};{p.Price};{p.Quantity}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при сохранении каталога:\n{ex.Message}");
            }
        }

        public static void LoadCatalog(List<Product> catalog, string path = "data/catalog.txt")
        {
            try
            {
                EnsureDataFolder();
                catalog.Clear();

                if (!File.Exists(path))
                {
                    // создаем дефолтный каталог
                    catalog.Add(new Product("Классическая гитара", "Гитары", 12000, 10));
                    catalog.Add(new Product("Акустическая гитара", "Гитары", 15000, 10));
                    catalog.Add(new Product("Бас-гитара", "Гитары", 18000, 10));
                    catalog.Add(new Product("Электрогитара", "Гитары", 20000, 10));
                    catalog.Add(new Product("Фортепиано", "Клавишные", 150000, 10));
                    catalog.Add(new Product("Синтезатор", "Клавишные", 35000, 10));
                    catalog.Add(new Product("Аккордеон", "Клавишные", 45000, 10));
                    catalog.Add(new Product("Флейта", "Духовые", 12000, 10));
                    catalog.Add(new Product("Кларнет", "Духовые", 18000, 10));
                    catalog.Add(new Product("Саксофон", "Духовые", 55000, 10));
                    catalog.Add(new Product("Барабанная установка", "Ударные", 60000, 10));
                    catalog.Add(new Product("Малый барабан", "Ударные", 10000, 10));
                    catalog.Add(new Product("Бонго", "Ударные", 8000, 10));
                    catalog.Add(new Product("Струны для гитары", "Аксессуары", 700, 10));
                    catalog.Add(new Product("Медиаторы", "Аксессуары", 100, 10));
                    catalog.Add(new Product("Чехлы", "Аксессуары", 1800, 10));
                    catalog.Add(new Product("Кабели и переходники", "Аксессуары", 500, 10));
                    catalog.Add(new Product("Тюнеры", "Аксессуары", 1000, 10));

                    SaveCatalog(catalog, path);
                    return;
                }

                foreach (var line in File.ReadAllLines(path))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var parts = line.Split(';');
                    if (parts.Length != 4) continue;

                    var name = parts[0];
                    var category = parts[1];
                    if (!double.TryParse(parts[2], out double price)) price = 0;
                    if (!int.TryParse(parts[3], out int qty)) qty = 0;

                    catalog.Add(new Product(name, category, price, qty));
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при загрузке каталога:\n{ex.Message}");
            }
        }

        public static void AppendSale(Sale sale, string path = "data/sales.txt")
        {
            try
            {
                EnsureDataFolder();
                File.AppendAllText(path, sale.ToText());
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при сохранении продажи:\n{ex.Message}");
            }
        }

        public static void AppendSupply(Supply supply, string path = "data/supplies.txt")
        {
            try
            {
                EnsureDataFolder();
                File.AppendAllText(path, supply.ToText());
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при сохранении поставки:\n{ex.Message}");
            }
        }
    }
}
