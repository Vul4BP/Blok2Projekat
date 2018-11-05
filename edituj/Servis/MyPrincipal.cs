using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Servis {
    public class MyPrincipal : IPrincipal {
        public IIdentity Identity { get; set; }
        public List<Role> ListOfRoles { get; set; }

        public List<String> ListOfGroups { get; set; }

        public MyPrincipal(WindowsIdentity wi) {
            Identity = wi;
            ListOfRoles = new List<Role>();
            ListOfGroups = new List<String>();
            InitRoles();

            foreach (IdentityReference group in wi.Groups) {
                SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
                var name = sid.Translate(typeof(NTAccount));
                ListOfGroups.Add(name.ToString()); 
            }
        }

        public bool IsInRole(string role) {
            foreach (String s in ListOfGroups) {
                if (s.Contains(role)) {
                    return true;
                }
            }

            return false;
        }

        public void InitRoles() {
            /*
            Role readRole = new Role(Roles.Reader);
            readRole.listOfPrms.Add(Prms.Read);

            Role modifyRole = new Role(Roles.Modifier);
            modifyRole.listOfPrms.Add(Prms.Read);
            modifyRole.listOfPrms.Add(Prms.Modify);

            Role adminRole = new Role(Roles.Administrator);
            adminRole.listOfPrms.Add(Prms.Read);
            adminRole.listOfPrms.Add(Prms.Modify);
            adminRole.listOfPrms.Add(Prms.Administrate);

            ListOfRoles.Add(readRole);
            ListOfRoles.Add(modifyRole);
            ListOfRoles.Add(adminRole);*/

            Role adminRole = new Role(Roles.Krompir); //ovde se pravi "Rola"
            adminRole.listOfPrms.Add(Prms.CreateDB);     // i dodaju joj se PRMS- tj koje metode moze da radit
            adminRole.listOfPrms.Add(Prms.DeleteDB);
            
            //[PrincipalPermission(SecurityAction.Demand, Role = "Admini")
            

        }
    }
}
