using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using OppaiSharp;
using System.Windows;
using System.Windows.Controls;

namespace vfnoshark_pp_calculator
{
    public partial class Calculation
    {
        public void Calc()
        {
            byte[] data = new WebClient().DownloadData("https://osu.ppy.sh/osu/");
            var stream = new MemoryStream(data, false);
            var reader = new StreamReader(stream);

            //read a beatmap
            var beatmap = Beatmap.Read(reader);

            //calculate star ratings for HDDT
            Mods mods = Mods.Hidden | Mods.DoubleTime;
            var diff = new DiffCalc().Calc(beatmap, mods);
            Console.WriteLine(string.Format("Star rating: {0:F2} (aim stars: {1:F2}, speed stars: {2:F2})",
                diff.Total, diff.Aim, diff.Speed));

            //calculate the PP for a play on this map
            //the play has no misses or 50's, so we don't specify them
            var pp = new PPv2(new PPv2Parameters(beatmap, diff, c100: 8, mods: mods));

            Console.WriteLine(string.Format("Play is worth {0:F2}pp ({1:F2} aim pp, {2:F2} acc pp, {3:F2} speed pp) " +
                                            "and has an accuracy of {4:F2}%",
                pp.Total, pp.Aim, pp.Acc, pp.Speed, pp.ComputedAccuracy.Value() * 100));
        }
       

    }
}
