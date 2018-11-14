using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IWriterService
    {
        [OperationContract]
        bool CreateDB(string name, byte[] signature);
        [OperationContract]
        bool DeleteDB(string name, byte[] signature);
        [OperationContract]
        bool WriteDB(string name, Element element, byte[] signature);
        [OperationContract]
        bool EditDB(string name, Element element, byte[] signature);
        [OperationContract]
        List<Element> ReadDB(string name, byte[] signature);
        [OperationContract]
        float MedianMonthlyIncomeByCity(string name, string city, byte[] signature);
        [OperationContract]
        float MedianMonthlyIncome(string name, string country, int year, byte[] signature);
        [OperationContract]
        Dictionary<string, Element> MaxIncomeByCountry(string name, byte[] signature);
    }
}
