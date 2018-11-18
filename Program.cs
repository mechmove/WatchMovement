using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch
{
    class Program
    {
        static void Main(string[] args)
        {

            Watch.Program oMakeMoonDisk = new Watch.Program();
            oMakeMoonDisk.MakeCompleteMoonDisk();
            // Test configurations 1 - 7 all combinations of single and compound gears
            //movement.makeDisks oMoonK = new movement.makeDisks();
            //movement.movementCase oCase = oMoonK.TestDiskConfiguration5();
        }

        public void MakeCompleteMoonDisk()
        {
            movement.supporting oMoonK = new movement.supporting();
            movement.MoonRealStatus oMoonRealStatus = new movement.MoonRealStatus();
            movement.makeDisks mD = new movement.makeDisks();
            movement.movementCase mC = new movement.movementCase();
            movement.MoonDiskStatus oMoonDiskStatus = new movement.MoonDiskStatus();

            double NotchesMoonDisk = 0;
            double SynoticPhaseThisMoonDiskMinutes = 0;

            DateTime dT = DateTime.Now;
            //DateTime dT = Convert.ToDateTime("Dec 22, 2018 9:48 am");
            //DateTime dT = Convert.ToDateTime("June 13, 2018 12:00 am");
            //DateTime dT = Convert.ToDateTime("Dec. 27, 2118 05:08 PM");
            //DateTime dT = Convert.ToDateTime("Dec. 26, 2140 12:00 AM");
            DateTime findPhaseUTC = dT.ToUniversalTime();
            TimeSpan ts = findPhaseUTC.Subtract(oMoonK.KnownFullMoon_UTC_used);

            // first, get the real moonphase:
            oMoonRealStatus = oMoonK.GetRealMoonPhase(oMoonK.SynoticPhase_Minutes_Real, ts, findPhaseUTC);
            
            //mC = mD.MakeMoonDisk("MakeMoonDisk135Hourly.txt");        // get moonphase for 135 notch disk
            //mC = mD.MakeMoonDisk("MakeMoonDisk90Daily.txt");          // get moonphase for 90 notch disk
            mC = mD.MakeMoonDisk("MakeMoonDisk59Daily.txt");            // get moonphase for 59 notch disk
            NotchesMoonDisk = oMoonK.GetFinalDiskNotches(mC);

            SynoticPhaseThisMoonDiskMinutes = oMoonK.CalcSynoticPhaseThisMoonDiskMinutes(NotchesMoonDisk, 
                mC, 
                movement.supporting._BaseDriverTransmission.smooth, 
                movement.supporting._BaseDriverStepMethod.runUnder);
            
            switch (mC.BaseGears[0].DiskBaseUnitDriver)
            {// now get the movement working
                case gears.BaseGear._DiskBaseUnitDriver.hourly:
                    mC.incNotches(ref mC, (int)Math.Floor(ts.TotalHours)); 
                    break;
                case gears.BaseGear._DiskBaseUnitDriver.daily:
                    mC.incNotches(ref mC, (int)Math.Floor(ts.TotalDays)); 
                    break;
            }
            // get all final moon phase parameters for type of movement and accuracy
            oMoonDiskStatus = oMoonK.RunMoonModule(mC, NotchesMoonDisk, SynoticPhaseThisMoonDiskMinutes, ts, findPhaseUTC, oMoonRealStatus.LunationsElapsed);
        }
    }
}
