using System;
using System.Collections.Generic;
using System.Linq;

namespace WC2CSV
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            var parts = new WyN.WeaponCore.Parts();

            ExportHelpers.CSVToXML(parts);
            ExportHelpers.CSVToXML(parts);

        }
    }

}
