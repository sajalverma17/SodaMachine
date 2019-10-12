using Microsoft.AspNetCore.Mvc;
using SodaMachine.Business;
using Newtonsoft.Json;
using SodaMachine.Services;
using System.Linq;

namespace SodaMachine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private MachineCommand _machineCommand;
        private ISodaMachineDbService _dbService;
        public OrderController(ISodaMachineDbService sodaMachineDbService)
        {
            _machineCommand = new MachineCommand(sodaMachineDbService.GetDBContext().SodaMachines.First());
            _dbService = sodaMachineDbService;
        }

        [HttpGet("[action]")]
        public string GetSodaInventory()
        {
            var sodas = _machineCommand.GetSodas();
            return JsonConvert.SerializeObject(sodas);
        }

        [HttpPost("[action]")]
        public string ProcessOrder([FromBody] int orderedSodaId)
        {
            var commandResult = "";
            commandResult = _machineCommand.PlaceOrder(orderedSodaId);
            _dbService.SaveChanges(_machineCommand.UpdatedMachine);
            return JsonConvert.SerializeObject(commandResult);
        }

        [HttpPost("[action]")]
        public string SmsOrder([FromBody] int orderedSodaId)
        {
            var commandResult = "";
            commandResult = _machineCommand.SmsOrder(orderedSodaId);
            return JsonConvert.SerializeObject(commandResult);
        }
    }
}