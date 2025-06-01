using CsvHelper;
using CsvHelper.Configuration;
using Libs.WyN;
using System.Globalization;
using System.Xml.Serialization;

namespace WC2CSV
{
    static class ExportHelpers
    {

        public static void CSVAmmoToXML(FileInfo input, FileInfo output, string[] headers)
        {
            Type pocoType = typeof(CSVAmmo);
            using var reader = new StreamReader(input.FullName);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                HeaderValidated = null,
            });

            var map = csv.Context.AutoMap(pocoType);
            if (headers.Length == 0) headers = [.. map.MemberMaps.Select(m => m.Data.Member.Name)];

            foreach (var member in map.MemberMaps.ToArray())
            {
                string propName = member.Data.Member.Name;

                if (!headers.Contains(propName, StringComparer.OrdinalIgnoreCase)) member.Data.Ignore = true;
            }

            csv.Context.RegisterClassMap(map);

            var overrides = new AmmoOverrides
            {
                Items = [.. csv.GetRecords<CSVAmmo>()]
            };

            var attrs = new XmlAttributeOverrides();
            foreach (var p in typeof(CSVAmmo).GetProperties())
            {
                if (p.Name == "AmmoRound") attrs.Add(typeof(CSVAmmo), p.Name, new XmlAttributes { XmlAttribute = new XmlAttributeAttribute("AmmoName") });
                else if (!headers.Contains(p.Name, StringComparer.OrdinalIgnoreCase)) { attrs.Add(typeof(CSVAmmo), p.Name, new XmlAttributes { XmlIgnore = true }); }
            }


            var serializer = new XmlSerializer(typeof(AmmoOverrides), attrs);
            serializer.Serialize(new StreamWriter(output.FullName), overrides);
        }

        public static void CSVWeaponToXML(FileInfo input, FileInfo output, string[] headers)
        {
            Type pocoType = typeof(CSVWeapon);
            using var reader = new StreamReader(input.FullName);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                HeaderValidated = null,
            });

            var map = csv.Context.AutoMap(pocoType);
            if (headers.Length == 0) headers = [.. map.MemberMaps.Select(m => m.Data.Member.Name)];

            foreach (var member in map.MemberMaps.ToArray())
            {
                string propName = member.Data.Member.Name;

                if (!headers.Contains(propName, StringComparer.OrdinalIgnoreCase)) member.Data.Ignore = true;
            }

            csv.Context.RegisterClassMap(map);

            var overrides = new WeaponOverrides
            {
                Items = [.. csv.GetRecords<CSVWeapon>()]
            };

            var attrs = new XmlAttributeOverrides();
            foreach (var p in typeof(CSVAmmo).GetProperties())
            {
                if (p.Name == "AmmoRound") attrs.Add(typeof(CSVAmmo), p.Name, new XmlAttributes { XmlAttribute = new XmlAttributeAttribute("AmmoName") });
                else if (!headers.Contains(p.Name, StringComparer.OrdinalIgnoreCase)) { attrs.Add(typeof(CSVAmmo), p.Name, new XmlAttributes { XmlIgnore = true }); }
            }


            var serializer = new XmlSerializer(typeof(AmmoOverrides), attrs);
            serializer.Serialize(new StreamWriter(output.FullName), overrides);
        }


    }
}