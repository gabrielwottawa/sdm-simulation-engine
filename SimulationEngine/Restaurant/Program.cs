using SimulationEngine.Api;
using SimulationEngine.Api.Datas;

internal class Program
{
    private static void Main(string[] args)
    {
        initializeMenu();
    }

    private static void initializeMenu()
    {
        Console.Clear();        
        Console.WriteLine("\nSelecione uma opção!");

        string option;

        do
        {
            printMenu();
            option = Console.ReadLine();
            executeOption(option);

        } while (option != "7");
    }    

    private static void printMenu()
    {        
        Console.WriteLine($"\nTempo atual do Sistema: {Engine.Time.ToString("N4")} {"min"}.");
        Console.WriteLine($"Quantidade de Eventos Futuros: {Engine.FutureEventSize}");
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
                Engine.SimulateOneExecution(callback);
                break;

            case "1":
                Engine.Simulate(callback);
                break;

            case "2":
                time = getTime($"Informe em quanto tempo ({"min"}) a simulação irá parar:");
                Engine.SimulateUntilDeterminedTime(time, callback);
                break;

            case "3":
                time = getTime($"Informe por quanto tempo ({"min"}) a simulação irá executar:");
                Engine.SimulateForDeterminedTime(time, callback);
                break;

            case "4":
                showQueueStatistics();
                showResourceStatistics();
                break;

            case "5":
                foreach(var history in DataCollect.HistoricList)
                {
                    Console.WriteLine("\n\n------------");
                    Console.WriteLine(history.Name + " maior tempo de vida " + history.LongerLifetime());
                    Console.WriteLine(history.Name + " menor tempo de vida " + history.LongerLifetime());
                    Console.WriteLine(history.Name + " tempo médio de vida " + history.LongerLifetime());
                    Console.WriteLine("------------\n\n");
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
            catch(Exception e)
            {
                Console.WriteLine("Valor Invalido");
                Thread.Sleep(500);                
            }
        }
    }

    private static void showResourceStatistics()
    {
        throw new NotImplementedException();
    }

    private static void showQueueStatistics()
    {
        throw new NotImplementedException();
    }
}