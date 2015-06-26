using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static SimpleStats.LolSkill.Utils;

namespace SimpleStats.Pages
{
    /// <summary>
    /// Interaction logic for Lookup.xaml
    /// </summary>
    public partial class Lookup : Page
    {
        public Lookup()
        {
            InitializeComponent();
            LookupPage = this;
        }

        private static Stats GetStats(string[] player)
        {
            try
            {
                var stats = GetStatSummary(player[0]);
                stats.Champion = player[1];
                return stats;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Error in GetStats - " + ex.Message);

                return null;
            }
        }

        public List<Stats> CurrentStats { get; set; }

        public int CurrentStatIndex { get; set; }

        public static string LookupName { get; set; }

        private async void LookupPlayer()
        {
            var players = GetNamesAndChampion(LookupName);

            if (players == null || players.Count < 1)
            {
                WriteToLog("Unable to find a game for that summoner.");
                return;
            }

            CurrentStats = new List<Stats>();
            tabControl.Visibility = Visibility.Visible;
            for (var i = 0; i < players.Count; i++)
            {
                WriteToLog(i + @": " + players[i][0] + '-' + players[i][1]);
                WriteToLog("Getting stats for player: " + players[i][0]);
                var i1 = i;
                var s = new Stats();
                await Dispatcher.BeginInvoke(new Action(() => s = GetStats(players[i1])));
                WriteToLog("Got stats for player: " + players[i][0]);
                Task.WaitAny();
                CurrentStats.Add(s);
            }

            DisplayStat(CurrentStatIndex);
            btnSearch.IsEnabled = true;
            lblGettingData.Content = "Done!";
        }
       

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            btnSearch.IsEnabled = false;
            WriteToLog("Initiating active game look up for " + txtPlayersName.Text);
            WriteToLog("Trying to get Game Information for " + txtPlayersName.Text);

            lblGettingData.Visibility = Visibility.Visible;
            LookupName = txtPlayersName.Text;
            Dispatcher.BeginInvoke(new Action(LookupPlayer), DispatcherPriority.Background);

            btnSearch.IsEnabled = true;

            lblGettingData.Visibility = Visibility.Hidden;
        }



        void DisplayStat(int index)
        {
            
            if (CurrentStats == null || index < 0 || index >= CurrentStats.Count)
            {
                return;
            }
            if (index <= 4)
            {
                txtResultsFor.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 255));
            }
            if (index >= 5)
            {
                txtResultsFor.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
            var stat = CurrentStats[index];
            if (stat == null)
            {
                WriteToLog("Failed to update stats. No valid data received, DisplayStat()");
                return;
            }
            CurrentStatIndex = index;
            txtKdaRes.Text = stat.Kda;
            txtResultsFor.Text = stat.Name;
            txtWinsRes.Text = stat.Wins.ToString();
            txtLossesRes.Text = stat.Losses.ToString();
            txtWinPercRes.Text = stat.WinPercentage.ToString("F");
            var champ = char.ToUpper(stat.Champion[0]) + stat.Champion.Substring(1);
            
            var champImgUrl = "http://ddragon.leagueoflegends.com/cdn/5.11.1/img/champion/" + champ + ".png";
            imgChampion.Source = new BitmapImage(new Uri(champImgUrl));
            var divImgSrc = stat.Division.Replace(" ", string.Empty);
            divImgSrc = char.ToLower(divImgSrc[0]) + divImgSrc.Substring(1);
            imgLeague.Source = new BitmapImage(new Uri("http://static.lolskill.net/img/tiers/192/" + divImgSrc + ".png"));
        }

        private void btnNextResult_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DisplayStat(CurrentStatIndex + 1);
        }

        private void btnPreviousResult_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DisplayStat(CurrentStatIndex -1);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDebug.Text = "";
        }

        public static Lookup LookupPage { get; set; }

        public void WriteToLog(string message)
        {
            txtDebug.AppendText($"[{DateTime.Now.ToString("HH:m:sssss")}] {message} \n");
        }

        private void btnPreviousResult_MouseEnter(object sender, MouseEventArgs e)
        {
            var currentWeight = btnPreviousResult.FontWeight.ToOpenTypeWeight();
            btnPreviousResult.FontWeight = FontWeight.FromOpenTypeWeight(currentWeight + 200);
        }

        private void btnPreviousResult_MouseLeave(object sender, MouseEventArgs e)
        {
            var currentWeight = btnPreviousResult.FontWeight.ToOpenTypeWeight();
            btnPreviousResult.FontWeight = FontWeight.FromOpenTypeWeight(currentWeight - 200);
        }

        private void btnNextResult_MouseEnter(object sender, MouseEventArgs e)
        {
            var currentWeight = btnNextResult.FontWeight.ToOpenTypeWeight();
            btnNextResult.FontWeight = FontWeight.FromOpenTypeWeight(currentWeight + 200);
        }

        private void btnNextResult_MouseLeave(object sender, MouseEventArgs e)
        {
            var currentWeight = btnNextResult.FontWeight.ToOpenTypeWeight();
            btnNextResult.FontWeight = FontWeight.FromOpenTypeWeight(currentWeight - 200);
        }
    }
}
