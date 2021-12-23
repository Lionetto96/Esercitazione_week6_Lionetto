using ClassLibrary;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisconnectedMode
{
    static class DisconnectedSpesa
    {
        static IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(@"C:\Users\alessia.lionetto\source\repos\Spesa_ConnectedMode\DisconnectedMode")
            .AddJsonFile("appsettings.json")
            .Build();
        static string ConnectionString = config.GetConnectionString("SpesaDB");

        #region SELECT
        public static void VisualizzaSpesa()
        {
            DataSet spesaDs = new DataSet();

            using SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    Console.WriteLine("connessione stabilita");
                }

                else
                {
                    Console.WriteLine("connessione non riuscita");
                    return;
                }


                
                SupportSpesa.InitSpesaDataSetAndAdapter(spesaDs, conn);

                conn.Close();
                
                Console.WriteLine("==== Spesa List ====");
                
                foreach (DataRow row in spesaDs.Tables["Spesa"].Rows)
                {
                    Console.WriteLine($"[{row["Id"]}]  data: {row["Data"]} descrizione: {row["Descrizione"]} " +
                        $"Utente: {row["Utente"]} importo: {row["Importo"]}  Approvata: {row["Approvata"]}  categoria:{row["IdCategory"]}");
                }


            }
            catch (SqlException ex)
            {
                Console.WriteLine($"sql error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" error: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
        #region DELETE
        public static void DeleteSpesa()
        {
            DataSet spesaDs = new DataSet();
           
            using SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();

                SqlDataAdapter adapter = SupportSpesa.InitSpesaDataSetAndAdapter(
                    spesaDs, connection);

                Console.WriteLine("inserisci id spesa da eliminare");
                int id = int.Parse(Console.ReadLine());
                DataRow rowToDelete = spesaDs.Tables["Spesa"].Rows.Find(id);
                if (rowToDelete != null)
                {

                    rowToDelete.Delete();  

                }
                adapter.Update(spesaDs, "Spesa");

                connection.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"error sql : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error : {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region SELECT BY UTENTI
        public static void VisualizzaSpesaPerUtenti()
        {
            DataSet spesaDs = new DataSet();

            using SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    Console.WriteLine("connessione stabilita");
                }

                else
                {
                    Console.WriteLine("connessione non riuscita");
                    return;
                }



                SupportSpesa.Init2SpesaDataSetAndAdapter(spesaDs, conn);

                conn.Close();

                Console.WriteLine("==== Spesa List ====");

                foreach (DataRow row in spesaDs.Tables["Spesa"].Rows)
                {
                    Console.WriteLine($"[{row["Id"]}]  data: {row["Data"]} descrizione: {row["Descrizione"]} " +
                        $"Utente: {row["Utente"]} importo: {row["Importo"]}  Approvata: {row["Approvata"]}  categoria:{row["IdCategory"]}");
                }


            }
            catch (SqlException ex)
            {
                Console.WriteLine($"sql error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" error: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region SELECT BY approvazione
        public static void VisualizzaSpesaPerApprovazione()
        {
            DataSet spesaDs = new DataSet();

            using SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    Console.WriteLine("connessione stabilita");
                }

                else
                {
                    Console.WriteLine("connessione non riuscita");
                    return;
                }



                SupportSpesa.Init3SpesaDataSetAndAdapter(spesaDs, conn);

                conn.Close();

                Console.WriteLine("==== Spesa List ====");

                foreach (DataRow row in spesaDs.Tables["Spesa"].Rows)
                {
                    Console.WriteLine($"[{row["Id"]}]  data: {row["Data"]} descrizione: {row["Descrizione"]} " +
                        $"Utente: {row["Utente"]} importo: {row["Importo"]}  Approvata: {row["Approvata"]}  categoria:{row["IdCategory"]}");
                }


            }
            catch (SqlException ex)
            {
                Console.WriteLine($"sql error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" error: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
    }
}
