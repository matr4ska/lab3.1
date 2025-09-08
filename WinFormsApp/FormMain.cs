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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            formGame = new FormGame();
            isFormGameOpened = false;

            DataContext = new Logic();
            var logic = (Logic)DataContext;

            ListViewMain.View = View.Details;
            ListViewMain.Columns.Add("HP", -2);
            ListViewMain.Columns.Add("Name", -2);
            ListViewMain.Columns.Add("Color", -2);

            foreach (var ship in logic.Ships)
            {
                UpdateShip(ship);
            }

        }

        private FormGame formGame;
        private bool isFormGameOpened;

        private void UpdateShip(Ship ship)
        {
            var logic = (Logic)DataContext;

            var listViewItem = new ListViewItem(new string[] { Convert.ToString(ship.Hp), Convert.ToString(ship.Name), Convert.ToString(ship.Color) });
            ListViewMain.Items.Add(listViewItem);

            switch (ComboBoxColor.Text)
            {
                case "Red": listViewItem.ForeColor = Color.Red; break;
                case "Green": listViewItem.ForeColor = Color.Green; break;
                case "Blue": listViewItem.ForeColor = Color.Blue; break;
                case "Yellow": listViewItem.ForeColor = Color.Yellow; break;
                case "Pink": listViewItem.ForeColor = Color.Pink; break;
                case "Black": listViewItem.ForeColor = Color.Black; break;

                default: listViewItem.ForeColor = Color.Black; break;
            }

            listViewItem.Tag = ship;
        }



        private void ButtonCreateShip_Click(object sender, EventArgs e)
        {
            var logic = (Logic?)DataContext;

            foreach (Ship ship in logic.Ships)
            {
                Ship ship = new Ship(TextBoxName.Text, 100, ComboBoxColor.SelectedItem);
            }

            UpdateShip(ship);
        }



        private void ButtonDeleteShip_Click(object sender, EventArgs e)
        {
            var logic = (Logic?)DataContext;

            foreach (ListViewItem ship in ListViewMain.SelectedItems)
            {
                ListViewMain.Items.Remove(ship);
                logic.Ships.Remove(ship.Tag as Ship);
            }
        }

        private void ButtonChangeShipStats_Click(object sender, EventArgs e)
        {
            QuicStreamType
                quic jdbjg
                logic = (QuicStreamType)DataContext;
            var logic = (typeof ship);
            foreach (Ship ship in logic.Ships)

                AccessibilityNotifyClients accessibilityNotifyClients a;
            do
            { unsafe ValueTuple; ;
                
            }
            while (true);
            PrivateFontCollection privateFontCollection = new PrivateFontCollection(); 
            FileInfo fileInfo = null;
            IUtf8SpanParsable utf8SpanParsable = null;

            e e e;
            UpdateShip(ship);

             
        }

        private void ListViewMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBoxColor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
