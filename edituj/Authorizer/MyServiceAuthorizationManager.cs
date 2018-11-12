using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading;
using Common;

namespace Authorizer {
    public class MyServiceAuthorizationManager : ServiceAuthorizationManager {
        protected override bool CheckAccessCore(OperationContext operationContext) {
            //WindowsPrincipal p = new WindowsPrincipal(operationContext.ServiceSecurityContext.WindowsIdentity);
            //return p.IsInRole("Reader");

            bool authorized = false;

            IPrincipal principal = operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] as IPrincipal;
            //Thread.CurrentPrincipal = principal;
            //string group = string.Format("{0}\\Viewer", Environment.MachineName);
            //string group = string.Format("{0}", Environment.MachineName);
            //principal.Identity.Name

            //---------------------------------------------------
            string calledMethod = OperationContext.Current.IncomingMessageHeaders.Action;   //ispisuje koju je metodu pozvao client
            string stringPermission = HelperFunctions.StringPermissionFromAction(calledMethod);
            //---------------------------------------------------
            if (principal.IsInRole(stringPermission)) {
                authorized = true;
            }

            return authorized;
        }
    }
}
