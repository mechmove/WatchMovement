using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.movement
{

    public class MoonRealStatus
    {
        public double LunationsElapsed { get; set; }
        public double AgeOfMoonDays { get; set; }
        public string QuarterStatus { get; set; }
        public double ofCompletePhasePct { get; set; }
        public DateTime RefDateUTC { get; set; }
        public DateTime TargetDateUTC { get; set; }

    }

    public class MoonDiskStatus
    {
        public double LunationsElapsed { get; set; }
        public double FastOrSlowMinutes { get; set; }
        public double ofCompletePhasePct { get; set; }
        public double PrecisionYrs { get; set; }
        public double Notches { get; set; }
        public double NotchPositionOnMoonDisk { get; set; }
        public double AgeOfMoonDays { get; set; }
        public string QuarterStatus { get; set; }
        public List<string> listGearProperties = new List<string>();
        public DateTime RefDateUTC { get; set; }
        public DateTime TargetDateUTC { get; set; }

    }

    class supporting

    {

        public enum _BaseDriverTransmission { smooth, stepwise }
        public enum _BaseDriverStepMethod { runOver, runUnder}
        public enum _MoonPhaseDiskBaseUnits { seconds, minutes, hourly, daily }

        public enum _SpinMode { Purist, FastAndDirty}
        public _SpinMode SpinMode { get; set; }


    // this is the publicly known constant used for long term calcs:
    public double SynoticPhase_Minutes_Real = 42524.0483333333;
        // real moonphase reference from Internet, this number will not be precise due to the 
        // nature of moonphase calcs
        public DateTime KnownFullMoon_UTC_used = Convert.ToDateTime("5/29/2018  14:19:00"); // UTC time

        public movement.MoonRealStatus GetRealMoonPhase(double SynoticPhaseRealMinutes, TimeSpan ts,DateTime findPhaseUTC)
        {
            movement.MoonRealStatus oRealMoonStatus = new movement.MoonRealStatus();
            movement.supporting oSupport = new movement.supporting();

            oRealMoonStatus.TargetDateUTC = findPhaseUTC;
            oRealMoonStatus.RefDateUTC = KnownFullMoon_UTC_used;
            
            double d = ts.TotalMinutes;

            double dPhasePercent = d / SynoticPhaseRealMinutes;
            oRealMoonStatus.LunationsElapsed = dPhasePercent;

            if (dPhasePercent > 1)
            {
                dPhasePercent = dPhasePercent - Math.Floor(dPhasePercent);
            }
            double OfCompletePhase = (dPhasePercent * 100)+50;

            if (OfCompletePhase > 100)
            {
                OfCompletePhase = OfCompletePhase - 100;
            }
            oRealMoonStatus.AgeOfMoonDays = oSupport.AgeOfMoon(OfCompletePhase, SynoticPhaseRealMinutes);
            oRealMoonStatus.ofCompletePhasePct = Math.Round(OfCompletePhase, 2);
             oRealMoonStatus.QuarterStatus = oSupport.GetPhaseQuarterStatus(dPhasePercent*100);


            return oRealMoonStatus;

        }

        public double GetPrecision(double k)
        {
            // find precision here:
            Double Years = (k * (1440 / (SynoticPhase_Minutes_Real - k))) / 60 / 24 / 365.25;

            if ((Years > 400) || (Years < -400)) // Negative means 1 day is lost, + 1 day gained
            {
                Years = (k * (1440 / (SynoticPhase_Minutes_Real -  k))) / 60 / 24 / 365.243;
            }
            return Years;
        }

        public gears.BaseGear._Rotation GetNxtRotation(gears.BaseGear._Rotation r)
        {
            switch (r)
            {
                case gears.BaseGear._Rotation.Clockwise:
                    return gears.BaseGear._Rotation.AntiClockwise;
                case gears.BaseGear._Rotation.AntiClockwise:
                    return gears.BaseGear._Rotation.Clockwise;
            }
            return gears.BaseGear._Rotation.Unknown;
        }


        public Double AgeOfMoon(double ofCompletePhase, double SynoticPhase_Minutes)
        {
            double DaysSynotic = SynoticPhase_Minutes / 60 / 24;
            return Math.Round(((ofCompletePhase) / 100) * (DaysSynotic), 2);
        }

        //public double Illumination(double OfCompletePhase)
        //{   // real illumination is not a simple function, therefore my definition is based on a SIN wave, 
        //    // which is highly inaccurate, but need something here to approximate the moon's illumination.
        //    // 1/2 a Sin wave with period = PI peaking at 50% to represent a full lunation, 0 - 100 - 0, 
        //    // AGAIN highly inaccurate!
        //    OfCompletePhase = (OfCompletePhase/100) * Math.PI;
        //    OfCompletePhase = Math.Sin(OfCompletePhase)*100;
        //    return OfCompletePhase;
        //}


        public string GetPhaseQuarterStatus(double ofCompletePhase)
        // close to 0 or 100 percent = Full Moon
        // close to 50 percent = New Moon

        {
            if (ofCompletePhase<0)
            {
                ofCompletePhase = Math.Abs(ofCompletePhase);
            }

            if (ofCompletePhase >= 97 || ofCompletePhase <= 3.0)
            {
                return "Full Moon";
            }
            if (ofCompletePhase > 3 && ofCompletePhase <= 23.0)
            {
                return "Waning Gibbious";
            }

            if (ofCompletePhase > 23 && ofCompletePhase <= 27.0)
            {
                return "Last Quarter";
            }

            if (ofCompletePhase > 27 && ofCompletePhase < 47.0)
            {
                return "Waning Cresent";
            }

            if (ofCompletePhase >= 47 && ofCompletePhase <= 53)
            {
                return "New Moon";
            }
            if (ofCompletePhase > 53 && ofCompletePhase <= 73.0)
            {
                return "Waxing Cresent";
            }

            if (ofCompletePhase > 73 && ofCompletePhase <= 77.0)
            {
                return "First Quarter";
            }

            if (ofCompletePhase > 77 && ofCompletePhase < 97.0)
            {
                return "Waxing Gibbious";
            }

            return "Transit";
        }

        public double CalcSynoticPhaseThisMoonDiskMinutes(double NotchesMoonDisk, 
            movement.movementCase mC, 
            _MoonPhaseDiskBaseUnits Units, 
            _BaseDriverTransmission BaseDriverTransmission, 
            _BaseDriverStepMethod BaseDriverStepMethod)

        {double SingleMoonPhase  = NotchesMoonDisk / 2.0;
            // how long before last gear reaches SingleMoonPhase = 1/2 moon disk?
            double iCntr = 1.0;

            double iCntr_Last=0;
            double dFinalGearPosition_Last = 0;

            double dFinalGearPosition = 0;
            for (iCntr = 1; iCntr < double.PositiveInfinity; iCntr++)
            {
                mC.incNotches(ref mC, 1); // now get the movement working
                dFinalGearPosition = mC.GetFinalGearPosition(mC);
                if (dFinalGearPosition > SingleMoonPhase)
                {
                    switch (BaseDriverTransmission)
                    {
                        // does the first input gear Step or Rotate Smoothly?
                        // ex.  gears can move smoothly for better resolution.
                        //      or they can STEP into the next notch, this results in the worest resolution!
                        case _BaseDriverTransmission.stepwise:
                            {
                                switch (BaseDriverStepMethod)
                                {   // do you want to assume the synotic phase runs just under or over 
                                     // 1/2 revolution of the moon disk?
                                    case _BaseDriverStepMethod.runOver:
                                        {
                                            break;
                                        }
                                    case _BaseDriverStepMethod.runUnder:
                                        {
                                            iCntr = iCntr_Last;
                                            dFinalGearPosition = dFinalGearPosition_Last ;
                                            break;
                                        }
                                }

                                break;
                            }
                        case _BaseDriverTransmission.smooth:
                            {
                                iCntr = (iCntr / dFinalGearPosition) * SingleMoonPhase;
                                break;
                            }
                    }

                    break;
                }
                iCntr_Last = iCntr;
                dFinalGearPosition_Last = dFinalGearPosition;
            }


            switch (Units)
            {// rtn Minutes
                case _MoonPhaseDiskBaseUnits.daily:
                    return (double)iCntr * 24 * 60; // rtn Minutes
                case _MoonPhaseDiskBaseUnits.hourly:
                    return (double)iCntr * 60; // rtn Minutes
                case _MoonPhaseDiskBaseUnits.minutes:
                    return -1; // not defined yet
                case _MoonPhaseDiskBaseUnits.seconds:
                    return -1;// not defined yet
            }
            return -1;
        }


        public movement.MoonDiskStatus RunMoonModule(movement.movementCase mC, double NotchesMoonDisk, double SynoticPhaseThisMoonDiskMinutes, TimeSpan ts, DateTime findPhaseUTC, double LunationsElapsedReal)
        {
            supporting oMoonK = new supporting();
            MoonDiskStatus oMoonDiskStatus = new MoonDiskStatus();
            makeDisks oMakeMoonDisk = new makeDisks();

            oMoonDiskStatus.LunationsElapsed = ts.TotalMinutes / SynoticPhaseThisMoonDiskMinutes;
            oMoonDiskStatus.FastOrSlowMinutes = (double)(oMoonDiskStatus.LunationsElapsed - LunationsElapsedReal) * SynoticPhase_Minutes_Real;
            oMoonDiskStatus.TargetDateUTC = findPhaseUTC;
            oMoonDiskStatus.RefDateUTC = KnownFullMoon_UTC_used;

            double GearNotches = mC.GetFinalGearPosition(mC);

            double FullMoonOffSet = NotchesMoonDisk / 4.0;
            double MoonDiskFullTravel = FullMoonOffSet + GearNotches;
            if (MoonDiskFullTravel > NotchesMoonDisk)
            {
                double MoonDiskFactor = MoonDiskFullTravel / NotchesMoonDisk;
                double OneMoonDiskRevolution = MoonDiskFactor - Math.Floor(MoonDiskFactor);
                MoonDiskFullTravel = OneMoonDiskRevolution * NotchesMoonDisk;
            }

            // for 135 notched disk, here is the legend:
            //new   - full      - new 
            //1     - 33.75     - 67.5
            //67.5  - 101.25    - 135

            // for 90 notched disk, here is the legend:
            //new   - full      - new 
            //1     - 22.5      - 45
            //45    - 67.5      - 90

            oMoonDiskStatus.NotchPositionOnMoonDisk = MoonDiskFullTravel;

            // close to 0 or 100 percent = Full Moon
            // close to 50 percent = New Moon
            if (MoonDiskFullTravel > FullMoonOffSet * 2)
            {
                MoonDiskFullTravel = MoonDiskFullTravel - FullMoonOffSet * 2;
            }
            double OfCompletePhase = (MoonDiskFullTravel / (FullMoonOffSet * 2)) * 100;

            oMoonDiskStatus.AgeOfMoonDays = oMoonK.AgeOfMoon(OfCompletePhase, SynoticPhaseThisMoonDiskMinutes);
            oMoonDiskStatus.ofCompletePhasePct = Math.Round(OfCompletePhase, 2);
            double NewCompleteOfPhase = OfCompletePhase + 50;
            if (NewCompleteOfPhase>100)
            {
                NewCompleteOfPhase = NewCompleteOfPhase - 100;
            }
            oMoonDiskStatus.QuarterStatus = oMoonK.GetPhaseQuarterStatus(NewCompleteOfPhase);
            oMoonDiskStatus.Notches = NotchesMoonDisk;
            oMoonDiskStatus.listGearProperties = mC.ListAllProperties(mC,false, "SavedList.txt");
            oMoonDiskStatus.PrecisionYrs = GetPrecision((double)SynoticPhaseThisMoonDiskMinutes);

            return oMoonDiskStatus;

        }
    }
}
