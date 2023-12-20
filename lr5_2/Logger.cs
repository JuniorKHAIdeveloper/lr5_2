using System;
using System.IO;
using System.Threading;

namespace lr5_2
{
    public class Logger
    {
        private readonly string filePath;
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(5, 5); 

        public Logger(string filePath)
        {
            this.filePath = filePath;
            ClearLogFile();
        }

        private void ClearLogFile()
        {
            File.WriteAllText(filePath, string.Empty);
        }

        public void Log(string message)
        {
            try
            {
                semaphore.Wait(); 
                File.AppendAllText(filePath, message + Environment.NewLine);
            }
            finally
            {
                semaphore.Release(); 
            }
        }
    }
}