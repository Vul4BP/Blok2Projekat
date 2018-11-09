using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Threading;
using Common;
using System.Security.Claims;

namespace Authorizer {
    public class MyPrincipal : IPrincipal
    {
        public IIdentity Identity { get; set; }
        //public WindowsIdentity WinIdentity { get; set; }
        public Role rola { get; set; }

        //public List<String> ListOfGroups { get; set; }
        //public Role rola { get; set; }

        public MyPrincipal(WindowsIdentity identity)
        {
            Identity = identity;
            if (Identity != null)
            {
                //ULAZI AKO JE ADMINS, TJ AKO JE WIN IDENTITY
                List<string> ListOfGroups = new List<String>();

                foreach (IdentityReference group in identity.Groups)
                {
                    SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
                    var name = sid.Translate(typeof(NTAccount));

                    if (name.ToString().Contains("Krompir"))
                    {
                        rola = new Role(Common.Roles.Admin);
                    }
                }
            }
        }

        public MyPrincipal(ClaimsIdentity identity)
        {
            //AKO NIJE WIN IDENTITY ZNACI DA JE SERTIFIKAT TJ IIDENTITY, ULAZI AKO JE READER I WRITER
            Identity = identity;
            if (Identity != null)
            {
                string x059Name = Identity.Name; //VRATICE CN={},OU={}...
                if (x059Name.Contains("OU=Rider"))
                {
                    rola = new Role(Common.Roles.Reader);
                }
                else if (x059Name.Contains("OU=Vrajter"))
                {
                    rola = new Role(Common.Roles.Writer);
                }
            }
        }

        public bool IsInRole(string role)
        {
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
