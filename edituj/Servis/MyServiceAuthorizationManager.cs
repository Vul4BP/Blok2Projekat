using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.ServiceModel;

namespace Servis {
    public class MyServiceAuthorizationManager : ServiceAuthorizationManager {
        protected override bool CheckAccessCore(OperationContext operationContext) {
            //WindowsPrincipal p = new WindowsPrincipal(operationContext.ServiceSecurityContext.WindowsIdentity);
            //return p.IsInRole("Reader");

            bool authorized = false;

            IPrincipal principal = operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] as IPrincipal;

            //string group = string.Format("{0}\\Viewer", Environment.MachineName);
            //string group = string.Format("{0}", Environment.MachineName);

            if (principal.IsInRole("Krompir")) {
                authorized = true;
            }


            return authorized;
        }
    }
}
