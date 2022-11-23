namespace PetriNets.Controller.Entities.Examples.LoadExamples
{
    public class Pattern
    {
        private PetriNet petriNet;

        public Pattern()
        {
            petriNet = new PetriNet();
        }

        public PetriNet Load_Pattern()
        {
            createPlace();
            createTransition();
            createConnection();

            return petriNet;
        }

        private void createConnection()
        {
            petriNet.Base_CreateConnection("1", "1", 2, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("2", "2", 2, ConnectionType.Normal, ConnectionDirection.Input);
                                                
            petriNet.Base_CreateConnection("2", "1", 2, ConnectionType.Normal, ConnectionDirection.Output);
            petriNet.Base_CreateConnection("3", "2", 2, ConnectionType.Normal, ConnectionDirection.Output);
        }

        private void createTransition()
        {
            petriNet.CreateTransition("1");
            petriNet.CreateTransition("2");
        }

        private void createPlace()
        {
            petriNet.CreatePlace("1", 2);
            petriNet.CreatePlace("2");
            petriNet.CreatePlace("3");
        }
    }
}
