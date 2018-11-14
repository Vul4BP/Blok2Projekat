using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IMainService
    {
        [OperationContract]
        bool CreateDB(string name);
        [OperationContract]
        bool DeleteDB(string name);
        [OperationContract]
        bool WriteDB(string name, Element element);
        [OperationContract]
        bool EditDB(string name, Element element);
        [OperationContract]
        List<Element> ReadDB(string name);
        [OperationContract]
        float MedianMonthlyIncomeByCity(string name, string city);
        [OperationContract]
        float MedianMonthlyIncome(string name, string country, int year);
        [OperationContract]
        Dictionary<string, Element> MaxIncomeByCountry(string name);
    }
}
