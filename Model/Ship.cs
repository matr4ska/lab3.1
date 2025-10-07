
namespace Model
{
    public class Ship : IDomainObject
    {
        private int id;
        private string name;
        private int hp;
        private FlagColor flagColor;
        private bool isYourTurn;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Name
        {
            get => name;
            set => name = value; 
        }

        public int Hp
        {
            get => hp;
            set => hp = value; 
        }

        public FlagColor FlagColor
        {
            get => flagColor;
            set => flagColor = value;
        }

        public bool IsYourTurn
        {
            get => isYourTurn;
            set => isYourTurn = value;
        }
    }
}
