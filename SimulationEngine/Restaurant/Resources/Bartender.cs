using PetriNets.Controller;
using PetriNets.Controller.Entities;
using Restaurant.Engine;
using Restaurant.Entities;
using Restaurant.Events.Bartender;
using Restaurant.Events.Cashier;
using Restaurant.Events.Clients;
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
            PetriNet.CreatePlace("1", 1);

            initializerReplaceCashier();
            initializerDeliveryOrder();
            initializerSanitizingTable();

            PetriNet.ExecuteCycle();
        }

        private void initializerReplaceCashier()
        {
            PetriNet.CreatePlace("2", 0, replaceCashierProducedToken, replaceCashierConsumedToken);
            PetriNet.CreatePlace("20", 0, replaceCashierProducedToken, replaceCashierConsumedToken);
            PetriNet.CreatePlace("21", 0, replaceCashierProducedToken, replaceCashierConsumedToken);

            PetriNet.CreateTransition("20");
            PetriNet.CreateTransition("21");


            var transition = PetriNet.GetTransition("20");
            PetriNet.CreateConnection(FreeBartender, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(ReplaceCashier, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(BartenderAtTheCashier, transition, 1, ConnectionType.Normal, ConnectionDirection.Output);

            transition = PetriNet.GetTransition("21");
            PetriNet.CreateConnection(BartenderAtTheCashier, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(ReturnCashier, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(FreeBartender, transition, 1, ConnectionType.Normal, ConnectionDirection.Output);
        }

        private void replaceCashierProducedToken(Place place)
        {
            if (place.Id == "20")
                SimulationEngine.Api.Engine.ScheduleIn(new FinishGoingToTheBathroom(), EngineRestaurant.BathroomReturnTime);
        }

        private void replaceCashierConsumedToken(Place place)
        {
            switch (place.Id)
            {
                case "2":
                    if (EngineRestaurant.Debug)
                        Console.WriteLine($"\t\t\tCaixa vai ao banheiro {SimulationEngine.Api.Engine.Time:N6}");
                    break;
                case "21":
                    if (EngineRestaurant.Debug)
                        Console.WriteLine($"\t\t\tCaixa volta do banheiro {SimulationEngine.Api.Engine.Time:N6}");
                    break;
                default:
                    break;
            }
        }

        private void initializerDeliveryOrder()
        {
            PetriNet.CreatePlace("3", 0, deliverOrderProducedToken, deliverOrderConsumedToken);
            PetriNet.CreatePlace("30", 0, deliverOrderProducedToken, deliverOrderConsumedToken);
            PetriNet.CreatePlace("31", 0, deliverOrderProducedToken, deliverOrderConsumedToken);

            PetriNet.CreateTransition("30");
            PetriNet.CreateTransition("31");

            var transition = PetriNet.GetTransition("30");
            PetriNet.CreateConnection(FreeBartender, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(OrderReady, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(TakingOrder, transition, 1, ConnectionType.Normal, ConnectionDirection.Output);

            transition = PetriNet.GetTransition("31");
            PetriNet.CreateConnection(TakingOrder, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(OrderOnTable, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(FreeBartender, transition, 1, ConnectionType.Normal, ConnectionDirection.Output);
        }

        private void deliverOrderProducedToken(Place place)
        {
            if (place.Id == "30")
                SimulationEngine.Api.Engine.ScheduleIn(new FinalizeDeliveryOrder(), EngineRestaurant.TimeDeliveryOrderByBartender);
        }

        private void deliverOrderConsumedToken(Place place)
        {
            switch (place.Id)
            {
                case "3":
                    ClientGroup = EngineRestaurant.QueueOrders.Remove();
                    if (EngineRestaurant.Debug) 
                        Console.WriteLine($"\tGarçom começa a entrega {ClientGroup.Id}! {SimulationEngine.Api.Engine.Time}");
                    break;
                case "31":
                    SimulationEngine.Api.Engine.ScheduleNow(new StartEating(ClientGroup));
                    if (EngineRestaurant.Debug) 
                        Console.WriteLine($"\tCliente {ClientGroup.Id} vai comecar comer! {SimulationEngine.Api.Engine.Time}");
                    break;
                default:
                    break;
            }
        }

        private void initializerSanitizingTable()
        {
            PetriNet.CreatePlace("4", 0, sanitizingTableProducedToken, sanitizingTableConsumedToken);
            PetriNet.CreatePlace("40", 0, sanitizingTableProducedToken, sanitizingTableConsumedToken);
            PetriNet.CreatePlace("41", 0, sanitizingTableProducedToken, sanitizingTableConsumedToken);

            PetriNet.CreateTransition("40");
            PetriNet.CreateTransition("41");

            var transition = PetriNet.GetTransition("40");
            PetriNet.CreateConnection(FreeBartender, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(CustomerGoSit, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(SanitizingTable, transition, 1, ConnectionType.Normal, ConnectionDirection.Output);

            transition = PetriNet.GetTransition("41");
            PetriNet.CreateConnection(SanitizingTable, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(SanitizedTable, transition, 1, ConnectionType.Normal, ConnectionDirection.Input);
            PetriNet.CreateConnection(FreeBartender, transition, 1, ConnectionType.Normal, ConnectionDirection.Output);
        }

        private void sanitizingTableProducedToken(Place place)
        {
            if (place.Id == "40")
            {
                SimulationEngine.Api.Engine.ScheduleIn(new FinalizeSanitizeTable(), EngineRestaurant.WeatherSanitizationTable);
                
                if(EngineRestaurant.Debug)
                    Console.WriteLine($"\t\tGarçom comeca limpar mesa! {SimulationEngine.Api.Engine.Time}");
            }
        }

        private void sanitizingTableConsumedToken(Place place)
        {
            switch (place.Id)
            {
                case "4":
                    if (EngineRestaurant.Debug)
                        Console.WriteLine($"\t\tCliente sentou! {SimulationEngine.Api.Engine.Time}");
                    break;
                case "41":
                    if (EngineRestaurant.Debug)
                        Console.WriteLine($"\t\tMesa Higienizada! {SimulationEngine.Api.Engine.Time}");
                    break;
                default:
                    break;
            }
        }
    }
}
