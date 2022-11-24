using Restaurant.Entities;
using Restaurant.Events.Cashier;
using Restaurant.Resources;
using SimulationEngine.Api.Managers;
using SimulationEngine.Api.Models;
using SimulationEngine.Api.Models.Enum;

namespace Restaurant.Engine
{
    public class EngineRestaurant
    {
        public const bool Debug = true;

        public static readonly EntitySet<ClientGroup> QueueCashierOne = new EntitySet<ClientGroup>("Fila do caixa 1", Mode.Fifo, int.MaxValue);

        public static readonly EntitySet<ClientGroup> QueueCashierTwo = new EntitySet<ClientGroup>("Fila do caixa 2", Mode.Fifo, int.MaxValue);

        public static readonly EntitySet<ClientGroup> QueueOrders = new EntitySet<ClientGroup>("Fila pedidos da cozinha", Mode.Fifo, int.MaxValue);

        public static readonly EntitySet<ClientGroup> QueueDelivery = new EntitySet<ClientGroup>("Fila pedidos para entrega", Mode.Fifo, int.MaxValue);

        public static readonly EntitySet<ClientGroup> QueueCounterChair = new EntitySet<ClientGroup>("Fila balcão", Mode.Fifo, int.MaxValue);

        public static readonly EntitySet<ClientGroup> QueueTwoSeaterTable = new EntitySet<ClientGroup>("Fila mesas de 2 lugares", Mode.Fifo, int.MaxValue);

        public static readonly EntitySet<ClientGroup> QueueFourSeaterTable = new EntitySet<ClientGroup>("Fila mesas de 4 lugares", Mode.Fifo, int.MaxValue);


        public const string UnitTime = "ms";

        public const double MaxTimeArrivalCustomers = 180.00;

        public const double MaximumTimeToBathroom = MaxTimeArrivalCustomers * 15;

        public const double WeatherFirstToBathroom = 180;


        public static double ArrivalCustomers => RandomizationManagerContext.ManagerRandom.Exponential(3);

        public static double TimeServiceCashier => RandomizationManagerContext.ManagerRandom.Normal(8, 2);

        public static double TimePreparationOrder => RandomizationManagerContext.ManagerRandom.Normal(14, 5, 0.1, 35);
        
        public static double TimeDeliveryOrderByBartender => RandomizationManagerContext.ManagerRandom.Normal(2, 0.3);

        public static double WeatherSanitizationTable => RandomizationManagerContext.ManagerRandom.Normal(3, 0.6);

        public static double MealTime => RandomizationManagerContext.ManagerRandom.Normal(20, 8, 0.1, 45);

        public static double TimeGoToBathroomCashier => RandomizationManagerContext.ManagerRandom.Normal(60, 8);

        public static double BathroomReturnTime => RandomizationManagerContext.ManagerRandom.Normal(2, 0.5);


        public static Bartender Bartender;
        
        public static void Initializer()
        {
            ResourceManager<Bartender>.CreateResource(1);
            Bartender = ResourceManager<Bartender>.Allocated(1).First().Resource;

            ResourceManager<CashierOne>.CreateResource(1);
            ResourceManager<CashierTwo>.CreateResource(1);
            ResourceManager<Chef>.CreateResource(3);

            ResourceManager<CounterChair>.CreateResource(6);
            ResourceManager<TwoSeaterTable>.CreateResource(4);
            ResourceManager<FourSeaterTable>.CreateResource(4);

            SimulationEngine.Api.Scheduler.ScheduleIn(new GoToTheBathroom(), TimeGoToBathroomCashier);
        }
    }
}
