using Restaurant.Engine;
using Restaurant.Entities;
using Restaurant.Events.Clients;
using Restaurant.Resources;
using SimulationEngine.Api;
using SimulationEngine.Api.Datas;
using SimulationEngine.Api.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        EngineRestaurant.Initializer();

        var ev = new ArrivalCustomers();
        Scheduler.ScheduleNow(ev);

        initializeMenu(() => executeCycle(EngineRestaurant.Bartender));
    }

    private static void executeCycle(Bartender bartender) => bartender.PetriNet.ExecuteCycle();

    private static void initializeMenu(Action callback = null)
    {
        Console.Clear();
        Console.WriteLine("\nSelecione uma opção!");

        string option;

        do
        {
            printMenu();
            option = Console.ReadLine();
            executeOption(option, callback);

        } while (option != "7");
    }

    private static void printMenu()
    {
        Console.WriteLine($"\nTempo atual do Sistema: {Scheduler.Time.ToString("N4")} {EngineRestaurant.UnitTime}.");
        Console.WriteLine($"Quantidade de Eventos Futuros: {Scheduler.FutureEventSize}");
        Console.WriteLine("\nDigite:\n");
        Console.WriteLine("Vazio -> Simular um passo.");
        Console.WriteLine("1 -> Simular todo o Sistema.");
        Console.WriteLine("2 -> Simular até um tempo determinado.");
        Console.WriteLine("3 -> Simular por um tempo determinado.");
        Console.WriteLine("4 -> Mostrar estatisticas Atuais.");
        Console.WriteLine("5 -> Mostrar historico completo.");
        Console.WriteLine("6 -> Limpar Terminal.");
        Console.WriteLine("7 -> Sair.");
    }

    private static void executeOption(string option, Action callback = null)
    {
        var time = 0d;
        switch (option)
        {
            case "":
                Scheduler.SimulateOneExecution(callback);
                break;

            case "1":
                Scheduler.Simulate(callback);
                break;

            case "2":
                time = getTime($"Informe em quanto tempo ({EngineRestaurant.UnitTime}) a simulação irá parar:");
                Scheduler.SimulateUntilDeterminedTime(time, callback);
                break;

            case "3":
                time = getTime($"Informe por quanto tempo ({EngineRestaurant.UnitTime}) a simulação irá executar:");
                Scheduler.SimulateForDeterminedTime(time, callback);
                break;

            case "4":
                showQueueStatistics();
                showResourceStatistics();
                break;

            case "5":
                foreach (var history in DataCollect.HistoricList)
                {
                    Console.WriteLine("\n------------");
                    Console.WriteLine(history.Name + " maior tempo de vida " + history.LongerLifetime());
                    Console.WriteLine(history.Name + " menor tempo de vida " + history.ShorterLifetime());
                    Console.WriteLine(history.Name + " tempo médio de vida " + history.AverageLifetime());
                    Console.WriteLine("------------\n");
                }
                break;

            case "6":
                Console.Clear();
                break;

            case "7":
                return;

            default:
                Console.WriteLine("Valor inválido! Por favor tente novamente.");
                break;
        }
    }

    private static double getTime(string message)
    {
        while (true)
        {
            try
            {
                Console.WriteLine(message);
                var value = double.Parse(Console.ReadLine());
                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine("Valor Invalido");
                Thread.Sleep(500);
            }
        }
    }

    private static void showResourceStatistics()
    {
        var allocationResourceCounterChair = DataCollect.GetHistoricBase("Historic ManagedAllocation CounterChair");
        var allocationResourceTwoSeaterTable = DataCollect.GetHistoricBase("Historic ManagedAllocation TwoSeaterTable");
        var allocationResourceFourSeaterTable = DataCollect.GetHistoricBase("Historic ManagedAllocation FourSeaterTable");

        var allocationResourceCashierOne = DataCollect.GetHistoricBase("Historic ManagedAllocation CashierOne");
        var allocationResourceCashierTwo = DataCollect.GetHistoricBase("Historic ManagedAllocation CashierTwo");

        var allocationResourceChef = DataCollect.GetHistoricBase("Historic ManagedAllocation Chef");

        printResourceStatistics(allocationResourceCounterChair, "Cadeira balcão");
        printResourceStatistics(allocationResourceTwoSeaterTable, "Mesa de 2 lugares");
        printResourceStatistics(allocationResourceFourSeaterTable, "Mesa de 4 lugares");

        printResourceStatistics(allocationResourceCashierOne, "Atendente caixa 1");
        printResourceStatistics(allocationResourceCashierTwo, "Atendente caixa 2");

        printResourceStatistics(allocationResourceChef, "Chefe de cozinha");
    }

    private static void printResourceStatistics(SimulationEngine.Api.Base.HistoricBase allocationResourceCounterChair, string resourceName)
    {
        if (allocationResourceCounterChair != null)
            Console.WriteLine($"O tempo médio que o recurso {resourceName} fica alocado é de " + $"{allocationResourceCounterChair.StandardDeviationOfLife().ToString("N4")} {EngineRestaurant.UnitTime}.");
    }

    private static void showQueueStatistics()
    {
        var historyArrivalCustomers = (Historic<ArrivalCustomers>)DataCollect.GetHistoricBase("Historic ManagedEvent ArrivalCustomers");

        if (historyArrivalCustomers != null)
            Console.WriteLine($"\nChegaram {historyArrivalCustomers.ListInstanceInfos.Count} clientes ao Restaurante.");

        printQueueStatistics(EngineRestaurant.QueueCashierOne, "cliente(s)");
        printQueueStatistics(EngineRestaurant.QueueCashierTwo, "cliente(s)");

        printQueueStatistics(EngineRestaurant.QueueCounterChair, "cliente(s)");
        printQueueStatistics(EngineRestaurant.QueueTwoSeaterTable, "cliente(s)");
        printQueueStatistics(EngineRestaurant.QueueFourSeaterTable, "cliente(s)");

        printQueueStatistics(EngineRestaurant.QueueOrders, "pedido(s)");
        printQueueStatistics(EngineRestaurant.QueueDelivery, "pedido(s)");
    }

    private static void printQueueStatistics(EntitySet<ClientGroup> queue, string queueName)
    {
        prinTwentThroughTheQueue(queue, queueName);
        printTimeQueue(queue, queueName);
    }

    private static void printTimeQueue(EntitySet<ClientGroup> queue, string queueName)
    {
        if (queue.Historic != null)
        {
            var deviation = $"Désvio médio: +/- {queue.Historic.StandardDeviationOfLife().ToString("N4")} {EngineRestaurant.UnitTime}.";
            Console.WriteLine($"Tempo médio dos {queueName} é de {queue.Historic.AverageLifetime().ToString("N4")} {EngineRestaurant.UnitTime}.\n{deviation}\n");
        }
    }

    private static void prinTwentThroughTheQueue(EntitySet<ClientGroup> queue, string queueName)
    {
        if (queue.Historic != null)
        {
            Console.WriteLine($"Quantidade: {queue.Historic.ListInstanceInfos.Count} {queueName} passaram pela {queue.Name.ToLower()}.");
            Console.WriteLine($"Tamanho atual da {queue.Name.ToLower()}: {queue.CurrentSize}.");
        }
    }
}