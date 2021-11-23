using System;
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

namespace ImageScreener
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SettingDialogWindow : Window
    {
        public SettingDialogWindow()
        {
            InitializeComponent();

            jpg.IsChecked = Properties.Settings.Default.jpg;
            jpeg.IsChecked = Properties.Settings.Default.jpeg;
            png.IsChecked = Properties.Settings.Default.png;
            gif.IsChecked = Properties.Settings.Default.gif;

            mpg.IsChecked = Properties.Settings.Default.mpg;
            mpeg.IsChecked = Properties.Settings.Default.mpeg;
            mp4.IsChecked = Properties.Settings.Default.mp4;
            webm.IsChecked = Properties.Settings.Default.webm;
        }

        private void Jpg_Click(object sender, System.EventArgs e)  
        {
            Properties.Settings.Default.jpg = (bool)(((CheckBox)sender).IsChecked);
            Properties.Settings.Default.Save();
        }

        private void Jpeg_Click(object sender, System.EventArgs e)  
        {
            Properties.Settings.Default.jpeg = (bool)(((CheckBox)sender).IsChecked);
            Properties.Settings.Default.Save();
        }

        private void Png_Click(object sender, System.EventArgs e)  
        {
            Properties.Settings.Default.png = (bool)(((CheckBox)sender).IsChecked);
            Properties.Settings.Default.Save();
        }

        private void Gif_Click(object sender, System.EventArgs e)  
        {
            Properties.Settings.Default.gif = (bool)(((CheckBox)sender).IsChecked);
            Properties.Settings.Default.Save();
        }

        private void Mpg_Click(object sender, System.EventArgs e)  
        {
            Properties.Settings.Default.mpg = (bool)(((CheckBox)sender).IsChecked);
            Properties.Settings.Default.Save();
        }

        private void Mpeg_Click(object sender, System.EventArgs e)  
        {
            Properties.Settings.Default.mpeg = (bool)(((CheckBox)sender).IsChecked);
            Properties.Settings.Default.Save();
        }

        private void Mp4_Click(object sender, System.EventArgs e)  
        {
            Properties.Settings.Default.mp4 = (bool)(((CheckBox)sender).IsChecked);
            Properties.Settings.Default.Save();
        }

        private void Webm_Click(object sender, System.EventArgs e)  
        {
            Properties.Settings.Default.webm = (bool)(((CheckBox)sender).IsChecked);
            Properties.Settings.Default.Save();
        }
        protected virtual void SettingDialogWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
