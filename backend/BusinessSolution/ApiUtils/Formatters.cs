using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtils;

public class Formatters
{
    public static string FormatName(string first, string last)
    {
        return $"{last}, {first}";
    }
}
