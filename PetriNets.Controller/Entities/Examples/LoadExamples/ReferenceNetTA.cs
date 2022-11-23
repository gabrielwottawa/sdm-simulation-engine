using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets.Controller.Entities.Examples.LoadExamples
{
    public class ReferenceNetTA
    {
        private PetriNet petriNet;

        public ReferenceNetTA()
        {
            petriNet = new PetriNet();
        }

        public PetriNet Load_ReferenceNetTA()
        {
            createPlace();
            createTransition();
            createConnection();

            return petriNet;
        }

        private void createConnection()
        {
            petriNet.Base_CreateConnection("1", "1", 2, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("1", "2", 1, ConnectionType.Normal, ConnectionDirection.Output);

            petriNet.Base_CreateConnection("2", "2", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("2", "6", 1, ConnectionType.Normal, ConnectionDirection.Output);

            petriNet.Base_CreateConnection("3", "3", 1, ConnectionType.Normal, ConnectionDirection.Input);

            petriNet.Base_CreateConnection("4", "4", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("4", "1", 1, ConnectionType.Normal, ConnectionDirection.Output);

            petriNet.Base_CreateConnection("5", "5", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("5", "2", 1, ConnectionType.Normal, ConnectionDirection.Output);

            petriNet.Base_CreateConnection("6", "5", 2, ConnectionType.Inibidor, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("6", "3", 1, ConnectionType.Normal, ConnectionDirection.Output);
            petriNet.Base_CreateConnection("6", "6", 1, ConnectionType.Normal, ConnectionDirection.Output);

            petriNet.Base_CreateConnection("7", "4", 1, ConnectionType.Reset, ConnectionDirection.Input);

            petriNet.Base_CreateConnection("8", "4", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("8", "7", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("8", "5", 1, ConnectionType.Normal, ConnectionDirection.Output);

            petriNet.Base_CreateConnection("9", "7", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("9", "5", 1, ConnectionType.Normal, ConnectionDirection.Output);

            petriNet.Base_CreateConnection("10", "7", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("10", "5", 1, ConnectionType.Normal, ConnectionDirection.Output);

            petriNet.Base_CreateConnection("11", "7", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("11", "4", 1, ConnectionType.Normal, ConnectionDirection.Output);

            petriNet.Base_CreateConnection("12", "7", 1, ConnectionType.Normal, ConnectionDirection.Output);

            petriNet.Base_CreateConnection("13", "6", 1, ConnectionType.Normal, ConnectionDirection.Input);
            petriNet.Base_CreateConnection("13", "7", 1, ConnectionType.Normal, ConnectionDirection.Output);
        }

        private void createTransition()
        {
            petriNet.CreateTransition("1");
            petriNet.CreateTransition("2");
            petriNet.CreateTransition("3");
            petriNet.CreateTransition("4");
            petriNet.CreateTransition("5");
            petriNet.CreateTransition("6");
            petriNet.CreateTransition("7");
        }

        private void createPlace()
        {
            petriNet.CreatePlace("1");
            petriNet.CreatePlace("2", 2);
            petriNet.CreatePlace("3", 1);
            petriNet.CreatePlace("4");
            petriNet.CreatePlace("5");
            petriNet.CreatePlace("6");
            petriNet.CreatePlace("7", 10);
            petriNet.CreatePlace("8");
            petriNet.CreatePlace("9");
            petriNet.CreatePlace("10");
            petriNet.CreatePlace("11");
            petriNet.CreatePlace("12");
            petriNet.CreatePlace("13");
        }
    }
}
