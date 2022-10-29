using System.Data;
using System.Data.OleDb;

namespace DbLibrary.Controller
{
    public static class SQL
    {
        public static string ReadData = @"
SELECT V_FIRM.TLGR AS Фирма, VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.UNTS AS [Учетный номер ТС] 
FROM(
    (V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID) 
    INNER JOIN TIPTR ON TIPTR.TID = PTS.TID
) 
INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT 
GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS;";

        public static string ReadDetailedData = @"
SELECT PTS.UIN AS ID, V_FIRM.TLGR AS Фирма, PTS.UNTS AS [Учетный номер ТС],VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.GRP AS [Грузоподъёмность], PTS.NORMT AS [Расход топлива] 
FROM (
    (V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID) 
    INNER JOIN TIPTR ON TIPTR.TID = PTS.TID) 
INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT 
GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS, PTS.GRP, PTS.NORMT, PTS.UIN;
";

        public static string ReadDataByTLGR = @"
SELECT V_FIRM.TLGR AS Фирма, VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.UNTS AS [Учетный номер ТС] 
FROM(
    (V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID) 
    INNER JOIN TIPTR ON TIPTR.TID = PTS.TID
) 
INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT 
WHERE V_FIRM.TLGR = @TLGR
GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS;
";

        public static string ReadDataBySHNAME = @"
SELECT V_FIRM.TLGR AS Фирма, VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.UNTS AS [Учетный номер ТС]
FROM(
    (V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID) 
    INNER JOIN TIPTR ON TIPTR.TID = PTS.TID
) 
INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT 
WHERE VIDTC.SHNAME = @SHNAME
GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS;";

        public static string ReadDataByTName = @"
SELECT V_FIRM.TLGR AS Фирма, VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.UNTS AS [Учетный номер ТС]
FROM(
    (V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID)
    INNER JOIN TIPTR ON TIPTR.TID = PTS.TID
)
INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT 
WHERE TIPTR.TNAME = @TNAME 
GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS;
";

        public static string ReadDataByUNTS = @"
SELECT V_FIRM.TLGR AS Фирма, PTS.UNTS AS [Учетный номер ТС],VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.GRP AS [Грузоподъёмность], PTS.NORMT AS [Расход топлива]
FROM (
    (V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID)
    INNER JOIN TIPTR ON TIPTR.TID = PTS.TID
)
INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT
WHERE PTS.UNTS = @UNTS
GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS, PTS.GRP, PTS.NORMT;
";

        public static string ReadTLGRs = "SELECT TLGR FROM V_FIRM;";

        public static string ReadTNames = "SELECT TNAME FROM TIPTR;";

        public static string FirmFilter = "SELECT FIRMID FROM V_FIRM WHERE TLGR = @TLGR;";

        public static string TipTRFilter = "SELECT TID FROM TIPTR WHERE TNAME = @TNAME;";

        public static string UpdatePTS = @"
UPDATE PTS SET 
    UNTS = COALESCE(@UNTS1, UNTS), 
    TID = COALESCE(@TID, TID),
    FIRMID = COALESCE(@FIRMID, FIRMID), 
    GRP = COALESCE(@GRP, GRP),
    NORMT = COALESCE(@NORMT, NORMT),
WHERE UNTS = @UNTS2";
    }

    public class Query
    {
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataAdapter dataAdapter;

        public Query(string Conn)
        {
            connection = new OleDbConnection(Conn);
        }
        public DataTable ReadData(OleDbCommand command)
        {
            var dt = new DataTable();
            command.Connection.Open();
            dataAdapter = new OleDbDataAdapter(command);
            dt.Clear();
            dataAdapter.Fill(dt);
            command.Connection.Close();
            return dt;
        }

        public DataTable ReadData() => ReadData(new OleDbCommand(SQL.ReadData, connection));

        public DataTable ReadDetailedData() => ReadData(new OleDbCommand(SQL.ReadDetailedData, connection));

        public DataTable ReadDataByTLGR(string tlgr)
        {
            var command = new OleDbCommand(SQL.ReadDataByTLGR, connection);
            command.Parameters.Add("TLGR", tlgr);
            return ReadData(command);
        }

        public DataTable ReadDataBySHNAME(string shname)
        {
            var command = new OleDbCommand(SQL.ReadDataBySHNAME, connection);
            command.Parameters.Add("SHNAME", shname);
            return ReadData(command);
        }

        public DataTable ReadDataByTNAME(string tname)
        {
            var command = new OleDbCommand(SQL.ReadDataByTName, connection);
            command.Parameters.Add("TNAME", tname);
            return ReadData(command);
        }

        public DataTable ReadDataByUNTS(string unts)
        {
            var command = new OleDbCommand(SQL.ReadDataByUNTS, connection);
            command.Parameters.Add("UNTS", unts);
            return ReadData(command);
        }

        public DataTable ReadTLGRs() => 
            ReadData(new OleDbCommand(SQL.ReadTLGRs, connection));

        public DataTable ReadTNames() => 
            ReadData(new OleDbCommand(SQL.ReadTNames, connection));

        public DataTable ReadFirm(string tlgr)
        {
            var command = new OleDbCommand(SQL.FirmFilter, connection);
            command.Parameters.Add("TLGR", tlgr);
            return ReadData(command);
        }

        public DataTable ReadTipTR(string tname)
        {
            var command = new OleDbCommand(SQL.TipTRFilter, connection);
            command.Parameters.Add("TNAME", tname);
            return ReadData(command);
        }

        public void UpdatePTS(string unts1, string tId, string firmId, 
            string grp, string normt, string unts2)
        {
            connection.Open();
            var command = new OleDbCommand(SQL.UpdatePTS, connection);
            command.Parameters.Add("UNTS1", unts1);
            command.Parameters.Add("TID", tId);
            command.Parameters.Add("FIRMID", firmId);
            command.Parameters.Add("GRP", grp);
            command.Parameters.Add("NORMT", normt);
            command.Parameters.Add("UNTS2", unts2);
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

        public void DeletePTS(int UNTS)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM PTS WHERE UNTS = @UNTS", connection);
            command.Parameters.AddWithValue("UNTS", UNTS);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
