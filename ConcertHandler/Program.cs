using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConcertHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            // how to call task, method 1
            Task task1 = new Task(() => Console.WriteLine("Billbords have the concert poster."));
            task1.Start();

            // how to call task, method 2
            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("TV has advertisment."));

            // how to call task, method 3
            Task task3 = Task.Run(() => Console.WriteLine("Place has illuminations."));

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            new Task(() =>
            {
                Console.WriteLine("Press any key for cancel.");
                Console.ReadKey();

                cancelTokenSource.Cancel();
            }).Start();

            var dancers = new List<string>() { "Jane", "Sara", "Angela", "July"};

            // parallel tasks
            Parallel.ForEach(dancers, x => Dance(x, token));

            Console.ReadLine();
        }

        private static void Dance(string name, CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                Console.WriteLine($"{name} is moving up...");
                Thread.Sleep(1000);
                Console.WriteLine($"{name} is moving down...");
                Thread.Sleep(1000);
                Console.WriteLine($"{name} is stoping...");
                Thread.Sleep(1000);
            }
        }
    }
}
