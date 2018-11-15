using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using Manager;
using System.Text.RegularExpressions;
using System.IO;

namespace Common
{
    public class HelperFunctions
    {
        public static NetTcpBinding SetBindingSecurity(NetTcpBinding binding) {
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
		  
		  binding.ReceiveTimeout = TimeSpan.FromMinutes(30);
		  binding.SendTimeout = TimeSpan.FromMinutes(30);
            return binding;
        }

        public static Tuple<NetTcpBinding, EndpointAddress> PrepBindingAndAddressForWriter(string ServiceCertCN) {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, ServiceCertCN);
            EndpointAddress address = new EndpointAddress(new Uri(Config.WriterServiceAddress), new X509CertificateEndpointIdentity(srvCert));
		  
		  binding.ReceiveTimeout = TimeSpan.FromMinutes(30);
		  binding.SendTimeout = TimeSpan.FromMinutes(30);
		  
            return new Tuple<NetTcpBinding, EndpointAddress>(binding, address);
        }

        public static Tuple<NetTcpBinding, EndpointAddress> PrepBindingAndAddressForReader(string ServiceCertCN)
        {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, ServiceCertCN);
            EndpointAddress address = new EndpointAddress(new Uri(Config.ReaderServiceAddress), new X509CertificateEndpointIdentity(srvCert));
		  
		  binding.ReceiveTimeout = TimeSpan.FromMinutes(30);
		  binding.SendTimeout = TimeSpan.FromMinutes(30);
		  
            return new Tuple<NetTcpBinding, EndpointAddress>(binding, address);
        }

        public static Roles RoleFromString(string txt) {
            var tmp = txt.Trim().ToLower();
            if (tmp == "admin") {
                return Roles.Admin;
            } else if (tmp == "reader") {
                return Roles.Reader;
            } else if (tmp == "writer") {
                return Roles.Writer;
            } else if (tmp == "unset") {
                return Roles.Unset;
            }
            throw new Exception("Role parsing error");
        }

        public static Permissions PermissionFromString(string txt) {
            var tmp = txt.Trim().ToLower();
            if (tmp == "createdb") {
                return Permissions.CreateDB;
            } else if (tmp == "deletedb") {
                return Permissions.DeleteDB;
            } else if (tmp == "readdb") {
                return Permissions.ReadDB;
            } else if (tmp == "writedb") {
                return Permissions.WriteDB;
            } else if (tmp == "editdb") {
                return Permissions.EditDB;
            } else if (tmp == "unset") {
                return Permissions.Unset;
            }

            throw new Exception("Permission parsing error");
        }

        public static int DisplayDefaultMenu(string title)
        {
            int option = -1;

            while (option < 0 || option > 8)
            {
                Console.WriteLine("========" + title + "========");
                Console.WriteLine("1 Napravi bazu");
                Console.WriteLine("2 Obrisi bazu");
                Console.WriteLine("3 Upisi u bazu");
                Console.WriteLine("4 Izmeni bazu");
                Console.WriteLine("5 Procitaj bazu");
                Console.WriteLine("6 MedianMonthlyIncomeByCity");
                Console.WriteLine("7 MedianMonthlyIncome");
                Console.WriteLine("8 MaxIncomeByCountry");
                Console.WriteLine("0 Izidji");
                Console.Write(">");

                string txtInput = Console.ReadLine().Trim();

                if (txtInput.Length == 0) {
                    Console.WriteLine("Unesi komandu...");
                    continue;
                }

                try {
                    option = int.Parse(txtInput);
                } catch {
                    option = -1;
                }
            }

            return option;
        }

        public static string ReadDatabaseName() {
            Console.WriteLine("Unesi naziv baze:");
            string name = Console.ReadLine().Trim();

            while (name.Length == 0) {
                Console.WriteLine("Unesi naziv baze opet:");
                name = Console.ReadLine().Trim();
            }

            return name;
        }

        public static string ReadCountry() {
            Console.WriteLine("Unesi naziv drzave:");
            string name = Console.ReadLine().Trim();

            while (name.Length == 0 || !Regex.IsMatch(name, @"^[a-zA-Z ]+$")) {
                Console.WriteLine("Unesi naziv drzave opet:");
                name = Console.ReadLine().Trim();
            }

            if (name.ToLower() == "kosovo") {
                Console.WriteLine("drzava ne postoji");
            }

            return name;
        }

        public static int ReadAge()
        {
            Console.WriteLine("Unesi godine:");
            string name = Console.ReadLine().Trim();

            while (name.Length == 0 || !Regex.IsMatch(name, @"^[0-9]+$"))
            {
                Console.WriteLine("Unesi godine opet:");
                name = Console.ReadLine().Trim();
            }
            int age = Int32.Parse(name);
            return age;
        }
        public static float ReadIncome()
        {
            Console.WriteLine("Unesi zaradu:");
            string name = Console.ReadLine().Trim();

            while (name.Length == 0 || !Regex.IsMatch(name, @"^[0-9]*(?:\.[0-9]*)?$"))
            {
                Console.WriteLine("Unesi zaradu opet:");
                name = Console.ReadLine().Trim();
            }
            float income = float.Parse(name);
            return income;
        }
        public static string ReadCity() {
            Console.WriteLine("Unesi naziv grada:");
            string name = Console.ReadLine().Trim();

            while (name.Length == 0 || !Regex.IsMatch(name, @"^[a-zA-Z ]+$")) {
                Console.WriteLine("Unesi naziv grada opet:");
                name = Console.ReadLine().Trim();
            }

            return name;
        }
        public static int ReadYear()
        {
            Console.WriteLine("Unesi godinu:");
            string name = Console.ReadLine().Trim();

            while (name.Length == 0 || !Regex.IsMatch(name, @"^[0-9]+$"))
            {
                Console.WriteLine("Unesi godinu opet:");
                name = Console.ReadLine().Trim();
            }
            int year = Int32.Parse(name);
            return year;
        }

        public static void ExecuteCommandAdmin(IMainService proxy, int op ) {
            string name = "";
            if (op != 0) {
                name = HelperFunctions.ReadDatabaseName();
            }
            switch (op) {
                case 1:
                    CheckIfExecuted(proxy.CreateDB(name));
                    break;
                case 2:
                    CheckIfExecuted(proxy.DeleteDB(name));
                    break;
                case 3:
                    Element tmpElem = Element.LoadFromConsole();
                    CheckIfExecuted(proxy.WriteDB(name, tmpElem));
                    break;
                case 4:
                    List<Element> elems = proxy.ReadDB(name);
                    Console.WriteLine("Ids of all elements:");
                    DisplayAllElements(elems, true);

                    Element toEdit = GetElementToEdit(elems);

                    Element newElem = Element.LoadFromConsole();
                    newElem.Id = toEdit.Id;
                    CheckIfExecuted(proxy.EditDB(name, newElem));
                    break;
                case 5:
                    DisplayAllElements(proxy.ReadDB(name));
                    break;
                case 6:
                    string city = HelperFunctions.ReadCity();
                    Console.Write("Prosecna plata za grad " + city + ": ");
                    Console.WriteLine(proxy.MedianMonthlyIncomeByCity(name, city));
                    break;
                case 7:
                    string country = HelperFunctions.ReadCountry();
                    Console.WriteLine("Unesi godinu:");
                    int year = int.Parse(Console.ReadLine());
                    float medianMonthlyIncome = proxy.MedianMonthlyIncome(name, country, year);
                    Console.WriteLine("Prosecna plata za " + country + " u " + year + " god.:" + medianMonthlyIncome);
                    break;
                case 8:
                    var tmpDict = proxy.MaxIncomeByCountry(name);
                    Console.WriteLine("Najveca plata za svaku drzavu:");
                    foreach (KeyValuePair<string, Element> kvp in tmpDict) {
                        Console.WriteLine(kvp.Key + " : id:" + kvp.Value.Id + " plata:" + kvp.Value.Income);
                    }
                    break;
                case 0:
                    Console.WriteLine("Cao poz");
                    break;
            }
        }

        public static void ExecuteCommandWriter(IWriterService proxy, int op, X509Certificate2 signCert)
        {
            string name = "";
            if (op != 0)
            {
                name = HelperFunctions.ReadDatabaseName();
            }
            switch (op)
            {
                case 1:
                    CheckIfExecuted(proxy.CreateDB(name,DigitalSignature.Create(name,"SHA1",signCert)));
                    break;
                case 2:
                    CheckIfExecuted(proxy.DeleteDB(name,DigitalSignature.Create(name, "SHA1", signCert)));
                    break;
                case 3:
                    Element tmpElem = Element.LoadFromConsole();
                    CheckIfExecuted(proxy.WriteDB(name, tmpElem, DigitalSignature.Create(name, "SHA1", signCert)));
                    break;
                case 4:
                    List<Element> elems = proxy.ReadDB(name, DigitalSignature.Create(name, "SHA1", signCert));
                    Console.WriteLine("Ids of all elements:");
                    DisplayAllElements(elems, true);

                    Element toEdit = GetElementToEdit(elems);

                    Element newElem = Element.LoadFromConsole();
                    newElem.Id = toEdit.Id;
                    CheckIfExecuted(proxy.EditDB(name, newElem, DigitalSignature.Create(name, "SHA1", signCert)));
                    break;
                case 5:
                    DisplayAllElements(proxy.ReadDB(name, DigitalSignature.Create(name, "SHA1", signCert)));
                    break;
                case 6:
                    string city = HelperFunctions.ReadCity();
                    Console.Write("Prosecna plata za grad " + city + ": ");
                    Console.WriteLine(proxy.MedianMonthlyIncomeByCity(name, city, DigitalSignature.Create(name, "SHA1", signCert)));
                    break;
                case 7:
                    string country = HelperFunctions.ReadCountry();
                    Console.WriteLine("Unesi godinu:");
                    int year = int.Parse(Console.ReadLine());
                    float medianMonthlyIncome = proxy.MedianMonthlyIncome(name, country, year, DigitalSignature.Create(name, "SHA1", signCert));
                    Console.WriteLine("Prosecna plata za " + country + " u " + year + " god.:" + medianMonthlyIncome);
                    break;
                case 8:
                    var tmpDict = proxy.MaxIncomeByCountry(name, DigitalSignature.Create(name, "SHA1", signCert));
                    Console.WriteLine("Najveca plata za svaku drzavu:");
                    foreach (KeyValuePair<string, Element> kvp in tmpDict)
                    {
                        Console.WriteLine(kvp.Key + " : id:" + kvp.Value.Id + " plata:" + kvp.Value.Income);
                    }
                    break;
                case 0:
                    Console.WriteLine("Cao poz");
                    break;
            }
        }

        public static void ExecuteCommandReader(IReaderService proxy, int op)
        {
            string name = "";
            if (op != 0)
            {
                name = HelperFunctions.ReadDatabaseName();
            }
            try
            {
                switch (op)
                {
                    case 1:
                        CheckIfExecuted(proxy.CreateDB(name).Item1);
                        break;
                    case 2:
                        CheckIfExecuted(proxy.DeleteDB(name).Item1);
                        break;
                    case 3:
                        Element tmpElem = Element.LoadFromConsole();
                        CheckIfExecuted(proxy.WriteDB(name, tmpElem).Item1);
                        break;
                    case 4:
                        List<Element> elems = proxy.ReadDB(name).Item1;
                        Console.WriteLine("Ids of all elements:");
                        DisplayAllElements(elems, true);

                        Element toEdit = GetElementToEdit(elems);

                        Element newElem = Element.LoadFromConsole();
                        newElem.Id = toEdit.Id;
                        CheckIfExecuted(proxy.EditDB(name, newElem).Item1);
                        break;
                    case 5:
                        DisplayAllElements(proxy.ReadDB(name).Item1);
                        break;
                    case 6:
                        string city = HelperFunctions.ReadCity();
                        float medianMonthlyIncomeByCity = proxy.MedianMonthlyIncomeByCity(name, city).Item1;
                        if(medianMonthlyIncomeByCity>=0)
                            Console.WriteLine("Prosecna plata za grad " + city + ": " + medianMonthlyIncomeByCity);
                        break;
                    case 7:
                        string country = HelperFunctions.ReadCountry();
                        Console.WriteLine("Unesi godinu:");
                        int year = int.Parse(Console.ReadLine());
                        float medianMonthlyIncome = proxy.MedianMonthlyIncome(name, country, year).Item1;
                        if(medianMonthlyIncome >= 0)
                            Console.WriteLine("Prosecna plata za " + country + " u " + year + " god.:" + medianMonthlyIncome);
                        break;
                    case 8:
                        var tmpDict = proxy.MaxIncomeByCountry(name).Item1;
                        Console.WriteLine("Najveca plata za svaku drzavu:");
                        foreach (KeyValuePair<string, Element> kvp in tmpDict)
                        {
                            Console.WriteLine(kvp.Key + " : id:" + kvp.Value.Id + " plata:" + kvp.Value.Income);
                        }
                        break;
                    case 0:
                        Console.WriteLine("Cao poz");
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool ValidateSignature(string s, byte[] signature,string signCert)
        {
            if(signature==null)
            {
                return false;
            }
            X509Certificate2 clientCertificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, signCert);
            /// Verify signature using SHA1 hash algorithm
            if (DigitalSignature.Verify(s, "SHA1", signature, clientCertificate))
            {
                Console.WriteLine("Digital Signature is valid.");
                //Console.WriteLine(message);
                return true;
            }
            else
            {
                Console.WriteLine("Digital Signature is invalid.");
                return false;
            }
        }

        public static void DisplayAllElements(List<Element> elements, bool idOnly = false) {
            foreach (Element tmpE in elements) {
                if (idOnly) {
                    Console.WriteLine(tmpE.Id);
                } else {
                    Console.WriteLine(tmpE);
                }
            }
        }

        private static Element GetElementToEdit(List<Element> allElements) {
            Element toEdit = null;
            while (toEdit == null) {
                Console.WriteLine("Unesi id elementa koji menjas:");
                int idEditElem = int.Parse(Console.ReadLine());
                toEdit = allElements.Find(x => x.Id == idEditElem);

                if (toEdit == null) {
                    Console.WriteLine("Ne postoji element sa tim ID-em");
                }
            }

            return toEdit;
        }

        private static void CheckIfExecuted(bool success) {
            if (success) {
                Console.WriteLine("Komanda uspesno izvrsena");
            } else {
                Console.WriteLine("Komanda nije izvrsena");
            }
        }

        public static string StringPermissionFromAction(string action)
        {
            Dictionary<string, string> Permissions = new Dictionary<string, string>
            {
                {"CreateDB", "createdb" },
                {"DeleteDB", "deletedb" },
                {"EditDB", "editdb" },
                {"MaxIncomeByCountry", "readdb" },
                {"MedianMonthlyIncome", "readdb" },
                {"MedianMonthlyIncomeByCity", "readdb" },
                {"ReadDB", "readdb" },
                {"WriteDB", "writedb" }
            };

            if (Permissions.ContainsKey(action))
            {
                return Permissions[action];
            }
            throw new Exception("Action invalid name:" + action);
        }

        public static string ReadFromConfig(string key)
        {
            try
            {
                using (StreamReader sr = new StreamReader("ipadrese.cfg"))
                {
                    string text = sr.ReadToEnd();
                    foreach (string line in text.Split('\n'))
                    {
                        if (line.Contains(key.ToLower() + ":"))
                        {
                            string adr = line.Split(':')[1].Trim();

                            if (adr.Length == 0)
                            {
                                Console.WriteLine("Adresa za servis nije ispravna");
                                adr = "localhost";
                            }

                            return adr;
                        }
                    }
                    return "localhost";
                }
            }
            catch
            {
                Console.WriteLine("Nema fajla u kom su ipadrese (ipadrese.cfg)");
                return "localhost";
            }
        }
    }
}