using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DatabaseManager;

namespace Servis {
    public class CommandExecutor : IMainService {
        public bool CreateDB(string name) {
            Console.WriteLine("Command: CREATEDB " + name);
            Database db = new Database(name);
            db.ForceSaveToDisk();
            return true;

        }

        public bool DeleteDB(string name) {
            Console.WriteLine("Command: DELETEDB " + name);
            Database db = new Database(name);
            return db.DeleteDB();
        }

        public bool EditDB(string name, Element element) {
            Console.WriteLine("Command: EDIT " + name);
            Database db = new Database(name);
            return db.EditElement(element);
        }

        public Dictionary<string, Element> MaxIncomeByCountry(string name) {
            Console.WriteLine("Command: MaxIncomeByCountry");
            Database db = new Database(name);
            return db.MaxIncomeByCountry();
        }

        public float MedianMonthlyIncome(string name, string country, int year) {
            Console.WriteLine("Command: MedianMonthlyIncome " + country + " year: " + year.ToString());
            Database db = new Database(name);
            return db.MedianIncomeByCountry(country, year);
        }

        public float MedianMonthlyIncomeByCity(string name, string city) {
            Console.WriteLine("Command: MedianMonthlyIncome " + city);
            Database db = new Database(name);
            return db.MedianIncomeByCity(city);
        }

        public List<Element> ReadDB(string name) {
            Console.WriteLine("Command: ReadDB " + name);
            Database db = new Database(name);
            return db.ElementsToList();
        }

        public bool WriteDB(string name, Element e) {
            Console.WriteLine("Command: WriteDB " + name);
            Database db = new Database(name);
            return db.AddElement(e);
        }
    }
}
