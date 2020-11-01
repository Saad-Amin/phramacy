using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp137
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "Pharmacy management system";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
            string line = "--------------------------------------------------------";
            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
            Console.WriteLine(line);
            string username, password;
            int ctr = 0;
            Console.Write("\nLOGIN FORM:\n");
            Console.Write("--------------------------------------------------------\n");
            do
            {
                Console.Write("Input a username: ");
                username = Console.ReadLine();
                Console.Write("Input a password: ");
                password = Console.ReadLine();
                if (username != "abcd" || password != "1234")
                {
                    ctr++;
                    Console.WriteLine("Wrong Input. Try Again!");
                }
                else
                {
                    ctr = 1;
                }
            }
            while ((username != "abcd" || password != "1234") && (ctr != 3));

            if (ctr == 3)
            {
                Console.Write("\nLogin attemp three or more times. Try later!\n\n");
            }
            else
            {
                Console.Write("\nLOGIN SUCCESFULL!\n\n");
                Console.WriteLine("--------------------------------------------------------");
                Menu();
            }
        }
        public static void Menu()
        {
            Console.WriteLine("Enter:");
            string[,] menu = new string[2, 2];
            menu[0, 0] = "1 for Purchase";
            menu[0, 1] = "2 for Supplies";
            menu[1, 0] = "3 for Stock";
            menu[1, 1] = "4 for Billing";
            for (int i = 0; i < menu.GetLength(0); i++)
            {
                for (int j = 0; j < menu.GetLength(1); j++)
                {
                    Console.Write(menu[i, j] + "\t\t");
                }
                Console.WriteLine();
            }
            char choice = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (choice)
            {
                case '1':
                    Purchase();
                    break;
                case '2':
                    Suppliers();
                    break;
                case '3':
                    Stock();
                    break;
                case '4':
                    Billing();
                    break;
                default:
                    Console.WriteLine("INVALID COMMAND");
                    break;
            }
        }
        public static void Stock()
        {
            Console.WriteLine("Search Category:");
            Console.WriteLine("1 for Tablets");
            Console.WriteLine("2 for Injections");
            Console.WriteLine("3 for Syrups");
            char cat = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (cat)
            {
                case '1':
                    Tablets();
                    break;
                case '2':
                    Injection();
                    break;
                case '3':
                    Syrups();
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    break;
            }
        }
        public static void Tablets()
        {
            string pathtablet = @"Tablets.txt";
            string[] tablet = File.ReadAllLines(pathtablet);
            int total_tablet = tablet.Length;
            string pathstock = @"quantity.txt";
            string[] lines = File.ReadAllLines(pathstock);
            int[] stock = Array.ConvertAll(lines, s => int.Parse(s));
            for (int i = 0; i < tablet.Length; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, tablet[i]);
            }
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Press 'E' to make an Entry");
            Console.WriteLine("Press 'S' for Tablet Information");
            Console.WriteLine("Press 'M' for menu");
            Console.WriteLine("Press '0' to Logout");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'e':
                case 'E':
                    TabletEntry(tablet, total_tablet, stock);
                    break;
                case 's':
                case 'S':
                    Stock_TabletInfo(tablet, total_tablet, stock);
                    break;
                case 'm':
                case 'M':
                    Menu();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    break;
            }
        }
        public static void TabletEntry(string[] tablet, int total_tablet, int[] stock)
        {
            Console.WriteLine("Enter:");
            string[,] tablet_Entry_Menu = new string[1, 4];
            tablet_Entry_Menu[0, 0] = "1 for Adding New Tablet";
            tablet_Entry_Menu[0, 1] = "2 for Increasing amount of tablet";
            tablet_Entry_Menu[0, 2] = "3 for Main Menu";
            tablet_Entry_Menu[0, 3] = "4 To Quit";
            for (int i = 0; i < tablet_Entry_Menu.GetLength(0); i++)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");

                for (int j = 0; j < tablet_Entry_Menu.GetLength(1); j++)
                {
                    Console.Write("|" + tablet_Entry_Menu[i, j] + "| \t");
                }
                Console.WriteLine("\n-------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
            }
            Console.Write("Choice: ");
            char ch = Convert.ToChar(Console.ReadLine());
            switch (ch)
            {
                case '1':
                    AddTablet(tablet, total_tablet, stock);
                    break;
                case '2':
                    IncreaseOne(tablet, total_tablet, stock);
                    break;
                case '3':
                    Menu();
                    break;
                case '4':
                    Console.WriteLine("Thanks for Using!");
                    break;
                default:
                    Console.WriteLine("Wrong! input Try Again");
                    Console.WriteLine();
                    TabletEntry(tablet, total_tablet, stock);
                    break;
            }
        }
        public static int[] AddTablet(string[] tablet, int total_tablet, int[] stock)
        {
            Console.Write("How Many Tablets Do You Want To Enter: ");
            int n_o_t = int.Parse(Console.ReadLine());
            tablet = new string[n_o_t];
            stock = new int[n_o_t];
            for (int i = 0; i < tablet.Length; i++)
            {
                Console.Write("Enter Tablet No.{0}: ", i + 1);
                tablet[i] = Console.ReadLine();
                Console.Write("Enter Quantity of of {1}: ", i + 1, tablet[i]);
                stock[i] = int.Parse(Console.ReadLine());
            }
            string[] quantity = new string[n_o_t];
            for (int i = 0; i < stock.Length; i++)
            {
                quantity[i] = "Name: " + tablet[i] + "\t QT:" + stock[i];
                Console.WriteLine(quantity[i]);
            }
            string[] result = Array.ConvertAll(stock, x => x.ToString());
            File.AppendAllLines(@"quantity.txt", result);
            Console.WriteLine();
            File.AppendAllLines(@"Tablets.txt", tablet);
            TabletEntry(tablet, total_tablet, stock);
            return stock;
        }
        public static void Stock_TabletInfo(string[] tablet, int total_tablet, int[] stock)
        {
            string pathtablet = @"Tablets.txt";
            string[] Read_stock = File.ReadAllLines(pathtablet);
            string path = @"quantity.txt";
            string[] Rea_stock = File.ReadAllLines(path);
            for (int i = 0; i < tablet.Length; i++)
            {
                Console.WriteLine("{0}.Name: {1} \t QT:{2}", i + 1, Read_stock[i], Rea_stock[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Press 'E' to make an Entry");
            Console.WriteLine("Press 'S' for Tablet Information");
            Console.WriteLine("Press 'M' for menu");
            Console.WriteLine("Press '0' to Logout");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'e':
                case 'E':
                    TabletEntry(tablet, total_tablet, stock);
                    break;
                case 's':
                case 'S':

                    Stock_TabletInfo(tablet, total_tablet, stock);
                    break;
                case 'm':
                case 'M':
                    Menu();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Menu();
                    break;
            }
        }
        public static int[] IncreaseOne(string[] tablet, int total_tablet, int[] stock)
        {
            string pathtablet = @"Tablets.txt";
            string[] Read_stock = File.ReadAllLines(pathtablet);
            string path = @"quantity.txt";
            string[] Rea_stock = File.ReadAllLines(path);
            for (int i = 0; i < Read_stock.Length; i++)
            {
                Console.WriteLine("{0}.Name: {1} \t QT:{2}", i + 1, Read_stock[i], Rea_stock[i]);
            }
            Console.Write("Enter Tablet Number: ");
            int val1 = int.Parse(Console.ReadLine());
            int val = val1 - 1;
            Console.Write("Enter  Amount of Quantity: ");
            int amount = int.Parse(Console.ReadLine());
            if (val < stock.Length)
            {
                stock[val] = stock[val] + amount;
                Console.WriteLine("Quantity of {0} is now {1}", val + 1, stock[val]);
                for (int i = 0; i < stock.Length; i++)
                {
                    stock[i] = stock[i];
                }
                string[] result = Array.ConvertAll(stock, x => x.ToString());
                File.WriteAllLines(@"quantity.txt", result);
                TabletEntry(tablet, total_tablet, stock);
                return stock;
            }
            else
            {
                Console.WriteLine("Error!");
                return stock;
            }
        }
        public static void Injection()
        {
            string pathInjection = @"Injection.txt";
            string[] injection = File.ReadAllLines(pathInjection);
            int total_injection = injection.Length;
            string pathstock = @"injection_quantity.txt";
            string[] lines = File.ReadAllLines(pathstock);
            int[] injection_stock = Array.ConvertAll(lines, s => int.Parse(s));
            for (int i = 0; i < injection.Length; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, injection[i]);

            }
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Press 'E' to make and Entry");
            Console.WriteLine("Press 'S' for Injection Information");
            Console.WriteLine("Press 'M' for menu");
            Console.WriteLine("Press '0' to Logout");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'e':
                case 'E':
                    InjectionEntry(injection, total_injection, injection_stock);
                    break;
                case 's':
                case 'S':
                    InjectionStock(injection, total_injection, injection_stock);
                    break;
                case 'm':
                case 'M':
                    Menu();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Menu();
                    break;
            }
        }
        public static void InjectionEntry(string[] injection, int total_injection, int[] injection_stock)
        {
            Console.WriteLine("Enter:");
            string[,] injection_Entry_Menu = new string[1, 4];
            injection_Entry_Menu[0, 0] = "1 for Adding New Injection";
            injection_Entry_Menu[0, 1] = "2 for Increasing amount of Injection";
            injection_Entry_Menu[0, 2] = "3 for Main Menu";
            injection_Entry_Menu[0, 3] = "4 To Quit";
            for (int i = 0; i < injection_Entry_Menu.GetLength(0); i++)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                for (int j = 0; j < injection_Entry_Menu.GetLength(1); j++)
                {
                    Console.Write("|" + injection_Entry_Menu[i, j] + "| \t");
                }
                Console.WriteLine("\n-------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
            }
            Console.Write("Choice: ");
            char ch = Convert.ToChar(Console.ReadLine());
            switch (ch)
            {
                case '1':
                    AddInjection(injection, total_injection, injection_stock);
                    break;
                case '2':
                    InjectionIncreaseOne(injection, total_injection, injection_stock);
                    break;
                case '3':
                    Menu();
                    break;
                case '4':
                    Console.WriteLine("Thanks for Using!");
                    break;
                default:
                    Console.WriteLine("Wrong! input Try Again");
                    Console.WriteLine();
                    SyrupEntry(injection, total_injection, injection_stock);
                    break;
            }
        }
        public static int[] AddInjection(string[] injection, int total_injection, int[] injection_stock)
        {
            Console.Write("How Many Injection Do You Want To Enter: ");
            int n_o_t = int.Parse(Console.ReadLine());
            injection = new string[n_o_t];
            injection_stock = new int[n_o_t];
            for (int i = 0; i < injection.Length; i++)
            {
                Console.Write("Enter Injection No.{0}: ", i + 1);
                injection[i] = Console.ReadLine();
                Console.Write("Enter Quantity of of {1}: ", i + 1, injection[i]);
                injection_stock[i] = int.Parse(Console.ReadLine());
            }
            string[] quantity = new string[n_o_t];
            for (int i = 0; i < injection_stock.Length; i++)
            {
                quantity[i] = "Name: " + injection[i] + "\t QT:" + injection_stock[i];
                Console.WriteLine(quantity[i]);
            }
            string[] result = Array.ConvertAll(injection_stock, x => x.ToString());
            File.AppendAllLines(@"injection_quantity.txt", result);
            Console.WriteLine();
            File.AppendAllLines(@"Injection.txt", injection);
            InjectionEntry(injection, total_injection, injection_stock);
            return injection_stock;
        }
        public static int[] InjectionIncreaseOne(string[] injection, int total_injection, int[] injection_stock)
        {
            string path1 = @"Injection.txt";
            string[] Read_stock = File.ReadAllLines(path1);
            string path = @"injection_quantity.txt";
            string[] Rea_stock = File.ReadAllLines(path);
            for (int i = 0; i < Read_stock.Length; i++)
            {
                Console.WriteLine("{0}.Name: {1} \t QT:{2}", i + 1, Read_stock[i], Rea_stock[i]);
            }
            Console.Write("Enter Injection Number: ");
            int val1 = int.Parse(Console.ReadLine());
            int val = val1 - 1;
            Console.Write("Enter  Amount of Quantity: ");
            int amount = int.Parse(Console.ReadLine());
            if (val < injection_stock.Length)
            {
                injection_stock[val] = injection_stock[val] + amount;
                Console.WriteLine("Quantity of {0} is now {1}", val + 1, injection_stock[val]);
                for (int i = 0; i < injection_stock.Length; i++)
                {
                    injection_stock[i] = injection_stock[i];
                }
                string[] result = Array.ConvertAll(injection_stock, x => x.ToString());
                File.WriteAllLines(@"injection_quantity.txt", result);
                InjectionEntry(injection, total_injection, injection_stock);
                return injection_stock;
            }
            else
            {
                Console.WriteLine("Error!");
                InjectionEntry(injection, total_injection, injection_stock);
                return injection_stock;
            }
        }
        public static void InjectionStock(string[] injection, int total_injection, int[] injection_stock)
        {
            string path1 = @"Injection.txt";
            string[] Read_stock = File.ReadAllLines(path1);
            string path = @"injection_quantity.txt";
            string[] Rea_stock = File.ReadAllLines(path);
            for (int i = 0; i < injection.Length; i++)
            {
                Console.WriteLine("{0}.Name: {1} \t QT:{2}", i + 1, Read_stock[i], Rea_stock[i]);

            }
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Press 'E' to make and Entry");
            Console.WriteLine("Press 'S' for Injection Information");
            Console.WriteLine("Press 'M' for menu");
            Console.WriteLine("Press '0' to Logout");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'e':
                case 'E':
                    InjectionEntry(injection, total_injection, injection_stock);
                    break;
                case 's':
                case 'S':
                    InjectionStock(injection, total_injection, injection_stock);
                    break;
                case 'm':
                case 'M':
                    Menu();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Menu();
                    break;
            }
        }
        public static void Syrups()
        {
            string path = @"Syrup.txt";
            string[] syrup = File.ReadAllLines(path);
            int total_syrup = syrup.Length;
            string pathstock = @"syrup_quantity.txt";
            string[] lines = File.ReadAllLines(pathstock);
            int[] syrup_stock = Array.ConvertAll(lines, s => int.Parse(s));
            for (int i = 0; i < syrup.Length; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, syrup[i]);
            }
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Press 'E' to make and Entry");
            Console.WriteLine("Press 'S' for Syrup Information");
            Console.WriteLine("Press 'M' for menu");
            Console.WriteLine("Press '0' to Logout");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'e':
                case 'E':
                    SyrupEntry(syrup, total_syrup, syrup_stock);
                    break;
                case 's':
                case 'S':
                    SyrupStock(syrup, total_syrup, syrup_stock);
                    break;
                case 'm':
                case 'M':
                    Menu();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Menu();
                    break;
            }
        }
        public static void SyrupEntry(string[] syrup, int total_syrup, int[] syrup_stock)
        {
            Console.WriteLine("Enter:");
            string[,] injection_Entry_Menu = new string[1, 4];
            injection_Entry_Menu[0, 0] = "1 for Adding New Injection";
            injection_Entry_Menu[0, 1] = "2 for Increasing amount of Injection";
            injection_Entry_Menu[0, 2] = "3 for Main Menu";
            injection_Entry_Menu[0, 3] = "4 To Quit";
            for (int i = 0; i < injection_Entry_Menu.GetLength(0); i++)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                for (int j = 0; j < injection_Entry_Menu.GetLength(1); j++)
                {
                    Console.Write("|" + injection_Entry_Menu[i, j] + "| \t");
                }
                Console.WriteLine("\n-------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
            }
            Console.Write("Choice: ");
            char ch = Convert.ToChar(Console.ReadLine());
            switch (ch)
            {
                case '1':
                    AddSyrup(syrup, total_syrup, syrup_stock);
                    break;
                case '2':
                    SyrupIncreaseOne(syrup, total_syrup, syrup_stock);
                    break;
                case '3':
                    Menu();
                    break;
                case '4':
                    Console.WriteLine("Thanks for Using!");
                    break;
                default:
                    Console.WriteLine("Wrong! input Try Again");
                    Console.WriteLine();
                    SyrupEntry(syrup, total_syrup, syrup_stock);
                    break;
            }
        }
        public static int[] AddSyrup(string[] syrup, int total_syrup, int[] syrup_stock)
        {
            Console.Write("How Many Syrup Do You Want To Enter: ");
            int n_o_t = int.Parse(Console.ReadLine());
            syrup = new string[n_o_t];
            syrup_stock = new int[n_o_t];
            for (int i = 0; i < syrup.Length; i++)
            {
                Console.Write("Enter Syrup No.{0}: ", i + 1);
                syrup[i] = Console.ReadLine();
                Console.Write("Enter Quantity of of {1}: ", i + 1, syrup[i]);
                syrup_stock[i] = int.Parse(Console.ReadLine());
            }
            string[] quantity = new string[n_o_t];
            for (int i = 0; i < syrup_stock.Length; i++)
            {
                quantity[i] = "Name: " + syrup[i] + "\t QT:" + syrup_stock[i];
                Console.WriteLine(quantity[i]);
            }
            string[] result = Array.ConvertAll(syrup_stock, x => x.ToString());
            File.AppendAllLines(@"syrup_quantity.txt", result);
            Console.WriteLine();
            File.AppendAllLines(@"Syrup.txt", syrup);
            SyrupEntry(syrup, total_syrup, syrup_stock);
            return syrup_stock;
        }
        public static int[] SyrupIncreaseOne(string[] syrup, int total_syrup, int[] syrup_stock)
        {
            string path1 = @"Syrup.txt";
            string[] Read_stock = File.ReadAllLines(path1);
            string path = @"syrup_quantity.txt";
            string[] Rea_stock = File.ReadAllLines(path);
            for (int i = 0; i < Read_stock.Length; i++)
            {
                Console.WriteLine("{0}.Name: {1} \t QT:{2}", i + 1, Read_stock[i], Rea_stock[i]);
            }
            Console.Write("Enter Syrup Number: ");
            int val1 = int.Parse(Console.ReadLine());
            int val = val1 - 1;
            Console.Write("Enter  Amount of Quantity: ");
            int amount = int.Parse(Console.ReadLine());
            if (val < syrup_stock.Length)
            {
                syrup_stock[val] = syrup_stock[val] + amount;
                Console.WriteLine("Quantity of {0} is now {1}", val + 1, syrup_stock[val]);
                for (int i = 0; i < syrup_stock.Length; i++)
                {
                    syrup_stock[i] = syrup_stock[i];
                }
                string[] result = Array.ConvertAll(syrup_stock, x => x.ToString());
                File.WriteAllLines(@"syrup_quantity.txt", result);
                SyrupEntry(syrup, total_syrup, syrup_stock);
                return syrup_stock;
            }
            else
            {
                Console.WriteLine("Error!");
                SyrupEntry(syrup, total_syrup, syrup_stock);
                return syrup_stock;
            }
        }
        public static void SyrupStock(string[] syrup, int total_syrup, int[] syrup_stock)
        {
            string path1 = @"Syrup.txt";
            string[] Read_stock = File.ReadAllLines(path1);
            string path = @"syrup_quantity.txt";
            string[] Rea_stock = File.ReadAllLines(path);
            for (int i = 0; i < syrup.Length; i++)
            {
                Console.WriteLine("{0}.Name: {1} \t QT:{2}", i + 1, Read_stock[i], Rea_stock[i]);
            }
            Console.Write("Enter Syrup Number: ");
            int val1 = int.Parse(Console.ReadLine());
            int val = val1 - 1;
            Console.Write("Enter  Amount of Quantity: ");
            int amount = int.Parse(Console.ReadLine());
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Press 'E' to make and Entry");
            Console.WriteLine("Press 'S' for Syrup Information");
            Console.WriteLine("Press 'M' for menu");
            Console.WriteLine("Press '0' to Logout");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'e':
                case 'E':
                    SyrupEntry(syrup, total_syrup, syrup_stock);
                    break;
                case 's':
                case 'S':
                    SyrupStock(syrup, total_syrup, syrup_stock);
                    break;
                case 'm':
                case 'M':
                    Menu();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Menu();
                    break;
            }
        }
        public static void Suppliers()
        {
            String path = @"SupplyName.txt";
            if (File.Exists(path))
            {
                Console.WriteLine("Suppliers:");
                StreamReader sr = new StreamReader(path);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                string str = sr.ReadLine();
                while (str != null)
                {
                    Console.WriteLine(str);
                    str = sr.ReadLine();
                }
                Console.ReadLine();
                sr.Close();
            }
            else
            {
                Console.WriteLine("File not found");
            }
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Press 'M' for menu");
            Console.WriteLine("Press 'D' for Dues");
            Console.WriteLine("Press '0' to Logout");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'm':
                case 'M':
                    Menu();
                    break;
                case 'd':
                case 'D':
                    Mortgage();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Menu();
                    break;
            }
        }
        public static void Mortgage()
        {
            String path = @"Mortgage.txt";
            if (File.Exists(path))
            {
                Console.WriteLine("Remaining:");
                StreamReader sr = new StreamReader(path);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                string str = sr.ReadLine();
                while (str != null)
                {
                    Console.WriteLine(str);
                    str = sr.ReadLine();
                }
                Console.ReadLine();
                sr.Close();
            }
            else
            {
                Console.WriteLine("File not found");
            }
            Console.WriteLine("Press 'M' for menu");
            Console.WriteLine("Press '0' to Logout");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'm':
                case 'M':
                    Menu();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Menu();
                    break;
            }
        }
        public static void Purchase()
        {
            Console.WriteLine("View Details:");
            Console.WriteLine("Press 1 for Tablet Information");
            Console.WriteLine("Press 2 for Injections Information");
            Console.WriteLine("Press 3 for Syrups Information");
            char cat = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (cat)
            {
                case '1':
                    TabletsInfo();
                    break;
                case '2':
                    InjectionInfo();
                    break;
                case '3':
                    SyrupsInfo();
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    break;
            }
        }
        public static void TabletsInfo()
        {
            string pathtablet = @"Tablets.txt";
            string[] Read_stock = File.ReadAllLines(pathtablet);
            string path = @"quantity.txt";
            string[] Rea_stock = File.ReadAllLines(path);
            for (int i = 0; i < Read_stock.Length; i++)
            {
                Console.WriteLine("{0}.Name: {1} \t QT:{2}", i + 1, Read_stock[i], Rea_stock[i]);
            }
            Console.Write("Enter Tablet Number: ");
            int val1 = int.Parse(Console.ReadLine());
            int val = val1 - 1;
            Console.Write("Enter Quantity: ");
            int amount = int.Parse(Console.ReadLine());
            Console.Write("Enter Price: ");
            int price = int.Parse(Console.ReadLine());
            int bill = price * amount;
            if (bill >= 5000)
            {
                Console.WriteLine("You will Have 20% Concession");
                double prc = 0.2 * bill;
                double total_bill = bill - prc;
                Console.WriteLine("Bill is Rs.{0}", total_bill);
                Console.Write("Cash Received: ");
                double cr = Convert.ToDouble(Console.ReadLine());
                double remain = cr - total_bill;
                Console.WriteLine("Remaining: {0}", remain);
            }
            else
            {
                Console.WriteLine("Bill is Rs.{0}", bill);
                Console.Write("Cash Received: ");
                int cr = int.Parse(Console.ReadLine());
                int remain = cr - bill;
                Console.WriteLine("Remaining: {0}", remain);
            } 
            Console.WriteLine("Press 'M' for menu");
            Console.WriteLine("Press '0' to Logout");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'm':
                case 'M':
                    Menu();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Menu();
                    break;
            }
        }
        public static void InjectionInfo()
        {
            string pathInjection = @"Injection.txt";
            string[] injection = File.ReadAllLines(pathInjection);
            int total_injection = injection.Length;
            string pathstock = @"injection_quantity.txt";
            string[] lines = File.ReadAllLines(pathstock);
            int[] injection_stock = Array.ConvertAll(lines, s => int.Parse(s));
            for (int i = 0; i < injection.Length; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, injection[i]);

            }
            Console.Write("Enter Injection Number: ");
            int val1 = int.Parse(Console.ReadLine());
            int val = val1 - 1;
            Console.Write("Enter Quantity: ");
            int amount = int.Parse(Console.ReadLine());
            Console.Write("Enter Price: ");
            int price = int.Parse(Console.ReadLine());
            int bill = price * amount;
            if (bill >= 5000)
            {
                Console.WriteLine("You will Have 20% Concession");
                double prc = 0.2 * bill;
                double total_bill = bill - prc;
                Console.WriteLine("Bill is Rs.{0}", total_bill);
                Console.Write("Cash Received: ");
                double cr = Convert.ToDouble(Console.ReadLine());
                double remain = cr - total_bill;
                Console.WriteLine("Remaining: {0}", remain);
            }
            else
            {
                Console.WriteLine("Bill is Rs.{0}", bill);
                Console.Write("Cash Received: ");
                int cr = int.Parse(Console.ReadLine());
                int remain = cr - bill;
                Console.WriteLine("Remaining: {0}", remain);
            }
            Console.WriteLine();
            Console.WriteLine("Press 'M' for menu");
            Console.WriteLine("Press '0' to Logout");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'm':
                case 'M':
                    Menu();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Menu();
                    break;
            }
        }
        public static void SyrupsInfo()
        {
            string pathInjection = @"Syrup.txt";
            string[] injection = File.ReadAllLines(pathInjection);
            int total_injection = injection.Length;
            string pathstock = @"syrup_quantity.txt";
            string[] lines = File.ReadAllLines(pathstock);
            int[] injection_stock = Array.ConvertAll(lines, s => int.Parse(s));
            for (int i = 0; i < injection.Length; i++)
            {
                Console.WriteLine("{0}.{1}", i + 1, injection[i]);

            }
            Console.Write("Enter Syrup Number: ");
            int val1 = int.Parse(Console.ReadLine());
            int val = val1 - 1;
            Console.Write("Enter Quantity: ");
            int amount = int.Parse(Console.ReadLine());
            Console.Write("Enter Price: ");
            int price = int.Parse(Console.ReadLine());
            int bill = price * amount;
            if (bill >= 5000)
            {
                Console.WriteLine("You will Have 20% Concession");
                double prc = 0.2 * bill;
                double total_bill = bill - prc;
                Console.WriteLine("Bill is Rs.{0}", total_bill);
                Console.Write("Cash Received: ");
                double cr = Convert.ToDouble(Console.ReadLine());
                double remain = cr - total_bill;
                Console.WriteLine("Remaining: {0}", remain);
            }
            else
            {
                Console.WriteLine("Bill is Rs.{0}", bill);
                Console.Write("Cash Received: ");
                int cr = int.Parse(Console.ReadLine());
                int remain = cr - bill;
                Console.WriteLine("Remaining: {0}", remain);
            }
            Console.WriteLine();
            Console.WriteLine("Press 'M' for menu");
            Console.WriteLine("Press '0' to Logout");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'm':
                case 'M':
                    Menu();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Menu();
                    break;
            }
        }
        public static void Billing()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Press 'D' To Creat Fee Bill");
            Console.WriteLine("Press 'S' Show Bill");
            Console.WriteLine("Press 'T' Show Total Amount");

            Console.WriteLine("Press '0' to Logout");
            Console.WriteLine("Press 'M' for Menu");
            char input = Convert.ToChar(Console.ReadLine());
            Console.WriteLine();
            switch (input)
            {
                case 'm':
                case 'M':
                    Menu();
                    break;
                case 'd':
                case 'D':
                    BillingAdd();
                    break;
                case 's':
                case 'S':
                    BillingShow();
                    break;
                case 'T':
                case 't':
                    BillingTotal();
                    break;
                case '0':
                    Console.WriteLine("See You Sir, Have a Good Day");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Menu();
                    break;
            }
        }
        public static void BillingAdd()
        {
            Console.Write("How Many Billing Info You Wanna Use: ");
            int n_o_t = int.Parse(Console.ReadLine());
            string[] Product = new string[n_o_t];
            int[] Amount = new int[n_o_t];
            int[] discount = new int[n_o_t];
            int[] QNT = new int[n_o_t];
            for (int i = 0; i < Product.Length; i++)
            {
                Console.Write("Enter Product: ", i + 1);
                Product[i] = Console.ReadLine();
                Console.Write("Enter Price of Product: ", i + 1, Product[i]);
                Amount[i] = int.Parse(Console.ReadLine());
                Console.Write("Enter DisCount no.{0}: ", i + 1);
                discount[i] = int.Parse(Console.ReadLine());
                Console.Write("Enter Item Quantities no.{0}: ", i + 1);
                QNT[i] = int.Parse(Console.ReadLine());
            }
            string[] bill = new string[n_o_t];
            for (int i = 0; i < Product.Length; i++)
            {
                bill[i] = "Name: " + Product[i] + "\t Price:" + Amount[i] + "\t Discount:" + discount[i] + "\t No. Of Items:" + QNT[i];
                Console.WriteLine(bill[i]);
            }
            string[] result = Array.ConvertAll(Amount, x => x.ToString());
            File.AppendAllLines(@"Amount.txt", result);
            string[] Dis = Array.ConvertAll(discount, x => x.ToString());
            File.AppendAllLines(@"discount.txt", Dis);
            string[] QT = Array.ConvertAll(QNT, x => x.ToString());
            File.AppendAllLines(@"QNT.txt", QT);
            Console.WriteLine();
            File.AppendAllLines(@"Product.txt", Product);
            Billing();
        }
        public static void BillingShow()
        {
            string path = @"Amount.txt";
            string path1 = @"discount.txt";
            string path2 = @"QNT.txt";
            string path3 = @"Product.txt";
            string[] Product = File.ReadAllLines(path3);
            int total = Product.Length - 1;
            string[] lines = File.ReadAllLines(path);
            int[] Amount = Array.ConvertAll(lines, s => int.Parse(s));
            string[] lines1 = File.ReadAllLines(path1);
            int[] discount = Array.ConvertAll(lines1, s1 => int.Parse(s1));
            string[] lines2 = File.ReadAllLines(path2);
            int[] QNT = Array.ConvertAll(lines2, s2 => int.Parse(s2));
            string[] bill = new string[total];
            for (int i = 0; i < total; i++)
            {
                bill[i] = "Product_Name: " + Product[i] + "\t Price:" + Amount[i] + "\t Discount:" + discount[i] + "\t No. Of Items:" + QNT[i];
                Console.WriteLine(bill[i]);
            }
            Billing();
        }
        public static void BillingTotal()
        {
            string path = @"Amount.txt";
            string path1 = @"discount.txt";
            string path2 = @"QNT.txt";
            string path3 = @"Product.txt";
            string[] Product = File.ReadAllLines(path3);
            int total = Product.Length - 1;
            string[] lines = File.ReadAllLines(path);
            int[] Amount = Array.ConvertAll(lines, s => int.Parse(s));
            string[] lines1 = File.ReadAllLines(path1);
            int[] discount = Array.ConvertAll(lines1, s1 => int.Parse(s1));
            string[] lines2 = File.ReadAllLines(path2);
            int[] QNT = Array.ConvertAll(lines2, s2 => int.Parse(s2));
            string[] bill = new string[total];
            for (int i = 0; i < total; i++)
            {
                bill[i] = "Product_Name: " + Product[i] + "\t Price:" + Amount[i] + "\t Discount:" + discount[i] + "\t No. Of Items:" + QNT[i];
                Console.WriteLine(bill[i]);
            }
            double sum = 0;
            for (int i = 0; i < Amount.Length; i++)
            {
                sum = sum + Amount[i];
            }
            Console.WriteLine("Total Amount :" + sum);
            Billing();
        }
    }
}