using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.gears
{
    public class BaseGear
    {
        // valid only for GearID = 1, all other values are calculated!
        public enum _Rotation { Unknown, Clockwise, AntiClockwise }
        public _Rotation Rotation { get; set; }

        public string GearName { get; set; }
        public int GearID { get; set; }

        public double EffectiveRatioBaseDrive { get; set; }

        public int BaseTeeth { get; set; }
        public double BaseTeethNotchesTraversed { get; set; }
        public double BaseGearRevolutions { get; set; }

        public bool BaseTeethDriving { get; set; }
        public bool BaseTeethDrivenBy { get; set; }

        public int BaseTeethDrivingGearID { get; set; }
        public int BaseTeethDrivenByGearID { get; set; }

        public Watch.gears.BaseGear BaseMove(Watch.gears.BaseGear previousbaseGear, gears.InnerGear previousInnerGear, int Notches) 
        {
            int OnceOnly = 0; // for trouble shooting purposes, if entry into more than 1 routine, there is a bug
            Watch.movement.supporting oSupport = new Watch.movement.supporting();

            EffectiveRatioBaseDrive = 1; // calculate intra-gear ratio

            if ((!BaseTeethDrivenBy) && (BaseTeethDriving))
            {
                BaseTeethNotchesTraversed = BaseTeethNotchesTraversed + (double)Notches;
                OnceOnly++;
            }

            if ((BaseTeethDrivenBy) && (previousbaseGear.BaseTeethDriving))
            {
                if (BaseTeethDrivenByGearID.Equals(previousbaseGear.GearID))
                {
                    if (previousbaseGear.BaseTeethDrivingGearID.Equals(GearID))
                    {
                        BaseTeethNotchesTraversed = previousbaseGear.BaseTeethNotchesTraversed;
                        Rotation = oSupport.GetNxtRotation(previousbaseGear.Rotation);
                        OnceOnly++;
                    }
                }
            }

            if ((BaseTeethDrivenBy) && (previousInnerGear.BaseTeethDriving))
            {
                if (BaseTeethDrivenByGearID.Equals(previousInnerGear.GearID))
                {
                    if (previousInnerGear.BaseTeethDrivingGearID.Equals(GearID))
                    {
                        BaseTeethNotchesTraversed = previousInnerGear.BaseTeethNotchesTraversed;
                        Rotation = oSupport.GetNxtRotation(previousInnerGear.Rotation);
                        OnceOnly++;
                    }
                }
            }
            if ((BaseTeethDrivenBy) && (previousInnerGear.InnerTeethDriving))
            {
                if (BaseTeethDrivenByGearID.Equals(previousInnerGear.GearID))
                {
                    if (previousInnerGear.InnerTeethDrivingGearID.Equals(GearID))
                    {
                        BaseTeethNotchesTraversed = previousInnerGear.InnerTeethNotchesTraversed;
                        Rotation = oSupport.GetNxtRotation(previousInnerGear.Rotation);
                        OnceOnly++;
                    }
                }
            }
            if (!OnceOnly.Equals(1))
            {
                throw new Exception("Base Teeth, entered " + OnceOnly.ToString() + " times!" );
            }
            BaseGearRevolutions = BaseTeethNotchesTraversed / BaseTeeth;
            return this;
        }
    }
}
