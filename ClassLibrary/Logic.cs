using Model;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace ClassLibrary
{
    public class Logic
    {
        private List<Ship> Ships = new List<Ship>
        {
            new Ship("Vista", FlagColor.Green),
            new Ship("Kingslayer", FlagColor.Red),
            new Ship("ObraDinn", FlagColor.Blue)
        };


        private List<Ship> ShipsInBattle = new List<Ship>();





        /// <summary>
        /// Создает объект корабля, добавляет его в общий список кораблей.
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="flagColor">Цвет</param>
        /// <returns>Объект корабля.</returns>
        public Ship CreateShip(string name, object flagColor)
        {
            Ship ship = new Ship(name, FlagColor.Black);
            SetFlagColor(ship, flagColor.ToString());
            Ships.Add(ship);
            return ship;
        }


        /// <summary>
        /// Удаляет объект корабля из общего списка кораблей.
        /// </summary>
        /// <param name="ship">Объект корабля.</param>
        public void DeleteShip(object ship) => Ships.Remove((Ship)ship);


        /// <summary>
        /// Возвращает объект корабля.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns>Объект корабля.</returns>
        public Ship GetShip(object ship) => Ships[Ships.IndexOf((Ship)ship)];


        /// <summary>
        /// Возвращает общий список кораблей.
        /// </summary>
        /// <returns>Общий список кораблей.</returns>
        public List<Ship> GetShipsList() => Ships;


        /// <summary>
        /// Меняет название и цвет флага корабля.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <param name="name">Новое название</param>
        /// <param name="flagColor">Новый цвет</param>
        public void ChangeShipAttributes(object ship, string name, string flagColor)
        {
            if (string.IsNullOrWhiteSpace(name) && flagColor == "_No_Color_")
                return;

            if (flagColor == "_No_Color_" || (!string.IsNullOrWhiteSpace(name) && flagColor != "_No_Color_"))
                Ships[Ships.IndexOf((Ship)ship)].Name = name;

            if (string.IsNullOrWhiteSpace(name) || (!string.IsNullOrWhiteSpace(name) && flagColor != "_No_Color_"))
                SetFlagColor((Ship)ship, flagColor);
        }





        /// <summary>
        /// Возвращает список названий возможных цветов флага из перечисления цветов флага.
        /// </summary>
        /// <returns>Строковый список возможных цветов флага.</returns>
        public List<string> GetColorFlagNames() => Enum.GetNames(typeof(FlagColor)).ToList();


        /// <summary>
        /// Задает объекту корабля цвет флага по названию цвета.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <param name="color">Цвет</param>
        private void SetFlagColor(Ship ship, string color)
        {
            switch (color)
            {
                case "Red": ship.FlagColor = FlagColor.Red; break;
                case "Green": ship.FlagColor = FlagColor.Green; break;
                case "Blue": ship.FlagColor = FlagColor.Blue; break;
                case "Yellow": ship.FlagColor = FlagColor.Yellow; break;
                case "Pink": ship.FlagColor = FlagColor.Pink; break;
                case "Black": ship.FlagColor = FlagColor.Black; break;

                default: ship.FlagColor = FlagColor._No_Color_; break;
            }
        }


        /// <summary>
        /// Возвращает цвет по цвету флага корабля.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <returns>Цвет.</returns>
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





        /// <summary>
        /// Создает новый список кораблей для новой игры.
        /// </summary>
        public void InitializeShipsInBattleList()
        {
            ShipsInBattle.Clear();
            ShipsInBattle = (from Ship ship in Ships select ship).ToList();
        }


        /// <summary>
        /// Возвращает ХП корабля после атаки на него, соответственно меняет кораблю ХП.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <returns>ХП корабля.</returns>
        public short GetAttackedShipHP(object ship)
        {
            Ships[Ships.IndexOf((Ship)ship)].Hp -= 20;
            return Ships[Ships.IndexOf((Ship)ship)].Hp;
        }


        /// <summary>
        /// Возвращает ХП корабля после его лечения, соответственно меняет кораблю ХП.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <returns>ХП корабля.</returns>
        public short GetHealedShipHP(object ship)
        {
            Ships[Ships.IndexOf((Ship)ship)].Hp += 15;
            return Ships[Ships.IndexOf((Ship)ship)].Hp;
        }


        /// <summary>
        /// Проверяет, опустилось ли ХП корабля ниже нуля.
        /// </summary>
        /// <param name="ship">Объект корабля</param>
        /// <returns>true, если опустилось. false, если нет.</returns>
        public bool CheckShipBeaten(object ship)
        {
            if (Ships[Ships.IndexOf((Ship)ship)].Hp <= 0)
            {
                ShipsInBattle.Remove((Ship)ship);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Проверяет, остался ли лишь один корабль с ХП выше нуля.
        /// </summary>
        /// <returns>true, если остался. false, если нет.</returns>
        public bool CheckGameOver()
        {
            if (ShipsInBattle.Count == 1)
            {
                return true;
            }

            return false;
        }





        /// <summary>
        /// Передает ход следующему кораблю (игроку).
        /// </summary>
        public void PassTheTurn()
        {
            if (ShipsInBattle.Last().IsYourTurn == true)
            {
                ShipsInBattle.Last().IsYourTurn = false;
                ShipsInBattle[0].IsYourTurn = true;
                return;
            }

            foreach (Ship ship in ShipsInBattle)
            {
                if (ship.IsYourTurn == true)
                {
                    ship.IsYourTurn = false;
                    ShipsInBattle[ShipsInBattle.IndexOf(ship) + 1].IsYourTurn = true;
                    return;
                }
            }

            ShipsInBattle[0].IsYourTurn = true;
        }


        /// <summary>
        /// Возвращает корабль, который сейчас ходит.
        /// </summary>
        /// <returns>Объект корабля.</returns>
        public Ship GetTurnShip()
        {
            foreach (Ship ship in ShipsInBattle) 
            {
                if (ship.IsYourTurn == true)
                {
                    return ship;
                }
            }

            return Ships[0];
        }


      


        /// <summary>
        /// Восстанавливает ХП всем кораблям.
        /// </summary>
        public void RecoverHP()
        {
            foreach (Ship ship in Ships)
            {
                ship.Hp = 100;
            }
        }   
    }
}
