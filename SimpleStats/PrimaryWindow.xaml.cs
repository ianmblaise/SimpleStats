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
using System.Windows.Shapes;

namespace SimpleStats
{
    /// <summary>
    /// Interaction logic for PrimaryWindow.xaml
    /// </summary>
    public partial class PrimaryWindow : Window
    {
        public PrimaryWindow()
        {
            InitializeComponent();
        }

        private void lblHome_MouseEnter(object sender, MouseEventArgs e)
        {
            lblHome.Background = new LinearGradientBrush(Color.FromArgb(30, 8, 90, 147), Color.FromArgb(50, 50, 50, 50),
                new Point(0.5, 0), new Point(0, 0.5));
        }

        private void lblHome_MouseLeave(object sender, MouseEventArgs e)
        {
            lblHome.Background = null;
        }


        private void lblSettings_MouseEnter(object sender, MouseButtonEventArgs e)
        {
            lblSettings.Background = new LinearGradientBrush(Color.FromArgb(30, 8, 90, 147), Color.FromArgb(50, 50, 50, 50),
                new Point(0.5, 0), new Point(0, 0.5));
        }

        private void lblSettings_MouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            lblSettings.Background = null;
        }

        private void lblSettings_MouseEnter(object sender, MouseEventArgs e)
        {
            lblSettings.Background = new LinearGradientBrush(Color.FromArgb(30, 8, 90, 147), Color.FromArgb(50, 50, 50, 50),
                new Point(0.5, 0), new Point(0, 0.5));
        }

        private void lblLookup_MouseEnter(object sender, MouseEventArgs e)
        {
            lblLookup.Background = new LinearGradientBrush(Color.FromArgb(30, 8, 90, 147), Color.FromArgb(50, 50, 50, 50),
                new Point(0.5, 0), new Point(0, 0.5));
        }

        private void lblLookup_MouseLeave(object sender, MouseEventArgs e)
        {
            lblLookup.Background = null;
        }

        private void lblHome_MouseUp(object sender, MouseButtonEventArgs e)
        {
            frame.Source = new Uri("/Pages/Home.xaml", UriKind.Relative);
        }

        private void lblLookup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            frame.Source = new Uri("/Pages/Lookup.xaml", UriKind.Relative);
        }

        private void lblSettings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            frame.Source = new Uri("/Pages/Settings.xaml", UriKind.Relative);
        }
    }
}
