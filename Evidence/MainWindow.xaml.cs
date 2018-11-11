using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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

namespace Evidence
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> something = new List<string>();
        public MainWindow()
        {
            InitializeComponent();


            APIFill();

            //lejbl.Content = something[0];
        }

        public async void APIFill()
        {
            LV.ItemsSource = null;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://student.sps-prosek.cz/~fiserkl15/public/ishall/AsiApi/readdata.php");
            string json = await response.Content.ReadAsStringAsync();
            dynamic c = JsonConvert.DeserializeObject(json);
            List<Osoba> Osoby = new List<Osoba>();
            
            //int j = c[y][4];

            int r = 1;
            try
            {
                while (c[r][1] != null)
                {
                    r++;
                }
            }
            catch
            {

            }


            for (int y = 0; y < r; y++)
            {
                Osoba ll = new Osoba();
                ll.ID = c[y][0];
                ll.jmeno = c[y][1];
                ll.prijmeni = c[y][2];
                ll.datumnarozeni = c[y][3];
                ll.rodnecislo = c[y][4];
                ll.rodnecislododatek = c[y][5];
                ll.pohlavi = c[y][6];
                Osoby.Add(ll);
            }
                                          
            /*
             0 = ID
             1 = jméno
             2 = příjmení
             3 = datum narození
             4 = rodný číslo
             5 = dodatek
             6 = pohlaví             
             
             */
            LV.ItemsSource = Osoby;

            //lejbl.Content = c.jmeno;


        }

        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Osoba o = (Osoba)LV.SelectedItem;
                jmeno.Text = o.jmeno;
                prijmeni.Text = o.prijmeni;
                datum.Text = o.datumnarozeni;
                rc.Text = o.rodnecislo.ToString();
                rcd.Text = o.rodnecislododatek.ToString();
                pohlavi.Text = o.pohlavi;
            }
            catch { }
            
        }

        private void UpdatePerson(object sender, RoutedEventArgs e)
        {
            //klient
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://student.sps-prosek.cz/~fiserkl15/public/ishall/AsiApi/updateperson.php");

            var keyValues = new List<KeyValuePair<string, string>>();

            //values :: 
            keyValues.Add(new KeyValuePair<string, string>("jmeno", jmeno.Text.ToString()));
            keyValues.Add(new KeyValuePair<string, string>("prijmeni", prijmeni.Text.ToString()));
            keyValues.Add(new KeyValuePair<string, string>("datumnarozeni", datum.Text.ToString()));
            keyValues.Add(new KeyValuePair<string, string>("rodnecislo", rc.Text.ToString()));
            keyValues.Add(new KeyValuePair<string, string>("rodnecislododatek", rcd.Text.ToString()));
            keyValues.Add(new KeyValuePair<string, string>("pohlavi", pohlavi.Text.ToString()));
            


            request.Content = new FormUrlEncodedContent(keyValues);
            //request sending

            var response = client.SendAsync(request);

            APIFill();
            //response :: 
            //string resonseContent = response.Content.ReadAsStringAsync();

            //
        }
        private void DeletePerson(object sender, RoutedEventArgs e)
        {

            Osoba o = (Osoba)LV.SelectedItem;
            jmeno.Text = o.jmeno;

            //
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://student.sps-prosek.cz/~fiserkl15/public/ishall/AsiApi/deleteperson.php");

            var keyValues = new List<KeyValuePair<string, string>>();

            //values :: 
            keyValues.Add(new KeyValuePair<string, string>("jmeno", jmeno.Text));

            request.Content = new FormUrlEncodedContent(keyValues);
            //request sending

            var response = client.SendAsync(request);

            //nefungujue asi, lolololo
            APIFill();
            //response :: 
            //string resonseContent = response.Content.ReadAsStringAsync();

            //

        }

        private void AddPerson(object sender, RoutedEventArgs e)
        {
            //klient
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://student.sps-prosek.cz/~fiserkl15/public/ishall/AsiApi/addperson.php");

            var keyValues = new List<KeyValuePair<string,string>>();

            //values :: 
            keyValues.Add(new KeyValuePair<string, string>("jmeno", jmeno.Text));
            keyValues.Add(new KeyValuePair<string, string>("prijmeni", prijmeni.Text));
            keyValues.Add(new KeyValuePair<string, string>("datumnarozeni", datum.Text.ToString()));
            keyValues.Add(new KeyValuePair<string, string>("rodnecislo", rc.Text));
            keyValues.Add(new KeyValuePair<string, string>("rodnecislododatek", rcd.Text));
            keyValues.Add(new KeyValuePair<string, string>("pohlavi", pohlavi.Text));


            request.Content = new FormUrlEncodedContent(keyValues);
            //request sending

            var response = client.SendAsync(request);

            //nefungujue asi, lolololo
            APIFill();
            //response :: 
            //string resonseContent = response.Content.ReadAsStringAsync();

            //

        }
    }
}
