using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Element
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public float Income { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public int Year { get; set; }

        public Element()
        {
            
        }

        public override string ToString()
        {
            string tmp = "ID: " + Id.ToString() + "\n";
            tmp += "Country: " + Country + "\n";
            tmp += "Age: " + Age + "\n";
            tmp += "Income: " + Income + "\n";
            tmp += "City: " + City + "\n";
            tmp += "Year: " + Year + "\n";

            return tmp;
        }

        public static Element LoadFromConsole()
        {
            //Console.WriteLine("ID:");
            //int id = int.Parse(Console.ReadLine());
            //Console.WriteLine("Country:");
            //string country = Console.ReadLine();
            string country = HelperFunctions.ReadCountry();
            //Console.WriteLine("Age:");
            //int age = int.Parse(Console.ReadLine());
            int age = HelperFunctions.ReadAge();
            //Console.WriteLine("Income:");
            //float income = float.Parse(Console.ReadLine());
            float income = HelperFunctions.ReadIncome();
            //Console.WriteLine("City:");
            string city = HelperFunctions.ReadCity();
            //Console.WriteLine("Year:");
            int year = HelperFunctions.ReadYear();

            return new Element()
            {
                Id = -1, //id ce biti dodeljen prilikom dodavanja u bazu
                Country = country,
                Age = age,
                City = city,
                Income = income,
                Year = year
            };
        }
    }
}
