using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dgDati.ItemsSource = new Persone("Persone.csv"); ;
        }

        //private void dgDati_LoadingRow(object sender, DataGridRowEventArgs e)
        //{
        //    if (e.Row.Item is IDT)
        //        e.Row.Background = Brushes.LightGreen;

        //    else if (e.Row.Item is ITP)
        //        e.Row.Background = Brushes.LightYellow;
        //}
    }

    class Persona
    {
        public Persona()
        { }

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

        // Property virtual che serve per il dimostrare il pomlimorfismo

        // Reddito nella versione base, ritorna Ore * 10
        // Ma nelle classi figlie, ognuno so lo calcola come crede...
        //
        public virtual double Reddito
        {
            get { return Ore * 10; }
        }
    }

    class IDT : Persona
    {
        // Costruttore di default che chiama il costruttore del padre...
        public IDT() : base() { }

        public override double Reddito
        {
            get { return Ore * 12; }
        }
    }
    class ITP : Persona
    {
        // Costruttore di default che chiama il costruttore del padre...
        public ITP() : base() { }

        public override double Reddito
        {
            get { return Ore * 10; }
        }
    }


    class Persone : List<Persona>
    {
        //
        // Note sull'architettura
        // Elementi non esiste più.
        //
        // Persone "è un" List<Persona> 
        // Persone è un List<> migliore.
        //
        // Usa List<> per come è stata progettata, ma le aggiunge quello che manca...
        // ...come ad esempio la possibilità di caricarsi da un file!!!
        // 
        // Applicare l'ereditarietà (quando si può) è meglio della composizione perchè aiuta a rispettare il principio di singola responsabilità.
        // Aiuta a disaccopiare il codice.
        // 

        public Persone()
        { }

        public Persone(string NomeFile)
            : this()
        {
            StreamReader rd = new StreamReader(NomeFile);

            string line = rd.ReadLine();
            while (!rd.EndOfStream)
            {
                line = rd.ReadLine();
                string[] campi = line.Split(new char[] { ';', ',', '.' });

                int id = Convert.ToInt32(campi[0]);

                // UpCasting!
                // Inserire un ITP o un IDT in una List<Persona> è perfettamente lecito perchè sono due derivate.
                // Qui entra in gioco il polimorfismo che chiama i metodi dichiarati virtual, nel modo corretto.

                // La magia infatti avviene qui!
                // Nonostante si tratti di una List<Persona> il sistema chiama la property "Reddito" in modo differenziato per ITP e IDT.

                try
                {
                    string ruolo = campi[1];
                    if (ruolo == "ITP")
                    {
                        ITP pers = new ITP
                        {
                            ID = Convert.ToInt32(campi[0]),
                            Nome = campi[2],
                            Cognome = campi[3],
                            Ore = Convert.ToInt32(campi[4])
                        };

                        // Add esiste perchè l'abbiamo ereditata da List<T>
                        // Qui inseriamo un ITP
                        Add(pers);
                    }
                    else
                    {
                        IDT pers = new IDT
                        {
                            ID = Convert.ToInt32(campi[0]),
                            Nome = campi[2],
                            Cognome = campi[3],
                            Ore = Convert.ToInt32(campi[4])
                        };

                        // Add esiste perchè l'abbiamo ereditata da List<T>
                        // Qui inseriamo un IDT
                        Add(pers);
                    }
                }
                catch (Exception err) { Console.WriteLine(err.Message); }
            }

            // Sempre meglio chiudere gli stream in uscita...
            rd.Close();
        }
    }
}
