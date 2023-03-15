using EasyNetQ;
using SharedModels;

namespace SessionsApi.Infrastructure
{
    public class MessagePublisher : IMessagePublisher, IDisposable
    {
        IBus bus;

        public MessagePublisher(string connectionString)
        {
            bus = RabbitHutch.CreateBus(connectionString);
        }

        public void Dispose()
        {
            bus.Dispose();
        }

        public void PublishProcedureStatusChangedMessage(int procedureId, string topic)
        {
            var message = new ProcedureStatusChangedMessage
            {
                ProcedureId = procedureId,
            };

            bus.PubSub.Publish(message, topic);
        }

    }
}
