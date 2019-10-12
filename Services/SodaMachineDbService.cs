using SodaMachine.Database;
using SodaMachine.Models;

namespace SodaMachine.Services
{
    public class SodaMachineDbService : ISodaMachineDbService
    {
        private SodaMachineDBContext _machineDbContext;
        
        public SodaMachineDbService(SodaMachineDBContext dbContext)
        {
            _machineDbContext = dbContext;
        }

        public SodaMachineDBContext GetDBContext()
        {
            return _machineDbContext;
        }

        public void SaveChanges(Machine updatedSodaMachine)
        {
            _machineDbContext.SodaMachines.Update(updatedSodaMachine);
            _machineDbContext.SaveChanges();
        }
        
    }
}
