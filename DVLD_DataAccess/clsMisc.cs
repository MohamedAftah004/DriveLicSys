using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text;

namespace DVLD_DataAccess
{
    public class clsMisc
    {

        //Log all exception on Event Viewer
        public static void LogExceptionOnEventViewr(string errorMessage, EventLogEntryType eventType = EventLogEntryType.Error)
        {
            string sourceName = "DVLD";

            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }

            EventLog.WriteEntry(sourceName, $"{errorMessage}", eventType);

        }


        //hashing function

        public static string ComputeHash(string input)
        {

            using (SHA256 sha256 = SHA256.Create())
            {

                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }

        }

    }
}
