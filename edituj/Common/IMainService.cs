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
        bool WriteDB(string name,string txt);
        [OperationContract]
        bool EditDB(string name, string txt);
        [OperationContract]
        bool ReadDB(string name);
        [OperationContract]
        bool MedianMonthlyIncomeByCity(string city);
        [OperationContract]
        bool MedianMonthlyIncome(string country, int year);
        [OperationContract]
        bool MaxIncomeByCountry();
    }
}
