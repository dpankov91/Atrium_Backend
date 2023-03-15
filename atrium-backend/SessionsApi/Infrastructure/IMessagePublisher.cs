namespace SessionsApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishProcedureStatusChangedMessage(int procedure, string topic);
    }
}
