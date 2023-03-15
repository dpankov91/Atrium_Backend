using EasyNetQ;
using ProceduresApi.Data;
using ProceduresApi.Models;
using SharedModels;

namespace ProceduresApi.Infrastructure
{
    public class MessageListener
    {
        IServiceProvider provider;
        string connectionString;

        // The service provider is passed as a parameter, because the class needs
        // access to the product repository. With the service provider, we can create
        // a service scope that can provide an instance of the product repository.
        public MessageListener(IServiceProvider provider, string connectionString)
        {
            this.provider = provider;
            this.connectionString = connectionString;
        }

        public void Start()
        {
            using (var bus = RabbitHutch.CreateBus(connectionString))
            {
                bus.PubSub.Subscribe<ProcedureStatusChangedMessage>("procedureApiHkInProcess",
                    HandleProcedureInProgress, x => x.WithTopic("in_process"));

                // Add code to subscribe to other OrderStatusChanged events:
                // * cancelled
                // * shipped
                // * paid
                // Implement an event handler for each of these events.
                // Be careful that each subscribe has a unique subscription id
                // (this is the first parameter to the Subscribe method). If they
                // get the same subscription id, they will listen on the same
                // queue.

                // Block the thread so that it will not exit and stop subscribing.
                lock (this)
                {
                    Monitor.Wait(this);
                }
            }

        }

        private async void HandleProcedureInProgress(ProcedureStatusChangedMessage message)
        {
            // A service scope is created to get an instance of the product repository.
            // When the service scope is disposed, the product repository instance will
            // also be disposed.
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var procedureRepo = services.GetService<IRepository<Procedure>>();

                var procedure = procedureRepo.Get(message.ProcedureId);
                Procedure proc = await procedure;
                proc.Status = (Procedure.ProcedureStatus)2;
                procedureRepo.Edit(proc);

            }
        }
    }
}
