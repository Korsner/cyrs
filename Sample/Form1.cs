using System;
using System.Data;
using System.Windows.Forms;
using Sample.Controller;

namespace Sample
{
    public partial class Form1 : Form
    {
        Query controller;

        public Form1()
        {
            InitializeComponent();
            controller = new Query(ConnectionString.ConnStr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            newForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show((int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Учетный номер ТС"].Value.ToString())).ToString());
            controller.Delete(int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Учетный номер ТС"].Value.ToString()));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //treeView1.BeginUpdate();
            NodeBuilder();
        }

        public void NodeBuilder()
        {
            DataTable table = controller.Update("SELECT V_FIRM.TLGR AS Фирма, VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.UNTS AS [Учетный номер ТС] FROM((V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID) INNER JOIN TIPTR ON TIPTR.TID = PTS.TID) INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS;");
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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //MessageBox.Show(treeView1.SelectedNode.Name);
            //MessageBox.Show(treeView1.SelectedNode.Level.ToString());
            int caseSwitch = treeView1.SelectedNode.Level;
            //MessageBox.Show(treeView1.SelectedNode.Level.ToString());
            DataTable table0;
            DataTable table1;
            DataTable table2;
            DataTable table3;
            switch (caseSwitch)
            {
                case 0:
                    table0 = controller.Update($"SELECT V_FIRM.TLGR AS Фирма, VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.UNTS AS [Учетный номер ТС] FROM((V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID) INNER JOIN TIPTR ON TIPTR.TID = PTS.TID) INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT WHERE V_FIRM.TLGR = '{treeView1.SelectedNode.Name}' GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS;");
                    dataGridView1.DataSource = table0;
                    break;
                case 1:
                    table1 = controller.Update($"SELECT V_FIRM.TLGR AS Фирма, VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.UNTS AS [Учетный номер ТС] FROM((V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID) INNER JOIN TIPTR ON TIPTR.TID = PTS.TID) INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT WHERE VIDTC.SHNAME = '{treeView1.SelectedNode.Name}' GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS;");
                    dataGridView1.DataSource = table1;
                    break;
                case 2:
                    table2 = controller.Update($"SELECT V_FIRM.TLGR AS Фирма, VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.UNTS AS [Учетный номер ТС] FROM((V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID) INNER JOIN TIPTR ON TIPTR.TID = PTS.TID) INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT WHERE TIPTR.TNAME = '{treeView1.SelectedNode.Name}' GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS;");
                    dataGridView1.DataSource = table2;
                    break;
                case 3:
                    table3 = controller.Update($"SELECT V_FIRM.TLGR AS Фирма,  PTS.UNTS AS [Учетный номер ТС],VIDTC.SHNAME AS [Вид ТС], TIPTR.TNAME AS [Тип ТС], PTS.GRP AS [Грузоподъёмность], PTS.NORMT AS [Расход топлива] FROM ((V_FIRM INNER JOIN PTS ON V_FIRM.FIRMID = PTS.FIRMID) INNER JOIN TIPTR ON TIPTR.TID = PTS.TID) INNER JOIN VIDTC ON VIDTC.VIDT = TIPTR.VIDT WHERE PTS.UNTS = {treeView1.SelectedNode.Name} GROUP BY V_FIRM.TLGR, VIDTC.SHNAME, TIPTR.TNAME, PTS.UNTS, PTS.GRP, PTS.NORMT;");
                    dataGridView1.DataSource = table3;
                    break;
                default:
                    throw new FormatException("Error of tree level");
                    break;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            newForm.Show();
        }
    }
}
