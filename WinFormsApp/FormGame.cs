using ClassLibrary;
using ClassLibrary.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Shared.ViewGame_EventArgs;
using Shared.WinForms_Interfaces;

namespace WinFormsApp
{
    public partial class FormGame : Form, IGameView
    {
        public event EventHandler<ViewGame_OnShipAttackedEventArgs> OnShipAttacked;
        public event EventHandler<ViewGame_OnShipHealedEventArgs> OnShipHealed;
        public event Action OnShipsInBattleListUpdated;
        public event Action OnNewBattleStarted;
        public event Action OnCheckIfGameIsOver;
        public event Action OnPassTheTurn;
        public event Action OnGetTurnShip;


        int selectedShipIndex;

        public FormGame()
        {
            InitializeComponent();
            selectedShipIndex = 0;
        }



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
            ListViewGame.Columns.Add("Id", -2);

            OnShipsInBattleListUpdated();
        }



        /// <summary>
        /// Кнопка для понижения ХП выбранного корабля.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonAttack_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedShip in ListViewGame.SelectedItems)
            {
                AttackShip(selectedShip.SubItems[3].Text);
                EndTheTurn(ListViewGame.Items.IndexOf(selectedShip));
            }
        }

       

        /// <summary>
        /// Кнопка для восстановления ХП выбранного корабля.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonHeal_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedShip in ListViewGame.SelectedItems)
            {      
                HealShip(selectedShip.SubItems[3].Text);
                EndTheTurn(ListViewGame.Items.IndexOf(selectedShip));
            }
        }



        public void AttackShip(string id)
        {
            OnShipAttacked(this, new ViewGame_OnShipAttackedEventArgs(id));
        }



        public void HealShip(string id)
        {
            OnShipHealed(this, new ViewGame_OnShipHealedEventArgs(id));
        }



        public void PassTheTurn()
        {
            OnPassTheTurn();
        }



        public void UpdateShipsInBattle()
        {
            OnShipsInBattleListUpdated();
        }



        public void SetTurnShip()
        {
            OnGetTurnShip();
        }



        public void EndTheTurn(int selectedShipIndex)
        {
            this.selectedShipIndex = selectedShipIndex;
            PassTheTurn();
            UpdateShipsInBattle();
            TryEndBattle();
            SetTurnShip();
        }



        public void TryEndBattle()
        {
            OnCheckIfGameIsOver();
        }



        /// <summary>
        /// Актуализирует список кораблей в ListViewGame.
        /// </summary>
        /// <param name="selectedItemIndex">Индекс выделенного в ListViewGame корабля</param>
        public void UpdateShipsInBattleList(List<List<string>> shipsInBattle)
        {
            ListViewGame.Items.Clear();

            for (int i = 0; i < shipsInBattle.Count(); i++)
            {
                ListViewItem listViewItem = new ListViewItem();

                listViewItem.SubItems[0].Text = shipsInBattle[i][0];
                listViewItem.SubItems.Add(shipsInBattle[i][1]);
                listViewItem.SubItems.Add(shipsInBattle[i][2]);
                listViewItem.SubItems.Add(shipsInBattle[i][3]);
                listViewItem.ForeColor = GetColorByFlagColor(shipsInBattle[i][2]);

                ListViewGame.Items.Add(listViewItem);
            }

            SetSelectedItemInListView(ListViewGame, selectedShipIndex);      
        }



        public void ChangeTurnShipName(string name)
        {
            labelPlayer.Text = $"Ход {name}";
        }



        /// <summary>
        /// Выделяет нужный item по его индексу в ListView.
        /// </summary>
        /// <param name="selectedItemIndex">Индекс выделенного item.</param>
        private void SetSelectedItemInListView(ListView listView, int selectedItemIndex)
        {
            if (selectedItemIndex >= 0)
            {
                if (listView.Items.Count - 1 <= selectedItemIndex)
                {
                    listView.Items[listView.Items.Count - 1].Selected = true;
                }

                else
                {
                    listView.Items[selectedItemIndex].Selected = true;
                }
            }
        }



        /// <summary>
        /// Выводит победное сообщение.
        /// </summary>
        public void ShowGameOverMessage()
        {
            MessageBox.Show($"Победа за {ListViewGame.Items[0].SubItems[1].Text}!!!");
        }



        public void StartNewBattle()
        {
            OnNewBattleStarted();
        }



        /// <summary>
        /// 
        /// </summary>
        public void ResetGame()
        {
            StartNewBattle();
            UpdateShipsInBattle();

            selectedShipIndex = 0;
            SetSelectedItemInListView(ListViewGame, selectedShipIndex);
            SetTurnShip();
        }



        /// <summary>
        /// Закрывает окно игры.
        /// </summary>
        public void CloseGameScreen()
        {  
            base.Close();
        }



        /// <summary>
        /// Возвращает цвет типа Color по свойству FlagColor объекта Ship.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <returns>Цвет типа Color.</returns>
        private Color GetColorByFlagColor(string flagColor)
        {
            switch (flagColor)
            {
                case "Red": return Color.Red;
                case "Green": return Color.Green;
                case "Blue": return Color.Blue;
                case "Yellow": return Color.DarkOrange;
                case "Pink": return Color.Magenta;

                default: return Color.Black;
            }
        }



        public void InitializeGameFormFirstTime()
        {
            InitializeListViewMain();
            ResetGame();
        }
    }
}
