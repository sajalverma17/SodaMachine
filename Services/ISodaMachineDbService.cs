using SodaMachine.Database;
using SodaMachine.Models;

namespace SodaMachine.Services
{
    /// <summary>
    /// Service type to inject in SodaMachine API controllers for handling of database operations.
    /// </summary>
    public interface ISodaMachineDbService
    {
        /// <summary>
        /// Returns the database context on which this service is operating.
        /// </summary>
        SodaMachineDBContext GetDBContext();

        /// <summary>
        /// Persist the soda machine in the database.
        /// </summary>
        void SaveChanges(Machine updatedMachine);
    }
}
