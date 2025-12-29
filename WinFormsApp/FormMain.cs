using Microsoft.VisualBasic.Logging;
using Model;
using Shared.ViewMain_EventArgs;
using Shared.WinForms_Interfaces;

namespace WinFormsApp
{
    public partial class FormMain : Form, IMainView
    {
        public event EventHandler<ViewMain_OnShipCreatedEventArgs> OnShipCreated;
        public event EventHandler<ViewMain_OnShipDeletedEventArgs> OnShipDeleted;
        public event EventHandler<ViewMain_OnShipNameChangedEventArgs> OnShipNameChanged;
        public event EventHandler<ViewMain_OnShipFlagColorChangedEventArgs> OnShipFlagColorChanged;
        public event Action OnShipListUpdated;
        public event Action OnHelpTextRequested;
        public event Action OnFlagColorNamesRequested;

        public FormGame formGame = new FormGame();

        public FormMain()
        {
            InitializeComponent(); 
        }

        



        /// <summary>
        /// Добавляет новый корабль в ListView.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonCreateShip_Click(object sender, EventArgs e)
        {
            CreateShip(TextBoxName.Text, ComboBoxColor.SelectedItem.ToString());

            TextBoxName.Text = "";
            OnShipListUpdated();
        }



        /// <summary>
        /// Удаляет выбранный корабль из ListView.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonDeleteShip_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in ListViewMain.SelectedItems)
            {
                DeleteShip(selectedItem.SubItems[3].Text);    
            }

            OnShipListUpdated();
        }



        /// <summary>
        /// Меняет название и цвет выбранного корабля в ListView.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonChangeShipStats_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in ListViewMain.SelectedItems)
            {
                ChangeShipName(selectedItem.SubItems[3].Text, TextBoxName.Text);
                ChangeShipFlagColor(selectedItem.SubItems[3].Text, ComboBoxColor.SelectedItem.ToString());
            }

            TextBoxName.Text = "";
            OnShipListUpdated();
        }



        /// <summary>
        /// Открывает игровое окно.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonStartGame_Click(object sender, EventArgs e)
        {
            StartGame();

            formGame.ResetGame();
            OnShipListUpdated();
        }






        /// <summary>
        /// Выводит окно с пояснениями.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            OnHelpTextRequested();
        }



        /// <summary>
        /// Возвращает цвет типа Color по цвету флага корабля.
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

        public void InitializeShipList()
        {
            ListViewMain.View = View.Details;
            ListViewMain.Columns.Add("HP", -2);
            ListViewMain.Columns.Add("Name", -2);
            ListViewMain.Columns.Add("Color", -2);
            ListViewMain.Columns.Add("Id", -2);

            OnShipListUpdated();
        }

        public void InitializeFlagColorOptions(List<string> flagColorNames)
        {
            foreach (var item in flagColorNames)
            {
                ComboBoxColor.Items.Add(item);
            }

            ComboBoxColor.SelectedIndex = 0;
        }

        public void InitializeMainFormFirstTime()
        {
            InitializeShipList();
            OnFlagColorNamesRequested();
            OnShipListUpdated();
        }

        public void CreateShip(string name, string flagColor)
        {
            OnShipCreated(this, new ViewMain_OnShipCreatedEventArgs(name, flagColor));
        }

        public void DeleteShip(string id)
        {
            OnShipDeleted(this, new ViewMain_OnShipDeletedEventArgs(id));
        }

        public void ChangeShipName(string id, string name)
        {
            OnShipNameChanged(this, new ViewMain_OnShipNameChangedEventArgs(id, name));
        }

        public void ChangeShipFlagColor(string id, string flagColor)
        {
            OnShipFlagColorChanged(this, new ViewMain_OnShipFlagColorChangedEventArgs(id, flagColor));
        }

        public void UpdateShipList(List<List<string>> shipsProperties)
        {
            ListViewMain.Items.Clear();

            for (int i = 0; i < shipsProperties.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem();

                listViewItem.SubItems[0].Text = shipsProperties[i][0];
                listViewItem.SubItems.Add(shipsProperties[i][1]);
                listViewItem.SubItems.Add(shipsProperties[i][2]);
                listViewItem.SubItems.Add(shipsProperties[i][3]);

                listViewItem.ForeColor = GetColorByFlagColor(shipsProperties[i][2]);

                ListViewMain.Items.Add(listViewItem);
            }
        }

        public void StartGame()
        {    
            formGame.ShowDialog(); 
        }

        public void ShowHelp(string helpText)
        {
            MessageBox.Show(helpText);
        }
    }
}
