using System;
using System.Diagnostics;
using System.Threading;
using AwesomeApi1.Controllers;
using Microsoft.ServiceFabric.Services.Runtime;

namespace AwesomeApi1
{
    internal static class Program
    {
        private static void Main()
        {
            using (var manualResetEvent = new ManualResetEventSlim(false))
            {
                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    Console.WriteLine("cancel!");
                    SetUnhealthy();
                    manualResetEvent?.Set();
                };

                try
                {
                    string serviceTypeName = nameof(AwesomeApi1) + "Type";
                    ServiceRuntime.RegisterServiceAsync(serviceTypeName, context => new AwesomeApi1(context)).GetAwaiter().GetResult();

                    ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(AwesomeApi1).Name);
                    Console.WriteLine("Running....");
                    manualResetEvent.Wait();
                }
                catch (Exception e)
                {
                    ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                    throw;
                }
            }
            Console.WriteLine("Exiting");
        }

        private static void SetUnhealthy()
        {
            HealthController.Healthy = false;
            Thread.Sleep(TimeSpan.FromSeconds(10));
        }
    }
}
