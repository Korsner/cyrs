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
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    comboBox1.Items.Add(row[0]);
                }
                comboBox1.SelectedIndex = 0;
                table = controller.ReadTNames();
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        comboBox2.Items.Add(row[0].ToString());
                    }
                    comboBox2.SelectedIndex = 0;
                    return;
                }
            }
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DataTable table = controller.ReadFirm(comboBox1.SelectedItem.ToString());
            if (table != null)
            {
                string firmid = "";
                foreach (DataRow row in table.Rows)
                {
                    firmid = row[0].ToString();
                }
                table = controller.ReadTipTR(comboBox2.SelectedItem.ToString());
                if (table != null)
                {
                    string tid = "";
                    foreach (DataRow row in table.Rows)
                    {
                        tid = row[0].ToString();
                    }
                    string unts = textBox1.Text;
                    string grp = textBox2.Text;
                    string normt = textBox3.Text;
                    string datasp = maskedTextBox1.Text == "" ? "NULL" : maskedTextBox1.Text;
                    var c = controller.Add(unts, tid, firmid, grp, normt, datasp);
                    if (c > 0)
                    {
                        DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                }
            }
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
