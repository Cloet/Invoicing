using System;
using System.IO;

namespace Invoicing.Base.Logging
{
    internal static class StreamWriter
    {

        //Set max size of a logger file to 3mb
        private static int MaxLoggerFile => 3000000;

        /// <summary>
        /// Writes a message to a file
        /// Returns true when the file has reached it's max log file
        /// </summary>
        /// <param name="location"></param>
        /// <param name="text"></param>
        internal static bool WriteMessageToFile(string location, string text)
        {

            try
            {

                FileInfo file = new FileInfo(Path.GetFullPath(location));
                file.Directory?.Create();

                using (System.IO.StreamWriter writer = file.AppendText())
                {
                    writer.WriteLineAsync(text).Wait();
                }

                if (file.Length > MaxLoggerFile)
                    return true;

                return false;

            }
            catch (Exception)
            {
                //Will probably change this in the future, not really clean but works for now
                return WriteMessageToFile(location, text);
            }
        }

    }

}
