using CoreSystems.Support;
using CsvHelper.Configuration;
using Libs.WyN;
using System.Globalization;
using System.Reflection;
using System.Text;
using WyN.WeaponCore;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.DamageScaleDef.DamageTypes.Damage;

namespace WC2CSV
{
    static class ExportHelpers
    {
        public static void ExportAmmoToCSV(Parts parts)
        {
            List<CSVAmmo> ammoList = [];

            var ammoProps = parts
                                .GetType()
                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                .Where(p => p.PropertyType == typeof(WeaponDefinition.AmmoDef))
                                .Select(p => p.GetValue(parts) as WeaponDefinition.AmmoDef)
                                .ToArray();

            foreach (var ammo in ammoProps)
            {
                if (ammo == null) continue;

                ammoList.Add(new CSVAmmo()
                {
                    AmmoRound = ammo.AmmoRound,
                    BaseDamage = ammo.BaseDamage,
                    AreaEffectDamage = ammo.AreaEffect.AreaEffectDamage,
                    AreaEffectRadius = ammo.AreaEffect.AreaEffectRadius,
                    DetonationDamage = ammo.AreaEffect.Detonation.DetonationDamage,
                    DetonationRadius = ammo.AreaEffect.Detonation.DetonationRadius,
                    HealthHitModifier = ammo.HeatModifier,
                    Health = ammo.Health,
                    Mass = ammo.Mass,
                    MaxTrajectory = ammo.Trajectory.MaxTrajectory,
                    DesiredSpeed = ammo.Trajectory.DesiredSpeed,
                    EnergyCost = ammo.EnergyCost,
                    GravityMultiplier = ammo.Trajectory.GravityMultiplier,
                    ShieldModifier = ammo.DamageScales.Shields.Modifier,
                    ShieldBypass = ammo.DamageScales.Shields.BypassModifier,
                    DisableClientPredictedAmmo = false,
                    FallOffDistance = ammo.DamageScales.FallOff.Distance,
                    FallOffMinMultipler = ammo.DamageScales.FallOff.MinMultipler,
                    ByBlockHitMaxAbsorb = ammo.AreaOfDamage.ByBlockHit.MaxAbsorb,
                    EndOfLifeMaxAbsorb = ammo.AreaOfDamage.EndOfLife.MaxAbsorb,
                    BackKickForce = ammo.BackKickForce,
                    EnergyBaseDamage = ammo.DamageScales.DamageType.Base == Energy,
                    EnergyAreaEffectDamage = ammo.DamageScales.DamageType.AreaEffect == Energy,
                    EnergyDetonationDamage = ammo.DamageScales.DamageType.Detonation == Energy,
                    EnergyShieldDamage = ammo.DamageScales.DamageType.Shield == Energy
                });
            }

            ExportToCSV(ammoList, "AmmoDefinitions");
        }

        public static void ExportWeaponCSV(Parts parts)
        {
            var weaponList = new List<CSVWeapon>();

            var weapons = parts.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.PropertyType == typeof(WeaponDefinition))
                .Select(p => p.GetValue(parts) as WeaponDefinition)
                .Where(w => w != null)
                .ToArray();

            foreach (var weapon in weapons)
            {
                if (weapon == null) continue;

                weaponList.Add(new CSVWeapon()
                {
                    PartName = weapon.HardPoint.PartName,
                    MaxTargetDistance = weapon.Targeting.MaxTargetDistance,
                    MinTargetDistance = weapon.Targeting.MinTargetDistance,
                    RateOfFire = weapon.HardPoint.Loading.RateOfFire,
                    ReloadTime = weapon.HardPoint.Loading.ReloadTime,
                    DeviateShotAngle = weapon.HardPoint.DeviateShotAngle,
                    AimingTolerance = weapon.HardPoint.AimingTolerance,
                    IdlePower = weapon.HardPoint.HardWare.IdlePower,
                    HeatSinkRate = weapon.HardPoint.Loading.HeatSinkRate,
                    HeatPerShot = weapon.HardPoint.Loading.HeatPerShot
                });
            }

            ExportToCSV(weaponList, "WeaponDefinitions");
        }


        public static void ExportToCSV<T>(List<T> rows, string fileName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            if (!Path.Exists($"Data/#{fileName}.csv")) Directory.CreateDirectory($"Data/");

            using var writer = new StreamWriter($"Data/#{fileName}.csv");
            using var csv = new CsvHelper.CsvWriter(writer, config);

            csv.WriteRecords(rows);
            Console.WriteLine($"Exported {rows.Count} records to {fileName}.csv");
        }

    }
}