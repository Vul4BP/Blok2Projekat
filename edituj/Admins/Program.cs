using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Admins
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AdminProxy proxy = new AdminProxy(new NetTcpBinding(), Config.AdminServiceAddress))
            {
                int op = -1;

                while (op != 0) {
                    op = DisplayAdminMenu();
                    ExecuteCommand(proxy, op);
                }
            }
        }

        static int DisplayAdminMenu()
        {
            return HelperFunctions.DisplayDefaultMenu("Admin menu");
        }

        static void ExecuteCommand(AdminProxy proxy, int op)
        {
            string name = "";
            if (op != 0)
            {
                name = ReadDatabaseName();
            }
            switch (op)
            {
                case 1:
                    proxy.CreateDB(name);
                    break;
                case 2:
                    proxy.DeleteDB(name);
                    break;
                case 3:
                    Element tmpElem = Element.LoadFromConsole();
                    proxy.WriteDB(name, tmpElem);
                    break;
                case 4:
                    List<Element> elems = proxy.ReadDB(name);
                    Console.WriteLine("Ids of all elements:");

                    foreach (Element tmpElem2 in elems)
                    {
                        Console.WriteLine(tmpElem2.Id);
                    }

                    Element toEdit = null;
                    while (toEdit == null)
                    {
                        Console.WriteLine("Unesi id elementa koji menjas:");
                        int idEditElem = int.Parse(Console.ReadLine());
                        toEdit = elems.Find(x => x.Id == idEditElem);

                        if (toEdit == null)
                        {
                            Console.WriteLine("Ne postoji element sa tim IDem");
                        }
                    }

                    Element newElem = Element.LoadFromConsole();
                    newElem.Id = toEdit.Id;

                    proxy.EditDB(name, newElem);
                    break;
                case 5:
                    foreach(Element tmpElem3 in proxy.ReadDB(name))
                    {
                        Console.WriteLine(tmpElem3);
                    }
                    break;
                case 6:
                    string city = ReadCity();
                    Console.Write("Prosecna plata za grad " + city + ": ");
                    Console.WriteLine(proxy.MedianMonthlyIncomeByCity(name, city));
                    break;
                case 7:
                    string country = ReadCountry();
                    Console.WriteLine("Unesi godinu:");
                    int year = int.Parse(Console.ReadLine());
                    proxy.MedianMonthlyIncome(name,country,year);
                    break;
                case 8:
                    var tmpDict = proxy.MaxIncomeByCountry(name);
                    foreach(KeyValuePair<string, Element> kvp in tmpDict)
                    {
                        Console.WriteLine(kvp.Key + " : id:" + kvp.Value.Id + " income:" + kvp.Value.Income);
                    }
                    break;
                case 0:
                    Console.WriteLine("Cao poz");
                    break;
            }
        }

        private static string ReadDatabaseName()
        {
            Console.WriteLine("Unesi naziv baze:");
            string name = Console.ReadLine().Trim();

            while (name.Length == 0)
            {
                Console.WriteLine("Unesi naziv baze opet:");
                name = Console.ReadLine().Trim();
            }

            return name;
        }

        private static string ReadCountry()
        {
            Console.WriteLine("Unesi naziv drzave:");
            string name = Console.ReadLine().Trim();

            while (name.Length == 0)
            {
                Console.WriteLine("Unesi naziv drzave opet:");
                name = Console.ReadLine().Trim();
            }

            if (name.ToLower() == "kosovo")
            {
                Console.WriteLine("drzava ne postoji");
            }

            return name;
        }

        private static string ReadCity()
        {
            Console.WriteLine("Unesi naziv grada:");
            string name = Console.ReadLine().Trim();

            while (name.Length == 0)
            {
                Console.WriteLine("Unesi naziv grada opet:");
                name = Console.ReadLine().Trim();
            }

            return name;
        }
    }
}
