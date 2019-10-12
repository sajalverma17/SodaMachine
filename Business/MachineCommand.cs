using SodaMachine.Database;
using SodaMachine.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SodaMachine.Business
{
    /// <summary>
    /// Executes commands on the soda machine and provides an updated instance of the machine.
    /// </summary>
    public class MachineCommand
    {
        private Machine _machine;

        /// <summary>
        /// Gets the updated instance of Soda machine. 
        /// Use it to persist changes to database after a machine command is executed.
        /// </summary>
        public Machine UpdatedMachine { get; private set; }

        public MachineCommand(Machine machine)
        {
            _machine = machine;
        }

        public int GetMoney()
        {
            return _machine.Money;
        }

        public List<Soda> GetSodas()
        {
            return _machine.Sodas;
        }

        public int InsertMoney(int moneyToInsert)
        {
            _machine.Money = _machine.Money + moneyToInsert;
            UpdatedMachine = _machine;           
            return UpdatedMachine.Money;
        }

        public int Recall()
        {
            var moneyReturned = _machine.Money;
            _machine.Money = 0;
            UpdatedMachine = _machine;
            return moneyReturned;
        }

        /// <summary>
        /// Returns the order's status in a message without actually processing the order. 
        /// Does NOT update the soda machine
        /// </summary>
        public string SmsOrder(int orderedSodaId)
        {
            var commandResult = "";
            if (IsOrderValid(orderedSodaId, _machine.Sodas, out commandResult))
            {
                //SMS the Order.
                var sodaName = _machine.Sodas.Where(s => s.Id == orderedSodaId).First().Name;
                commandResult = "Giving out " + sodaName;
            }
            return commandResult;
        }

        /// <summary>
        /// Processes the order and returns the result of processing.
        /// Updates the soda machine.
        /// </summary>
        public string PlaceOrder(int orderedSodaId)
        {
            var commandResult = "";
            if(IsOrderValid(orderedSodaId, _machine.Sodas, out commandResult))
            {
                /* Place the order */
                var orderedSoda = _machine.Sodas.Where(s => s.Id == orderedSodaId).First();
                var changeToReturn = _machine.Money - orderedSoda.Price;

                //update soda list
                var sodasCopy = _machine.Sodas.ToList<Soda>();
                for (int i = 0; i < sodasCopy.Count(); i++)
                {
                    if (sodasCopy[i].Id == orderedSoda.Id)
                    {
                        sodasCopy[i].Nr = sodasCopy[i].Nr - 1;
                    }
                }
                //Update the soda machine with updated soda list and give out all change money
                _machine.Money = 0;
                _machine.Sodas = sodasCopy;                
                commandResult = "Giving out " + orderedSoda.Name + ". Returning " + changeToReturn + " in change";
            }
            UpdatedMachine = _machine;
            return commandResult;
        }

        private bool IsOrderValid(int orderedSodaId, List<Soda> sodas, out string invalidOrderReason)
        {
            invalidOrderReason = "";
            var validationResult = true;
            //Ouery for the soda ordered.
            var sodaQuery = from soda in sodas
                            where soda.Id == orderedSodaId
                            select new Soda { Price = soda.Price, Nr = soda.Nr, Id = soda.Id, Name = soda.Name };

            if (sodaQuery.Any() == false)
            {
                invalidOrderReason = "This Soda is not found in our inventory";
                validationResult = false;
            }
            else
            {
                var orderedSoda = sodaQuery.First();
                if (orderedSoda.Price > _machine.Money)
                {
                    invalidOrderReason = "Insert " + (orderedSoda.Price - _machine.Money) + " more";
                    validationResult = false;
                }
                else if (orderedSoda.Nr < 1)
                {
                    invalidOrderReason = "No " + orderedSoda.Name + " left";
                    validationResult = false;
                }
            }
            return validationResult;
        }
    }
}
