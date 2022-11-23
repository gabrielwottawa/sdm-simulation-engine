using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNets.Controller.Entities.Examples.LoadExamples
{
    public static class BaseLoadExamples
    {
        public static void Base_CreateConnection(this PetriNet petriNet, string id_place, string id_transition, int weight, ConnectionType connectionType, ConnectionDirection connectionDirection)
        {
            var place = petriNet.GetPlace(id_place);

            if (place == null)
                return;

            var transition = petriNet.GetTransition(id_transition);

            if (transition == null)
                return;

            petriNet.CreateConnection(place, transition, weight, connectionType, connectionDirection);
        }
    }
}
