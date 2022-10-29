using Sample.Controller;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sample
{
    public partial class Form2 : Form
    {
        Query controller;

        public Form2()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable table = controller.Update("SELECT TLGR FROM V_FIRM;");
            foreach (DataRow row in table.Rows)
            {
                comboBox1.Items.Add(row[0]);
            }
            comboBox1.SelectedIndex = 0;
            DataTable table1 = controller.Update("SELECT TNAME FROM TIPTR;");
            foreach (DataRow row in table1.Rows)
            {
                comboBox2.Items.Add(row[1].ToString());
            }
            comboBox2.SelectedIndex = 0;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable table = controller.Update($"SELECT FIRMID FROM V_FIRM WHERE TLGR = '{comboBox1.SelectedItem.ToString()}';");
            string firmid = "";
            foreach (DataRow row in table.Rows)
            {
                firmid = row[2].ToString();
            }
            DataTable table1 = controller.Update($"SELECT TID FROM TIPTR WHERE TNAME = '{comboBox2.SelectedItem.ToString()}';");
            string tid = "";
            foreach (DataRow row in table.Rows)
            {
                tid = row[3].ToString();
            }
            string unts = textBox1.Text;
            string grp = textBox2.Text;
            string normt = textBox3.Text;
            string datasp = "";
            if (maskedTextBox1.Text=="")
            {
                datasp = "NULL";
            }
            else
            {
                datasp = maskedTextBox1.Text;
            }
            controller.Add(unts,tid,firmid,grp,normt,datasp);
        }
    }
}
