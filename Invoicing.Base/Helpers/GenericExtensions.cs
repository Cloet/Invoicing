using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Invoicing.Base.Helpers
{
    public static class GenericExtensions
    {

        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source) => source ?? Enumerable.Empty<T>();

    }
}
