using ClassLibrary.Interfaces;
using ClassLibrary.ModelEventArgs;
using Shared.ViewMain_EventArgs;
using Shared.WinForms_Interfaces;
using Shared.Console_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.ConsolePresenter
{
    public class MainConsolePresenter
    {
        IFlagColorManager flagColorManager;
        IHelper helper;
        IShipManager shipManager;

        IConsoleMainView mainView;


        public MainConsolePresenter(IConsoleMainView mainView, IShipManager shipManager, IHelper helper, IFlagColorManager flagColorManager)
        {
            this.mainView = mainView;
            this.shipManager = shipManager;
            this.helper = helper;
            this.flagColorManager = flagColorManager;


            mainView.OnShipCreated += inModelCreateShip;

            mainView.OnShipListUpdated += inModelUpdateShipList;
            shipManager.OnShipListUpdated += inViewUpdateShipList;

            mainView.OnShipDeleted += inModelDeleteShip;

            mainView.OnShipNameChanged += inModelChangeShipName;

            mainView.OnShipFlagColorChanged += inModelChangeShipFlagColor;

            mainView.OnFlagColorNamesRequested += inModelGetFlagColorNamesList;
            flagColorManager.OnFlagColorNamesRequested += inViewGetFlagColorNamesList;
        }


        /// <summary>
        /// Просит модель создать новый корабль
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Данные для события</param>
        public void inModelCreateShip(object sender, ViewMain_OnShipCreatedEventArgs e)
        {
            shipManager.CreateShip(e.Name, e.FlagColor);
        }

        /// <summary>
        /// Передает представлению список всех кораблей
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Данные для события</param>
        public void inViewUpdateShipList(object sender, OnShipListUpdatedEventArgs e)
        {
            mainView.UpdateShipList(e.Ships);
        }

        /// <summary>
        /// Просит у модели вернуть список всех кораблей
        /// </summary>
        public void inModelUpdateShipList()
        {
            shipManager.GetShipsListInView();
        }

        /// <summary>
        /// Просит у модели удалить корабль
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Данные для события</param>
        public void inModelDeleteShip(object sender, ViewMain_OnShipDeletedEventArgs e)
        {
            shipManager.DeleteShip(e.Id);
        }

        /// <summary>
        /// Просит у модели изменить название кораблю
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Данные для события</param>
        public void inModelChangeShipName(object sender, ViewMain_OnShipNameChangedEventArgs e)
        {
            shipManager.ChangeShipName(e.Id, e.Name);
        }

        /// <summary>
        /// Просит у модели изменить цвет флага кораблю
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Данные для события</param>
        public void inModelChangeShipFlagColor(object sender, ViewMain_OnShipFlagColorChangedEventArgs e)
        {
            shipManager.ChangeFlagColor(e.Id, e.FlagColor);
        }

        /// <summary>
        /// Передает в представление список названий доступных цветов флагов
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Данные для события</param>
        public void inViewGetFlagColorNamesList(object sender, OnFlagColorNamesRequestedEventArgs e)
        {
            mainView.InitializeFlagColorOptions(e.FlagColorNames);
        }

        /// <summary>
        /// Запрашивает у модели список названий доступных цветов флагов
        /// </summary>
        public void inModelGetFlagColorNamesList()
        {
            flagColorManager.GetFlagColorNames();
        }
    }
}





