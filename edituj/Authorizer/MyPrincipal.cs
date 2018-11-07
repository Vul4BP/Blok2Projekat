using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Authorizer {
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
            Role adminRole = new Role(Common.Roles.Admin);
            Role readerRole = new Role(Common.Roles.Reader);
            Role writerRole = new Role(Common.Roles.Writer);
        }
    }
}
