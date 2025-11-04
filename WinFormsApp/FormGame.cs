using ClassLibrary;
using Microsoft.VisualBasic.Logging;
using Model;

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

            logic.InitializeGame();

            logic.GameOverNotify += ShowGameOverMessage;
            logic.GameOverNotify += CloseGameScreen;

            UpdateListViewGame(0);
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
                logic.AttackShipHP(selectedItem.Tag);
                InitializeNextTurn(selectedItem);
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
                logic.HealShipHP(selectedItem.Tag);
                InitializeNextTurn(selectedItem);
            }
        }



        /// <summary>
        /// Делает все нужное для инициализации следующего хода.
        /// </summary>
        /// <param name="selectedItem"></param>
        private void InitializeNextTurn(ListViewItem selectedItem)
        {
            Logic logic = (Logic)DataContext;

            logic.PassTheTurn();
            UpdateListViewGame(ListViewGame.Items.IndexOf(selectedItem));
            logic.CheckIfGameIsOver();
        }



        /// <summary>
        /// Актуализирует отображение объектов в ListViewGame.
        /// </summary>
        /// <param name="selectedItemIndex">Индекс выделенного в ListViewGame корабля</param>
        private void UpdateListViewGame(int selectedItemIndex)
        {
            Logic logic = (Logic)DataContext;

            ListViewGame.Items.Clear();

            foreach (var ship in logic.GetShipsInBattleList())
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = ship;

                listViewItem.SubItems[0].Text = logic.GetShip(ship).Hp.ToString();
                listViewItem.SubItems.Add(logic.GetShip(ship).Name.ToString());
                listViewItem.SubItems.Add(logic.GetShip(ship).FlagColor.ToString());
                listViewItem.ForeColor = GetColorByFlagColor(ship);

                ListViewGame.Items.Add(listViewItem);     
            }

            if (ListViewGame.Items.Count - 1 < selectedItemIndex) { ListViewGame.Items[0].Selected = true; }
            else { ListViewGame.Items[selectedItemIndex].Selected = true; }

            labelPlayer.Text = $"Ход {logic.GetTurnShip().Name}";
        }



        /// <summary>
        /// Выводит победное сообщение и закрывает окно игры.
        /// </summary>
        private void ShowGameOverMessage()
        {
            MessageBox.Show($"Победа за {ListViewGame.Items[0].SubItems[1].Text}!!!");
        }



        /// <summary>
        /// Закрывает окно игры.
        /// </summary>
        private void CloseGameScreen()
        {
            base.Close();
        }



        /// <summary>
        /// Возвращает цвет типа Color по цвету флага корабля.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <returns>Цвет типа Color.</returns>
        public Color GetColorByFlagColor(object ship)
        {
            switch (((Ship)ship).FlagColor)
            {
                case FlagColor.Red: return Color.Red;
                case FlagColor.Green: return Color.Green;
                case FlagColor.Blue: return Color.Blue;
                case FlagColor.Yellow: return Color.DarkOrange;
                case FlagColor.Pink: return Color.Magenta;

                default: return Color.Black;
            }
        }
    }
}
