namespace ProceduresApi.Data
{
    public interface IDbInitializer
    {
        void Initialize(ProcedureDbContext context);
    }
}
