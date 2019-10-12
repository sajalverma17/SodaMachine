using Microsoft.AspNetCore.Mvc;
using SodaMachine.Business;
using SodaMachine.Services;
using System.Linq;

namespace SodaMachine.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private MachineCommand _machineCommand;
        private ISodaMachineDbService _dbService;

        public MoneyController(ISodaMachineDbService sodaMachineDbService)
        {
            _machineCommand = new MachineCommand(sodaMachineDbService.GetDBContext().SodaMachines.First());
            _dbService = sodaMachineDbService;
        }

        [HttpGet("[action]")]
        public int Get()
        {
            return _machineCommand.GetMoney();            
        }

        [HttpGet("[action]")]
        public int Recall()
        {
            var moneyReturned = _machineCommand.Recall();
            _dbService.SaveChanges(_machineCommand.UpdatedMachine);
            return moneyReturned;
        }

        [HttpPost("[action]")]
        public int Insert([FromBody] int moneyToInsert)
        {
            var updatedMoney = _machineCommand.InsertMoney(moneyToInsert);
            _dbService.SaveChanges(_machineCommand.UpdatedMachine);
            return updatedMoney;
        }
    }
}