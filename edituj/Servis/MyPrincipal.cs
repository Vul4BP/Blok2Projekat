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
            popuniRole();

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

        public Roles getRole(string name) {
            if (name.Equals("Reader")) {
                return Roles.Reader;
            } else if (name.Equals("Modifier")) {
                return Roles.Modifier;
            } else if (name.Equals("Admin")) {
                return Roles.Administrator;
            } else
                return Roles.Error;
        }

        public void popuniRole() {
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
            ListOfRoles.Add(adminRole);
        }
    }
}
