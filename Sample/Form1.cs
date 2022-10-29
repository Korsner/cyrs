using DbLibrary.Controller;
using Sample.Controller;
using System.Data;

namespace Sample
{
    public partial class Form1 : Form
    {
        readonly Query controller;

        public Form1()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var newForm = new Form3();
            newForm.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var newForm = new Form2();
            newForm.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var unts = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Учетный номер ТС"].Value.ToString());
            controller.DeletePTS(unts);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NodeBuilder();
        }

        public void NodeBuilder()
        {
            DataTable table = controller.ReadData();
            foreach (DataRow row in table.Rows)
            {
                if (!treeView1.Nodes.ContainsKey(row[0].ToString()))
                {
                    treeView1.Nodes.Add(row[0].ToString(), row[0].ToString());
                }
                if (!treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes.ContainsKey(row[1].ToString()))
                {
                    treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes.Add(row[1].ToString(), row[1].ToString());
                }
                if (!treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes[treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes.IndexOfKey(row[1].ToString())].Nodes.ContainsKey(row[2].ToString()))
                {
                    treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes[treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes.IndexOfKey(row[1].ToString())].Nodes.Add(row[2].ToString(), row[2].ToString());
                }
                if (!treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes[treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes.IndexOfKey(row[1].ToString())].Nodes[treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes[treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes.IndexOfKey(row[1].ToString())].Nodes.IndexOfKey(row[2].ToString())].Nodes.ContainsKey(row[3].ToString()))
                {
                    treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes[treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes.IndexOfKey(row[1].ToString())].Nodes[treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes[treeView1.Nodes[treeView1.Nodes.IndexOfKey(row[0].ToString())].Nodes.IndexOfKey(row[1].ToString())].Nodes.IndexOfKey(row[2].ToString())].Nodes.Add(row[3].ToString(), row[3].ToString());
                }
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int caseSwitch = treeView1.SelectedNode.Level;
            var param = treeView1.SelectedNode.Name;
            DataTable table = caseSwitch switch
            {
                0 => controller.ReadDataByTLGR(param),
                1 => controller.ReadDataBySHNAME(param),
                2 => controller.ReadDataByTNAME(param),
                3 => controller.ReadDataByUNTS(param),
                _ => throw new FormatException("Error of tree level"),
            };
            dataGridView1.DataSource = table;
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            newForm.Show();
        }
    }
}
