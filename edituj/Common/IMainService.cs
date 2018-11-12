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
        //Element
        [OperationContract]
        List<Element> ReadDB(string name);
        //float
        [OperationContract]
        float MedianMonthlyIncomeByCity(string name, string city);
        //float
        [OperationContract]
        float MedianMonthlyIncome(string name, string country, int year);
        //list<Element>
        [OperationContract]
        Dictionary<string, Element> MaxIncomeByCountry(string name);
    }
}
