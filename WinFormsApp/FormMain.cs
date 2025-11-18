using ClassLibrary;
using DataAccessLayer;
using Model;
using Microsoft.Extensions.DependencyInjection;


namespace WinFormsApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            configModule = new ConfigModule();
            shipManager = configModule.serviceProvider.GetService<ShipManager>();

            InitializeListViewMain();
            InitializeComboBoxColor();
        }

        private ConfigModule configModule;
        private ShipManager shipManager;



        /// <summary>
        /// Инициализирует ListViewMain.
        /// </summary>
        private void InitializeListViewMain()
        {
            ListViewMain.View = View.Details;
            ListViewMain.Columns.Add("HP", -2);
            ListViewMain.Columns.Add("Name", -2);
            ListViewMain.Columns.Add("Color", -2);

            UpdateViewListMain();
        }



        /// <summary>
        /// Создает combobox с названиями цветов флагов кораблей.
        /// </summary>
        private void InitializeComboBoxColor()
        {
            foreach (var item in FlagColorManager.GetFlagColorNames())
            {
                ComboBoxColor.Items.Add(item);
            }

            ComboBoxColor.SelectedIndex = 0;
        }



        /// <summary>
        /// Добавляет новый корабль в ListView.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonCreateShip_Click(object sender, EventArgs e)
        {
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Tag = shipManager.CreateShip(TextBoxName.Text, ComboBoxColor.SelectedItem);
            TextBoxName.Text = "";

            UpdateViewListMain();   
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
                shipManager.DeleteShip(selectedItem.Tag);

                UpdateViewListMain();
            }
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
                shipManager.ChangeShipName(selectedItem.Tag, TextBoxName.Text);
                shipManager.ChangeFlagColor(selectedItem.Tag, ComboBoxColor.SelectedItem.ToString());
            }

            TextBoxName.Text = "";
            UpdateViewListMain();
        }



        /// <summary>
        /// Открывает окно битвы.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonStartGame_Click(object sender, EventArgs e)
        {
            if (shipManager.GetShipsList().Count > 1)
            {
                FormGame formGame = new FormGame(configModule, ListViewMain);
                formGame.ShowDialog();
            }
            else
            {
                MessageBox.Show("Нужно больше кораблей, капитан!!");
            }

            UpdateViewListMain();
        }



        /// <summary>
        /// Актуализирует список кораблей в ListViewMain.
        /// </summary>
        private void UpdateViewListMain()
        {
            ListViewMain.Items.Clear();

            foreach (var ship in shipManager.GetShipsList())
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = ship;

                listViewItem.SubItems[0].Text = shipManager.GetShip(ship).Hp.ToString();
                listViewItem.SubItems.Add(shipManager.GetShip(ship).Name.ToString());
                listViewItem.SubItems.Add(shipManager.GetShip(ship).FlagColor.ToString());

                listViewItem.ForeColor = GetColorByFlagColor(ship);

                ListViewMain.Items.Add(listViewItem);
            }
        }



        /// <summary>
        /// Выводит окно с пояснениями.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Helper.GetHelpText());
        }



        /// <summary>
        /// Возвращает цвет типа Color по цвету флага корабля.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <returns>Цвет типа Color.</returns>
        private Color GetColorByFlagColor(object ship)
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
