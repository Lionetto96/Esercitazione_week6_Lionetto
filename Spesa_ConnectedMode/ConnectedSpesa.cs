using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectedMode
{
    static class ConnectedSpesa
    {
        static string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=GestioneSpesa;Trusted_Connection=True;";

        #region READ 
        public static void VisualizzaSpesa()
        {
            //creo connessione
            using SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
               
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("connessi al DB");
                else
                    Console.WriteLine("non connessi al DB");


                
                string sqlStatement = "select * from Spesa";
                SqlCommand readCommand2 = new SqlCommand(sqlStatement, conn);


                SqlDataReader reader = readCommand2.ExecuteReader();
                Console.WriteLine("SPESA");
                
                while (reader.Read())
                {
                    Console.WriteLine("{0} - {1} {2} {3} {4} {5} {6}",
                        reader["Id"],
                        reader["Data"],
                        reader["Descrizione"],
                        reader["Utente"],
                        reader["Importo"],
                        reader["Approvata"],
                        reader["IdCategory"]);
                    

                }
                
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"errore: {ex.Message}");

            }
            finally
            {
                
                conn.Close();
            }
        }
        #endregion
        #region INSERT WITH PARAMETER
        public static void InsertWithParameter()
        {
            using SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("connesso al Db");
                else
                    Console.WriteLine("non connesso al Db");

                Console.WriteLine("inserisci data della spesa");
                var Data = Console.ReadLine();
                Console.WriteLine("inserisci descrizione spesa");
                var Descrizione = Console.ReadLine();
                Console.WriteLine("inserisci nome utente");
                var Utente = Console.ReadLine();
                Console.WriteLine("inserisci importo della spesa");
                var Importo= Console.ReadLine();
                Console.WriteLine("scegli id categoria della spesa" +
                    "\n[1] bollette" +
                    "\n[2] affitto");

                var IdCategory = Console.ReadLine();

                var Approvata = 0;

                string insertSqlStatement = "insert into Spesa values(@data, @descr, @utente,@importo,@approvata,@idCategory)";

                SqlCommand insertCommand = conn.CreateCommand();
                insertCommand.CommandText = insertSqlStatement;

                //METODO 1 
                insertCommand.Parameters.AddWithValue("@data", Data);
                insertCommand.Parameters.AddWithValue("@descr", Descrizione);
                insertCommand.Parameters.AddWithValue("@utente", Utente); 
                insertCommand.Parameters.AddWithValue("@importo", Importo);
                insertCommand.Parameters.AddWithValue("@approvata", Approvata);
                insertCommand.Parameters.AddWithValue("@idCategory", IdCategory);


                int result = insertCommand.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine("inserimento avvenuto con successo");
                else
                    Console.WriteLine("errore di inserimento");

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"error sql : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"generic Error: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region APPROVA SPESA
        public static void UpdateApproved()
        {
            using SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("connesso al Db");
                else
                    Console.WriteLine("non connesso al Db");


                Console.WriteLine("inserisci id della spesa da approvare");
                
                int id=int.Parse(Console.ReadLine());

                string updateSqlStatement = "update Spesa set Approvata=1 where Id=@id";

                SqlCommand updateCommand = conn.CreateCommand();
                updateCommand.CommandText = updateSqlStatement;

                int result = updateCommand.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine("spesa approvata con successo");
                else
                    Console.WriteLine("errore ");

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"error sql : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"generic Error: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion


        #region UPDATE SPESA
        public static void UpdateSpesa()
        {
            using SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                    Console.WriteLine("connesso al Db");
                else
                    Console.WriteLine("non connesso al Db");


                Console.WriteLine("inserisci id della spesa da modificare");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("inserisci descrizione nuova della spesa da modificare");
                var descrNuova=Console.ReadLine();

                string updateSqlStatement = "update Spesa set Descrizione=@descrNuova where Approvata=0 and Id=@id";

                SqlCommand updateCommand = conn.CreateCommand();
                updateCommand.CommandText = updateSqlStatement;

                updateCommand.Parameters.AddWithValue("@descrNuova",descrNuova);
                updateCommand.Parameters.AddWithValue("@id", id);

                int result = updateCommand.ExecuteNonQuery();
                if (result == 1)
                    Console.WriteLine("spesa modificata con successo");
                else
                    Console.WriteLine("errore ");

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"error sql : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"generic Error: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
    }
    



}





