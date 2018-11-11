using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence
{
    public class Osoba
    {
        public int ID;
        public string jmeno;
        public string prijmeni;
        public string datumnarozeni;
        public int rodnecislo;
        public int rodnecislododatek;
        public string pohlavi;

        public Osoba()
        {
        }
        public Osoba(int iD, string jmeno, string prijmeni, string datumnarozeni, int rodnecislo, int rodnecislododatek, string pohlavi)
        {
            ID = iD;
            this.jmeno = jmeno;
            this.prijmeni = prijmeni;
            this.datumnarozeni = datumnarozeni;
            this.rodnecislo = rodnecislo;
            this.rodnecislododatek = rodnecislododatek;
            this.pohlavi = pohlavi;
        }
        public override string ToString()
        {
            return jmeno + " " + prijmeni;
        }
    }
}
