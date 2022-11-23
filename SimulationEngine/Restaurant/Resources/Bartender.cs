using PetriNets.Controller;
using PetriNets.Controller.Entities;
using Restaurant.Entities;
using SimulationEngine.Api.Models;

namespace Restaurant.Resources
{
    public sealed class Bartender : Resource
    {
        public PetriNet PetriNet { get; }

        public Place FreeBartender => PetriNet.GetPlace("1");

        public Place ReplaceCashier => PetriNet.GetPlace("2");
        public Place BartenderAtTheCashier => PetriNet.GetPlace("20");
        public Place ReturnCashier => PetriNet.GetPlace("21");

        public Place OrderReady => PetriNet.GetPlace("3");
        public Place TakingOrder => PetriNet.GetPlace("30");
        public Place OrderOnTable => PetriNet.GetPlace("31");

        public Place CustomerGoSit => PetriNet.GetPlace("4");
        public Place SanitizingTable => PetriNet.GetPlace("40");
        public Place SanitizedTable => PetriNet.GetPlace("41");

        public ClientGroup ClientGroup;

        public Bartender()
        {
            PetriNet = new PetriNet();
            PetriNet.CreatePlace("3", 0, deliverOrderProducedToken, deliverOrderConsumedToken);
            PetriNet.CreatePlace("30", 0, deliverOrderProducedToken, deliverOrderConsumedToken);
            PetriNet.CreatePlace("31", 0, deliverOrderProducedToken, deliverOrderConsumedToken);

            PetriNet.CreateTransition("30");
            PetriNet.CreateTransition("31");
        }

        private void deliverOrderConsumedToken(Place obj)
        {
            throw new NotImplementedException();
        }

        private void deliverOrderProducedToken(Place obj)
        {
            throw new NotImplementedException();
        }
    }
}
