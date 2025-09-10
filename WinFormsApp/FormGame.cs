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
        public FormGame(Logic logic, ListView listViewMain)
        {
            InitializeComponent();

            DataContext = logic;
            logic = (Logic)DataContext;

            ListViewGame.Items.Clear();
            ListViewGame.View = View.Details;
            ListViewGame.Columns.Add("HP", -2);
            ListViewGame.Columns.Add("Name", -2);
            ListViewGame.Columns.Add("Color", -2);

            ListViewGame.Items.AddRange((from ListViewItem item in listViewMain.Items
                                         select (ListViewItem)item
                                         .Clone())
                                         .ToArray());

            for(int i = 0; i < logic.Ships.Count; i++)
            {
                ListViewGame.Items[i].Tag = logic.Ships[i];
            }

            labelPlayer.Text = $"Ход {logic.PassTheTurn().Name}";
        }



        private void ButtonAttack_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;
            short currentHp;

            foreach (ListViewItem selectedItem in ListViewGame.SelectedItems)
            {
                labelPlayer.Text = $"Ход {logic.PassTheTurn().Name}";

                currentHp = logic.AttackShip(selectedItem.Tag);
                selectedItem.SubItems[0].Text = currentHp.ToString();

                if (logic.CheckShipBeaten(selectedItem.Tag) == true)
                {
                    ListViewGame.Items.Remove(selectedItem);
                }

                if (logic.CheckGameOver() == true)
                {
                    labelPlayer.Text = $"{logic.PassTheTurn().Name} победил";
                    MessageBox.Show("Победа");
                    base.Close();
                }
            }
        }



        private void ButtonHeal_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;
            short currentHp;

            foreach (ListViewItem selectedItem in ListViewGame.SelectedItems)
            {
                labelPlayer.Text = $"Ход {logic.PassTheTurn().Name}";

                currentHp = logic.HealShip(selectedItem.Tag);
                selectedItem.SubItems[0].Text = currentHp.ToString();
            }
        }
    }
}
