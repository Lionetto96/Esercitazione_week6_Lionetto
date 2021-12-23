namespace LinqSpesa
{
    internal class Program
    {
        #region Data
        public static List<Spesa> GetSpesa()
        {
            var spese = new List<Spesa>()
            {
                new Spesa()
                {
                    Id = 1,
                    Data =new DateTime(2021,11,23),
                    Descrizione ="bolletta luce",
                    Utente ="ale",
                    Importo =149,
                    Approvata=false,
                    IdCategory=1
                },
                new Spesa()
                {
                    Id=2,
                    Data =new DateTime(2021,12,20),
                    Descrizione ="affitto casa al mare",
                    Utente ="eli",
                    Importo =490,
                    Approvata=false,
                    IdCategory=2
                },
                new Spesa(){
                    Id=3,
                    Data =DateTime.Now,
                    Descrizione ="bolletta gas",
                    Utente ="ale",
                    Importo =125,
                    Approvata=false,
                    IdCategory=1
                }
                
            };
            return spese;
        }

        public static List<Categoria> GetCategory()
        {
            var categories = new List<Categoria>()
            {
                new Categoria()
                {
                    IdCategory=1,
                    Nome="bollette",
                    
                },
                new Categoria()
                {
                    IdCategory=2,
                    Nome="affitto",

                }


            };
            return categories;
        }

       
        


        #endregion

        static void Main(String[] args)
        {
            QuerySyntax();
            LambdaSyntax();
            

        }

        

        public static void LambdaSyntax()
        {
            var spese = GetSpesa();
            var categories = GetCategory();

            //spese con importo superiore a 100
            Console.WriteLine("spese con importi maggiori di 100");
            var spesaOver100 = spese.Where(s=>s.Importo>100);
            
            foreach (var spesa in spesaOver100)
            {
                Console.WriteLine($"{spesa.Id}- {spesa.Descrizione} - {spesa.Importo}");
            }
            //spese mese di dicembre
            Console.WriteLine("spese dicembre");
            var speseDicembre = spese.Where(s => s.Data.Month == 12);
            foreach (var spesa in speseDicembre)
            {
                Console.WriteLine($"{spesa.Id}- {spesa.Data} -{spesa.Descrizione} - {spesa.Importo} ");
            }
            //orina spese per data cresecnte e importo decrescente
            Console.WriteLine("spese con date crescenti e importi decrescenti");
            var orderedSpese = spese.OrderBy(s=> s.Data).ThenByDescending(s => s.Importo);
            foreach (var spesa in orderedSpese)
            {
                Console.WriteLine($"{spesa.Id}- {spesa.Data} -{spesa.Descrizione} - {spesa.Importo} ");

            }


            //raggruppamento spesa per categoria
            Console.WriteLine("spese per categoria");
            var speseGroup = spese.GroupBy(s => s.IdCategory).Select(s => new
            {
                Key = s.Key,
                Spese = s.Select(s => s)
            });
            foreach (var spesa in speseGroup)
            {
                Console.WriteLine(spesa.Key);
                foreach (var spesa2 in spesa.Spese)
                {
                    Console.WriteLine($"{spesa2.Id}- {spesa2.Descrizione}-{spesa2.IdCategory}");

                }
            }
            // visualizzo somma, media, min e max degli importi delle spese
            var maxSpesa= spese.Max(s => s.Importo);
            Console.WriteLine($"spesa max importo: {maxSpesa}");
            //min
            var minSpesa = spese.Min(s => s.Importo);
            Console.WriteLine($"spesa min importo: {minSpesa}");
            //media
            var meanSpesa = spese.Average(s => s.Importo);
            Console.WriteLine($"l'importo medio delle spese è : {meanSpesa}");
            //somma 
            var sumSpesa = spese.Sum(s => s.Importo);
            Console.WriteLine($"somma della spesa è : {sumSpesa}");
        }
        public static void QuerySyntax()
        {
            var spese = GetSpesa();
            var categories = GetCategory();



            //con LINQ


            //spese con importo superiore a 100
            var speseOver100 = from spesa in spese
                               where spesa.Importo > 100
                               select spesa;
                                
            foreach (var spesa in speseOver100)
            {
                Console.WriteLine($"{spesa.Id}- {spesa.Descrizione} - {spesa.Importo}");
            }
            Console.WriteLine("spese dicembre");
            //spese  nel mese di dicembre
            var speseDicembre = from spesa in spese
                                where spesa.Data.Month == 12
                                select spesa;
            foreach (var spesa in speseDicembre)
            {
                Console.WriteLine($"{spesa.Id}-  {spesa.Data} - {spesa.Descrizione} - {spesa.Importo}");
            }
            Console.WriteLine("spese con date crescenti e importi decrescenti");
            //ordina spese per data crescente e per importo decr
            var orderedSpese = from spesa in spese
                                orderby spesa.Data ascending, spesa.Importo descending
                                select spesa;
            foreach (var spesa in orderedSpese)
            {
                Console.WriteLine($"{spesa.Id}-  {spesa.Data} - {spesa.Descrizione} - {spesa.Importo}");
            }


            Console.WriteLine("spese per categoria");
            //raggruppamento spese per categoria
            var spesePerCategory = from spesa in spese
                                   group spesa by spesa.IdCategory into spesaCategory
                                   select spesaCategory;
            foreach (var spesa in spesePerCategory)
            {
                Console.WriteLine(spesa.Key);
                foreach (var spesa2 in spesa)
                {
                    Console.WriteLine($"id: {spesa2.Id} - descrizione: {spesa2.Descrizione} - idcategoria: {spesa2.IdCategory}");
                }
            }
            
        }
    }
}
