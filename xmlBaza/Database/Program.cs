using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database {
    class Program {
        static void Main(string[] args) {
            Database baza = new Database("imeBaze.xml");

            //ako nemas fajl pokreni ovaj kod
            //kad napravi fajl onda opet zakomentarisi i treba da bude isti rezultat
            /*Element el = Element.LoadFromConsole();
            Element el2 = new Element() { Id = 2, Age = 22, City = "Novi Sad", Country = "Srbija", Income = 200, Year = 2013 };
            Element el3 = new Element() { Id = 3, Age = 24, City = "Novi Sad", Country = "Srbija", Income = 250, Year = 2013 };
            Element el4 = new Element() { Id = 4, Age = 26, City = "Novi Sad", Country = "USA", Income = 300, Year = 2015 };
            Element el5 = new Element() { Id = 5, Age = 29, City = "Beograd", Country = "Srbija", Income = 240, Year = 2012 };
            Element el6 = new Element() { Id = 6, Age = 32, City = "Beograd", Country = "Srbija", Income = 120, Year = 2013 };
            Element el7 = new Element() { Id = 7, Age = 34, City = "Kac", Country = "USA", Income = 500, Year = 2014 };

            baza.AddElement(el);
            baza.AddElement(el2);
            baza.AddElement(el3);
            baza.AddElement(el4);
            baza.AddElement(el5);
            baza.AddElement(el6);
            baza.AddElement(el7);*/

            Console.WriteLine("All elements");
            foreach (Element e in baza.ElementsToList()) {
                Console.WriteLine(e);
            }
            Console.WriteLine("=============");
            Console.WriteLine("MedianIncome Novi Sad");
            Console.WriteLine(baza.MedianIncomeByCity("Novi Sad"));
            Console.WriteLine("MedianIncome Serbia 2013");
            Console.WriteLine(baza.MedianIncomeByCountry("Srbija", 2013));
            Console.WriteLine("MaxIncome by country Srbija");
            Console.WriteLine(baza.MaxIncomeByCountry("SrBiJa"));
        }
    }
}
