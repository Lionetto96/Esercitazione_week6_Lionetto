using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class SupportSpesa
    {
        public static SqlDataAdapter InitSpesaDataSetAndAdapter(DataSet spesaDs, SqlConnection conn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            
            
            adapter.DeleteCommand = GenerateDeleteCommand(conn);

            adapter.SelectCommand = GenerateSelectCommand(conn);

            

            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapter.Fill(spesaDs, "Spesa");

            return adapter;


        }
        public static SqlDataAdapter Init2SpesaDataSetAndAdapter(DataSet spesaDs, SqlConnection conn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = GenerateSelectCommandByUtente(conn);

            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapter.Fill(spesaDs, "Spesa");

            return adapter;


        }
        public static SqlDataAdapter Init3SpesaDataSetAndAdapter(DataSet spesaDs, SqlConnection conn)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = GenerateSelectCommandByApprovazione(conn);

            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapter.Fill(spesaDs, "Spesa");

            return adapter;


        }

        private static SqlCommand GenerateSelectCommandByApprovazione(SqlConnection conn)
        {
            SqlCommand spesaSelectCommandByApprovazione = new SqlCommand();
            spesaSelectCommandByApprovazione.Connection = conn;
            spesaSelectCommandByApprovazione.CommandType = CommandType.Text;

            spesaSelectCommandByApprovazione.CommandText = "select * from Spesa where Approvazione=1";

            return spesaSelectCommandByApprovazione;
        }

        private static SqlCommand GenerateSelectCommandByUtente(SqlConnection conn)
        {
            SqlCommand spesaSelectCommandByUtente = new SqlCommand();
            spesaSelectCommandByUtente.Connection = conn;
            spesaSelectCommandByUtente.CommandType = CommandType.Text;

            spesaSelectCommandByUtente.CommandText = "select * from Spesa where Utente=@utente";

            return spesaSelectCommandByUtente;
        }

        private static SqlCommand GenerateSelectCommand(SqlConnection conn)
        {
            SqlCommand spesaSelectCommand = new SqlCommand();
            spesaSelectCommand.Connection = conn;
            spesaSelectCommand.CommandType = CommandType.Text;
            spesaSelectCommand.CommandText = "select * from Spesa";

            return spesaSelectCommand;
        }

        private static SqlCommand GenerateDeleteCommand(SqlConnection conn)
        {
            SqlCommand deleteCommand = new SqlCommand();
            deleteCommand.Connection = conn;
            deleteCommand.CommandType = CommandType.Text;
            deleteCommand.CommandText = "delete from Spesa where Id=@id";

            deleteCommand.Parameters.Add(new SqlParameter(
                "@id", SqlDbType.Int, 0, "Id"
            ));
            return deleteCommand;
        }
    }
}
