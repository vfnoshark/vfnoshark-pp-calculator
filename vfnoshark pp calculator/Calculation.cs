using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OppaiSharp;

namespace vfnoshark_pp_calculator
{
    public class Calculation
    {
        public double Calc(string mapID, int Count100, int combo, int missCount)
        {





            byte[] data = new WebClient().DownloadData("https://osu.ppy.sh/osu/" + mapID);
            var stream = new MemoryStream(data, false);
            var reader = new StreamReader(stream);

            //read a beatmap
            var beatmap = Beatmap.Read(reader);
            ModsDefinition Def = new ModsDefinition();
            //calculate star ratings for HDDT
            Mods mods = (Mods)Def.ModsEnum();
            var diff = new DiffCalc().Calc(beatmap, mods);
         

            //calculate the PP for a play on this map
            //the play has no misses or 50's, so we don't specify them
            var pp = new PPv2(new PPv2Parameters(beatmap, diff, c100: Count100, cMiss: missCount, combo:combo, mods: mods));

            return pp.Total;



            /*void Acc(double accPercent, int countObjects, int countMiss)
            {
                new Accuracy();

                Count50 = 0;
                missCount = Math.Min(countObjects, countMiss);
                int max300 = countObjects - countMiss;

                double maxAcc = new Accuracy(max300, 0, 0, countMiss).Value() * 100.0;

                accPercent = Math.Max(0.0, Math.Min(maxAcc, accPercent));

                //just some black magic maths from wolfram alpha
                Count100 = (int)Math.Round(-3.0 * ((accPercent * 0.01 - 1.0) * countObjects + countMiss) * 0.5);

                if (Count100 > max300)
                {
                    //acc lower than all 100s, use 50s
                    Count100 = 0;
                    Count50 = (int)Math.Round(-6.0 * ((accPercent * 0.01 - 1.0) * countObjects + countMiss) * 0.5);
                    Count50 = Math.Min(max300, Count50);
                }

                Count300 = countObjects - Count100 - Count50 - countMiss;
            }*/
        }
    }            
}

