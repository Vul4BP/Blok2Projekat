using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Logger {
    public class Audit {

        static string SourceName = Config.LoggerSourceName;
        static string LogName = Config.LoggerLogName;
        static EventLog eventLog;

        static Audit() {
            try {
                if (!EventLog.SourceExists(SourceName)) {
                    EventLog.CreateEventSource(SourceName, LogName);
                }
                eventLog = new EventLog(LogName, Environment.MachineName, SourceName);
            } catch (Exception e) {
                Console.WriteLine("GRESKA AUDIT: " + e.ToString());
            }
        }

        public static void LogAuthenticationSuccess(string user) {
            eventLog.WriteEntry("User " + user + " is successfully authenticated.", EventLogEntryType.SuccessAudit);
        }

        public static void LogAuthenticationFailure(string user, string reason = "") {
            if (reason.Length > 0) {
                eventLog.WriteEntry("User " + user + " failed to authenticate. Reason:" + reason, EventLogEntryType.FailureAudit);
            } else {
                eventLog.WriteEntry("User " + user + " failed to authenticate.", EventLogEntryType.FailureAudit);
            }
        }

        public static void LogAuthorizationSuccess(string user, string method) {
            eventLog.WriteEntry("User " + user + " successfully accessed to " + method + ".");
        }

        public static void LogAuthorizationFailure(string user, string method, string reason) {
            if (reason.Length > 0) {
                eventLog.WriteEntry("User " + user + " failed to access " + method + ". Reason: " + reason, EventLogEntryType.FailureAudit);
            } else {
                eventLog.WriteEntry("User " + user + " failed to access " + method + ".", EventLogEntryType.FailureAudit);
            }
        }
    }
}

