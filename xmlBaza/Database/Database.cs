using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Database {
    public class Database {

        public string Name { get; set; }
        public Dictionary<int, Element> Elements = null;

        public Database (string name) {
            Name = name;
            Elements = LoadFromDisk();
        }

        public List<Element> ElementsToList() {
            return Elements.Values.ToList();
        }

        public bool AddElement(Element e) {
            if (Elements.ContainsKey(e.Id)) {
                return false;
            } else {
                Elements.Add(e.Id, e);
                SaveToDisk();
                return true;
            }
        }

        public bool EditElement(Element e) {
            if (Elements.ContainsKey(e.Id)) {
                Elements[e.Id] = e;
                SaveToDisk();
                return true;
            } else {
                return false;
            }
        }

        public float MedianIncomeByCity(string city) {
            return MedianIncomeByCityCalculate(city, 20, 30);
        }

        private float MedianIncomeByCityCalculate(string city, int ageMin, int ageMax) {
            List<Element> allElems = ElementsToList();
            allElems = allElems.FindAll(x => (x.City.ToLower() == city.ToLower() && x.Age >= ageMin && x.Age <= ageMax));

            float incomeSum = 0;
            foreach (Element tmpE in allElems) {
                incomeSum += tmpE.Income;
            }

            if (allElems.Count > 0) {
                return incomeSum / allElems.Count;
            }
            return 0;
        }

        public float MedianIncomeByCountry(string country, int year) {
            return MedianIncomeByCountryCalculate(country, year);
        }

        private float MedianIncomeByCountryCalculate(string country, int year) {
            List<Element> allElems = ElementsToList();
            allElems = allElems.FindAll(x => (x.Country.ToLower() == country.ToLower() && x.Year == year));

            float incomeSum = 0;
            foreach (Element tmpE in allElems) {
                incomeSum += tmpE.Income;
            }

            if (allElems.Count > 0) {
                return incomeSum / allElems.Count;
            }
            return 0;
        }
        
        public Element MaxIncomeByCountry(string country) {
            Dictionary<string, Element> highestIncomeByCountry = new Dictionary<string, Element>();
            
            foreach(KeyValuePair<int, Element> kvp in Elements) {
                string tmpCountry = kvp.Value.Country.ToLower();
                if (highestIncomeByCountry.ContainsKey(tmpCountry)) {
                    if (highestIncomeByCountry[tmpCountry].Income < kvp.Value.Income) {
                        highestIncomeByCountry[tmpCountry] = kvp.Value;
                    }
                } else {
                    highestIncomeByCountry.Add(tmpCountry, kvp.Value);
                }
            }

            return highestIncomeByCountry[country.ToLower()];
        }

        private void SaveToDisk() {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<Element>));
            var xml = "";

            using (var sww = new StringWriter()) {
                using (XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings() { Indent = true })) {
                    xsSubmit.Serialize(writer, ElementsToList());
                    xml = sww.ToString();
                }
            }

            using (StreamWriter sw = new StreamWriter(Name)) {
                sw.Write(xml);
            }
        }

        private Dictionary<int, Element> LoadFromDisk() {
            List<Element> allElements = null;
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Element>));

            using (StreamReader sr = new StreamReader(Name)) {
                allElements = (List<Element>)deserializer.Deserialize(sr);
            }
            
            if (allElements == null) {
                return new Dictionary<int, Element>();
            }

            Dictionary<int, Element> returnVal = new Dictionary<int, Element>();
            foreach (Element el in allElements) {
                if (!returnVal.ContainsKey(el.Id)) {
                    returnVal.Add(el.Id, el);
                }
            }

            return returnVal;
        }
    }
}
