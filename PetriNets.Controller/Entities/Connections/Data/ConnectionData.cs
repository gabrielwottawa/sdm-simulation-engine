namespace PetriNets.Controller.Entities
{
    public class ConnectionData
    {
        public Place? Place { get; private set; }
        public Transition? Transition { get; private set; }
        public int Weight { get; private set; } = 1;
        public ConnectionDirection Direction { get; private set; }

        public ConnectionData(Place? place, Transition? transition, int weight = 1, ConnectionDirection direction = ConnectionDirection.Input)
        {
            Place = place;
            Transition = transition;
            Weight = weight < 1 ? 1 : weight;
            Direction = direction;
        }
    }
}
