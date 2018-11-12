using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading;
using Common;
using Logger;

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
            string calledMethod = OperationContext.Current.IncomingMessageHeaders.Action.Split('/').Last();   //ispisuje koju je metodu pozvao client
            string stringPermission = HelperFunctions.StringPermissionFromAction(calledMethod);
            //---------------------------------------------------
            string currentUser = ((MyPrincipal)principal).rola.CurrentRole.ToString();

            if (principal.IsInRole(stringPermission)) {
                authorized = true;
                Audit.LogAuthorizationSuccess(currentUser, calledMethod);
            } else {
                Audit.LogAuthorizationFailure(currentUser, calledMethod, "No permission to access");
            }

            return authorized;
        }
    }
}
