using ClassLibrary;
using Microsoft.VisualBasic.Logging;
using System.Data;

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

            logic.InitializeShipsInBattleList();
            ChangePlayerTurn();
        }



        /// <summary>
        /// Кнопка для понижения ХП выбранного корабля.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonAttack_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;

            foreach (ListViewItem selectedItem in ListViewGame.SelectedItems)
            {
                ChangePlayerTurn();
                selectedItem.SubItems[0].Text = logic.GetAttackedShipHP(selectedItem.Tag).ToString();

                if (logic.CheckShipBeaten(selectedItem.Tag))
                {
                    selectedItem.Remove();
                }

                if (logic.CheckGameOver() == true)
                {
                    logic.PassTheTurn();
                    labelPlayer.Text = $"{logic.GetTurnShip().GetType().GetProperty("Name").GetValue(logic.GetTurnShip())} победил";

                    MessageBox.Show("Победа");
                    base.Close();
                }
            }
        }



        /// <summary>
        /// Кнопка для восстановления ХП выбранного корабля.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonHeal_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;

            foreach (ListViewItem selectedItem in ListViewGame.SelectedItems)
            {
                ChangePlayerTurn();
                selectedItem.SubItems[0].Text = logic.GetHealedShipHP(selectedItem.Tag).ToString();
            }
        }



        /// <summary>
        /// Вызывает метод смены хода. Меняет надпись, какой игрок сейчас ходит.
        /// </summary>
        private void ChangePlayerTurn()
        {
            Logic logic = (Logic)DataContext;

            logic.PassTheTurn();
            labelPlayer.Text = $"Ход {logic.GetTurnShip().GetType().GetProperty("Name").GetValue(logic.GetTurnShip())}";  
        }
    }
}
