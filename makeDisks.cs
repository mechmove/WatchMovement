using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Watch.movement
{
    class makeDisks
    {

        public movementCase MakeMoonDisk (string sMoonDiskConfig) {

            Watch.movement.movementCase oMovementCase = new Watch.movement.movementCase();

            string GearType=string.Empty;
            int GearID;
            string line=string.Empty;

            using (StreamReader reader = new StreamReader(sMoonDiskConfig))
            {
                while (!reader.EndOfStream) {
                line = reader.ReadLine(); 
                GearType= line.Substring(line.IndexOf("=",0)+1);
                line = reader.ReadLine(); 
                GearID= Convert.ToInt16(line.Substring(line.IndexOf("=",0)+1));
                switch (GearType)
                {
                    case "Base":
                        {
                            Watch.gears.BaseGear Base = new Watch.gears.BaseGear();
                            line = reader.ReadLine(); 
                            Base.GearName = line.Substring(line.IndexOf("=",0)+1);

                            line = reader.ReadLine(); 
                            string DiskBaseUnitDriver = line.Substring(line.IndexOf("=",0)+1);
                            if (DiskBaseUnitDriver.Equals("hourly")) {
                                Base.DiskBaseUnitDriver = gears.BaseGear._DiskBaseUnitDriver.hourly;
                                } else if (DiskBaseUnitDriver.Equals("daily")) {Base.DiskBaseUnitDriver= gears.BaseGear._DiskBaseUnitDriver.daily;}

                            Base.GearID = GearID;
                            line = reader.ReadLine(); 
                            string Direction= line.Substring(line.IndexOf("=",0)+1);

                            if (Direction.Equals("Clockwise")) {
                                Base.Rotation = gears.BaseGear._Rotation.Clockwise;
                                } else if (Direction.Equals("AntiClockwise")) {Base.Rotation = gears.BaseGear._Rotation.AntiClockwise;}

                            line = reader.ReadLine(); 
                            Base.BaseTeeth = Convert.ToInt16(line.Substring(line.IndexOf("=",0)+1));
                            line = reader.ReadLine(); 
                            Base.BaseTeethDrivenBy = Convert.ToBoolean(line.Substring(line.IndexOf("=",0)+1));
                            line = reader.ReadLine(); 
                            if (Base.BaseTeethDrivenBy) {
                                Base.BaseTeethDrivenByGearID = Convert.ToInt16(line.Substring(line.IndexOf("=",0)+1));
                                }
                            line = reader.ReadLine(); 
                            Base.BaseTeethDriving =  Convert.ToBoolean(line.Substring(line.IndexOf("=",0)+1));
                            line = reader.ReadLine(); 

                            if (Base.BaseTeethDriving) {
                                Base.BaseTeethDrivingGearID = Convert.ToInt16(line.Substring(line.IndexOf("=",0)+1));
                                }
                            oMovementCase.BaseGears.Add(Base);
                            break;
                        }
                    case "InnerGear":
                        {
                            Watch.gears.InnerGear Inner = new Watch.gears.InnerGear();
                            line = reader.ReadLine(); 
                            Inner.GearName = line.Substring(line.IndexOf("=",0)+1);

                            line = reader.ReadLine(); 
                            string DiskBaseUnitDriver = line.Substring(line.IndexOf("=",0)+1);
                            if (DiskBaseUnitDriver.Equals("hourly")) {
                                Inner.DiskBaseUnitDriver = gears.BaseGear._DiskBaseUnitDriver.hourly;
                                } else if (DiskBaseUnitDriver.Equals("daily")) {Inner.DiskBaseUnitDriver= gears.BaseGear._DiskBaseUnitDriver.daily;}


                            Inner.GearID = GearID;
                            line = reader.ReadLine(); 
                            string Direction= line.Substring(line.IndexOf("=",0)+1);
                            if (Direction.Equals("Clockwise")) {
                                Inner.Rotation = gears.BaseGear._Rotation.Clockwise;
                                } else if (Direction.Equals("AntiClockwise")) {Inner.Rotation = gears.BaseGear._Rotation.AntiClockwise;}
                            
                            line = reader.ReadLine(); 
                            Inner.BaseTeeth = Convert.ToInt16(line.Substring(line.IndexOf("=",0)+1));
                            line = reader.ReadLine(); 
                            Inner.InnerTeeth= Convert.ToInt16(line.Substring(line.IndexOf("=",0)+1));
                            line = reader.ReadLine(); 
                            Inner.BaseTeethDrivenBy = Convert.ToBoolean(line.Substring(line.IndexOf("=",0)+1));
                            line = reader.ReadLine(); 
                            if (Inner.BaseTeethDrivenBy) {
                                Inner.BaseTeethDrivenByGearID = Convert.ToInt16(line.Substring(line.IndexOf("=",0)+1));
                                }

                            line = reader.ReadLine(); 
                            Inner.BaseTeethDriving = Convert.ToBoolean(line.Substring(line.IndexOf("=",0)+1));
                            line = reader.ReadLine(); 
                            if (Inner.BaseTeethDriving) {
                                Inner.BaseTeethDrivingGearID = Convert.ToInt16(line.Substring(line.IndexOf("=",0)+1));
                                }

                            line = reader.ReadLine(); 
                            Inner.InnerTeethDriving =  Convert.ToBoolean(line.Substring(line.IndexOf("=",0)+1));
                            line = reader.ReadLine(); 
                            if (Inner.InnerTeethDriving) {
                                Inner.InnerTeethDrivingGearID= Convert.ToInt16(line.Substring(line.IndexOf("=",0)+1));
                                }

                            line = reader.ReadLine(); 
                            Inner.InnerTeethDrivenBy =  Convert.ToBoolean(line.Substring(line.IndexOf("=",0)+1));
                            line = reader.ReadLine(); 
                            if (Inner.InnerTeethDrivenBy) {
                                Inner.InnerTeethDrivenByGearID= Convert.ToInt16(line.Substring(line.IndexOf("=",0)+1));
                                }

                            oMovementCase.BaseGears.Add(Inner);
                            break;
                        }
                }
                }
            }

            return oMovementCase;
        }

        public movementCase MakeMoonDisk90Daily()
        {
            // create each gear and assign name first
            Watch.gears.BaseGear BaseDailyDriver = new Watch.gears.BaseGear();
            BaseDailyDriver.GearName = "BaseDailyDriver";
            BaseDailyDriver.GearID = 1;
            BaseDailyDriver.Rotation = gears.BaseGear._Rotation.AntiClockwise;
            BaseDailyDriver.DiskBaseUnitDriver = gears.BaseGear._DiskBaseUnitDriver.daily;

            Watch.gears.InnerGear DailyDisk = new Watch.gears.InnerGear();
            DailyDisk.GearName = "DailyDriver";
            DailyDisk.GearID = 2;

            Watch.gears.InnerGear Intermediatetepper = new Watch.gears.InnerGear();
            Intermediatetepper.GearName = "Intermediatetepper";
            Intermediatetepper.GearID = 3;

            Watch.gears.BaseGear MoonDisk = new Watch.gears.BaseGear();
            MoonDisk.GearName = "MoonDisk";
            MoonDisk.GearID = 4;

            // now create the properties of each gear and their relationships to each other

            BaseDailyDriver.BaseTeeth = 24;
            BaseDailyDriver.BaseTeethDriving = true;
            BaseDailyDriver.BaseTeethDrivingGearID = DailyDisk.GearID;

            DailyDisk.InnerTeeth = 7;
            DailyDisk.BaseTeeth = 30;
            DailyDisk.InnerTeethDrivenBy = true;
            DailyDisk.InnerTeethDrivenByGearID = BaseDailyDriver.GearID;
            DailyDisk.BaseTeethDriving = true;
            DailyDisk.BaseTeethDrivingGearID = Intermediatetepper.GearID;

            Intermediatetepper.BaseTeeth = 90;
            Intermediatetepper.InnerTeeth = 32;
            Intermediatetepper.BaseTeethDrivenBy = true;
            Intermediatetepper.BaseTeethDrivenByGearID = DailyDisk.GearID;
            Intermediatetepper.InnerTeethDriving = true;
            Intermediatetepper.InnerTeethDrivingGearID = MoonDisk.GearID;

            MoonDisk.BaseTeeth = 90;
            MoonDisk.BaseTeethDrivenBy = true;
            MoonDisk.BaseTeethDrivenByGearID = Intermediatetepper.GearID;

            // create the movement
            Watch.movement.movementCase oMovementCase = new Watch.movement.movementCase();
            oMovementCase.BaseGears.Add(BaseDailyDriver);
            oMovementCase.BaseGears.Add(DailyDisk);
            oMovementCase.BaseGears.Add(Intermediatetepper);
            oMovementCase.BaseGears.Add(MoonDisk);

            return oMovementCase;
        }

        public movementCase MakeMoonDisk59Daily()
        {
            // create each gear and assign name first
            Watch.gears.BaseGear DailyDriver = new Watch.gears.BaseGear();
            DailyDriver.GearName = "DailyDriver";
            DailyDriver.GearID = 1;
            DailyDriver.Rotation = gears.BaseGear._Rotation.AntiClockwise;
            DailyDriver.DiskBaseUnitDriver = gears.BaseGear._DiskBaseUnitDriver.daily;

            Watch.gears.BaseGear MoonDisk = new Watch.gears.BaseGear();
            MoonDisk.GearName = "MoonDisk";
            MoonDisk.GearID = 2;

            // now create the properties of each gear and their relationships to each other
            DailyDriver.BaseTeeth = 12;
            DailyDriver.BaseTeethDriving = true;
            DailyDriver.BaseTeethDrivingGearID = MoonDisk.GearID;

            MoonDisk.BaseTeeth = 59;
            MoonDisk.BaseTeethDrivenBy = true;
            MoonDisk.BaseTeethDrivenByGearID = DailyDriver.GearID;

            // create the movement
            Watch.movement.movementCase oMovementCase = new Watch.movement.movementCase();
            oMovementCase.BaseGears.Add(DailyDriver);
            oMovementCase.BaseGears.Add(MoonDisk);

            return oMovementCase;
        }

        public movementCase MakeMoonDisk135Hourly()
        {
            // create each gear and assign name first
            Watch.gears.BaseGear HourDisk = new Watch.gears.BaseGear();
            HourDisk.GearName = "HourlyDriver";
            HourDisk.GearID = 1;
            HourDisk.Rotation = gears.BaseGear._Rotation.Clockwise;

            Watch.gears.InnerGear Stepper = new Watch.gears.InnerGear();
            Stepper.GearName = "MoonDiskDriver";
            Stepper.GearID = 2;

            Watch.gears.BaseGear Intermediate1 = new Watch.gears.BaseGear();
            Intermediate1.GearName = "Intermediate1";
            Intermediate1.GearID = 3;


            Watch.gears.BaseGear Intermediate2 = new Watch.gears.BaseGear();
            Intermediate2.GearName = "Intermediate2";
            Intermediate2.GearID = 4;

            Watch.gears.BaseGear MoonDisk = new Watch.gears.BaseGear();
            MoonDisk.GearName = "MoonDisk";
            MoonDisk.GearID = 5;

            // now create the properties of each gear and their relationships to each other
            HourDisk.BaseTeeth = 12;
            HourDisk.BaseTeethDriving = true;
            HourDisk.BaseTeethDrivingGearID = Stepper.GearID;

            Stepper.BaseTeeth = 105;
            Stepper.InnerTeeth = 10;
            Stepper.BaseTeethDrivenBy = true;
            Stepper.BaseTeethDrivenByGearID = HourDisk.GearID;
            Stepper.InnerTeethDriving = true;
            Stepper.InnerTeethDrivingGearID = Intermediate1.GearID;

            Intermediate1.BaseTeeth = 50;
            Intermediate1.BaseTeethDrivenBy = true;
            Intermediate1.BaseTeethDrivenByGearID = Stepper.GearID;
            Intermediate1.BaseTeethDriving = true;
            Intermediate1.BaseTeethDrivingGearID = Intermediate2.GearID;

            Intermediate2.BaseTeeth = 90;
            Intermediate2.BaseTeethDrivenBy = true;
            Intermediate2.BaseTeethDrivenByGearID = Intermediate1.GearID;
            Intermediate2.BaseTeethDriving = true;
            Intermediate2.BaseTeethDrivingGearID = MoonDisk.GearID;

            MoonDisk.BaseTeeth = 135;
            MoonDisk.BaseTeethDrivenBy = true;
            MoonDisk.BaseTeethDrivenByGearID = Intermediate2.GearID;

            // create the movement
            Watch.movement.movementCase oMovementCase = new Watch.movement.movementCase();
            oMovementCase.BaseGears.Add(HourDisk);
            oMovementCase.BaseGears.Add(Stepper);
            oMovementCase.BaseGears.Add(Intermediate1);
            oMovementCase.BaseGears.Add(Intermediate2);
            oMovementCase.BaseGears.Add(MoonDisk);

            return oMovementCase;
        }
        public movementCase TestDiskConfiguration7()
        {
            // create each gear and assign name first
            Watch.gears.BaseGear Disk1 = new Watch.gears.BaseGear();
            Disk1.GearName = "Disk1";
            Disk1.GearID = 1;
            Disk1.Rotation = gears.BaseGear._Rotation.Clockwise;

            Watch.gears.InnerGear Disk2 = new Watch.gears.InnerGear();
            Disk2.GearName = "Disk2";
            Disk2.GearID = 2;


            Disk1.BaseTeeth = 50;
            Disk1.BaseTeethDriving = true;
            Disk1.BaseTeethDrivingGearID = Disk2.GearID;


            Disk2.BaseTeeth = 50;
            Disk2.InnerTeeth = 15;
            Disk2.BaseTeethDrivenBy = true;
            Disk2.BaseTeethDrivenByGearID = Disk1.GearID;

            // create the movement
            Watch.movement.movementCase oMovementCase = new Watch.movement.movementCase();
            oMovementCase.BaseGears.Add(Disk1);
            oMovementCase.BaseGears.Add(Disk2);

            // now get the movement working
            oMovementCase.incNotches(ref oMovementCase, 1);
            return oMovementCase;
        }

        public movementCase TestDiskConfiguration6()
        {
            // create each gear and assign name first
            Watch.gears.InnerGear Disk1 = new Watch.gears.InnerGear();
            Disk1.GearName = "Disk1";
            Disk1.GearID = 1;
            Disk1.Rotation = gears.BaseGear._Rotation.Clockwise;

            Watch.gears.BaseGear Disk2 = new Watch.gears.BaseGear();
            Disk2.GearName = "Disk2";
            Disk2.GearID = 2;

            Disk1.BaseTeeth = 50;
            Disk1.InnerTeeth = 15;
            Disk1.InnerTeethDriving = true;
            Disk1.InnerTeethDrivingGearID = Disk2.GearID;

            Disk2.BaseTeeth = 50;
            Disk2.BaseTeethDrivenBy = true;
            Disk2.BaseTeethDrivenByGearID = Disk1.GearID;


            // create the movement
            Watch.movement.movementCase oMovementCase = new Watch.movement.movementCase();
            oMovementCase.BaseGears.Add(Disk1);
            oMovementCase.BaseGears.Add(Disk2);

            // now get the movement working
            oMovementCase.incNotches(ref oMovementCase, 1);
            return oMovementCase;
        }

        public movementCase TestDiskConfiguration5()
        {
            // create each gear and assign name first
            Watch.gears.InnerGear Disk1 = new Watch.gears.InnerGear();
            Disk1.GearName = "Disk1";
            Disk1.GearID = 1;
            Disk1.Rotation = gears.BaseGear._Rotation.Clockwise;

            Watch.gears.BaseGear Disk2 = new Watch.gears.BaseGear();
            Disk2.GearName = "Disk2";
            Disk2.GearID = 2;

            Disk1.BaseTeeth = 50;
            Disk1.InnerTeeth = 15;
            Disk1.BaseTeethDriving = true;
            Disk1.BaseTeethDrivingGearID = Disk2.GearID;

            Disk2.BaseTeeth = 50;
            Disk2.BaseTeethDrivenBy = true;
            Disk2.BaseTeethDrivenByGearID = Disk1.GearID;


            // create the movement
            Watch.movement.movementCase oMovementCase = new Watch.movement.movementCase();
            oMovementCase.BaseGears.Add(Disk1);
            oMovementCase.BaseGears.Add(Disk2);

            // now get the movement working
            oMovementCase.incNotches(ref oMovementCase, 1);
            return oMovementCase;
        }


        public movementCase TestDiskConfiguration4()
        {
            // create each gear and assign name first
            Watch.gears.InnerGear Disk1 = new Watch.gears.InnerGear();
            Disk1.GearName = "Disk1";
            Disk1.GearID = 1;
            Disk1.Rotation = gears.BaseGear._Rotation.Clockwise;

            Watch.gears.InnerGear Disk2 = new Watch.gears.InnerGear();
            Disk2.GearName = "Disk2";
            Disk2.GearID = 2;

            Disk1.BaseTeeth = 50;
            Disk1.InnerTeeth = 15;
            Disk1.InnerTeethDriving = true;
            Disk1.InnerTeethDrivingGearID = Disk2.GearID;

            Disk2.BaseTeeth = 50;
            Disk2.InnerTeeth = 15;
            Disk2.BaseTeethDrivenBy = true;
            Disk2.BaseTeethDrivenByGearID = Disk1.GearID;

            // create the movement
            Watch.movement.movementCase oMovementCase = new Watch.movement.movementCase();
            oMovementCase.BaseGears.Add(Disk1);
            oMovementCase.BaseGears.Add(Disk2);

            // now get the movement working
            oMovementCase.incNotches(ref oMovementCase, 1);
            return oMovementCase;
        }



        public movementCase TestDiskConfiguration3()
        {
            // create each gear and assign name first
            Watch.gears.InnerGear Disk1 = new Watch.gears.InnerGear();
            Disk1.GearName = "Disk1";
            Disk1.GearID = 1;
            Disk1.Rotation = gears.BaseGear._Rotation.Clockwise;

            Watch.gears.InnerGear Disk2 = new Watch.gears.InnerGear();
            Disk2.GearName = "Disk2";
            Disk2.GearID = 2;

            Disk1.BaseTeeth = 50;
            Disk1.InnerTeeth = 15;
            Disk1.BaseTeethDriving = true;
            Disk1.BaseTeethDrivingGearID = Disk2.GearID;

            Disk2.BaseTeeth = 50;
            Disk2.InnerTeeth = 15;
            Disk2.BaseTeethDrivenBy = true;
            Disk2.BaseTeethDrivenByGearID = Disk1.GearID;

            // create the movement
            Watch.movement.movementCase oMovementCase = new Watch.movement.movementCase();
            oMovementCase.BaseGears.Add(Disk1);
            oMovementCase.BaseGears.Add(Disk2);

            // now get the movement working
            oMovementCase.incNotches(ref oMovementCase, 1);
            return oMovementCase;
        }


        public movementCase TestDiskConfiguration2()
        {
            // create each gear and assign name first
            Watch.gears.BaseGear Disk1 = new Watch.gears.BaseGear();
            Disk1.GearName = "Disk1";
            Disk1.GearID = 1;
            Disk1.Rotation = gears.BaseGear._Rotation.Clockwise;

            Watch.gears.InnerGear Disk2 = new Watch.gears.InnerGear();
            Disk2.GearName = "Disk2";
            Disk2.GearID = 2;

            Disk1.BaseTeeth = 30;
            Disk1.BaseTeethDriving = true;
            Disk1.BaseTeethDrivingGearID = Disk2.GearID;

            Disk2.BaseTeeth = 50;
            Disk2.InnerTeeth = 15;
            Disk2.InnerTeethDrivenBy = true;
            Disk2.InnerTeethDrivenByGearID = Disk1.GearID;

            // create the movement
            Watch.movement.movementCase oMovementCase = new Watch.movement.movementCase();
            oMovementCase.BaseGears.Add(Disk1);
            oMovementCase.BaseGears.Add(Disk2);

            // now get the movement working
            oMovementCase.incNotches(ref oMovementCase, 1);
            return oMovementCase;
        }



        public movementCase TestDiskConfiguration1()
        {
            // create each gear and assign name first
            Watch.gears.BaseGear Disk1 = new Watch.gears.BaseGear();
            Disk1.GearName = "Disk1";
            Disk1.GearID = 1;
            Disk1.Rotation = gears.BaseGear._Rotation.Clockwise;

            Watch.gears.BaseGear Disk2 = new Watch.gears.BaseGear();
            Disk2.GearName = "Disk2";
            Disk2.GearID = 2;

            Disk1.BaseTeeth = 30;
            Disk1.BaseTeethDriving = true;
            Disk1.BaseTeethDrivingGearID = Disk2.GearID;

            Disk2.BaseTeeth = 50;
            Disk2.BaseTeethDrivenBy = true;
            Disk2.BaseTeethDrivenByGearID = Disk1.GearID;

            // create the movement
            Watch.movement.movementCase oMovementCase = new Watch.movement.movementCase();
            oMovementCase.BaseGears.Add(Disk1);
            oMovementCase.BaseGears.Add(Disk2);

            // now get the movement working
            oMovementCase.incNotches(ref oMovementCase, 1);
            return oMovementCase;
        }


    }
}
