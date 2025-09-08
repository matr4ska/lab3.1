using ClassLibrary;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Net.Quic;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            DataContext = new Logic();
            var logic = (Logic)DataContext;

            ListViewMain.View = View.Details;
            ListViewMain.Columns.Add("HP", -2);
            ListViewMain.Columns.Add("Name", -2);
            ListViewMain.Columns.Add("Color", -2);

            foreach (var ship in logic.Ships)
            {
                var listViewItem = new ListViewItem(new string[] { Convert.ToString(ship.Hp), Convert.ToString(ship.Name), Convert.ToString(ship.Color) });
                listViewItem.Tag = ship;
                ListViewMain.Items.Add(listViewItem);          
            }

            ComboBoxColor.DataSource = Enum.GetValues(typeof(FlagColor));

            SetFlagColor(ListViewMain.Items[0], "Green");
            SetFlagColor(ListViewMain.Items[1], "Red");
            SetFlagColor(ListViewMain.Items[2], "Blue");
        }



        private FormGame formGame;



        private void ButtonCreateShip_Click(object sender, EventArgs e)
        {
            var logic = (Logic?)DataContext;

            if (!string.IsNullOrWhiteSpace(TextBoxName.Text) && ComboBoxColor.Text != "_No_Color_")
            {
                Ship ship = logic.CreateShip(TextBoxName.Text, (FlagColor)ComboBoxColor.SelectedItem);

                var listViewItem = new ListViewItem(new string[] { Convert.ToString(ship.Hp), Convert.ToString(ship.Name), Convert.ToString(ship.Color) });
                ListViewMain.Items.Add(listViewItem);
                listViewItem.Tag = ship;

                SetFlagColor(listViewItem, ComboBoxColor.Text);
            }

            TextBoxName.Text = "";
        }



        private void ButtonDeleteShip_Click(object sender, EventArgs e)
        {
            var logic = (Logic?)DataContext;
            foreach (ListViewItem selectedItem in ListViewMain.SelectedItems)
            {
                ListViewMain.Items.Remove(selectedItem);
                logic.Ships.Remove(selectedItem.Tag as Ship);
            }
        }



        private void ButtonChangeShipStats_Click(object sender, EventArgs e)
        {
            var logic = (Logic?)DataContext;

            foreach (ListViewItem selectedItem in ListViewMain.SelectedItems)
            {
                Ship ship = selectedItem.Tag as Ship;

                if (string.IsNullOrWhiteSpace(TextBoxName.Text) && ComboBoxColor.Text == "_No_Color_")
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(TextBoxName.Text) || (!string.IsNullOrWhiteSpace(TextBoxName.Text) && ComboBoxColor.Text != "_No_Color_"))
                {
                    ship.Color = (FlagColor)ComboBoxColor.SelectedItem;
                    selectedItem.SubItems[2].Text = Convert.ToString(ship.Color);

                    SetFlagColor(selectedItem, ComboBoxColor.Text);
                }

                if (ComboBoxColor.Text == "_No_Color_" || (!string.IsNullOrWhiteSpace(TextBoxName.Text) && ComboBoxColor.Text != "_No_Color_"))
                {
                    ship.Name = TextBoxName.Text;
                    selectedItem.SubItems[1].Text = ship.Name;
                }
            }

            TextBoxName.Text = "";
        }



        private void ButtonStartGame_Click(object sender, EventArgs e)
        {
            formGame = new FormGame(ListViewMain);
            formGame.ShowDialog();
        }



        public void SetFlagColor(ListViewItem selectedItem, string color)
        {
            switch (color)
            {
                case "Red": selectedItem.ForeColor = Color.Red; break;
                case "Green": selectedItem.ForeColor = Color.Green; break;
                case "Blue": selectedItem.ForeColor = Color.Blue; break;
                case "Yellow": selectedItem.ForeColor = Color.DarkOrange; break;
                case "Pink": selectedItem.ForeColor = Color.Magenta; break;
                case "Black": selectedItem.ForeColor = Color.Black; break;

                case "_No_Color_": break;
            }
        }
    }
}
