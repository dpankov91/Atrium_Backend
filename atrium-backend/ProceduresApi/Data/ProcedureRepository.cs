using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProceduresApi.Models;

namespace ProceduresApi.Data
{
    public class ProcedureRepository : IRepository<Procedure>
    {
        private readonly ProcedureDbContext _procedureDbContext;

        public ProcedureRepository(ProcedureDbContext procedureDbContext)
        {
            _procedureDbContext = procedureDbContext;
        }

        public async Task<Procedure> Add(Procedure procedure)
        {
            await _procedureDbContext.Procedures.AddAsync(procedure);
            await _procedureDbContext.SaveChangesAsync();
            return procedure;
        }

        public async Task<Procedure> Get(int id)
        {
            var procedure = await _procedureDbContext.Procedures.FindAsync(id);
            return procedure;
        }

        public IEnumerable<Procedure> GetAll()
        {
            return _procedureDbContext.Procedures.ToList();
        }

        public async void Remove(int id)
        {
            var procedure = await _procedureDbContext.Procedures.FindAsync(id);
            _procedureDbContext.Procedures.Remove(procedure);
            await _procedureDbContext.SaveChangesAsync();
        }

        public async void Edit(Procedure procedure)
        {
            _procedureDbContext.Entry(procedure).State = EntityState.Modified;
            _procedureDbContext.SaveChanges();
        }
    }
}
