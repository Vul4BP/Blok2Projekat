using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Threading;
using Common;

namespace Authorizer {
    public class MyPrincipal : IPrincipal {
        public IIdentity Identity { get; set; }

        //public List<String> ListOfGroups { get; set; }
        public Role rola { get; set; }

        public MyPrincipal(WindowsIdentity wi) {
            Identity = wi;
            //ListOfGroups = new List<String>();

            foreach (IdentityReference group in wi.Groups) {
                SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
                var name = sid.Translate(typeof(NTAccount));

                if (name.ToString().Contains("Krompir"))
                {
                    rola = new Role(Common.Roles.Admin);
                }
            }
        }

        public bool IsInRole(string role) {
            Common.Permissions requiredPerm = HelperFunctions.PermissionFromString(role);
            if (rola.GrantedPermissions.Contains(requiredPerm))
            {
                Console.WriteLine("Pozivam is in role TRUE " + role);
                return true;
            }
            Console.WriteLine("Pozivam is in role FALSE " + role);
            return false;
        }
    }
}
