using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.gears
{
    public class InnerGear : BaseGear
    {
        public int InnerTeeth { get; set; }
        public double InnerTeethNotchesTraversed { get; set; }

        public double EffectiveRatioInnerDrive { get; set; }

        public bool InnerTeethDriving { get; set; }
        public bool InnerTeethDrivenBy { get; set; }

        public double InnerGearRevolutions { get; set; }

        public int InnerTeethDrivingGearID { get; set; }
        public int InnerTeethDrivenByGearID { get; set; }


        public Watch.gears.InnerGear InnerMove(Watch.gears.BaseGear previousbaseGear, gears.InnerGear previousInnerGear, int Notches)
        {
            int OnceOnly = 0;
            Watch.movement.supporting oSupport = new Watch.movement.supporting();

            // calculate intra-gear ratios
            EffectiveRatioBaseDrive = (Double)BaseTeeth / InnerTeeth; 
            EffectiveRatioInnerDrive = (Double)InnerTeeth / BaseTeeth;

            if (GearID.Equals(1))
            {
                if (((!BaseTeethDrivenBy) && (InnerTeethDriving))||((BaseTeethDriving) && (!InnerTeethDriving)))
                    {
                        BaseTeethNotchesTraversed = BaseTeethNotchesTraversed + (double)Notches;
                        InnerTeethNotchesTraversed = BaseTeethNotchesTraversed * EffectiveRatioInnerDrive;
                        OnceOnly++;
                    }
            }
            else
            {
                    if ((InnerTeethDrivenBy) && (previousbaseGear.BaseTeethDriving))
                    {
                        if (InnerTeethDrivenByGearID.Equals(previousbaseGear.GearID))
                        {
                            if (previousbaseGear.BaseTeethDrivingGearID.Equals(GearID))
                            {
                                InnerTeethNotchesTraversed = previousbaseGear.BaseTeethNotchesTraversed;
                                BaseTeethNotchesTraversed = InnerTeethNotchesTraversed * EffectiveRatioBaseDrive;
                                Rotation = oSupport.GetNxtRotation(previousbaseGear.Rotation);
                                OnceOnly++;
                            }
                        }
                    }

                    if ((BaseTeethDrivenBy) && (previousbaseGear.BaseTeethDriving))
                    {
                        if (BaseTeethDrivenByGearID.Equals(previousbaseGear.GearID))
                        {
                            if (previousbaseGear.BaseTeethDrivingGearID.Equals(GearID))
                            {
                                BaseTeethNotchesTraversed = previousbaseGear.BaseTeethNotchesTraversed;
                                InnerTeethNotchesTraversed = EffectiveRatioInnerDrive * BaseTeethNotchesTraversed;
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
                                InnerTeethNotchesTraversed = BaseTeethNotchesTraversed * EffectiveRatioInnerDrive;
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
                            InnerTeethNotchesTraversed = BaseTeethNotchesTraversed * EffectiveRatioInnerDrive;
                            Rotation = oSupport.GetNxtRotation(previousInnerGear.Rotation);
                            OnceOnly++;
                        }
                    }
                }
            }

            if (!OnceOnly.Equals(1))
            {
                throw new Exception("Inner Teeth, entered " + OnceOnly.ToString() + " times!");
            }

            BaseGearRevolutions = Math.Round(BaseTeethNotchesTraversed / BaseTeeth, 10);
            InnerGearRevolutions = Math.Round(InnerTeethNotchesTraversed / InnerTeeth, 10);

            if (!BaseGearRevolutions.Equals(InnerGearRevolutions))
            {
                throw new Exception("Inner and Base teeth revs do not match!");
            }
            return this;
        }
    }
 }
