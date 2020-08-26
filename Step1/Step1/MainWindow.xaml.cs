/*
 * Mini serie di esempi su ereditarietà e polimorfismo.
 * maurizio.conti@ittsrimini.edu.it - Settembre 2020
 */

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

namespace Step1
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Persona> persone = new List<Persona>();

        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Nella versione "Step1" si ha un approccio ingenuo al problema.
            // Si usano le classi ma il codice che si scrive è tutto nel "main" della applicazione.
            // Difficilmente disaccoppiabile dalla UI.
            // Funziona ma possiamo fare di meglio
            try
            {
                StreamReader rd = new StreamReader("Persone.csv");
                string line = rd.ReadLine();

                while ( !rd.EndOfStream )
                {
                    line = rd.ReadLine();
                    string[] campi = line.Split(new char[] { ';', ',', '.' });

                    Persona p = new Persona { 
                        ID = Convert.ToInt32(campi[0]), 
                        Nome = campi[1], 
                        Cognome = campi[2], 
                        Ore = Convert.ToInt32(campi[3]) 
                    };

                    persone.Add(p);
                }
            }
            catch (Exception err) { Console.WriteLine(err.Message); }

            dgDati.ItemsSource = persone;
        }
    }

    class Persona
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

        // Field data
        private int _ore;
        
        // Property appoggiata al field data
        public int Ore
        {
            get 
            { 
                return _ore; 
            }

            set 
            {
                if (value > 100)
                    throw new Exception("EhEhEh!!!! Non posso scrivere un valore maggiore di 100...");

                _ore = value; 
            }
        }
    }
}
