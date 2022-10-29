using Sample.Controller;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sample
{
    public partial class Form3 : Form
    {
        readonly Query controller;

        public Form3()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
        }

        DataTable table3;

        private void Form3_Load(object sender, EventArgs e)
        {
            table3 = controller.Update($"SELECT PTS.UIN AS ID, V_FIRM.TLGR AS Фирма,  PTS.UNTS AS [Учетный номер ТС],VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.GRP AS [Грузоподъёмность], PTS.NORMT AS [Расход топлива] FROM ((V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID) INNER JOIN TIPTR ON TIPTR.TID = PTS.TID) INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS, PTS.GRP, PTS.NORMT, PTS.UIN;");
            dataGridView1.DataSource = table3;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //UPDATE PTS SET ContactName = 'Alfred Schmidt', City = 'Frankfurt' WHERE UNTS = 1;

        private void Button2_Click(object sender, EventArgs e)
        {
            controller.Edit(BuildUpdate());
            //BuildUpdate();
        }

        private string BuildUpdate()
        {
            string update = "UPDATE PTS SET ";
            string where = "WHERE UNTS = ";
            string unts = "UNTS = ";
            string tid = "TID = ";
            string firmid = "FIRMID = ";
            string grp = "GRP = ";
            string normt = "NORMT = ";
            string comma = ", ";
            string result = update;
            if (textBox3.Text != "")
            {
                result += unts;
                result += textBox3.Text;
                result += " ";
            }
            if (textBox2.Text != "")
            {
                if ((textBox2.Text != "") && (textBox3.Text != ""))
                {
                    result += comma;
                }
                result += tid;
                result += textBox2.Text;
                result += " ";
            }
            if (textBox1.Text != "")
            {
                if ((textBox1.Text != "") && ((textBox3.Text != "") || (textBox2.Text != "")))
                {
                    result += comma;
                }
                result += firmid;
                result += textBox1.Text;
                result += " ";
            }
            if (textBox4.Text != "")
            {
                if ((textBox4.Text != "") && ((textBox3.Text != "") || (textBox2.Text != "") || (textBox1.Text != "")))
                {
                    result += comma;
                }
                result += grp;
                result += textBox4.Text;
                result += " ";
            }
            if (textBox5.Text != "")
            {
                if ((textBox5.Text != "") && ((textBox3.Text != "") || (textBox2.Text != "") || (textBox1.Text != "") || (textBox4.Text != "")))
                {
                    result += comma;
                }
                result += normt;
                result += textBox5.Text;
                result += " ";
            }
            result += where;
            result += (int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Учетный номер ТС"].Value.ToString())).ToString();

            return result;
        }
    }
}
