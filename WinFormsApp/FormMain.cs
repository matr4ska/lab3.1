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

            foreach (var item in logic.GetColorFlagNames())
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
            Logic logic = (Logic)DataContext;

            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Tag = logic.CreateShip(TextBoxName.Text, ComboBoxColor.SelectedItem);
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
            Logic logic = (Logic)DataContext;

            foreach (ListViewItem selectedItem in ListViewMain.SelectedItems)
            {
                logic.DeleteShip(selectedItem.Tag);

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
            Logic logic = (Logic)DataContext;

            foreach (ListViewItem selectedItem in ListViewMain.SelectedItems)
            {
                logic.ChangeShipAttributes(selectedItem.Tag, TextBoxName.Text, ComboBoxColor.SelectedItem.ToString());
            }

            TextBoxName.Text = "";
            UpdateViewListMain();
        }



        /// <summary>
        /// Открывает игровое окно.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Доп. информация о событии для обработчика.</param>
        private void ButtonStartGame_Click(object sender, EventArgs e)
        {
            Logic logic = (Logic)DataContext;

            FormGame formGame = new FormGame((Logic)DataContext, ListViewMain);
            formGame.ShowDialog();
            
            UpdateViewListMain();
        }



        /// <summary>
        /// Актуализирует отображение объектов в ListView.
        /// </summary>
        private void UpdateViewListMain()
        {
            Logic logic = (Logic)DataContext;

            ListViewMain.Items.Clear();

            foreach (var ship in logic.GetShipsList())
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = ship;

                listViewItem.SubItems[0].Text = logic.GetShip(ship).Hp.ToString();
                listViewItem.SubItems.Add(logic.GetShip(ship).Name.ToString());
                listViewItem.SubItems.Add(logic.GetShip(ship).FlagColor.ToString());

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
            Logic logic = (Logic)DataContext;

            MessageBox.Show(logic.GetHelpText());
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
