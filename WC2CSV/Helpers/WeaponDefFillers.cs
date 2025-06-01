using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRageMath;
using static CoreSystems.Support.WeaponDefinition.AmmoDef.TrajectoryDef.ApproachDef;
using static CoreSystems.Support.WeaponDefinition.ModelAssignmentsDef;

namespace WyN.WeaponCore
{
    partial class Parts
    {
        public RelativeTo Current { get; private set; }
        public UpRelativeTo RelativeToBlock { get; private set; }
        public UpRelativeTo RelativeToGravity { get; private set; }

        internal Vector4 Color(float red, float green, float blue, float alpha)
        {
            return new Vector4(red, green, blue, alpha);
        }

        internal Vector3D Vector(double x, double y, double z)
        {
            return new Vector3D(x, y, z);
        }

        internal MountPointDef[] CreateSimilarMountPoints(MountPointDef mountPointDef, params string[] v)
        {
            return [mountPointDef];
        }
    }
}
