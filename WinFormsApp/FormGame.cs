using ClassLibrary;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace WinFormsApp
{
    public partial class FormGame : Form
    {
        public FormGame(ConfigModule configModule, ListView listViewMain)
        {
            InitializeComponent();

            shipManager = configModule.serviceProvider.GetService<ShipManager>();
            shipHPManager = configModule.serviceProvider.GetService<ShipHPManager>();
            shipIsYourTurnManager = configModule.serviceProvider.GetService<ShipIsYourTurnManager>();
            battleManager = configModule.serviceProvider.GetService<BattleManager>();

            InitializeListViewMain();

            battleManager.InitializeNewBattle();
            UpdateListViewGame(0);

            SetGameOverNotify();   
        }

        private ShipManager shipManager;
        private BattleManager battleManager;
        private ShipHPManager shipHPManager;
        private ShipIsYourTurnManager shipIsYourTurnManager;



        /// <summary>
        /// Создает названия столбцов в ListViewGame.
        /// </summary>
        private void InitializeListViewMain()
        {
            ListViewGame.Items.Clear();
            ListViewGame.View = View.Details;
            ListViewGame.Columns.Add("HP", -2);
            ListViewGame.Columns.Add("Name", -2);
            ListViewGame.Columns.Add("Color", -2);
        }



        /// <summary>
        /// Привязывает методы к уведомлению о конце игры.
        /// </summary>
        private void SetGameOverNotify()
        {
            battleManager.BattleIsOverNotify += ShowGameOverMessage;
            battleManager.BattleIsOverNotify += CloseGameScreen;
        }



        /// <summary>
        /// Кнопка для понижения ХП выбранного корабля.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonAttack_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in ListViewGame.SelectedItems)
            {
                shipHPManager.AttackShipHP(selectedItem.Tag);
                shipIsYourTurnManager.PassTheTurn();
                UpdateListViewGame(ListViewGame.Items.IndexOf(selectedItem));
                battleManager.CheckIfBattleIsOver();
            }
        }



        /// <summary>
        /// Кнопка для восстановления ХП выбранного корабля.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonHeal_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in ListViewGame.SelectedItems)
            {
                shipHPManager.HealShipHP(selectedItem.Tag);
                shipIsYourTurnManager.PassTheTurn();
                UpdateListViewGame(ListViewGame.Items.IndexOf(selectedItem));
            }
        }



        /// <summary>
        /// Актуализирует список кораблей в ListViewGame.
        /// </summary>
        /// <param name="selectedItemIndex">Индекс выделенного в ListViewGame корабля</param>
        private void UpdateListViewGame(int selectedItemIndex)
        {
            ListViewGame.Items.Clear();

            foreach (var ship in shipManager.GetNotDeadShipsList())
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = ship;

                listViewItem.SubItems[0].Text = shipManager.GetShip(ship).Hp.ToString();
                listViewItem.SubItems.Add(shipManager.GetShip(ship).Name.ToString());
                listViewItem.SubItems.Add(shipManager.GetShip(ship).FlagColor.ToString());
                listViewItem.ForeColor = GetColorByFlagColor(ship);

                ListViewGame.Items.Add(listViewItem);     
            }

            SetSelectedItemInListView(ListViewGame, selectedItemIndex);

            labelPlayer.Text = $"Ход {shipIsYourTurnManager.GetTurnShip().Name}";
        }



        /// <summary>
        /// Выделяет нужный item по его индексу в ListView.
        /// </summary>
        /// <param name="selectedItemIndex">Индекс выделенного item.</param>
        private void SetSelectedItemInListView(ListView listView, int selectedItemIndex)
        {
            if (listView.Items.Count - 1 < selectedItemIndex) 
            { 
                listView.Items[listView.Items.Count - 1].Selected = true; 
            }
            else 
            { 
                listView.Items[selectedItemIndex].Selected = true; 
            }
        }



        /// <summary>
        /// Выводит победное сообщение.
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
        /// Возвращает цвет типа Color по свойству FlagColor объекта Ship.
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
