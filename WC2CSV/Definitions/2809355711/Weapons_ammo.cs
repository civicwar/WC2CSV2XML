using static CoreSystems.Support.WeaponDefinition;
using static CoreSystems.Support.WeaponDefinition.AmmoDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.AreaOfDamageDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.AreaOfDamageDef.AoeShape;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.AreaOfDamageDef.Falloff;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.DamageScaleDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.DamageScaleDef.CustomScalesDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.DamageScaleDef.CustomScalesDef.SkipMode;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.DamageScaleDef.DamageTypes.Damage;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.DamageScaleDef.ShieldDef.ShieldType;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.EjectionDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.EjectionDef.SpawnType;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.EwarDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.EwarDef.EwarMode;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.EwarDef.EwarType;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.EwarDef.PushPullDef.Force;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.FragmentDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.FragmentDef.TimedSpawnDef.PointTypes;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.GraphicDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.GraphicDef.LineDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.GraphicDef.LineDef.Texture;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.GraphicDef.LineDef.TracerBaseDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.PatternDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.PatternDef.PatternModes;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.ShapeDef.Shapes;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.TrajectoryDef;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType;

namespace WyN.WeaponCore
{
    partial class Parts
    {
        public AmmoDef TBC101_Blank => new AmmoDef
        {
            AmmoMagazine = "TBC101_Blank_Magazine",
            AmmoRound = "TBC101 Shell",
            HybridRound = true, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 1f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 10600.0f, //2000.0f,
            Mass = 10.0f, // In kilograms; how much force the impact will apply to the target.
            Health = 0.0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 50.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0.0f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1.0, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1.0f, // Character damage multiplier; defaults to 1 if zero or less.
                                    // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 2000.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.6f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1.0f, // Multiplier for damage against large grids.
                    Small = 0.5f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1.0f,
                    Light = 0.5f,
                    Heavy = 0.75f,
                    NonArmor = 2f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 2.5f,
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1f,
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Kinetic, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Kinetic,
                    Shield = Kinetic, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 0.5f, // Meters
                    Damage = 1000f,
                    Depth = 10.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = false,
                    Radius = 0.5f, // Radius of AOE effect, in meters.
                    Damage = 0,
                    Depth = 5.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Curve, //.NoFalloff applies the same damage to all blocks in radius
                                     // Linear drops evenly by distance from center out to max radius
                                     // Curve drops off damage sharply as it approaches the max radius
                                     // InvCurve drops off sharply from the middle and tapers to max radius
                                     // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                     // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                     // Exponential drops off exponentially.  Does not scale to max radius
                                     // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 1, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink,  // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                    // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                    // Emp : Targets & Shutdown any Block capable of being powered
                                    // Offense : Targets & Shutdowns Weaponry
                                    // Nav : Targets & Shutdown Gyros or Locks them down
                                    // Dot : Deals Damage to Blocks in radius
                                    // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                    // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                    // Tractor : Affects target with Physics
                                    // Pull : Affects target with Physics
                                    // Push : Affects target with Physics
                                    // Anchor : Targets & Shutdowns Thrusters
                                    //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 180.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 3000.0f,
                MaxTrajectory = 8200.0f, //1800.0f,
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 1.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1800, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 1800,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\OPC_Ammo\\TBC101_Sabot", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 2000.0f,
                            MaxDuration = 1.0f,
                            Scale = 1.0f,
                            HitPlayChance = 0.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "MaterialHit_Metal",
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 50.0f,
                            MaxDuration = 1.0f,
                            Scale = 2.0f,
                            HitPlayChance = 0.70f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 5.0f, end: 10.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 100.0f,
                        Width = 0.2f,
                        Color = Color(red: 0.4f, green: 0.925f, blue: 1.0f, alpha: 1.0f),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"ProjectileTrailLine", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 0.4f, green: 0.925f, blue: 1.0f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 128, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "OPC_Common_Flight", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "OPC_Common_Imp_Lht",
                ShotSound = "",
                ShieldHitSound = "OPC_Common_Imp_Shield",
                PlayerHitSound = "",
                VoxelHitSound = "OPC_TBC_Imp_Vxl",
                FloatingHitSound = "OPC_Common_Imp_Lht",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef TBC202_Blank => new AmmoDef
        {
            AmmoMagazine = "TBC202_Blank_Magazine", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "TBC202 Shell", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
                                        //AmmoRound = OPC_Localization.GetText("OPC_TBC202_AmmoRound_WC_LID"),
            HybridRound = true, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 1f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 32000.3f,
            Mass = 60.0f, // In kilograms; how much force the impact will apply to the target.
            Health = 0.0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 10000.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0.1f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = true, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1.0, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = 1.2f,
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 3000.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.9f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1.0f, // Multiplier for damage against large grids.
                    Small = 0.5f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1.0f,
                    Light = 0.5f,
                    Heavy = 0.75f,
                    NonArmor = 2.0f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 2.5f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal, Bypass, EmpRetired
                    BypassModifier = -1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Kinetic, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Kinetic,
                    Shield = Kinetic, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 0.5f, // Meters
                    Damage = 1600f,
                    Depth = 20f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = false,
                    Radius = 6.0, // Meters
                    Damage = 6000.0f,
                    Depth = 10.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 1, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink,  // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                    // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                    // Emp : Targets & Shutdown any Block capable of being powered
                                    // Offense : Targets & Shutdowns Weaponry
                                    // Nav : Targets & Shutdown Gyros or Locks them down
                                    // Dot : Deals Damage to Blocks in radius
                                    // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                    // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                    // Tractor : Affects target with Physics
                                    // Pull : Affects target with Physics
                                    // Push : Affects target with Physics
                                    // Anchor : Targets & Shutdowns Thrusters
                                    //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 180.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 2000.0f, // voxel phasing if you go above 5100
                MaxTrajectory = 10000.0f, //2500.0f, // Max Distance the projectile or beam can Travel.
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1800, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 1800,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\OPC_Ammo\\TBC202_Sabot", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 8.0f, green: 0.0f, blue: 0.0f, alpha: 32.0f),
                        Offset = Vector(x: 0.0, y: -1.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 2000.0f,
                            MaxDuration = 1.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "MaterialHit_Metal", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 100.0f,
                            MaxDuration = 1.0f,
                            Scale = 4.0f, // Scale of effect.
                            HitPlayChance = 0.75f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 5.0f, end: 10.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 300f,
                        Width = 0.4f,
                        Color = Color(red: 0.4f, green: 0.925f, blue: 1.0f, alpha: 1.0f), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"ProjectileTrailLine", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 0.4f, green: 0.925f, blue: 1.0f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 128, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "OPC_Common_Flight", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "OPC_Common_Imp_Med",
                ShotSound = "",
                ShieldHitSound = "OPC_Common_Imp_Shield",
                PlayerHitSound = "",
                VoxelHitSound = "OPC_TBC_Imp_Vxl",
                FloatingHitSound = "OPC_Common_Imp_Med",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef TBC101_Blank_Elite => new AmmoDef
        {
            AmmoMagazine = "TBC101_Blank_Magazine_Elite",
            AmmoRound = "TBC101 Enhanced Shell",
            HybridRound = true, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 1f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 12600.0f, //2000.0f,
            Mass = 10.0f, // In kilograms; how much force the impact will apply to the target.
            Health = 0.0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 50.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0.0f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1.0, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1.0f, // Character damage multiplier; defaults to 1 if zero or less.
                                    // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 2000.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.6f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1.0f, // Multiplier for damage against large grids.
                    Small = 0.5f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1.0f,
                    Light = 0.5f,
                    Heavy = 0.75f,
                    NonArmor = 2f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 2.5f,
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = 0.1f,
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Kinetic, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Kinetic,
                    Shield = Kinetic, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 0.5f, // Meters
                    Damage = 1500f,
                    Depth = 10.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = false,
                    Radius = 0.5f, // Radius of AOE effect, in meters.
                    Damage = 0,
                    Depth = 5.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Curve, //.NoFalloff applies the same damage to all blocks in radius
                                     // Linear drops evenly by distance from center out to max radius
                                     // Curve drops off damage sharply as it approaches the max radius
                                     // InvCurve drops off sharply from the middle and tapers to max radius
                                     // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                     // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                     // Exponential drops off exponentially.  Does not scale to max radius
                                     // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 1, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink,  // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                    // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                    // Emp : Targets & Shutdown any Block capable of being powered
                                    // Offense : Targets & Shutdowns Weaponry
                                    // Nav : Targets & Shutdown Gyros or Locks them down
                                    // Dot : Deals Damage to Blocks in radius
                                    // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                    // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                    // Tractor : Affects target with Physics
                                    // Pull : Affects target with Physics
                                    // Push : Affects target with Physics
                                    // Anchor : Targets & Shutdowns Thrusters
                                    //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 180.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 3000.0f,
                MaxTrajectory = 8200.0f, //1800.0f,
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 1.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1800, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 1800,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\OPC_Ammo\\TBC101_Sabot", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 2000.0f,
                            MaxDuration = 1.0f,
                            Scale = 1.0f,
                            HitPlayChance = 0.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "MaterialHit_Metal",
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 50.0f,
                            MaxDuration = 1.0f,
                            Scale = 2.0f,
                            HitPlayChance = 0.70f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 5.0f, end: 10.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 100.0f,
                        Width = 0.2f,
                        Color = Color(red: 0.4f, green: 0.925f, blue: 1.0f, alpha: 1.0f),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"ProjectileTrailLine", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 0.4f, green: 0.925f, blue: 1.0f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = false,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 128, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "OPC_Common_Flight", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "OPC_Common_Imp_Lht",
                ShotSound = "",
                ShieldHitSound = "OPC_Common_Imp_Shield",
                PlayerHitSound = "",
                VoxelHitSound = "OPC_TBC_Imp_Vxl",
                FloatingHitSound = "OPC_Common_Imp_Lht",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef TBC202_Blank_Elite => new AmmoDef
        {
            AmmoMagazine = "TBC202_Blank_Magazine_Elite", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "TBC202 Enchanced Shell", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
            HybridRound = true, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 1f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 34000.3f,
            Mass = 60.0f, // In kilograms; how much force the impact will apply to the target.
            Health = 0.0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 10000.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0.1f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = true, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1.0, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = 1.2f,
                // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 3000.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.9f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1.0f, // Multiplier for damage against large grids.
                    Small = 0.5f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1.0f,
                    Light = 0.5f,
                    Heavy = 0.75f,
                    NonArmor = 2.0f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 2.5f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal, Bypass, EmpRetired
                    BypassModifier = 0.1f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Kinetic, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Kinetic,
                    Shield = Kinetic, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 0.5f, // Meters
                    Damage = 1600f,
                    Depth = 20f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = false,
                    Radius = 6.0, // Meters
                    Damage = 7000.0f,
                    Depth = 10.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 1, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = true,
                    NoSound = true,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink,  // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                    // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                    // Emp : Targets & Shutdown any Block capable of being powered
                                    // Offense : Targets & Shutdowns Weaponry
                                    // Nav : Targets & Shutdown Gyros or Locks them down
                                    // Dot : Deals Damage to Blocks in radius
                                    // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                    // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                    // Tractor : Affects target with Physics
                                    // Pull : Affects target with Physics
                                    // Push : Affects target with Physics
                                    // Anchor : Targets & Shutdowns Thrusters
                                    //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 180.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 2000.0f, // voxel phasing if you go above 5100
                MaxTrajectory = 10000.0f, //2500.0f, // Max Distance the projectile or beam can Travel.
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1800, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 1800,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\OPC_Ammo\\TBC202_Sabot", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 8.0f, green: 0.0f, blue: 0.0f, alpha: 32.0f),
                        Offset = Vector(x: 0.0, y: -1.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 2000.0f,
                            MaxDuration = 1.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "MaterialHit_Metal", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 100.0f,
                            MaxDuration = 1.0f,
                            Scale = 4.0f, // Scale of effect.
                            HitPlayChance = 0.75f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 5.0f, end: 10.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 300f,
                        Width = 0.4f,
                        Color = Color(red: 0.4f, green: 0.925f, blue: 1.0f, alpha: 1.0f), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"ProjectileTrailLine", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 0.4f, green: 0.925f, blue: 1.0f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 128, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "OPC_Common_Flight", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "OPC_Common_Imp_Med",
                ShotSound = "",
                ShieldHitSound = "OPC_Common_Imp_Shield",
                PlayerHitSound = "",
                VoxelHitSound = "OPC_TBC_Imp_Vxl",
                FloatingHitSound = "OPC_Common_Imp_Med",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef FBC101_Round_APFSDS => new AmmoDef
        {
            AmmoMagazine = "FBC101_Shell_APFSDS", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "FBC101 APFSDS Round", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
            HybridRound = false, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 120f, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 1500f, //2000.0f, // Direct damage; one steel plate is worth 100.
            Mass = 20.0f, // In kilograms; how much force the impact will apply to the target.
            Health = 20.0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 50000.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0.0f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = true, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 0.75, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = 1.2f, // Character damage multiplier; defaults to 1 if zero or less.
                                   // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 2000.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.8f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 0.75f, // Multiplier for damage against large grids.
                    Small = 1.2f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 0.7f, // Multiplier for damage against light armor.
                    Heavy = 1f, // Multiplier for damage against heavy armor.
                    NonArmor = 0.2f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 0.5f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal, Bypass, EmpRetired
                    BypassModifier = -1.0f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Kinetic, // Base Damage uses this
                    AreaEffect = Kinetic,
                    Detonation = Kinetic,
                    Shield = Kinetic, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 1.5f, // Meters
                    Damage = 500,
                    Depth = 10.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 1.5f, // Radius of AOE effect, in meters.
                    Damage = 5000,
                    Depth = 5.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 1, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink,  // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                    // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                    // Emp : Targets & Shutdown any Block capable of being powered
                                    // Offense : Targets & Shutdowns Weaponry
                                    // Nav : Targets & Shutdown Gyros or Locks them down
                                    // Dot : Deals Damage to Blocks in radius
                                    // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                    // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                    // Tractor : Affects target with Physics
                                    // Pull : Affects target with Physics
                                    // Push : Affects target with Physics
                                    // Anchor : Targets & Shutdowns Thrusters
                                    //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 180.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 1000f, // voxel phasing if you go above 5100
                MaxTrajectory = 3000f, // Max Distance the projectile or beam can Travel.
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 1800,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\OPC_Ammo\\FBC101_Sabot_APFSDS", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 8.0f, green: 0.0f, blue: 0.0f, alpha: 32.0f),
                        Offset = Vector(x: 0.0, y: -1.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 2000.0f,
                            MaxDuration = 1.0f,
                            Scale = 1.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "MaterialHit_Metal",
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 100.0f,
                            MaxDuration = 1.0f,
                            Scale = 5.0f,
                            HitPlayChance = 1.0f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 5.0f, end: 10.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 5.0f, //
                        Width = 0.4f, //
                        Color = Color(red: 0.941f, green: 0.992f, blue: 1.0f, alpha: 1.0f),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"ProjectileTrailLine", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 0.941f, green: 0.992f, blue: 1.0f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 128, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "OPC_Common_Imp_Med",
                ShotSound = "",
                ShieldHitSound = "OPC_Common_Imp_Shield",
                PlayerHitSound = "",
                VoxelHitSound = "OPC_FBC_Imp_Vxl",
                FloatingHitSound = "OPC_Common_Imp_Med",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef FBC101_Round_HESH => new AmmoDef
        {
            AmmoMagazine = "FBC101_Shell_HESH", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "FBC101 HESH Round", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
                                             //AmmoRound = OPC_Localization.GetText("OPC_FBC101_HE_AmmoRound_WC_LID"),
            HybridRound = false, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 120f, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 500f, //2000.0f, // Direct damage; one steel plate is worth 100.
            Mass = 20.0f, // In kilograms; how much force the impact will apply to the target.
            Health = 20.0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 50000.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0.0f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = true, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.2, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 0.5, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = 1.2f, // Character damage multiplier; defaults to 1 if zero or less.
                                   // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 2000.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.6f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 0.75f, // Multiplier for damage against large grids.
                    Small = 1.2f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1.2f, // Multiplier for damage against light armor.
                    Heavy = 0.3f, // Multiplier for damage against heavy armor.
                    NonArmor = 1.5f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 0.5f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal, Bypass, EmpRetired
                    BypassModifier = -1.0f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Kinetic, // Base Damage uses this
                    AreaEffect = Kinetic,
                    Detonation = Kinetic,
                    Shield = Kinetic, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 6f, // Meters
                    Damage = 1000f,
                    Depth = 5.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 3f, // Meters
                    Damage = 10000.0f,
                    Depth = 6f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 5, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink,  // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                    // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                    // Emp : Targets & Shutdown any Block capable of being powered
                                    // Offense : Targets & Shutdowns Weaponry
                                    // Nav : Targets & Shutdown Gyros or Locks them down
                                    // Dot : Deals Damage to Blocks in radius
                                    // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                    // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                    // Tractor : Affects target with Physics
                                    // Pull : Affects target with Physics
                                    // Push : Affects target with Physics
                                    // Anchor : Targets & Shutdowns Thrusters
                                    //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 180.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 800.0f, // voxel phasing if you go above 5100
                MaxTrajectory = 3000.0f, // Max Distance the projectile or beam can Travel.
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.5f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 1800,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\OPC_Ammo\\FBC101_Sabot_HESH", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 8.0f, green: 0.0f, blue: 0.0f, alpha: 32.0f),
                        Offset = Vector(x: 0.0, y: -1.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 2000.0f,
                            MaxDuration = 3.0f,
                            Scale = 1.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "Explosion_S1",
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 5.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 100.0f,
                            MaxDuration = 1.0f,
                            Scale = 2.5f,
                            HitPlayChance = 1.0f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 5.0f, end: 10.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 5.0f, //
                        Width = 0.4f, //
                        Color = Color(red: 0.925f, green: 0.749f, blue: 0.611f, alpha: 1.0f),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"ProjectileTrailLine", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 0.941f, green: 0.992f, blue: 1.0f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 128, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "OPC_Common_Imp_Med",
                ShotSound = "",
                ShieldHitSound = "OPC_Common_Imp_Shield",
                PlayerHitSound = "",
                VoxelHitSound = "OPC_FBC_Imp_Vxl",
                FloatingHitSound = "OPC_Common_Imp_Med",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef TBR202_Round_Default => new AmmoDef
        {
            AmmoMagazine = "TBR202_Round", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "TBR202 Round", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
            HybridRound = false, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 120f, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 2000f, //2000.0f, // Direct damage; one steel plate is worth 100.
            Mass = 35.0f, // In kilograms; how much force the impact will apply to the target.
            Health = 0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 20000.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0.0f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1.0, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = 1.2f, // Character damage multiplier; defaults to 1 if zero or less.
                                   // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 4000.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.75f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = 0.4f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 1.2f, // Multiplier for damage against light armor.
                    Heavy = 1.5f, // Multiplier for damage against heavy armor.
                    NonArmor = 1.1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 0.3f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal, Bypass, EmpRetired
                    BypassModifier = -1.0f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Kinetic, // Base Damage uses this
                    AreaEffect = Kinetic,
                    Detonation = Kinetic,
                    Shield = Kinetic, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = false,
                    Radius = 1.5f, // Meters
                    Damage = 500,
                    Depth = 10.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 6f, // Radius of AOE effect, in meters.
                    Damage = 6000,
                    Depth = 4.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 1, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink,  // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                    // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                    // Emp : Targets & Shutdown any Block capable of being powered
                                    // Offense : Targets & Shutdowns Weaponry
                                    // Nav : Targets & Shutdown Gyros or Locks them down
                                    // Dot : Deals Damage to Blocks in radius
                                    // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                    // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                    // Tractor : Affects target with Physics
                                    // Pull : Affects target with Physics
                                    // Push : Affects target with Physics
                                    // Anchor : Targets & Shutdowns Thrusters
                                    //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 180.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 1000f, // voxel phasing if you go above 5100
                MaxTrajectory = 6000f, // Max Distance the projectile or beam can Travel.
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.1f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 1800,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\OPC_Ammo\\TBR202_Sabot", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 8.0f, green: 0.0f, blue: 0.0f, alpha: 32.0f),
                        Offset = Vector(x: 0.0, y: -1.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 2000.0f,
                            MaxDuration = 1.0f,
                            Scale = 1.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "Explosion_S1",
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 100.0f,
                            MaxDuration = 1.0f,
                            Scale = 3.5f,
                            HitPlayChance = 1.0f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 5.0f, end: 10.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 5.0f, //
                        Width = 0.4f, //
                        Color = Color(red: 0.941f, green: 0.992f, blue: 1.0f, alpha: 1.0f),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"ProjectileTrailLine", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 0.941f, green: 0.992f, blue: 1.0f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 128, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "ArcWepSmallWarheadExpl",
                ShotSound = "",
                ShieldHitSound = "OPC_Common_Imp_Shield",
                PlayerHitSound = "",
                VoxelHitSound = "OPC_FBC_Imp_Vxl",
                FloatingHitSound = "OPC_Common_Imp_Med",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef TBC404_Round_Default => new AmmoDef
        {
            AmmoMagazine = "TBC404_Round", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "TBC404 Round", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
            HybridRound = false, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 120f, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 10000f, //2000.0f, // Direct damage; one steel plate is worth 100.
            Mass = 40.0f, // In kilograms; how much force the impact will apply to the target.
            Health = 0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 100000.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0.0f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = 1.2f, // Character damage multiplier; defaults to 1 if zero or less.
                                   // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 4000.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.75f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f, // Multiplier for damage against large grids.
                    Small = 0.3f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = -1f, // Multiplier for damage against light armor.
                    Heavy = 2.25f, // Multiplier for damage against heavy armor.
                    NonArmor = 0.25f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 0.3f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal, Bypass, EmpRetired
                    BypassModifier = -1.0f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Kinetic, // Base Damage uses this
                    AreaEffect = Kinetic,
                    Detonation = Kinetic,
                    Shield = Kinetic, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 6f, // Meters
                    Damage = 5000,
                    Depth = 3.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 15f, // Radius of AOE effect, in meters.
                    Damage = 20000,
                    Depth = 8f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 1, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink,  // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                    // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                    // Emp : Targets & Shutdown any Block capable of being powered
                                    // Offense : Targets & Shutdowns Weaponry
                                    // Nav : Targets & Shutdown Gyros or Locks them down
                                    // Dot : Deals Damage to Blocks in radius
                                    // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                    // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                    // Tractor : Affects target with Physics
                                    // Pull : Affects target with Physics
                                    // Push : Affects target with Physics
                                    // Anchor : Targets & Shutdowns Thrusters
                                    //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 180.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 2000, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 540f, // voxel phasing if you go above 5100
                MaxTrajectory = 10000f, // Max Distance the projectile or beam can Travel.
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 1800,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\OPC_Ammo\\TBC404_Sabot", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 8.0f, green: 0.0f, blue: 0.0f, alpha: 32.0f),
                        Offset = Vector(x: 0.0, y: -1.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 2000.0f,
                            MaxDuration = 1.0f,
                            Scale = 1.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "Explosion_LargeCaliberShell",
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 100.0f,
                            MaxDuration = 1.0f,
                            Scale = 3.0f,
                            HitPlayChance = 1.0f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 5.0f, end: 10.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 5.0f, //
                        Width = 0.4f, //
                        Color = Color(red: 0.941f, green: 0.992f, blue: 1.0f, alpha: 1.0f),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"ProjectileTrailLine", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 0.941f, green: 0.992f, blue: 1.0f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 128, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "ArcWepSmallWarheadExpl",
                ShotSound = "",
                ShieldHitSound = "OPC_Common_Imp_Shield",
                PlayerHitSound = "",
                VoxelHitSound = "OPC_FBC_Imp_Vxl",
                FloatingHitSound = "OPC_Common_Imp_Med",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef TBG101_50_BMG => new AmmoDef
        {
            AmmoMagazine = "TBG101_50_BMG_Magazine", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "F/TBG BMG Round", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
                                           //AmmoRound = OPC_Localization.GetText("OPC_50_BMG_AmmoRound_WC_LID"),
            HybridRound = false, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 0.0001f, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 75f, //29.0f, // Direct damage; one steel plate is worth 100.
            Mass = 0.1f, // In kilograms; how much force the impact will apply to the target.
            Health = 10.0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 800.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "MagicFragment", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 15.0f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = true, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1.0, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1.0f, // Character damage multiplier; defaults to 1 if zero or less.
                                    // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 0.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.5f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1.0f, // Multiplier for damage against large grids.
                    Small = -1.0f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 0.8f, // Multiplier for damage against light armor.
                    Heavy = 1.0f, // Multiplier for damage against heavy armor.
                    NonArmor = 0.35f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 0.5f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal, Bypass, EmpRetired
                    BypassModifier = -1.0f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Kinetic, // Base Damage uses this
                    AreaEffect = Kinetic,
                    Detonation = Kinetic,
                    Shield = Kinetic, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 0.25, // Meters
                    Damage = 22.0f,
                    Depth = 2.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 0.25, // Radius of AOE effect, in meters.
                    Damage = 22.0f,
                    Depth = 2.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 100, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink, // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                   // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                   // Emp : Targets & Shutdown any Block capable of being powered
                                   // Offense : Targets & Shutdowns Weaponry
                                   // Nav : Targets & Shutdown Gyros or Locks them down
                                   // Dot : Deals Damage to Blocks in radius
                                   // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                   // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                   // Tractor : Affects target with Physics
                                   // Pull : Affects target with Physics
                                   // Push : Affects target with Physics
                                   // Anchor : Targets & Shutdowns Thrusters
                                   //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 80.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 900, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 500.0f, // voxel phasing if you go above 5100
                MaxTrajectory = 1100.0f, //800.0f, // Max Distance the projectile or beam can Travel.
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 0.0,
                    DeCloakRadius = 0.0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f, // %
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 50.0f,
                            MaxDuration = 1.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 0.5f, end: 10.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 3.0f, //
                        Width = 0.15f, //
                        Color = Color(red: 0.850f, green: 0.968f, blue: 0.921f, alpha: 1.0f), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"ProjectileTrailLine", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 1.0f, green: 1.0f, blue: 2.0f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 3, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "OPC_Common_Imp_Lht",
                ShotSound = "",
                ShieldHitSound = "OPC_Common_Imp_Shield",
                PlayerHitSound = "",
                VoxelHitSound = "OPC_TBR_Imp_Vxl",
                FloatingHitSound = "OPC_Common_Imp_Lht",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef TBR102_APBC_T => new AmmoDef
        {
            AmmoMagazine = "TBR102_APBC_T_Magazine",
            AmmoRound = "F/TBR APBC-T Chaingun Round", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
                                                       //AmmoRound = OPC_Localization.GetText("OPC_APBC_T_AmmoRound_WC_LID"),
            HybridRound = false, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 0.001f, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 150f, //89.0f, // Direct damage; one steel plate is worth 100.
            Mass = 0.5f, // In kilograms; how much force the impact will apply to the target.
            Health = 0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 5000.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 15.0f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1.0, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1.0f, // Character damage multiplier; defaults to 1 if zero or less.
                                    // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 1000.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.25f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 0.75f, // Multiplier for damage against large grids.
                    Small = 0.8f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1.0f,
                    Light = 0.5f,
                    Heavy = 0.3f,
                    NonArmor = 1.5f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 0.5f,
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1.0f,
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Kinetic, // Base Damage uses this
                    AreaEffect = Kinetic,
                    Detonation = Kinetic,
                    Shield = Kinetic, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 1, // Meters
                    Damage = 75.0f,
                    Depth = 4.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 0.5, // Radius of AOE effect, in meters.
                    Damage = 25.0f,
                    Depth = 4.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Squeeze, //.NoFalloff applies the same damage to all blocks in radius
                                       // Linear drops evenly by distance from center out to max radius
                                       // Curve drops off damage sharply as it approaches the max radius
                                       // InvCurve drops off sharply from the middle and tapers to max radius
                                       // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                       // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                       // Exponential drops off exponentially.  Does not scale to max radius
                                       // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 100, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink,  // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                    // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                    // Emp : Targets & Shutdown any Block capable of being powered
                                    // Offense : Targets & Shutdowns Weaponry
                                    // Nav : Targets & Shutdown Gyros or Locks them down
                                    // Dot : Deals Damage to Blocks in radius
                                    // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                    // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                    // Tractor : Affects target with Physics
                                    // Pull : Affects target with Physics
                                    // Push : Affects target with Physics
                                    // Anchor : Targets & Shutdowns Thrusters
                                    //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 180.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 900, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 1000.0f, // voxel phasing if you go above 5100
                MaxTrajectory = 2500.0f, //1450.0f, // Max Distance the projectile or beam can Travel.
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1800, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 200.0,
                    DeCloakRadius = 100.0,
                    FieldTime = 1800,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "MaterialHit_Metal_GatlingGun",
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 50.0f,
                            MaxDuration = 1.0f,
                            Scale = 1.5f,
                            HitPlayChance = 1.0f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 5.0f, end: 10.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 3.0f,
                        Width = 0.15f,
                        Color = Color(red: 1.0f, green: 0.760f, blue: 0.560f, alpha: 1.0f),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"ProjectileTrailLine", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 1.0f, green: 2.0f, blue: 2.5f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 128, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "OPC_Common_Imp_Lht",
                ShotSound = "",
                ShieldHitSound = "OPC_Common_Imp_Shield",
                PlayerHitSound = "",
                VoxelHitSound = "OPC_TBR_Imp_Vxl",
                FloatingHitSound = "OPC_Common_Imp_Lht",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef TBR102_HE_T => new AmmoDef
        {
            AmmoMagazine = "TBR102_HE_T_Magazine",
            AmmoRound = "F/TBR HE-T Chaingun Round", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
                                                     //AmmoRound = OPC_Localization.GetText("OPC_HE_T_AmmoRound_WC_LID"),
            HybridRound = false, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 0.001f, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 75.0f, //50.0f, // Direct damage; one steel plate is worth 100.
            Mass = 0.5f, // In kilograms; how much force the impact will apply to the target.
            Health = 0, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 5000.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 15.0f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.5, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 1.0, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = -1.0f, // Character damage multiplier; defaults to 1 if zero or less.
                                    // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 1500.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.9f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 0.6f, // Multiplier for damage against large grids.
                    Small = 0.7f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f,
                    Light = 0.3f,
                    Heavy = 0.2f,
                    NonArmor = 2.5f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 0.75f,
                    Type = Default, // Damage vs healing against shields; Default, Heal
                    BypassModifier = -1.0f,
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Kinetic, // Base Damage uses this
                    AreaEffect = Kinetic,
                    Detonation = Kinetic,
                    Shield = Kinetic, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = true,
                    Radius = 10.0, // Meters
                    Damage = 60.0f,
                    Depth = 6f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = true,
                    Radius = 5.0, // Radius of AOE effect, in meters.
                    Damage = 30.0f,
                    Depth = 3f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 30, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink,  // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                    // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                    // Emp : Targets & Shutdown any Block capable of being powered
                                    // Offense : Targets & Shutdowns Weaponry
                                    // Nav : Targets & Shutdown Gyros or Locks them down
                                    // Dot : Deals Damage to Blocks in radius
                                    // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                    // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                    // Tractor : Affects target with Physics
                                    // Pull : Affects target with Physics
                                    // Push : Affects target with Physics
                                    // Anchor : Targets & Shutdowns Thrusters
                                    //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 180.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 900, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 600.0f, // voxel phasing if you go above 5100
                MaxTrajectory = 2500.0f, //1200.0f, // Max Distance the projectile or beam can Travel.
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1800, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 200.0,
                    DeCloakRadius = 100.0,
                    FieldTime = 1800,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "Explosion_MediumCaliberShell",
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 50.0f,
                            MaxDuration = 1.0f,
                            Scale = 0.3f,
                            HitPlayChance = 1.0f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 5.0f, end: 10.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 3.0f,
                        Width = 0.15f,
                        Color = Color(red: 0.925f, green: 0.749f, blue: 0.611f, alpha: 1.0f),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"ProjectileTrailLine", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 1.0f, green: 2.0f, blue: 2.5f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 128, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "OPC_Common_Imp_Lht",
                ShotSound = "",
                ShieldHitSound = "OPC_Common_Imp_Shield",
                PlayerHitSound = "",
                VoxelHitSound = "OPC_TBR_Imp_Vxl",
                FloatingHitSound = "OPC_Common_Imp_Lht",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef TEB101_Energy => new AmmoDef
        {
            AmmoMagazine = "Energy", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "Energy", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
            HybridRound = false, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 1f, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 10f, // 53.0f, // Direct damage; one steel plate is worth 100.
            Mass = 0.0f, // In kilograms; how much force the impact will apply to the target.
            Health = 0.0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 0.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 420, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = false, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = false, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = 0.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "", // AmmoRound field of the ammo to spawn.
                Fragments = 0, // Number of projectiles to spawn.
                Degrees = 0.0f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = false, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 1, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 0.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.33f, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 0.1f, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = 0.33f, // Character damage multiplier; defaults to 1 if zero or less.
                                    // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 1400.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.3f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 0.1f, // Multiplier for damage against large grids.
                    Small = 0.1f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = -1f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = -1f, // Multiplier for damage against light armor.
                    Heavy = -1f, // Multiplier for damage against heavy armor.
                    NonArmor = -1f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = -1f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal, Bypass, EmpRetired
                    BypassModifier = -1.0f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = false,
                    Radius = 5.0, // Meters
                    Damage = 5.0f,
                    Depth = 10.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = NoFalloff, // NoFalloff applies the same damage to all blocks in radius
                                         // Linear drops evenly by distance from center out to max radius
                                         // Curve drops off damage sharply as it approaches the max radius
                                         // InvCurve drops off sharply from the middle and tapers to max radius
                                         // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                         // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                         // Exponential drops off exponentially.  Does not scale to max radius
                                         // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = false,
                    Radius = 0.0, // Radius of AOE effect, in meters.
                    Damage = 0.0f,
                    Depth = 0.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = NoFalloff, //.NoFalloff applies the same damage to all blocks in radius
                                         // Linear drops evenly by distance from center out to max radius
                                         // Curve drops off damage sharply as it approaches the max radius
                                         // InvCurve drops off sharply from the middle and tapers to max radius
                                         // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                         // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                         // Exponential drops off exponentially.  Does not scale to max radius
                                         // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 100, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1.0f,
                    CustomParticle = "", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink, // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                   //EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                   //Emp : Targets & Shutdown any Block capable of being powered
                                   //Offense : Targets & Shutdowns Weaponry
                                   //Nav : Targets & Shutdown Gyros or Locks them down
                                   //Dot : Deals Damage to Blocks in radius
                                   //AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                   //JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                   //Tractor : Affects target with Physics
                                   //Pull : Affects target with Physics
                                   //Push : Affects target with Physics
                                   //Anchor : Targets & Shutdowns Thrusters
                                   //
                Mode = Effect, // Effect , Field
                Strength = 0.0f,
                Radius = 0.0, // Meters
                Duration = 0, // In Ticks
                StackDuration = false, // Combined Durations
                Depletable = false,
                MaxStacks = 0, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = false, // Show Block damage effect.
                    TriggerRange = 0.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = true, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = true, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 75.0f, // Degrees, Is pointed forward
                TargetLossTime = 120, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 600, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 50.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 100.0f, // voxel phasing if you go above 5100
                MaxTrajectory = 1400.0f, //800.0f, // Max Distance the projectile or beam can Travel.
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.01, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.0, // controls how sharp the trajectile may turn
                    TrackingDelay = 0.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = true, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.0f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 0, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 0.0,
                    DeCloakRadius = 0.0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 1.0f, // %
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = true,
                        Color = Color(red: 8.0f, green: 0.0f, blue: 0.0f, alpha: 32.0f),
                        Offset = Vector(x: 0.0, y: -1.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 2000.0f,
                            MaxDuration = 1.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 100.0f,
                            MaxDuration = 1.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.75f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = true,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 1.0f, end: 1.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.01f,
                        Width = 0.03f,
                        Color = Color(red: 10.0f, green: 10.0f, blue: 10.0f, alpha: 1.0f), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 2, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"WeaponLaser", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 1.0f, green: 2.0f, blue: 2.5f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "WeaponLaser", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 1, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 10.0f, green: 10.0f, blue: 10.0f, alpha: 0.0f),
                        Back = false,
                        CustomWidth = 0.03f,
                        UseWidthVariance = false,
                        UseColorFade = false,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.0,
                        MaxLength = 0.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "",
                ShotSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.0f,
                HitPlayShield = false,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };

        public AmmoDef TR100_Energy => new AmmoDef
        {
            AmmoMagazine = "Energy", // SubtypeId of physical ammo magazine. Use "Energy" for weapons without physical ammo.
            AmmoRound = "Energy", // Name of ammo in terminal, should be different for each ammo type used by the same weapon. Is used by Shrapnel.
            HybridRound = false, // Use both a physical ammo magazine and energy per shot.
            EnergyCost = 0.00000000001f, // Scaler for energy per shot (EnergyCost * BaseDamage * (RateOfFire / 3600) * BarrelsPerShot * TrajectilesPerBarrel). Uses EffectStrength instead of BaseDamage if EWAR.
            BaseDamage = 0.0f, // Direct damage; one steel plate is worth 100.
            Mass = 0.0f, // In kilograms; how much force the impact will apply to the target.
            Health = 0.0f, // How much damage the projectile can take from other projectiles (base of 1 per hit) before dying; 0 disables this and makes the projectile untargetable.
            BackKickForce = 0.0f, // Recoil. This is applied to the Parent Grid.
            DecayPerShot = 0.0f, // Damage to the firing weapon itself.
            HardPointUsable = true, // Whether this is a primary ammo type fired directly by the turret. Set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0, // For energy weapons, how many shots to fire before reloading.
            IgnoreWater = true, // Whether the projectile should be able to penetrate water when using WaterMod.
            IgnoreVoxels = true, // Whether the projectile should be able to penetrate voxels.
            Synchronize = false, // For future use
            HeatModifier = -1.0, // Allows this ammo to modify the amount of heat the weapon produces per shot.
            Shape = new ShapeDef // Defines the collision shape of the projectile, defaults to LineShape and uses the visual Line Length if set to 0.
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 0.0, // Diameter is minimum length of LineShape or minimum diameter of SphereShape.
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // Limits the number of entities (grids, players, projectiles) the projectile can penetrate; 0 = unlimited.
                CountBlocks = false, // Counts individual blocks, not just entities hit.
            },
            Fragment = new FragmentDef // Formerly known as Shrapnel. Spawns specified ammo fragments on projectile death (via hit or detonation).
            {
                AmmoRound = "MagicFragment", // AmmoRound field of the ammo to spawn.
                Fragments = 1, // Number of projectiles to spawn.
                Degrees = 0.0f, // Cone in which to randomize direction of spawned projectiles.
                BackwardDegrees = 0.0f,
                Reverse = false, // Spawn projectiles backward instead of forward.
                DropVelocity = false, // fragments will not inherit velocity from parent.
                Offset = 0.0f, // Offsets the fragment spawn by this amount, in meters (positive forward, negative for backwards), value is read from parent ammo type.
                Radial = 0.0f, // Determines starting angle for Degrees of spread above.  IE, 0 degrees and 90 radial goes perpendicular to travel path
                MaxChildren = 0, // number of maximum branches for fragments from the roots point of view, 0 is unlimited
                IgnoreArming = true, // If true, ignore ArmOnHit or MinArmingTime in EndOfLife definitions
                AdvOffset = Vector(x: 0.0, y: 0.0, z: 0.0), // advanced offsets the fragment by xyz coordinates relative to parent, value is read from fragment ammo type.
                TimedSpawns = new TimedSpawnDef // disables FragOnEnd in favor of info specified below
                {
                    Enable = true, // Enables TimedSpawns mechanism
                    Interval = 0, // Time between spawning fragments, in ticks, 0 means every tick, 1 means every other
                    StartTime = 0, // Time delay to start spawning fragments, in ticks, of total projectile life
                    MaxSpawns = 0, // Max number of fragment children to spawn
                    Proximity = 1000.0, // Starting distance from target bounding sphere to start spawning fragments, 0 disables this feature.  No spawning outside this distance
                    ParentDies = true, // Parent dies once after it spawns its last child.
                    PointAtTarget = true, // Start fragment direction pointing at Target
                    PointType = Predict, // Point accuracy, Direct (straight forward), Lead (always fire), Predict (only fire if it can hit)
                    DirectAimCone = 0.0f, //Aim cone used for Direct fire, in degrees
                    GroupSize = 5, // Number of spawns in each group
                    GroupDelay = 120, // Delay between each group.
                },
            },
            Pattern = new PatternDef
            {
                Enable = false,
                Patterns = new[] { // If enabled, set of multiple ammos to fire in order instead of the main ammo.
					"",
                },
                Mode = Fragment, // Select when to activate this pattern, options: Never, Weapon, Fragment, Both
                TriggerChance = 1.0f, // This is %
                Random = false, // This randomizes the number spawned at once, NOT the list order.
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false, // Skip the Ammo itself, in the list
                PatternSteps = 1, // Number of Ammos activated per round, will progress in order and loop. Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0.0f, // Blocks with integrity higher than this value will be immune to damage from this projectile; 0 = disabled.
                DamageVoxels = false, // Whether to damage voxels.
                SelfDamage = false, // Whether to damage the weapon's own grid.
                HealthHitModifier = 0.0, // How much Health to subtract from another projectile on hit; defaults to 1 if zero or less.
                VoxelHitModifier = 0.0, // Voxel damage multiplier; defaults to 1 if zero or less.
                Characters = 0.0f, // Character damage multiplier; defaults to 1 if zero or less.
                                   // For the following modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01f = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 0.0f, // Distance at which damage begins falling off.
                    MinMultipler = 0.0f, // Value from 0.0001f to 1f where 0.1f would be a min damage of 10% of base damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 0.0f, // Multiplier for damage against large grids.
                    Small = 0.0f, // Multiplier for damage against small grids.
                },
                Armor = new ArmorDef
                {
                    Armor = 0.0f, // Multiplier for damage against all armor. This is multiplied with the specific armor type multiplier (light, heavy).
                    Light = 0.0f, // Multiplier for damage against light armor.
                    Heavy = 0.0f, // Multiplier for damage against heavy armor.
                    NonArmor = 0.0f, // Multiplier for damage against every else.
                },
                Shields = new ShieldDef
                {
                    Modifier = 0.0f, // Multiplier for damage against shields.
                    Type = Default, // Damage vs healing against shields; Default, Heal, Bypass, EmpRetired
                    BypassModifier = 0.0f, // If greater than zero, the percentage of damage that will penetrate the shield.
                },
                DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy, ShieldDefault
                {
                    Base = Energy, // Base Damage uses this
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile. Shield Bypass Weapons, always Deal Energy regardless of this line
                },
                Custom = new CustomScalesDef
                {
                    SkipOthers = NoSkip, // Controls how projectile interacts with other blocks in relation to those defined here, NoSkip, Exclusive, Inclusive.
                    IgnoreAllOthers = false,
                    Types = new[] // List of blocks to apply custom damage multipliers to.
					{
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "",
                            Modifier = -1.0f,
                        },
                    },
                },
            },
            AreaOfDamage = new AreaOfDamageDef
            {
                ByBlockHit = new ByBlockHitDef
                {
                    Enable = false,
                    Radius = 5.0, // Meters
                    Damage = 5.0f,
                    Depth = 1.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Pooled, //.NoFalloff applies the same damage to all blocks in radius
                                      // Linear drops evenly by distance from center out to max radius
                                      // Curve drops off damage sharply as it approaches the max radius
                                      // InvCurve drops off sharply from the middle and tapers to max radius
                                      // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                      // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                      // Exponential drops off exponentially.  Does not scale to max radius
                                      // Legacy - ???
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
                EndOfLife = new EndOfLifeDef
                {
                    Enable = false,
                    Radius = 5.0, // Radius of AOE effect, in meters.
                    Damage = 5.0f,
                    Depth = 1.0f, // Max depth of AOE effect, in meters. 0=disabled, and AOE effect will reach to a depth of the radius value
                    MaxAbsorb = 0.0f, // Soft cutoff for damage, except for pooled falloff.  If pooled falloff, limits max damage per block.
                    Falloff = Squeeze, //.NoFalloff applies the same damage to all blocks in radius
                                       // Linear drops evenly by distance from center out to max radius
                                       // Curve drops off damage sharply as it approaches the max radius
                                       // InvCurve drops off sharply from the middle and tapers to max radius
                                       // Squeeze does little damage to the middle, but rapidly increases damage toward max radius
                                       // Pooled damage behaves in a pooled manner that once exhausted damage ceases.
                                       // Exponential drops off exponentially.  Does not scale to max radius
                                       // Legacy - ???
                    ArmOnlyOnHit = false, // Detonation only is available, After it hits something, when this is true. IE, if shot down, it won't explode.
                    MinArmingTime = 100, // In ticks, before the Ammo is allowed to explode, detonate or similar; This affects shrapnel spawning.
                    NoVisuals = false,
                    NoSound = false,
                    ParticleScale = 1.0f,
                    CustomParticle = "particleName", // Particle SubtypeID, from your Particle SBC
                    CustomSound = "soundName", // SubtypeID from your Audio SBC, not a filename
                    Shape = Diamond, // Round or Diamond shape.  Diamond is more performance friendly.
                },
            },
            Ewar = new EwarDef
            {
                Enable = false, // Enables EWAR effects AND DISABLES BASE DAMAGE AND AOE DAMAGE!!
                Type = EnergySink,  // EnergySink, Emp, Offense, Nav, Dot, AntiSmart, JumpNull, Anchor, Tractor, Pull, Push,
                                    // EnergySink : Targets & Shutdowns Power Supplies, such as Batteries & Reactor
                                    // Emp : Targets & Shutdown any Block capable of being powered
                                    // Offense : Targets & Shutdowns Weaponry
                                    // Nav : Targets & Shutdown Gyros or Locks them down
                                    // Dot : Deals Damage to Blocks in radius
                                    // AntiSmart : Effects & Scrambles the Targeting List of Affected Missiles
                                    // JumpNull : Shutdown & Stops any Active Jumps, or JumpDrive Units in radius
                                    // Tractor : Affects target with Physics
                                    // Pull : Affects target with Physics
                                    // Push : Affects target with Physics
                                    // Anchor : Targets & Shutdowns Thrusters
                                    //
                Mode = Effect, // Effect , Field
                Strength = 100.0f,
                Radius = 5.0, // Meters
                Duration = 100, // In Ticks
                StackDuration = true, // Combined Durations
                Depletable = true,
                MaxStacks = 10, // Max Debuffs at once
                NoHitParticle = false,
                Force = new PushPullDef
                {
                    ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    DisableRelativeMass = false,
                    TractorRange = 0.0,
                    ShooterFeelsForce = false,
                },
                Field = new FieldDef
                {
                    Interval = 0, // Time between each pulse, in game ticks (60 == 1 second), starts at 0 (59 == tick 60).
                    PulseChance = 0, // Chance from 0 - 100 that an entity in the field will be hit by any given pulse.
                    GrowTime = 0, // How many ticks it should take the field to grow to full size.
                    HideModel = false, // Hide the default bubble, or other model if specified.
                    ShowParticle = true, // Show Block damage effect.
                    TriggerRange = 250.0, //range at which fields are triggered
                    Particle = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    }
                },
            },
            Beams = new BeamDef
            {
                Enable = false, // Enable beam behaviour. Please have 3600 RPM, when this Setting is enabled. Please do not fire Beams into Voxels.
                VirtualBeams = false, // Only one damaging beam, but with the effectiveness of the visual beams combined (better performance).
                ConvergeBeams = false, // When using virtual beams, converge the visual beams to the location of the real beam.
                RotateRealBeam = false, // The real beam is rotated between all visual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None, // None, Remote, TravelTo, Smart, DetectTravelTo, DetectSmart, DetectFixed, DroneAdvanced
                TargetLossDegree = 180.0f, // Degrees, Is pointed forward
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..). time begins at 0 and time must EXCEED this value to trigger "time > maxValue". Please have a value for this, It stops Bad things.
                AccelPerSec = 0.0f, // Meters Per Second. This is the spawning Speed of the Projectile, and used by turning.
                DesiredSpeed = 1000.0f, // voxel phasing if you go above 5100
                MaxTrajectory = 17000.0f, // Max Distance the projectile or beam can Travel.
                DeaccelTime = 0, // 0 is disabled, a value causes the projectile to come to rest overtime, (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0.0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable. Natural Gravity Only.
                SpeedVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from DesiredSpeed. Be warned, you can make your projectile go backwards.
                RangeVariance = Random(start: 0.0f, end: 0.0f), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // ONLY POSITIVE VALUES; How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0.0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1.0, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 1.0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1800, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    CheckFutureIntersection = false, // Utilize obstacle avoidance?
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                    OffsetRatio = 0.05f, // The ratio to offset the random direction (0 to 1)
                    OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
                },
                Mines = new MinesDef  // Note: This is being investigated. Please report to Github, any issues.
                {
                    DetectRadius = 0.0,
                    DeCloakRadius = 0.0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "", // Model Path goes here.  "\\Models\\Ammo\\Starcore_Arrow_Missile_Large"
                VisualProbability = 0.0f, // %
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 0.0f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = true,
                        DisableCameraCulling = true,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "", // ShipWelderArc; SubtypeId of field particle effect.
                        ApplyToShield = false,
                        DisableCameraCulling = false,
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 0.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            Scale = 1.0f, // Scale of effect.
                            HitPlayChance = 1.0f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "", //probably obsolete
                    ColorVariance = Random(start: 0.0f, end: 0.0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0.0f, end: 0.0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = false,
                        Length = 5.0f, //
                        Width = 0.1f, //
                        Color = Color(red: 3.0f, green: 2.0f, blue: 1.0f, alpha: 1.0f), // RBG 255 is Neon Glowing, 100 is Quite Bright.
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
							"WeaponLaser", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "", // Please always have this Line set, if this Section is enabled.
							},
                            SegmentLength = 0.0, // Uses the values below.
                            SegmentGap = 0.0, // Uses Tracer textures and values
                            Speed = 1.0, // meters per second
                            Color = Color(red: 1.0f, green: 2.0f, blue: 2.5f, alpha: 1.0f),
                            WidthMultiplier = 1.0,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0.0f, end: 0.0f),
                            ColorVariance = Random(start: 0.0f, end: 0.0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
                            "", // Please always have this Line set, if this Section is enabled.
						},
                        TextureMode = Normal,
                        DecayTime = 3, // In Ticks. 1 = 1 Additional Tracer generated per motion, 33 is 33 lines drawn per projectile. Keep this number low.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 1.0f, alpha: 1.0f),
                        Back = false,
                        CustomWidth = 0.0f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0.0,// 0 offset value disables this effect
                        MinLength = 0.2,
                        MaxLength = 3.0,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "", // SubtypeID for your Sound File. Travel, is sound generated around your Projectile in flight
                HitSound = "",
                ShotSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.0f,
                HitPlayShield = false,
            },
            Ejection = new EjectionDef // Optional Component, allows generation of Particle or Item (Typically magazine), on firing, to simulate Tank shell ejection
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100.0f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
    }
}