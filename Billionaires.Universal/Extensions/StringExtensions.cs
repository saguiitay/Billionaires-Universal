using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billionaires.Universal.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string text, string other) =>
            text?.Equals(other, StringComparison.CurrentCultureIgnoreCase) ?? false;
    }
}
