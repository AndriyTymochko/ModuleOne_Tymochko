using System;
using System.Collections.Generic;
using System.Linq;

using ConsoleModernWriter;

namespace Lab_1_3_Operators.OperationType.PuzzleFarmer
{
    class Transfer
    {
        #region Fields 
        private const string _fromBankDescr = "From bank:";
        private const string _toBankDescr = "To bank:";
        private const string _bankStatusDelimiter = "|";

        /// <summary>
        /// all object/persons/animals that must be transferred to another river bank
        /// </summary>
        private static readonly ICollection<FarmerPazle_ObjectToTransfer> _whomToTransferList;
        /// <summary>
        /// posible directions for transferring
        /// </summary>
        private static readonly ICollection<FarmerPazle_Direction> _transferPoint;

        /// <summary>
        /// all objects/persons/animals/vegetables that MUST BE transfering from river bank
        /// </summary>
        protected readonly ICollection<FarmerPazle_ObjectToTransfer> FromBank;
        /// <summary>
        /// all objects/persons/animals/vegetables that have been already transferred to opposite river bank
        /// </summary>
        protected readonly ICollection<FarmerPazle_ObjectToTransfer> ToBank;
        #endregion 

        #region Constructors 
        static Transfer()
        {
                //to initialise a new unique set of object/persons/animals that must be transferred to another river bank 
            _whomToTransferList = new HashSet<FarmerPazle_ObjectToTransfer>() {
                    FarmerPazle_ObjectToTransfer.farmer,
                    FarmerPazle_ObjectToTransfer.wolf,
                    FarmerPazle_ObjectToTransfer.goat,
                    FarmerPazle_ObjectToTransfer.cabbage
            };
                //to initialise a new unique set of directions for transferring
            _transferPoint = new HashSet<FarmerPazle_Direction>() {
                FarmerPazle_Direction.there,
                FarmerPazle_Direction.back
            }; 
        }

        public Transfer()
        {
            FromBank = new HashSet<FarmerPazle_ObjectToTransfer>(_whomToTransferList);
            ToBank = new HashSet<FarmerPazle_ObjectToTransfer>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Show the current status of objects/persons/animals/vegetables transferring
        /// </summary>
        /// <param name="tranferDescr"></param>
        public virtual void ShowCurrentTransferStatus(string tranferDescr)
        {
            try
            {
                    //to transfer objects/persons/animals depends on the the user answer
                TransferObjectsByFerry(tranferDescr);
                    //to show the objects/persons/animals/vegetables that are in the first river bank
                ConsoleIO.WriteColorText(_fromBankDescr);
                FromBank.ToList().ForEach(x => ConsoleIO.WriteColorText(string.Format(" {0} {1}", x, _bankStatusDelimiter), ConsoleColor.Red, _bankStatusDelimiter, ConsoleColor.White));
                ConsoleIO.WriteColorTextNewLine();

                    //to show the objects/persons/animals/vegetables that are in the opposite river bank
                ConsoleIO.WriteColorText(_toBankDescr);
                ToBank.ToList().ForEach(x => ConsoleIO.WriteColorText(string.Format(" {0} {1}", x, _bankStatusDelimiter), ConsoleColor.Green, _bankStatusDelimiter, ConsoleColor.White));
                ConsoleIO.WriteColorTextNewLine();
                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Transfer objects/persons/animals depends on the the user answer
        /// </summary>
        /// <param name="tranferDescr"></param>
        public virtual void TransferObjectsByFerry(string tranferDescr)
        {
            try
            {
                    //get the list of objects for transferring
                ICollection<FarmerPazle_ObjectToTransfer> objForTr = GetListOfObjectsToTransfer(tranferDescr);
                if (objForTr == default(ICollection<FarmerPazle_ObjectToTransfer>)) //check if there are some objects for transferring
                    return;

                    //get current direction for transferring
                FarmerPazle_Direction direction = GetDirectionForTransfer(tranferDescr);
                objForTr.ToList().ForEach(obj => {
                    switch (direction) //depends on the direction of transferring 
                    {
                        case FarmerPazle_Direction.there:
                            FromBank.Remove(obj); //remove object from first bank
                            ToBank.Add(obj);   //add object to destination bank
                            break;
                        case FarmerPazle_Direction.back:
                            ToBank.Remove(obj);  //remove object from destination bank
                            FromBank.Add(obj);  //add object to opposite bank
                            break;
                    }
                });
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Get the list of objects for transferring
        /// </summary>
        /// <param name="tranferDescr"></param>
        /// <returns></returns>
        public virtual List<FarmerPazle_ObjectToTransfer> GetListOfObjectsToTransfer(string tranferDescr)
        {
            try
            {
                    //check if input string arg is null or empty
                if (string.IsNullOrEmpty(tranferDescr))
                    return default(List<FarmerPazle_ObjectToTransfer>);

                return tranferDescr
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) //split the current user answer by ' ' 
                    .Where(x => Enum.IsDefined(typeof(FarmerPazle_ObjectToTransfer), x.ToLower()))
                    .ToEnumListButFirstlyTryParse<FarmerPazle_ObjectToTransfer>()
                    .ToList();
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return default(List<FarmerPazle_ObjectToTransfer>);
        }

        /// <summary>
        /// Get current direction for transferring
        /// </summary>
        /// <param name="tranferDescr"></param>
        /// <returns></returns>
        public virtual FarmerPazle_Direction GetDirectionForTransfer(string tranferDescr)
        {
            try
            {
                    //check if input string arg is null or empty
                if (string.IsNullOrEmpty(tranferDescr))
                    return default(FarmerPazle_Direction);
                    
                    //split the current user answer by ' ' 
                string[] posVariants = tranferDescr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    //check if the type of direction is defined ('there', 'back', etc...) in user answer
                if (posVariants.Any(x => Enum.IsDefined(typeof(FarmerPazle_Direction), x.ToLower())))
                {
                       //return the type of direction whitch is cast to the FarmerPazle_Direction enum
                    return posVariants
                        .Where(x => Enum.IsDefined(typeof(FarmerPazle_Direction), x.ToLower()))
                        .First()
                        .ToEnumButFirstlyTryParse<FarmerPazle_Direction>();
                }
                return default(FarmerPazle_Direction);
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return default(FarmerPazle_Direction); ;
        }
        #endregion
    }
}
