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

namespace Step2
{
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
    class Persone
    {
        // Note sull'architettura
        // Elementi, ha una relazione di tipo "Composizione" con Persone perchè:
        // - La responsabilità per la creazione di Elementi è di Persone
        // - Se viene distrutto Persone, anche Elementi viene distrutto
        // - Elementi può essere visto solo da dentro Persone, non esistono altre classi che lo usano.
        //
        // Quando abbiamo una relazione di composizione, l'aspetto sul quale stiamo ragionando è legato alla domanda "ha un?"
        // Se la classe che andiamo a modellare invece ci sembra sempre di più vicino al concetto di "è un", allora l'ereditarietà è lo strumneto più giusto da usare.
        //
        // E lo vediamo nello Step3

        public List<Persona> Elementi { get; private set;}

        public Persone()
        {
            Elementi = new List<Persona>();
        }

        public Persone(string NomeFile)
            : this()
        {
            StreamReader rd = new StreamReader(NomeFile);
            string line = rd.ReadLine();

            while (!rd.EndOfStream)
            {
                line = rd.ReadLine();
                string[] campi = line.Split(new char[] { ';', ',', '.' });

                try
                {
                    Persona p = new Persona
                    {
                        ID = Convert.ToInt32(campi[0]),
                        Nome = campi[1],
                        Cognome = campi[2],
                        Ore = Convert.ToInt32(campi[3])
                    };

                    Elementi.Add(p);
                }
                catch(Exception err) { Console.WriteLine(err.Message); }
            }
        }
    }
}
