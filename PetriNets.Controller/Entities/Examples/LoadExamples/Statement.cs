namespace PetriNets.Controller.Entities.Examples.LoadExamples
{
    public class Statement
    {
        private PetriNet petriNet;

        public Statement()
        {
            petriNet = new PetriNet();
        }

        public PetriNet Load_Statement()
        {
            createPlace();
            createTransition();

            petriNet.Base_CreateConnection("1", "1", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("2", "2", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("3", "2", 2, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("5", "2", 3, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("4", "3", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("6", "4", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("7", "4", 1, ConnectionType.Normal, ConnectionDirection.Input);

            petriNet.Base_CreateConnection("2", "1", 1, ConnectionType.Normal, ConnectionDirection.Output);
            petriNet.Base_CreateConnection("4", "2", 1, ConnectionType.Normal, ConnectionDirection.Output);
            petriNet.Base_CreateConnection("3", "3", 2, ConnectionType.Normal, ConnectionDirection.Output);
            petriNet.Base_CreateConnection("7", "3", 1, ConnectionType.Normal, ConnectionDirection.Output);
            petriNet.Base_CreateConnection("6", "3", 1, ConnectionType.Normal, ConnectionDirection.Output);
            petriNet.Base_CreateConnection("8", "4", 1, ConnectionType.Normal, ConnectionDirection.Output);
            petriNet.Base_CreateConnection("5", "4", 3, ConnectionType.Normal, ConnectionDirection.Output);

            return petriNet;
        }

        private void createTransition()
        {
            petriNet.CreateTransition("1");
            petriNet.CreateTransition("2");
            petriNet.CreateTransition("3");
            petriNet.CreateTransition("4");
        }

        private void createPlace()
        {
            petriNet.CreatePlace("1", 2);
            petriNet.CreatePlace("2");
            petriNet.CreatePlace("3", 2);
            petriNet.CreatePlace("4");
            petriNet.CreatePlace("5", 5);
            petriNet.CreatePlace("6");
            petriNet.CreatePlace("7");
            petriNet.CreatePlace("8");
        }
    }
}
