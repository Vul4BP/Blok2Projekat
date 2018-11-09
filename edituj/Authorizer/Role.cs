using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Authorizer {
    
    public class Role {

        private static Dictionary<Roles, List<Permissions>> PermissionsForRoles = null;

        static Role()
        {
            PermissionsForRoles = LoadPermissionsFromFile(Config.PermissionsConfigPath);
        }

        public Role(Roles cr) {
            CurrentRole = cr;
            GrantedPermissions = PermissionsForRoles[CurrentRole];
        }

        public Roles CurrentRole { get; set; }
        public List<Permissions> GrantedPermissions { get; set; }

        private static Dictionary<Roles, List<Permissions>> LoadPermissionsFromFile(string filePath)
        {
            var returnValue = new Dictionary<Roles, List<Permissions>>();
            string textFromFile = "FILE_ERROR";

            using (StreamReader sr = new StreamReader(Config.PermissionsConfigPath))
            {
                textFromFile = sr.ReadToEnd();
            }

            //ako je doslo do greske prilikom citanja fajla onda ce promenljiva textFromFile ostati ista
            if (textFromFile.Equals("FILE_ERROR"))
            {
                Console.WriteLine("Config fajl za permisije nije ucitan. Ne moze da se garantuje rad programa! Ocekivana lokacija configa: " + filePath);
                throw new Exception("Config fajl za permisije nije ucitan.Ne moze da se garantuje rad programa!Ocekivana lokacija configa: " + filePath);
            }

            List<string> TextLinesFromFile = textFromFile.Split('\n').ToList();
            //prodjemo kroz svaki red, parsiramo ga i na osnovu toga roli dodamo dozvolu
            for(int i = 0; i < TextLinesFromFile.Count; i++)
            {
                //preskace se prazan red
                if (TextLinesFromFile[i].Trim().Length == 0 || TextLinesFromFile[i].Trim()[0] == '#')
                {
                    continue;
                }

                //jedan red treba da izgleda ovako: Admin,ReadDB
                //ovde proverava da li ima dva elementa
                string[] parsedData = TextLinesFromFile[i].Trim().Split(',');
                if (parsedData.Length != 2)
                {
                    Console.WriteLine("Invalid permission rule");
                    continue;
                }

                Roles tmpRole = HelperFunctions.RoleFromString(parsedData[0]);
                Permissions tmpPerm = HelperFunctions.PermissionFromString(parsedData[1]);

                if (returnValue.ContainsKey(tmpRole))
                {
                    returnValue[tmpRole].Add(tmpPerm);
                } else
                {
                    returnValue.Add(tmpRole, new List<Permissions>());
                    returnValue[tmpRole].Add(tmpPerm);
                }
            }
            
            return returnValue;
        }
    }

}
