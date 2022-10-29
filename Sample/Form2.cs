using DbLibrary.Controller;
using Sample.Controller;
using System.Data;

namespace Sample
{
    public partial class Form2 : Form
    {
        readonly Query controller;

        public Form2()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable table = controller.ReadTLGRs();
            foreach (DataRow row in table.Rows)
            {
                comboBox1.Items.Add(row[0]);
            }
            comboBox1.SelectedIndex = 0;
            DataTable table1 = controller.ReadTNames();
            foreach (DataRow row in table1.Rows)
            {
                comboBox2.Items.Add(row[0].ToString());
            }
            comboBox2.SelectedIndex = 0;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DataTable table = controller.ReadFirm(comboBox1.SelectedItem.ToString());
            string firmid = "";
            foreach (DataRow row in table.Rows)
            {
                firmid = row[0].ToString();
            }
            DataTable table1 = controller.ReadTipTR(comboBox2.SelectedItem.ToString());
            string tid = "";
            foreach (DataRow row in table1.Rows)
            {
                tid = row[0].ToString();
            }
            string unts = textBox1.Text;
            string grp = textBox2.Text;
            string normt = textBox3.Text;
            string datasp = maskedTextBox1.Text == "" ? "NULL" : maskedTextBox1.Text;
            controller.Add(unts, tid, firmid, grp, normt, datasp);
        }
    }
}
