using System;
using System.IO;

namespace PlayerZero
{
    public class LoggerLight
    {
        public static void BeeLog(string sLogs, TextWriter tEx)
        {
            try
            {
                tEx.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()} {"--" + sLogs}");
            }
            catch
            {

            }
        }
        public static void GetLogging(string sMyLog)
        {
            using (StreamWriter tEx = File.AppendText(DataStore.sBeeLogs))
                BeeLog(sMyLog, tEx);
        }
    }
}
