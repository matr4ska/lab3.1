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
using ClassLibrary;

namespace WinFormsApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            DataContext = new Logic();
            Logic logic = (Logic)DataContext;

            ListViewMain.View = View.Details;
            ListViewMain.Columns.Add("HP", -2);
            ListViewMain.Columns.Add("Name", -2);
            ListViewMain.Columns.Add("Color", -2);

            UpdateViewListMain();

            ComboBoxColor.DataSource = Enum.GetValues(typeof(FlagColor));
        }



        private FormGame formGame;



        public void UpdateViewListMain()
        {
            Logic logic = (Logic)DataContext;

            ListViewMain.Items.Clear();

            foreach (Ship ship in logic.Ships)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = ship;
                listViewItem.SubItems[0].Text = logic.GetShip(listViewItem.Tag).Hp.ToString();
                listViewItem.SubItems.Add(logic.GetShip(listViewItem.Tag).Name.ToString());
                listViewItem.SubItems.Add(logic.GetShip(listViewItem.Tag).FlagColor.ToString());

                SetRowColor(listViewItem, ship.FlagColor.ToString());

                ListViewMain.Items.Add(listViewItem);
            }
        }



        private void ButtonCreateShip_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;

            if (!string.IsNullOrWhiteSpace(TextBoxName.Text) && ComboBoxColor.Text != "_No_Color_")
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = logic.CreateShip(TextBoxName.Text, ComboBoxColor.SelectedItem);

                TextBoxName.Text = "";
            }
            UpdateViewListMain();
        }



        private void ButtonDeleteShip_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;

            foreach (ListViewItem selectedItem in ListViewMain.SelectedItems)
            {
                logic.DeleteShip(selectedItem.Tag);
                UpdateViewListMain();
            }
        }



        private void ButtonChangeShipStats_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;

            foreach (ListViewItem selectedItem in ListViewMain.SelectedItems)
            {
                if (string.IsNullOrWhiteSpace(TextBoxName.Text) && ComboBoxColor.Text == "_No_Color_")
                {
                    return;
                }

                if (ComboBoxColor.Text == "_No_Color_" || (!string.IsNullOrWhiteSpace(TextBoxName.Text) && ComboBoxColor.Text != "_No_Color_"))
                {
                    logic.ChangeShipAttributes(selectedItem.Tag, TextBoxName.Text);
                    UpdateViewListMain();
                }

                if (string.IsNullOrWhiteSpace(TextBoxName.Text) || (!string.IsNullOrWhiteSpace(TextBoxName.Text) && ComboBoxColor.Text != "_No_Color_"))
                {
                    logic.ChangeShipAttributes(selectedItem.Tag, ComboBoxColor.SelectedItem);
                    UpdateViewListMain();
                }       
            }
            TextBoxName.Text = "";
        }



        private void ButtonStartGame_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;
            logic.RecoverHP();
            UpdateViewListMain();
            formGame = new FormGame((Logic)DataContext, ListViewMain);
            formGame.ShowDialog();
            UpdateViewListMain();
        }



        public void SetRowColor(ListViewItem selectedItem, string color)
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
