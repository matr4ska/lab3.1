namespace WinFormsApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            formGame = new FormGame();
            isFormGameOpened = false;
        }

        private FormGame formGame;
        private bool isFormGameOpened;







    }
}
