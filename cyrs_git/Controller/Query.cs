using System.Data.OleDb;
using System.Data;

namespace cyrs_git.Controller
{
    class Query
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataAdapter dataAdapter;
        DataTable dt;

        public Query(string Conn)
        {
            connection = new OleDbConnection(Conn);
            dt = new DataTable();
        }

        public DataTable Update(string select)
        {
            connection.Open();
            dataAdapter = new OleDbDataAdapter(select, connection);
            dt.Clear();
            dataAdapter.Fill(dt);
            connection.Close();
            return dt;
        }

        public void Edit(string edit)
        {
            connection.Open();
            command = new OleDbCommand(edit, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Add(string UNTS, string TID, string FIRMID, string GRP, string NORMT, string DATASP)
        {
            connection.Open();
            command = new OleDbCommand($"INSERT INTO PTS(UNTS, TID, FIRMID, GRP, NORMT, DATASP) VALUES(@UNTS, @TID, @FIRMID, @GRP, @NORMT, @DATASP)", connection);
            command.Parameters.AddWithValue("UNTS", UNTS);
            command.Parameters.AddWithValue("TID", TID);
            command.Parameters.AddWithValue("FIRMID", FIRMID);
            command.Parameters.AddWithValue("GRP", GRP);
            command.Parameters.AddWithValue("NORMT", NORMT);
            command.Parameters.AddWithValue("DATASP", DATASP);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(int UNTS)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM PTS WHERE UNTS = {UNTS}", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
