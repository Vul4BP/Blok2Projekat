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
            Console.WriteLine("ID:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Country:");
            string country = Console.ReadLine();
            Console.WriteLine("Age:");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Income:");
            float income = float.Parse(Console.ReadLine());
            Console.WriteLine("City:");
            string city = Console.ReadLine();
            Console.WriteLine("Year:");
            int year = int.Parse(Console.ReadLine());

            return new Element()
            {
                Id = id,
                Country = country,
                Age = age,
                City = city,
                Income = income,
                Year = year
            };
        }
    }
}
