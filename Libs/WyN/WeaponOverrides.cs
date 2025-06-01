using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Libs.WyN
{
    [XmlRoot("WeaponOverrides")]
    public class WeaponOverrides
    {
        [XmlElement("WeaponOverride")]
        public List<CSVWeapon> Items { get; set; } = [];
    }
}
