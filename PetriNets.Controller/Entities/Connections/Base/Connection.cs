using PetriNets.Controller.Extensions;

namespace PetriNets.Controller.Entities
{
    public abstract class Connection
    {
        public Place? Place { get; private set; }
        public Transition? Transition { get; private set; }
        public int Weight { get; private set; } = 1;
        public ConnectionDirection Direction { get; private set; }
        public virtual bool IsEnabled { get { return Direction == ConnectionDirection.Input; } }
        private string ConnectionTypeName => this.ConvertToString();

        public Connection(ConnectionData data)
        {
            Place = data.Place;
            Transition = data.Transition;
            Weight = data.Weight;
            Direction = data.Direction;
        }

        public abstract void ConsumeTokens();

        public override string? ToString()
        {
            switch (Direction)
            {
                case ConnectionDirection.Input:
                    return $"Lugar {Place?.Id} -> Transição {Transition?.Id} - Peso {Weight} - Tipo {ConnectionTypeName}";

                case ConnectionDirection.Output:
                    return $"Transição {Transition?.Id} -> Lugar {Place?.Id} - Peso {Weight}";
            }

            return "";
        }
    }
}
