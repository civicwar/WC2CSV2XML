using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;

namespace WC2CSV
{
    internal partial class Program
    {
        static async Task<int> Main(string[] args)
        {
            var inOpt = new Option<FileInfo>(
                   aliases: new[] { "--in", "-i" },
                   description: "Input file")
            { IsRequired = true };

            var outOpt = new Option<FileInfo>(
                aliases: new[] { "--out", "-o" },
                description: "Output file")
            { IsRequired = true };

            var isAmmoCSV = new Option<bool>(
                aliases: new[] { "--ammo", "-a" },
                description: "Is this an ammo CSV? (default: false)")
            { IsRequired = false };

            var isWeaponCSV = new Option<bool>(
                aliases: new[] { "--weapon", "-w" },
                description: "Is this a weapon CSV? (default: false)")
            { IsRequired = false };

            var headersOpt = new Option<string[]>(
            aliases: new[] { "--headers", "-h" },
            description: "CSV column headers to copy to XML")
            {
                AllowMultipleArgumentsPerToken = true,
                Arity = ArgumentArity.OneOrMore
            };

            var root = new RootCommand("WC2CSV App") { inOpt, outOpt, isAmmoCSV, isWeaponCSV, headersOpt };

            Console.WriteLine("Welcome to the WC2CSV tool!");
            Console.WriteLine("This tool will convert a CSV file to an XML file for use in Space Engineers WeaponCore.");

            root.AddValidator(cmd =>
            {
                bool ammo = cmd.GetValueForOption(isAmmoCSV);
                bool weapon = cmd.GetValueForOption(isWeaponCSV);

                if (ammo && weapon || (!ammo && !weapon)) cmd.ErrorMessage = "Specify either --ammo OR --weapon, not both.";
            });

            root.SetHandler(
             (FileInfo input, FileInfo output, bool ammo, bool weapon, string[] headers) =>
             {
                 Console.WriteLine($"Input : {input.FullName}");
                 Console.WriteLine($"Output: {output.FullName}");
                 Console.WriteLine($"Mode  : {(ammo ? "Ammo" : "Weapon")}");
                 Console.WriteLine($"Headers : {string.Join(", ", headers)}");

                 if (ammo) ExportHelpers.CSVAmmoToXML(input, output, headers);
                 else if (weapon) ExportHelpers.CSVWeaponToXML(input, output, headers);
             },
             inOpt, outOpt, isAmmoCSV, isWeaponCSV, headersOpt);


            return await root.InvokeAsync(args);
        }
    }

}
