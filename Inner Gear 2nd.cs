using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.gears
{
    // this class not yet implemented yet!
    public class InnerGear2nd : InnerGear 
    {
        public int _2ndInnerTeeth { get; set; }
        public double _2ndInnerTeethNotchesTraversed { get; set; }
        public double _2ndEffectiveRatioInnerDrive { get; set; }

        public double _2InnerGearRevolutions { get; set; }

        public bool _2ndInnerTeethDriving { get; set; }
        public bool _2ndInnerTeethDrivenBy { get; set; }

        public int _2ndInnerTeethDrivingGearID { get; set; }
        public int _2ndInnerTeethDrivenByGearID { get; set; }

    }
}
