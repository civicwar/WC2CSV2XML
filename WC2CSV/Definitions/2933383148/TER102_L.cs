using CoreSystems.Support;
using static CoreSystems.Support.WeaponDefinition;
using static CoreSystems.Support.WeaponDefinition.HardPointDef;
using static CoreSystems.Support.WeaponDefinition.HardPointDef.HardwareDef;
using static CoreSystems.Support.WeaponDefinition.HardPointDef.HardwareDef.HardwareType;
using static CoreSystems.Support.WeaponDefinition.HardPointDef.Prediction;
using static CoreSystems.Support.WeaponDefinition.ModelAssignmentsDef;
using static CoreSystems.Support.WeaponDefinition.TargetingDef.BlockTypes;
using static CoreSystems.Support.WeaponDefinition.TargetingDef.Threat;

namespace WyN.WeaponCore
{
    partial class Parts
    {
        WeaponDefinition OPC_TER102_L => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = CreateSimilarMountPoints(
                    new MountPointDef
                    {
                        SpinPartId = "None", // For weapons with a spinning barrel such as Gatling Guns. Subpart_Boomsticks must be written as Boomsticks.
                        MuzzlePartId = "MissileTurretBarrels", // The subpart where your muzzle empties are located. This is often the elevation subpart. Subpart_Boomsticks must be written as Boomsticks.
                        AzimuthPartId = "MissileTurretBase1", // Your Rotating Subpart, the bit that moves sideways.
                        ElevationPartId = "MissileTurretBarrels",// Your Elevating Subpart, that bit that moves up.
                        DurabilityMod = 0.25f, // GeneralDamageMultiplier, 0.25f = 25% damage taken.
                        IconName = "Inventory\\Plasma.dds" // Overlay for block inventory slots, like reactors, refineries, etc.
                    }
                    , "TER102_L"
                    , "TER102_S"
                    , "TER102C_L"
                    , "TER102C_S"
                ),
                Muzzles = new[] { // Where your Projectiles spawn. Use numbers not Letters. IE Muzzle_01 not Muzzle_A
					"muzzle_missile_1",
                    "muzzle_missile_2"
                },
                Ejector = "", // Optional; empty from which to eject "shells" if specified.
                Scope = "camera", // Where line of sight checks are performed from. Must be clear of block collision.
            },

            Targeting = new TargetingDef
            {
                Threats = new[] {
                    Grids, Projectiles, Characters, Meteors, // Types of threat to engage: Grids, Projectiles, Characters, Meteors, Neutrals
				},
                SubSystems = new[] {
                    Offense, Utility, Power, Production, Thrust, Jumping, Steering, Any, // Subsystem targeting priority: Offense, Utility, Power, Production, Thrust, Jumping, Steering, Any
				},
                ClosestFirst = true, // Tries to pick closest targets first (blocks on grids, projectiles, etc...).
                IgnoreDumbProjectiles = false, // Don't fire at non-smart projectiles.
                LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
                MinimumDiameter = 0.0f, // Minimum radius of threat to engage.
                MaximumDiameter = 0.0f, // Maximum radius of threat to engage; 0 = unlimited.
                MaxTargetDistance = 1500.0f, // Maximum distance at which targets will be automatically shot at; 0 = unlimited.
                MinTargetDistance = 0.0f, // Minimum distance at which targets will be automatically shot at.
                TopTargets = 4, // Maximum number of targets to randomize between; 0 = unlimited.
                TopBlocks = 8, // Maximum number of blocks to randomize between; 0 = unlimited.
                StopTrackingSpeed = 0.0, // Do not track threats traveling faster than this speed; 0 = unlimited.
            },

            HardPoint = new HardPointDef
            {
                PartName = "TER102 L Beagalltach", // Name of the weapon in terminal, should be unique for each weapon definition that shares a SubtypeId (i.e. multiweapons).
                                                   //PartName = OPC_Localization.GetText("OPC_TBC202L_PartName_WC_LID"),
                DeviateShotAngle = 0.1f, // Projectile inaccuracy in degrees.
                AimingTolerance = 0.5, // How many degrees off target a turret can fire at. 0 - 180 firing angle.
                AimLeadingPrediction = Accurate, // Level of turret aim prediction; Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 second, etc..). Length of time the weapon continues firing after trigger is released.
                AddToleranceToTracking = true, // Allows turret to track to the edge of the AimingTolerance cone instead of dead centre.
                CanShootSubmerged = false, // Whether the weapon can be fired underwater when using WaterMod.
                Ui = new UiDef
                {
                    RateOfFire = false, // Enables terminal slider for changing rate of fire.
                    DamageModifier = false, // Enables terminal slider for changing damage per shot.
                    ToggleGuidance = false, // Enables terminal option to disable smart projectile guidance.
                    EnableOverload = true, // Enables terminal option to turn on Overload; this allows energy weapons to double damage per shot, at the cost of quadrupled power draw and heat gain, and 2% self damage on overheat.
                },
                Ai = new AiDef
                {
                    TrackTargets = true, // Whether this weapon tracks its own targets, or (for multiweapons) relies on the weapon with PrimaryTracking enabled for target designation. Turrets Need this set to True.
                    TurretAttached = true, // Whether this weapon is a turret and should have the UI and API options for such. Turrets Need this set to True.
                    TurretController = true, // Whether this weapon can physically control the turret's movement. Turrets Need this set to True.
                    PrimaryTracking = false, // For multiweapons: whether this weapon should designate targets for other weapons on the platform without their own tracking.
                    LockOnFocus = true, // If enabled, weapon will only fire at targets that have been HUD selected AND locked onto by pressing Numpad 0.
                    SuppressFire = false, // If enabled, weapon can only be fired manually.
                    OverrideLeads = false, // Disable target leading on fixed weapons, or allow it for turrets.
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.05f, // Max traversal speed of azimuth subpart in radians per tick (0.1 is approximately 360 degrees per second).
                    ElevateRate = 0.05f, // Max traversal speed of elevation subpart in radians per tick.
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -16,
                    MaxElevation = 90,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 0, // Default resting elevation
                    InventorySize = 0.001f, // Inventory capacity in kL.
                    IdlePower = 0.2f, // Constant base power draw in MW.
                    FixedOffset = false, // Deprecated.
                    Offset = Vector(x: 0.0, y: 0.0, z: 0.0), // Offsets the aiming/firing line of the weapon, in metres.
                    Type = BlockWeapon, // What type of weapon this is; BlockWeapon, HandWeapon, Phantom
                    CriticalReaction = new CriticalDef
                    {
                        Enable = false, // Enables Warhead behaviour.
                        DefaultArmedTimer = 120, // Sets default countdown duration.
                        PreArmed = false, // Whether the warhead is armed by default when placed. Best left as false.
                        TerminalControls = true, // Whether the warhead should have terminal controls for arming and detonation.
                        AmmoRound = "", // Optional. If specified, the warhead will always use this ammo on detonation rather than the currently selected ammo.
                    },
                },
                Other = new OtherDef
                {
                    ConstructPartCap = 0, // Maximum number of blocks with this weapon on a grid; 0 = unlimited.
                    RotateBarrelAxis = 0, // For spinning barrels, which axis to spin the barrel around; 0 = none.
                    EnergyPriority = 0, // Deprecated.
                    MuzzleCheck = false, // Whether the weapon should check LOS from each individual muzzle in addition to the scope.
                    Debug = false, // Force enables debug mode.
                    RestrictionRadius = 0.0, // Prevents other blocks of this type from being placed within this distance of the centre of the block.
                    CheckInflatedBox = false, // If true, the above distance check is performed from the edge of the block instead of the centre.
                    CheckForAnyWeapon = false, // If true, the check will fail if ANY weapon is present, not just weapons of the same subtype.
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 250, // Set this to 3600 for beam weapons. This is how fast your Gun fires.
                    BarrelsPerShot = 1, // How many muzzles will fire a projectile per fire event.
                    TrajectilesPerBarrel = 1, // Number of projectiles per muzzle per fire event.
                    SkipBarrels = 0, // Number of muzzles to skip after each fire event.
                    ReloadTime = 1, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MagsToLoad = 1, // Number of physical magazines to consume on reload.
                    DelayUntilFire = 0, // How long the weapon waits before shooting after being told to fire. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 75, // Heat generated per shot.
                    MaxHeat = 3800, // Max heat before weapon enters cooldown (70% of max heat).
                    Cooldown = 0.15f, // Percentage of max heat to be under to start firing again after overheat; accepts 0 - 0.95
                    HeatSinkRate = 150, // Amount of heat lost per second.
                    DegradeRof = false, // Progressively lower rate of fire when over 80% heat threshold (80% of max heat).
                    ShotsInBurst = 0, // Use this if you don't want the weapon to fire an entire physical magazine in one go. Should not be more than your magazine capacity.
                    DelayAfterBurst = 0, // How long to spend "reloading" after each burst. Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFull = false, // Whether the weapon should fire the full magazine (or the full burst instead if ShotsInBurst > 0), even if the target is lost or the player stops firing prematurely.
                    GiveUpAfter = false, // Whether the weapon should drop its current target and reacquire a new target after finishing its magazine or burst.
                    BarrelSpinRate = 0, // Visual only, 0 disables and uses RateOfFire.
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                    SpinFree = false, // Spin barrel while not firing.
                    StayCharged = true, // Will start recharging whenever power cap is not full.
                    MaxActiveProjectiles = 0, // Maximum number of projectiles in flight
                    MaxReloads = 0, // Maximum number of reloads in the LIFETIME of a weapon
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "", // Audio for warmup effect.
                    FiringSound = "OPC_TER_Shot", // Audio for firing.
                    FiringSoundPerShot = true, // Whether to replay the sound for each shot, or just loop over the entire track while firing.
                    ReloadSound = "", // Sound SubtypeID, for when your Weapon is in a reloading state
                    NoAmmoSound = "",
                    HardPointRotationSound = "OPC_Common_Rotate_Hvy", // Audio played when turret is moving.
                    BarrelRotationSound = "",
                    FireSoundEndDelay = 0, // ONLY POSITIVE VALUES; How long the firing audio should keep playing after firing stops. Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                    FireSoundNoBurst = false, // Don't stop firing sound from looping when delaying after burst.
                },
                Graphics = new HardPointParticleDef
                {
                    Effect1 = new ParticleDef
                    {
                        Name = "OPC_TER102_MFlash", // SubtypeId of muzzle particle effect.
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 1.0f), // Deprecated, set color in particle sbc.
                        Offset = Vector(x: 0.0, y: 0.0, z: -1.75), // Offsets the effect from the muzzle empty.
                        Extras = new ParticleOptionDef
                        {
                            Loop = false, // Set this to the same as in the particle sbc!
                            Restart = true, // Whether to end a looping effect instantly when firing stops.
                            Scale = 1.75f, // Scale of effect.
                            MaxDistance = 500.0f,
                            MaxDuration = 5.0f,
                            HitPlayChance = 1.0f,
                        },
                    },
                    Effect2 = new ParticleDef
                    {
                        Name = "",
                        Color = Color(red: 0.0f, green: 0.0f, blue: 0.0f, alpha: 1.0f),
                        Offset = Vector(x: 0.0, y: 0.0, z: 0.0),
                        ApplyToShield = false,
                        Extras = new ParticleOptionDef
                        {
                            Loop = false, // Set this to the same as in the particle sbc!
                            Restart = false,
                            Scale = 1.0f,
                            MaxDistance = 0.0f,
                            MaxDuration = 0.0f,
                            HitPlayChance = 0.0f,
                        },
                    },
                },
            },

            Ammos = new[]
            {
                TER102_Plasma, // Must list all primary, shrapnel, and pattern ammos.
			},

            //Upgrades = UpgradeModules,
        };
    }
}