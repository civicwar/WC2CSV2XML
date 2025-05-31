namespace Libs.WyN
{
    public class CSVAmmo
    {
        public string? AmmoRound { get; set; }
        public float? BaseDamage { get; set; }
        public float? AreaEffectDamage { get; set; }
        public double? AreaEffectRadius { get; set; }
        public float? DetonationDamage { get; set; }
        public float? DetonationRadius { get; set; }
        public double? HealthHitModifier { get; set; }
        public float? Health { get; set; }
        public float? Mass { get; set; }
        public float? MaxTrajectory { get; set; }
        public float? DesiredSpeed { get; set; }
        public float? EnergyCost { get; set; }
        public float? GravityMultiplier { get; set; }
        public float? ShieldModifier { get; set; }
        public float? ShieldBypass { get; set; }
        public bool? DisableClientPredictedAmmo { get; set; }
        public float? FallOffDistance { get; set; }
        public float? FallOffMinMultipler { get; set; }
        public float? ByBlockHitMaxAbsorb { get; set; }
        public float? EndOfLifeMaxAbsorb { get; set; }
        public float? BackKickForce { get; set; }

        public bool? EnergyBaseDamage { get; set; }
        public bool? EnergyAreaEffectDamage { get; set; }
        public bool? EnergyDetonationDamage { get; set; }
        public bool? EnergyShieldDamage { get; set; }

    }

}
