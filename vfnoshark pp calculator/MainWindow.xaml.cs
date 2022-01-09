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



namespace vfnoshark_pp_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //globall  vars
        public int missCount { get; set; } = 0;
        public int combo { get; set; } = 0;
        public double acc { get; set; } = 0.00;
        public string mapID { get; set; } = string.Empty;
        public bool hdMod { get; set; } = false;
        public bool dtMod { get; set; } = false;
        public bool hrMod { get; set; } = false;
        public bool flMod { get; set; } = false;
        public bool nfMod { get; set; } = false;
        public bool soMod { get; set; } = false;
        public bool hfMod { get; set; } = false; 
        public bool esMod { get;set; } = false;
        
        public MainWindow()
        {
            InitializeComponent();

        }



        //Events
        private void MissBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            missCount = Convert.ToInt32(missCount.ToString(MapIDBox.Text));
        }

        private void MapIDBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mapID = MapIDBox.Text;
        }

        private void AccuracyBox_TextChanged(object sender, TextChangedEventArgs e) 
        {
            acc = Convert.ToDouble(acc.ToString(AccBox.Text));
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            combo = Convert.ToInt32(ComboBox.Text);
        }

        private void NF_Button_Checked(object sender, RoutedEventArgs e)
        {
            hfMod = true;
        }

        private void HD_Button_Checked(object sender, RoutedEventArgs e)
        {
            hdMod = true;
        }

        private void HR_Button_Checked(object sender, RoutedEventArgs e)
        {
            hrMod = true;
        }

        private void SO_Button_Checked(object sender, RoutedEventArgs e)
        {
            soMod = true;
        }

        private void DT_Button_Checked(object sender, RoutedEventArgs e)
        {
            dtMod = true;
        }

        private void HF_Button_Checked(object sender, RoutedEventArgs e)
        {
            hfMod = true;
        }        
        private void Start_Calc_Click(object sender, RoutedEventArgs e)
        {

        }
    }
       
}
