using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OppaiSharp;
using Microsoft.CSharp;

namespace vfnoshark_pp_calculator
{
    /// <summary>
    /// First time solution. The method checks Window's Mods vars and if they are false just replaces OppaiSharps's Mods with NoMode
    /// </summary>
    public class ModsDefinition
    {
       
      
            public int ModsEnum()
            {
            int a =  0;
            
            MainWindow mainWindow = new MainWindow();
            


            //check the bool state of Vars.Mod and changes useless parameters to Nothing

            
                if (mainWindow.DT_Button.IsChecked == true)
                {                    
                    a += (int)Mods.DoubleTime;
                }
                 if (mainWindow.HR_Button.IsChecked == true)
                {                   
                    a += (int)Mods.Hardrock;
                }
                 if (mainWindow.HD_Button.IsChecked == true)
                {
                    a += (int)Mods.Hidden;

                }
                 if (mainWindow.HT_Button.IsChecked == true)
                {
                    a += (int)Mods.HalfTime;

                }
                 if (mainWindow.FL_Button.IsChecked == true)
                {
                    a += (int)Mods.Flashlight;
                }
                 if (mainWindow.SO_Button.IsChecked == true)
                {                   
                    a += (int)Mods.SpunOut;
                }

                 if (mainWindow.ES_Button.IsChecked == true)
                {
                    a+= (int)Mods.Easy;

                }
                 if (mainWindow.NF_Button.IsChecked == true)
                {
                    a += (int)Mods.NoFail;
                }
                               
            
            return a;
        }                    
    }
}
