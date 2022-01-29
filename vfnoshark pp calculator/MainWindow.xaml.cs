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
        public int MissCount  = 0;
        public int Combo  = 0;
        public int Hundreds  = 0;
        public string MapID  = string.Empty;


        
        
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
            vars.MissCount = Convert.ToInt32(MissBox.Text);
        }

        private void MapIDBox_TextChanged(object sender, TextChangedEventArgs e)
        {
             
            var vars = new Vars();
            vars.MapID = MapIDBox.Text;
        }

        private void AccuracyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vars = new Vars();
            bool parse = false;
            while(parse == false)
            {
                if ((Int32.TryParse(AccBox.Text, out int acc)) == true)
                {
                    vars.Hundreds = acc;
                    parse = true;
                }
            }
           
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vars = new Vars();
            vars.Combo = Convert.ToInt32(ComboBox.Text);
        }

        private void NF_Button_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void HD_Button_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void HR_Button_Checked(object sender, RoutedEventArgs e)
        {

            if (HR_Button.IsChecked == true)
            {
                ES_Button.IsChecked = false;
            }
           
        }

        private void SO_Button_Checked(object sender, RoutedEventArgs e)
        {
           
        }

        private void DT_Button_Checked(object sender, RoutedEventArgs e)
        {

            if (DT_Button.IsChecked == true)
            {
                HT_Button.IsChecked = false;
            }

           
        }

        private void HT_Button_Checked(object sender, RoutedEventArgs e)
        {

            if (HT_Button.IsChecked == true)
            {
          
                DT_Button.IsChecked = false;
            }
            
        }
        private void ES_Button_Checked(object sender, RoutedEventArgs e)
        {

            if (ES_Button.IsChecked == true)
            {
 
                HR_Button.IsChecked = false;
            }
        }
        private void FL_Button_Checked(object sender, RoutedEventArgs e)
        {

        }



        //Start calculation
        private void Start_Calc_Click(object sender, RoutedEventArgs e)
        {
            Vars Vars = new();
            Calculation calculation = new Calculation();
            PPBox.Text = Convert.ToString(calculation.Calc(Vars.MapID, Vars.Hundreds, Vars.Combo, Vars.MissCount));
        }

        //Parses 
       
    }     
}
