using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch.movement
{
    class movementCase 
    {
        private List<Watch.gears.BaseGear> baseGears = new List<Watch.gears.BaseGear>();
        public List<Watch.gears.BaseGear> BaseGears { get { return baseGears; } set { baseGears = value; } }

        public void incNotches(ref movementCase oC, int iNotchInc)
        {
            //MoveGears(ref oC, iNotchInc);
            MoveGears(ref oC, iNotchInc,supporting._SpinMode.FastAndDirty);
        }


        private void MoveGears(ref movementCase oC, int Notches, movement.supporting._SpinMode sM)
        {
            switch (sM)
            {
                case movement.supporting._SpinMode.FastAndDirty:
                    { // move each gear X times, unrealistic, but fast and accurate
                        MoveGears(ref oC, Notches);
                        return;
                    }
                case movement.supporting._SpinMode.Purist:
                    { // move each gear 1 notch at a time, slow and accurate
                        if (Notches>1)
                        {
                            int iCnt;
                            for (iCnt =1; iCnt<=Notches; iCnt++)
                            {
                                MoveGears(ref oC, 1);
                            }
                        }
                        return;
                    }
            }
        }

        private void MoveGears(ref movementCase oC, int Notches)
        {
            gears.InnerGear Inner = new gears.InnerGear();
            gears.BaseGear LastbaseG = new gears.BaseGear();
            gears.InnerGear LastInnerG = new gears.InnerGear();

            foreach (gears.BaseGear baseG in oC.baseGears)
            {
                String GearType = baseG.GetType().ToString();
                switch (GearType)
                {
                    case "Watch.gears.BaseGear":
                        {
                            baseG.BaseMove(LastbaseG, LastInnerG, Notches);
                            LastbaseG = baseG;
                            break;
                        }
                    case "Watch.gears.InnerGear":
                        {
                            Inner = (gears.InnerGear)baseG;
                            Inner.InnerMove(LastbaseG, LastInnerG, Notches);
                            LastInnerG = Inner;
                            break;
                        }
                }
            }
            return;
        }

        public double GetFinalGearPosition(movementCase oC)
        {
            double FinalGearPosition = 0;
            int i = oC.baseGears.Count()-1;
            switch (oC.baseGears[i].GetType().ToString())
            {
                case "Watch.gears.BaseGear":
                    {
                        FinalGearPosition = oC.baseGears[i].BaseTeethNotchesTraversed;
                        break;
                    }
                case "Watch.gears.InnerGear":
                    {
                        Watch.gears.InnerGear oInner = (Watch.gears.InnerGear)oC.baseGears[i];
                        FinalGearPosition = oInner.BaseTeethNotchesTraversed;
                        break;
                    }

            }
            return FinalGearPosition;
        }

        public List<string> ListAllProperties(movementCase oC, bool toFile, string Fln)
        {
            List<string> listProperties = new List<string>();

            foreach (gears.BaseGear baseG in oC.baseGears)
            {

                switch (baseG.GetType().ToString())
                {
                    case "Watch.gears.BaseGear":
                        {
                            listProperties.Add(" >>>>>>>>>>> BaseGear <<<<<<<<<<<");
                            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(baseG))
                            {
                                string name = descriptor.Name;
                                object value = descriptor.GetValue(baseG);
                                listProperties.Add(name + "=" + value);
                            }
                            continue;
                        }
                    case "Watch.gears.InnerGear":
                        {
                            listProperties.Add(" >>>>>>>>>>> InnerGear <<<<<<<<<<<");
                            Watch.gears.InnerGear oInner = (Watch.gears.InnerGear)baseG;
                            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(oInner))
                            {
                                string name = descriptor.Name;
                                object value = descriptor.GetValue(oInner);
                                listProperties.Add(name + "=" + value);
                            }
                            continue;
                        }
                }
            }


            if (toFile)
            {
                TextWriter tw = new StreamWriter(Fln);
                foreach (String s in listProperties)
                    tw.WriteLine(s);
                tw.Close();
                //Console.Write("saved to filename:" + Fln);
                //Console.Read();

            }
            return listProperties;
        }


    }
}
