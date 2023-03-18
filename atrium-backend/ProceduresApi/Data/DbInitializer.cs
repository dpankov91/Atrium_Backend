using ProceduresApi.Models;

namespace ProceduresApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        public void Initialize(ProcedureDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Procedures.Any())
            {
                return;   // DB has been seeded
            }

            List<Procedure> products = new List<Procedure>
            {
                new Procedure { Name = "Divorce Griffith Family", IsCivil = 1, AdditionalInfo = "Family has 2 kids. Husband is alkoholic", Status = (Procedure.ProcedureStatus)1 },
                new Procedure { Name = "Murder in a nightclub", IsCivil = 2, AdditionalInfo = "Fight with a knife", Status = (Procedure.ProcedureStatus)1 },
                new Procedure { Name = "Credit card fraud", IsCivil = 2, AdditionalInfo = "Scammers chase for old people a take credits on their name", Status = (Procedure.ProcedureStatus)1 }
            };

            context.Procedures.AddRange(products);
            context.SaveChanges();
        }
    }
}
