using DbLibrary.Controller;
using Sample.Controller;

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

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            var dr = controller.ReadDetailedData();
            dataGridView1.DataSource = dr;
            if (dr == null)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var unts = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Учетный номер ТС"].Value.ToString()).ToString();
            var c = controller.UpdatePTS(
                textBox3.NullIfEmpty(),
                textBox2.NullIfEmpty(),
                textBox1.NullIfEmpty(),
                textBox4.NullIfEmpty(),
                textBox5.NullIfEmpty(),
                unts
                );
            DialogResult = c > 0 ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
        }
    }
}
