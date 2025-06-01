
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Libs.WyN
{
    [XmlRoot("AmmoOverrides")]
    public class AmmoOverrides
    {
        [XmlElement("AmmoOverride")]
        public List<CSVAmmo> Items { get; set; } = [];
    }
}
