/*Завдання 2
В одному з попередніх практичних завдань ви створювали клас «Магазин». 
Додайте до вже створеного класу інформацію про площу магазину. 
Виконайте навантаження + (для збільшення площі магазину на вказаний розмір), 
— (для зменшення площі магазину на вказаний розмір), 
== (перевірка на рівність площ магазинів),
< і > (перевірка магазинів менших або більших за площею),
!= і Equals. Використовуйте механізм властивостей
полів класу.*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Stores_And_Area
{
    class Store
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public double Area { get; set; }

        public Store(string name, string address, string description, string contactPhone, string email, double area)
        {
            Name = name;
            Address = address;
            Description = description;
            ContactPhone = contactPhone;
            Email = email;
            Area = area;
        }

        public void DisplayData()
        {
            Console.WriteLine("Название магазина: " + Name);
            Console.WriteLine("Адрес: " + Address);
            Console.WriteLine("Описание профиля магазина: " + Description);
            Console.WriteLine("Контактный телефон: " + ContactPhone);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Площадь магазина: " + Area);
        }

        public static bool operator ==(Store store1, Store store2)
        {
            return store1.Area == store2.Area;
        }

        public static bool operator !=(Store store1, Store store2)
        {
            if (store1 is null && store2 is null) 
            {
                return false;
            }
            if (store1 is null || store2 is null) 
            {
                return true;
            }
            return store1.Area != store2.Area;
        }

        public static bool operator >(Store store1, Store store2)
        {
            return store1.Area > store2.Area;
        }

        public static bool operator <(Store store1, Store store2)
        {
            return store1.Area < store2.Area;
        }

        public override bool Equals(object obj)
        {
            if (obj is Store store)
            {
                return this.Area == store.Area;
            }
            return false;
        }
    }

    class Program
    {
        static List<Store> stores = new List<Store>();
        static HashSet<string> usedAddresses = new HashSet<string>();
        static HashSet<string> usedPhones = new HashSet<string>();
        static HashSet<string> usedEmails = new HashSet<string>();

        static void Main()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Добавить магазин");
                Console.WriteLine("2. Вывести информацию о магазинах");
                Console.WriteLine("3. Увеличить площадь магазина");
                Console.WriteLine("4. Уменьшить площадь магазина");
                Console.WriteLine("5. Сравнить магазины по площади");
                Console.WriteLine("6. Выход");
                Console.Write("Выберите пункт меню: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddStore();
                            break;
                        case 2:
                            DisplayStores();
                            break;
                        case 3:
                            ChangeStoreArea(true);
                            break;
                        case 4:
                            ChangeStoreArea(false);
                            break;
                        case 5:
                            CompareStoresByArea();
                            break;
                        case 6:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                }
            }
        }

        static void AddStore()
        {
            Console.Write("Введите название магазина: ");
            string name = Console.ReadLine();

            Console.Write("Введите адрес магазина: ");
            string address = Console.ReadLine();
            if (usedAddresses.Contains(address))
            {
                Console.WriteLine("Магазин с таким адресом уже существует.");
                return;
            }
            usedAddresses.Add(address);

            Console.Write("Введите описание профиля магазина: ");
            string description = Console.ReadLine();

            Console.Write("Введите контактный телефон: ");
            string contactPhone = Console.ReadLine();
            if (usedPhones.Contains(contactPhone))
            {
                Console.WriteLine("Магазин с таким телефоном уже существует.");
                return;
            }
            usedPhones.Add(contactPhone);

            Console.Write("Введите email: ");
            string email = Console.ReadLine();
            if (usedEmails.Contains(email))
            {
                Console.WriteLine("Магазин с таким email уже существует.");
                return;
            }
            usedEmails.Add(email);

            Console.Write("Введите площадь магазина: ");
            if (double.TryParse(Console.ReadLine(), out double area))
            {
                Store store = new Store(name, address, description, contactPhone, email, area);
                stores.Add(store);
                Console.WriteLine("Магазин успешно добавлен.");
            }
            else
            {
                Console.WriteLine("Ошибка ввода площади магазина.");
            }
        }
        static void DisplayStores()
        {
            if (stores.Count == 0)
            {
                Console.WriteLine("Нет добавленных магазинов.");
            }
            else
            {
                Console.WriteLine("Информация о магазинах:");
                foreach (var store in stores)
                {
                    store.DisplayData();
                    Console.WriteLine();
                }
            }
        }

        static void ChangeStoreArea(bool increase)
        {
            if (stores.Count == 0)
            {
                Console.WriteLine("Нет добавленных магазинов.");
                return;
            }

            Console.WriteLine("Список доступных магазинов:");
            for (int i = 0; i < stores.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {stores[i].Name}");
            }

            Console.Write("Выберите номер магазина для изменения площади: ");
            if (int.TryParse(Console.ReadLine(), out int storeIndex) && storeIndex >= 1 && storeIndex <= stores.Count)
            {
                Store selectedStore = stores[storeIndex - 1];

                Console.Write("Введите размер изменения площади: ");
                if (double.TryParse(Console.ReadLine(), out double changeSize))
                {
                    if (increase)
                    {
                        selectedStore.Area += changeSize;
                        Console.WriteLine("Площадь магазина успешно увеличена.");
                    }
                    else
                    {
                        if (selectedStore.Area - changeSize >= 0)
                        {
                            selectedStore.Area -= changeSize;
                            Console.WriteLine("Площадь магазина успешно уменьшена.");
                        }
                        else
                        {
                            Console.WriteLine("Площадь магазина не может быть меньше нуля.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка ввода размера изменения площади.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный выбор магазина.");
            }
        }


        static void CompareStoresByArea()
        {
            if (stores.Count < 2)
            {
                Console.WriteLine("Добавьте как минимум два магазина для сравнения.");
            }
            else
            {
                Console.Write("Введите названия магазинов, которые вы хотите сравнить (разделите их запятой): ");
                string input = Console.ReadLine();
                string[] storeNames = input.Split(',');

                if (storeNames.Length != 2)
                {
                    Console.WriteLine("Введите ровно два названия магазинов для сравнения.");
                    return;
                }

                Store store1 = stores.Find(s => s.Name == storeNames[0].Trim());
                Store store2 = stores.Find(s => s.Name == storeNames[1].Trim());

                if (store1 != null && store2 != null)
                {
                    if (store1 == store2)
                    {
                        Console.WriteLine("Магазины имеют равную площадь.");
                    }
                    else if (store1.Area < store2.Area)
                    {
                        Console.WriteLine($"{store1.Name} имеет меньшую площадь, чем {store2.Name}.");
                    }
                    else if (store1.Area > store2.Area)
                    {
                        Console.WriteLine($"{store1.Name} имеет большую площадь, чем {store2.Name}.");
                    }
                    else
                    {
                        Console.WriteLine("Что-то пошло не так при сравнении.");
                    }
                }
                else
                {
                    Console.WriteLine("Один или оба магазина не найдены.");
                }
            }
        }



    }
}
