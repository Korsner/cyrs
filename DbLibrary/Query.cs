using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace DbLibrary.Controller
{
    public class Query
    {
        private readonly string conn;
        public Query(string Conn) => conn = Conn;

        private DataTable ReadDataTable(Func<OleDbCommand> getCommand)
        {
            try
            {
                var dt = new DataTable();
                using var connection = new OleDbConnection(conn);
                using var command = getCommand();
                command.Connection = connection;
                connection.Open();
                using var dataAdapter = new OleDbDataAdapter(command);
                dt.Clear();
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        public DataTable ReadData() =>
            ReadDataTable(() => new OleDbCommand(SQL.ReadData));

        public DataTable ReadDetailedData()
            => ReadDataTable(() => new OleDbCommand(SQL.ReadDetailedData));

        public DataTable ReadDataByTLGR(string tlgr) =>
            ReadDataTable(() => new OleDbCommand(SQL.ReadDataByTLGR)
            { Parameters = { new OleDbParameter("TLGR", tlgr) } });

        public DataTable ReadDataBySHNAME(string shname) =>
            ReadDataTable(() => new OleDbCommand(SQL.ReadDataBySHNAME)
            { Parameters = { new OleDbParameter("SHNAME", shname) } });

        public DataTable ReadDataByTNAME(string tname) =>
            ReadDataTable(() => new OleDbCommand(SQL.ReadDataByTName)
            { Parameters = { new OleDbParameter("TNAME", tname) } });

        public DataTable ReadDataByUNTS(string unts) =>
            ReadDataTable(() => new OleDbCommand(SQL.ReadDataByUNTS)
            { Parameters = { new OleDbParameter("UNTS", unts) } });

        public DataTable ReadTLGRs() =>
            ReadDataTable(() => new OleDbCommand(SQL.ReadTLGRs));

        public DataTable ReadTNames() =>
            ReadDataTable(() => new OleDbCommand(SQL.ReadTNames));

        public DataTable ReadFirm(string tlgr) =>
            ReadDataTable(() => new OleDbCommand(SQL.FirmFilter)
            { Parameters = { new OleDbParameter("TLGR", tlgr) } });

        public DataTable ReadTipTR(string tname) =>
            ReadDataTable(() => new OleDbCommand(SQL.TipTRFilter)
            { Parameters = { new OleDbParameter("TNAME", tname) } });

        public int UpdatePTS(string unts1, string tId, string firmId,
            string grp, string normt, string unts2)
        {
            try
            {
                using var connection = new OleDbConnection(conn);
                connection.Open();
                using var command = new OleDbCommand(SQL.UpdatePTS, connection);
                command.Parameters.AddWithValue("UNTS1", unts1);
                var tID = string.IsNullOrWhiteSpace(tId) ? "-1" : tId;
                command.Parameters.AddWithValue("TID", tID);
                var fID = string.IsNullOrWhiteSpace(firmId) ? "-1" : tId;
                command.Parameters.AddWithValue("FIRMID", fID);
                command.Parameters.AddWithValue("GRP", grp);
                command.Parameters.AddWithValue("NORMT", normt);
                command.Parameters.AddWithValue("UNTS2", unts2);
                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return -1;
            }
        }

        public int Add(string UNTS, string TID, string FIRMID, string GRP, string NORMT, string DATASP)
        {
            try
            {
                using var connection = new OleDbConnection(conn);
                connection.Open();
                using var command = new OleDbCommand($"INSERT INTO PTS(UNTS, TID, FIRMID, GRP, NORMT, DATASP) VALUES(@UNTS, @TID, @FIRMID, @GRP, @NORMT, @DATASP)", connection);
                command.Parameters.AddWithValue("UNTS", UNTS);
                command.Parameters.AddWithValue("TID", TID);
                command.Parameters.AddWithValue("FIRMID", FIRMID);
                command.Parameters.AddWithValue("GRP", GRP);
                command.Parameters.AddWithValue("NORMT", NORMT);
                command.Parameters.AddWithValue("DATASP", DATASP);
                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return -1;
            }
        }

        public int DeletePTS(int UNTS)
        {
            try
            {
                using var connection = new OleDbConnection(conn);
                connection.Open();
                using var command = new OleDbCommand($"DELETE FROM PTS WHERE UNTS = @UNTS", connection);
                command.Parameters.AddWithValue("UNTS", UNTS);
                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return -1;
            }
        }
    }
}