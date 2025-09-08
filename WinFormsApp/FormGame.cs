using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinFormsApp
{
    public partial class FormGame : Form
    {
        public FormGame(ListView listView)
        {
            InitializeComponent();

            ListViewGame.Items.Clear();
            ListViewGame.View = View.Details;
            ListViewGame.Columns.Add("HP", -2);
            ListViewGame.Columns.Add("Name", -2);
            ListViewGame.Columns.Add("Color", -2);

            ListViewGame.Items.AddRange((from ListViewItem item in listView.Items
                                         select (ListViewItem)item.Clone()).ToArray());

        }



        private void ButtonAttack_Click(object sender, EventArgs e)
        {

        }



        private void ButtonHeal_Click(object sender, EventArgs e)
        {

        }
    }
}
