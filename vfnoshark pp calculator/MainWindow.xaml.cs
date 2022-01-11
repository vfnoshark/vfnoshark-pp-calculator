using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OppaiSharp;
using System.Reflection;



namespace vfnoshark_pp_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Vars
    {
        public int MissCount { get; set; } = 0;
        public int Combo { get; set; } = 0;
        public double Acc { get; set; } = 0.00;
        public string MapID { get; set; } = string.Empty;
        public bool Hidden { get; set; } = false;
        public bool DoubleTime { get; set; } = false;
        public bool HardRock { get; set; } = false;
        public bool FlashLight { get; set; } = false;
        public bool NoFail { get; set; } = false;
        public bool SpunOut { get; set; } = false;
        public bool HalfTime { get; set; } = false;
        public bool Easy { get; set; } = false;
        
        
    }

    public partial class MainWindow : Window
    {
        //global vars
        


        public MainWindow()
        {
            InitializeComponent();

        }



        //Events
        private void MissBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vars = new Vars();
            vars.MissCount = Convert.ToInt32(vars.MissCount.ToString(MapIDBox.Text));
        }

        private void MapIDBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vars = new Vars();
            vars.MapID = MapIDBox.Text;
        }

        private void AccuracyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vars = new Vars();
            vars.Acc = Convert.ToDouble(vars.Acc.ToString(AccBox.Text));
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vars = new Vars();
            vars.Combo = Convert.ToInt32(ComboBox.Text);
        }

        private void NF_Button_Checked(object sender, RoutedEventArgs e)
        {
            var vars = new Vars();
            if (NF_Button.IsChecked == true)
            {
                vars.NoFail = true;
            }
            else vars.NoFail = false;
        }

        private void HD_Button_Checked(object sender, RoutedEventArgs e)
        {
            var vars = new Vars();
            if (HD_Button.IsChecked == true)
            {
                vars.Hidden = true;
            }
            else vars.Hidden = false;
        }

        private void HR_Button_Checked(object sender, RoutedEventArgs e)
        {
            var vars = new Vars();
            if (HR_Button.IsChecked == true)
            {
                vars.HardRock = true;
                ES_Button.IsChecked = false;
            }
            else vars.HardRock = false;
        }

        private void SO_Button_Checked(object sender, RoutedEventArgs e)
        {
            var vars = new Vars();
            if (SO_Button.IsChecked == true)
            {
                vars.SpunOut = true;
            }
            else vars.SpunOut = false;
        }

        private void DT_Button_Checked(object sender, RoutedEventArgs e)
        {
            var vars = new Vars();
            if (DT_Button.IsChecked == true)
            {
                vars.DoubleTime = true;
                HT_Button.IsChecked = false;
            }
            else vars.DoubleTime = false;
           
        }

        private void HT_Button_Checked(object sender, RoutedEventArgs e)
        {
            var vars = new Vars();
            if (HT_Button.IsChecked == true)
            {
                vars.HalfTime = true;
                DT_Button.IsChecked = false;
            }
            else vars.HalfTime = false;
        }
        private void ES_Button_Checked(object sender, RoutedEventArgs e)
        {
            var vars = new Vars();
            if (ES_Button.IsChecked == true)
            {
                vars.Easy = true;
                HR_Button.IsChecked = false;
            }
            else vars.Easy = false;
        }
        private void FL_Button_Checked(object sender, RoutedEventArgs e)
        {
            var vars = new Vars();
            if (FL_Button.IsChecked == true)
            {
                vars.FlashLight = true;
            }
            else vars.FlashLight = false;
        }



        //Start calculation
        private void Start_Calc_Click(object sender, RoutedEventArgs e)
        {
            Vars Vars = new();
            PPBox.Text = Convert.ToString(Calculation.Calc(Vars.MapID, Vars.Acc, Vars.Combo, Vars.MissCount));
        }      
    }


    
    //Calculation script
    public class Calculation
    {

           
       static public double Calc(string mapID, double accPercent, int combo, int missCount)
        {
            Vars Vars = new Vars();
            byte[] data = new WebClient().DownloadData("https://osu.ppy.sh/osu/" + Vars.MapID);
            var stream = new MemoryStream(data, false);
            var reader = new StreamReader(stream);
            List<HitObject> obj = new List<HitObject>();
            int countObjects = obj.Count; 
            //read a beatmap
            var beatmap = Beatmap.Read(reader);
            //accuracy percents to the amount of 100s or 50s
            Accuracy accuracy = new();
            int Count100 = accuracy.Count100;
            int Count50 = accuracy.Count50;
            int mCount = missCount;
            int Count300 = accuracy.Count300;


            void Acc(double accPercent, int countObjects, int countMiss)
            {
                Count50 = 0;
                mCount = Math.Min(countObjects, countMiss);
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
            }
            Acc(accPercent, countObjects, missCount);
           

            //calculate star ratings for slected mods
            Mods mods = 
            Remove(Mods.Hidden) | Remove(Mods.NoFail) | Remove(Mods.Easy) | Remove(Mods.DoubleTime) | Remove(Mods.Hardrock) | Remove(Mods.HalfTime) | Remove(Mods.Flashlight) |Remove(Mods.SpunOut);
            var diff = new DiffCalc().Calc(beatmap, mods);
            Console.WriteLine(string.Format("Star rating: {0:F2} (aim stars: {1:F2}, speed stars: {2:F2})",
                diff.Total, diff.Aim, diff.Speed));

            //calculate the PP for a play on this map
            //the play has no misses or 50's, so we don't specify them
            var pp = new PPv2(new PPv2Parameters(beatmap, diff, c100: Count100, c50: Count50, cMiss: mCount, mods: mods));


            return pp.Total;



            
            OppaiSharp.Mods Remove(Mods mod)
            {
                Vars modBoolean = new Vars();
                bool modBool = false;

                //check the bool state of Vars.Mod and changes useless parameters to NoMode
                switch (mod)
                {
                    case Mods.Easy:
                        modBool = modBoolean.Easy;
                        if (modBool == true)
                        { 
                            return Mods.Easy; 
                        }
                        else if (modBool == false)
                        {
                            return Mods.NoMod;
                        }
                        break;

                    case Mods.SpunOut:
                        modBool = modBoolean.SpunOut;
                        if (modBool == true)
                        {
                            return Mods.SpunOut;
                        }
                        else if(modBool == false)
                        {
                            return Mods.NoMod;
                        }
                        break;

                    case Mods.DoubleTime:
                        modBool = modBoolean.DoubleTime;
                        if(modBool == true)
                        {
                            return Mods.DoubleTime;
                        }
                        else if (modBool== false)
                        {
                            return Mods.NoMod;
                        }
                        break;

                    case Mods.Hidden:
                        modBool = modBoolean.Hidden;
                        if (modBool == true)
                        {
                            return Mods.Hidden;
                        }
                        else if (modBool == false)
                        {
                            return Mods.NoMod;
                        }
                        break;

                    case Mods.Flashlight:
                        modBool = modBoolean.FlashLight;
                        if (modBool == true)
                        {
                            return Mods.Flashlight;
                        }
                        else if (modBool == false)
                        {
                            return Mods.NoMod;
                        }
                        break;

                    case Mods.Hardrock:
                        modBool = modBoolean.HardRock;
                        if (modBool == true)
                        {
                            return Mods.Hardrock;
                        }
                        else if (modBool == false)
                        {
                            return Mods.NoMod;
                        }
                        break;

                    case Mods.HalfTime:
                        modBool = modBoolean.HalfTime;
                        if (modBool == true)
                        {
                            return Mods.HalfTime;
                        }
                        else if (modBool == false)
                        {
                            return Mods.NoMod;
                        }
                        break;

                    case Mods.NoFail:
                        modBool = modBoolean.NoFail;
                        if (modBool == true)
                        {
                            return Mods.NoFail;
                        }
                        else if (modBool == false)
                        {
                            return Mods.NoMod;
                        }
                        break;
                }
                return mod;
            }
        
        }
    }
}
