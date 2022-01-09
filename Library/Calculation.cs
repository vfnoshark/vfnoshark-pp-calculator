using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OppaiSharp;

namespace Library
{
    public class Calculation
    {
        byte[] data = new WebClient().DownloadData("https://osu.ppy.sh/osu/" + MapIDBox.Text);
        var stream = new MemoryStream(data, false);
        var reader = new StreamReader(stream);

        //read a beatmap
        var beatmap = Beatmap.Read(reader);

        //calculate star ratings for mods
        Mods mods = Mods.Hidden | Mods.DoubleTime | Mods.NoFail | Mods.Easy | Mods.Hardrock | Mods.HalfTime | Mods.NoMod | Mods.SpunOut | Mods.Flashlight;
        var diff = new DiffCalc().Calc(beatmap, mods);
        var combo = Convert.ToInt32(ComboBox.Text);
        var cMiss = Convert.ToInt32(MissBox.Text);
        var accPercent = Convert.ToDouble(AccBox.Text);
        var countObject = beatmap.Objects.Count;
        new Accuracy(accPercent, countObject, cMiss);
        //calculate the PP for a play on this map
        var pp = new PPv2(new PPv2Parameters(beatmap, diff, cMiss, combo, mods: mods));

        PPBox.Text = (string.Format(Convert.ToString(pp.Total)));
    }
}
