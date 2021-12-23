using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSpesa
{
    public class Spesa
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        
        public string Descrizione { get; set; }
        public string Utente { get; set; }
        public decimal Importo { get; set; }
        public bool Approvata { get; set; }
        public int IdCategory { get; set; }


    }
}
