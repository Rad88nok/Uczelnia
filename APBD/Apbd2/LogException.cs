using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Apbd2
{
    class LogException
    {
        private static LogException logI= null;
        private string errorLogFilePath = "./log.txt";

        private LogException()
        {
        }

        public static LogException GetInstance()
        {
            if (logI== null)
            {
                logI = new LogException();
            }

            return logI;
        }

        public void Error(Exception e)
        {
            var eMessage = e.Message + "\n";
            var ePath = this.errorLogFilePath;

            File.AppendAllText(ePath, eMessage);
            
        }
        public void DuplicatedStudentDataException(string studentData)
        {
            Exception ex = new Exception(string.Format("Duplicated student data: {0}", studentData));
            Error(ex);
        }
        public void NotEnoughStudentDataException(string studentData)
        {
            Exception ex= new Exception(String.Format("Not enough student data: {0}", studentData));
            Error(ex);
        }
    }
}
