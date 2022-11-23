using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets.Controller.Entities.Examples.LoadExamples
{
    public class Concurrence
    {
        private PetriNet petriNet;

        public Concurrence()
        {
            petriNet = new PetriNet();
        }

        public PetriNet Load_Concurrence()
        {
            createPlace();
            createTransition();
            createConnection();

            return petriNet;
        }

        private void createConnection()
        {
            petriNet.Base_CreateConnection("1", "1", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("1", "2", 1, ConnectionType.Normal, ConnectionDirection.Input);

            petriNet.Base_CreateConnection("2", "1", 1, ConnectionType.Normal, ConnectionDirection.Output);
            petriNet.Base_CreateConnection("3", "2", 1, ConnectionType.Normal, ConnectionDirection.Output);
        }

        private void createTransition()
        {
            petriNet.CreateTransition("1");
            petriNet.CreateTransition("2");
        }

        private void createPlace()
        {
            petriNet.CreatePlace("1", 1);
            petriNet.CreatePlace("2");
            petriNet.CreatePlace("3");
        }
    }
}
