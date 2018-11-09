using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DatabaseManager
{
    public class Database
    {

        public string Name { get; set; }
        public Dictionary<int, Element> Elements = null;

        //trebace negde if (!Directory.Exists(@"C:\my\dir")) Directory.CreateDirectory(@"C:\my\dir");

        public Database(string name)
        {
            Name = name + ".xml";
            Elements = LoadFromDisk();
        }

        public void ForceSaveToDisk()
        {
            SaveToDisk();
        }
        //citanje
        public List<Element> ElementsToList()
        {
            return Elements.Values.ToList();
        }

        private int getNextId()
        {
            try
            {
                return Elements.Keys.ToList().Max() + 1;
            } catch
            {
                return 1;
            }
        }

        public bool AddElement(Element e)
        {
            int elemId = getNextId();
            if (Elements.ContainsKey(elemId))
            {
                return false;
            }
            else
            {
                e.Id = elemId;
                Elements.Add(e.Id, e);
                SaveToDisk();
                return true;
            }
        }

        public bool EditElement(Element e)
        {
            if (Elements.ContainsKey(e.Id))
            {
                Elements[e.Id] = e;
                SaveToDisk();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDB()
        {
            if (!ArchiveDatabase())
            {
                return false;
            }
            try
            {
                File.Delete(Config.DBsPath + Name);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private bool ArchiveDatabase()
        {
            if (!Directory.Exists(Config.Archived_DBsPath))
                Directory.CreateDirectory(Config.Archived_DBsPath);

            try
            {
                File.Copy(Config.DBsPath + Name, Config.Archived_DBsPath + Name, true);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public float MedianIncomeByCity(string city)
        {
            return MedianIncomeByCityCalculate(city, 20, 30);
        }

        private float MedianIncomeByCityCalculate(string city, int ageMin, int ageMax)
        {
            List<Element> allElems = ElementsToList();
            allElems = allElems.FindAll(x => (x.City.ToLower() == city.ToLower() && x.Age >= ageMin && x.Age <= ageMax));

            float incomeSum = 0;
            foreach (Element tmpE in allElems)
            {
                incomeSum += tmpE.Income;
            }

            if (allElems.Count > 0)
            {
                return incomeSum / allElems.Count;
            }
            return 0;
        }

        public float MedianIncomeByCountry(string country, int year)
        {
            return MedianIncomeByCountryCalculate(country, year);
        }

        private float MedianIncomeByCountryCalculate(string country, int year)
        {
            List<Element> allElems = ElementsToList();
            allElems = allElems.FindAll(x => (x.Country.ToLower() == country.ToLower() && x.Year == year));

            float incomeSum = 0;
            foreach (Element tmpE in allElems)
            {
                incomeSum += tmpE.Income;
            }

            if (allElems.Count > 0)
            {
                return incomeSum / allElems.Count;
            }
            return 0;
        }

        public Dictionary<string, Element> MaxIncomeByCountry()
        {
            Dictionary<string, Element> highestIncomeByCountry = new Dictionary<string, Element>();

            foreach (KeyValuePair<int, Element> kvp in Elements)
            {
                string tmpCountry = kvp.Value.Country.ToLower();
                if (highestIncomeByCountry.ContainsKey(tmpCountry))
                {
                    if (highestIncomeByCountry[tmpCountry].Income < kvp.Value.Income)
                    {
                        highestIncomeByCountry[tmpCountry] = kvp.Value;
                    }
                }
                else
                {
                    highestIncomeByCountry.Add(tmpCountry, kvp.Value);
                }
            }

            return highestIncomeByCountry;
        }

        private void SaveToDisk()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<Element>));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings() { Indent = true }))
                {
                    xsSubmit.Serialize(writer, ElementsToList());
                    xml = sww.ToString();
                }
            }

            if (!Directory.Exists(Config.DBsPath))
                Directory.CreateDirectory(Config.DBsPath);
      
            using (StreamWriter sw = new StreamWriter(Config.DBsPath + Name))
            {
                sw.Write(xml);
            }
        }

        private Dictionary<int, Element> LoadFromDisk()
        {
            List<Element> allElements = null;
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Element>));

            try
            {
                using (StreamReader sr = new StreamReader(Config.DBsPath + Name))
                {
                    allElements = (List<Element>)deserializer.Deserialize(sr);
                }
            }
            catch
            {
                //Console.WriteLine(e.Message);
                //Console.WriteLine("Database LoadFromDisk: " + e.ToString());
                return new Dictionary<int, Element>();
            }

            if (allElements == null)
            {
                return new Dictionary<int, Element>();
            }

            Dictionary<int, Element> returnVal = new Dictionary<int, Element>();
            foreach (Element el in allElements)
            {
                if (!returnVal.ContainsKey(el.Id))
                {
                    returnVal.Add(el.Id, el);
                }
            }

            return returnVal;
        }
    }
}
