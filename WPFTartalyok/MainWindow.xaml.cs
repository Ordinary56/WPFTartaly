using System;
using System.Collections.Generic;
using System.IO;
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
using WPFTartalyok.Models;
namespace WPFTartalyok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Tartaly> tartalylist = new();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void rdoKocka_Checked(object? sender, RoutedEventArgs e)
        {

            txtAel.IsEnabled = false;
            txtBel.IsEnabled = false;
            txtCel.IsEnabled = false;
            txtAel.Text = 10.ToString();
            txtBel.Text = 10.ToString();
            txtCel.Text = 10.ToString();
        }

        private void btnFelvesz_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtAel.Text, out int aEl) &&
                int.TryParse(txtBel.Text, out int bEl) &&
                int.TryParse(txtCel.Text, out int cEl))
            {
                Tartaly ujtartaly;
                if (rdoKocka.IsChecked == true)
                {
                    ujtartaly = new(txtNev.Text);

                }
                else
                {
                    ujtartaly = new(txtNev.Text, aEl, bEl, cEl);
                }
                lbTartalyok.Items.Add(ujtartaly.Info());
                tartalylist.Add(ujtartaly);
            }
            else
            {
                MessageBox.Show("Hiba.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new(@"./tartaly.csv", true))
            {

                tartalylist.ForEach(x => sw.WriteLine($"{x.Nev};{x.aEl};{x.bEl};{x.cEl};{x.akt}"));
            }
        }

        private void btntolt_Click(object sender, RoutedEventArgs e)
        {
            if (lbTartalyok.SelectedIndex == -1)
            {
                MessageBox.Show("Hiba, Nincs tartály kiválasztva ",
                    "nullError",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            tartalylist[lbTartalyok.SelectedIndex].Tolt(Convert.ToDouble(txtMennyitTolt.Text));
            lbTartalyok.Items[lbTartalyok.SelectedIndex] = tartalylist[lbTartalyok.SelectedIndex].Info();

        }

        private void btnLeenged_Click(object sender, RoutedEventArgs e)
        {
            if (lbTartalyok.SelectedIndex == -1)
            {
                MessageBox.Show("Hiba, Nincs tartály kiválasztva ",
                    "nullError",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            tartalylist[lbTartalyok.SelectedIndex].TeljesLeengedes();
            lbTartalyok.Items[lbTartalyok.SelectedIndex] = tartalylist[lbTartalyok.SelectedIndex].Info();
            lbTartalyok.Items.Refresh();

        }

        private void btnDuplaz_Click(object sender, RoutedEventArgs e)
        {
            if(lbTartalyok.SelectedIndex == -1)
            {
                MessageBox.Show("Hiba, Nincs tartály kiválasztva ",
                    "nullError",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            tartalylist[lbTartalyok.SelectedIndex].DuplazMeretet();
            lbTartalyok.Items[lbTartalyok.SelectedIndex] = tartalylist[lbTartalyok.SelectedIndex].Info();
            lbTartalyok.Items.Refresh();
        }

        private void rdoTeglatest_Checked(object sender, RoutedEventArgs e)
        {
            txtAel.Text = "";
            txtBel.Text = "";
            txtCel.Text = "";
            txtAel.IsEnabled = true;
            txtBel.IsEnabled = true;
            txtCel.IsEnabled = true;
        }
    }
}
