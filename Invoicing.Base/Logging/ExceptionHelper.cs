using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.Base.Logging
{
    internal static class ExceptionHelper
    {
        internal static int LineNumber(this Exception e)
        {
            try
            {
                string lineSubstring = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));

                bool numeric = int.TryParse(lineSubstring, out var line);

                if (numeric)
                    return line;

                return -1;

            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
