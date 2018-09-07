using System;
using System.Collections.Generic;
using System.Text;

using ConsoleModernWriter;

namespace Lab_1_3_Operators.OperationType.SimpleCalculator
{
    internal class CalcOperation : Calc<MathOperation, char>
    {
        #region Fields
        /// <summary>
        /// to store the flag that shows if the current operation is a math operation
        /// it neccessary, when user has pressed the key that means one math operation, (for exapmle '+')
        /// but then he changed his mind and preessed another one button whitch means the another one math operation (for exapmle '/')
        /// so it must be changed the graphic view of this operation in the calc buffer (upper row in calc display)
        /// </summary>
        private bool _wasAlreadyCalcOp = false;
        #endregion

        #region Methods
        /// <summary>
        /// method that is handled the event 
        /// of adding a new numeric (or decimal number separator) symbol
        /// that has been pressed by user
        /// </summary>
        /// <param name="number"></param>
        public override void AddNewNumeric(char number)
        {
            try
            {
                    //unlock the changing of result in local buffer
                OpProvider.LockResult = false;
                    //get the user input string in calc screen
                string inputText = GetRow(RowInputData).ToString().TrimEnd();
                    //get only user input data (numbers and decimal number separator)
                string alreadyInpNumber = inputText.Substring(inputText.LastIndexOf('_') + 1, inputText.LastIndexOf("|") - inputText.LastIndexOf('_') - 1);
                    //variable that will be contained an output string in calc display
                StringBuilder outputText = new StringBuilder(number.ToString());
                    //if the current screen state is by default (___0) and user current input key is '0' 
                    //or user current input string (with numbers) length > 10 and user current input operation isn't a math operation
                    //or user current input key == decimal number separator (',') and  user current input string already consist that separator 
                    //than return
                if ((alreadyInpNumber == DefaultValueForInputString && number.ToString() == DefaultValueForInputString) 
                     || (alreadyInpNumber.Length > 10 && !_wasAlreadyCalcOp) 
                     || (number == NumberDecimalSeparator && inputText.IndexOf(NumberDecimalSeparator) >= 0))
                    return;

                    //if the row with input data != 0 (DefaultValueForInputString) or user current input key == decimal number separator
                    //and user current input operation isn't a math operation
                if ((alreadyInpNumber != DefaultValueForInputString || number == NumberDecimalSeparator) && !_wasAlreadyCalcOp)
                    outputText.Insert(0, alreadyInpNumber);
                else 
                    _wasAlreadyCalcOp = false;

                    //define a new view of the calculator screen (row with input data), based on the current input data (and on it length)
                inputText = "|" + new string('_', UndelineSpaceCalcUI.Length - outputText.Length) + outputText + "|";
                    //write the user input data to the buffer 
                Draw(inputText, 0, RowInputData);
                    //clear console 
                Console.Clear();
                     //print the buffer to the console screen.
                Print();
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// method that is handled the event 
        /// of deleting the last entered number (or decimal number separator)
        /// that has been pressed by user
        /// </summary>
        public override void DeleteLastNumeric()
        {
            try
            {
                    //get the user input string in calc screen
                string inputText = GetRow(RowInputData).ToString().TrimEnd();
                    //get only user input data (numbers and decimal number separator)
                string alreadyInpNumber = inputText.Substring(inputText.LastIndexOf('_') + 1, inputText.LastIndexOf("|") - inputText.LastIndexOf('_') - 1);
                    //variable that will be contained an output string in calc display
                string outputText = string.Empty;
                    
                    //if there is only one number on the calc screen, set the output as default (in init screen)
                if (alreadyInpNumber.Length == 1)
                    outputText = DefaultValueForInputString;
                else //delete the last entered number or decimal number separator
                    outputText = alreadyInpNumber.Substring(0, alreadyInpNumber.Length - 1);

                    //define a new view of the calculator screen (row with input data), based on the current input data (and on it length)
                inputText = "|" + new string('_', UndelineSpaceCalcUI.Length - outputText.Length) + outputText + "|";
                    
                    //write the user input data to the buffer 
                Draw(inputText, 0, RowInputData);
                    //clear console 
                Console.Clear();
                    //print the buffer to the console screen.
                Print();
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// Method that is handled the event 
        /// of deleting the last entered number (or decimal number separator)
        /// that has been pressed by user
        /// </summary>
        /// <param name="operation"></param>
        public override void MathOperation(KeyValuePair<MathOperation, char> operation)
        {
            try
            {
                    //unlock the changing of result in local buffer
                OpProvider.LockResult = false;
                string buffer = string.Empty;
                    //get the user input string in calc screen
                string inputText = GetRow(RowInputData).ToString().TrimEnd();
                    //get only user input data (numbers and decimal number separator)
                string alreadyInpNumber = inputText.Substring(inputText.LastIndexOf('_') + 1, inputText.LastIndexOf("|") - inputText.LastIndexOf('_') - 1);
                    //check if the last operation wsa the math operation
                if (_wasAlreadyCalcOp)
                {
                        //check if the local buffer is empty
                    if (OpProvider.BufferNumber.Length == 0)
                    {
                            //check if the user current input string has a decimal.NaN value 
                            //if yes, than clear local buffer, show calculator initial screen and return from this method
                        if (alreadyInpNumber.IndexOf(double.NaN.ToString()) >= 0)
                        {
                            ClearBufferAndShowInitScreen();
                            return;
                        }
                            //set the current operation buffer value as default ('0')
                        OpProvider.Result = 0;
                            //set the flag that shows if the current operation is a math operation as false
                        _wasAlreadyCalcOp = false;
                    } //check if the last math operation was the same as this one (for examle + and +) 
                    else if (operation.Key != OpProvider.LastOperation.Key)
                    {
                            //set a new value to the string that represents as log of previous user math operations
                            //it is being shown on the row that is upper 
                            //until user will press the 'Enter' key ()
                        OpProvider.BufferNumber = OpProvider.BufferNumber.Replace(
                            OpProvider.LastOperation.Value, 
                            operation.Value, 
                            OpProvider.BufferNumber.LastIndexOf(OpProvider.LastOperation.Value), 
                            1);

                            //save the current math operation type to buffer
                        OpProvider.LastOperation = operation;

                            //define a new view of the calculator buffer screen (row with the previous math operations)
                        buffer = OpProvider.BufferNumber.Length <= EmptySpaceCalcUI.Length
                            ? "|" + new string(' ', EmptySpaceCalcUI.Length - OpProvider.BufferNumber.Length) + OpProvider.BufferNumber + "|"
                            : "|" + OpProvider.BufferNumber.ToString(OpProvider.BufferNumber.Length - EmptySpaceCalcUI.Length, EmptySpaceCalcUI.Length) + "|";
                            //replace the row with the previous math operations in the buffer 
                        Draw(buffer, 0, RowBufferOperation);
                    }
                }

                if (!_wasAlreadyCalcOp)
                {
                    if (OpProvider.LastOperation.Equals(default(KeyValuePair<MathOperation, char>)))
                        OpProvider.LastOperation = operation;

                    string outputText = string.Empty;

                    if (OpProvider.BufferNumber.Length == 0)
                        OpProvider.Result = Double.Parse(alreadyInpNumber);
                    else
                    {
                        if (OpProvider.LastOperation.Key != SimpleCalculator.MathOperation.Exponentiation)
                        {
                            string resultAsString = OpProvider.Result.ToString();
                            resultAsString += ((resultAsString.IndexOf(',') < 0 && resultAsString.IndexOf(',') < 0) ? ",0" : string.Empty);
                            alreadyInpNumber += ((alreadyInpNumber.IndexOf(',') < 0 && alreadyInpNumber.IndexOf(',') < 0) ? ",0" : string.Empty);

                            OpProvider.Result = Evaluate(string.Format("({0}{1}{2})", resultAsString, OpProvider.LastOperation.Value.ToString(), alreadyInpNumber));
                        }
                        else
                            OpProvider.Result = Math.Pow(OpProvider.Result, Double.Parse(alreadyInpNumber));
                    }

                    OpProvider.BufferNumber.AppendFormat(" {0} {1}", alreadyInpNumber, operation.Value);
                    OpProvider.LastOperation = operation;

                    buffer = OpProvider.BufferNumber.Length <= EmptySpaceCalcUI.Length
                        ? "|" + new string(' ', EmptySpaceCalcUI.Length - OpProvider.BufferNumber.Length) + OpProvider.BufferNumber + "|"
                        : "|" + OpProvider.BufferNumber.ToString(OpProvider.BufferNumber.Length - EmptySpaceCalcUI.Length, EmptySpaceCalcUI.Length) + "|";

                    string resultOfOperation = "|" + new string('_', UndelineSpaceCalcUI.Length - OpProvider.Result.ToString().Length) + OpProvider.Result + "|";

                    Draw(buffer, 0, RowBufferOperation);
                    Draw(resultOfOperation, 0, RowInputData);
                    _wasAlreadyCalcOp = true;
                }

                    //clear console 
                Console.Clear();
                    //print the buffer to the console screen.
                Print();
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// Method that is handled the event 
        /// of 'Enter' button pressing
        /// </summary>
        public override void ClearBufferAndShowResult()
        {
            try
            {
                    //check if there is a saved previous math operation in buffer 
                    //it's necessary when enter key was pressed a several times one by one
                    //and the next math operation must be the same as a previous one
                if (!OpProvider.LastOperation.Equals(default(KeyValuePair<MathOperation, char>)))
                {
                        //to store the result of math operation
                    double result = 0.0;
                        //get the user input string in calc screen
                    string inputText = GetRow(RowInputData).ToString().TrimEnd();
                        //to store only user input data (numbers and decimal number separator)
                    double alreadyInpNumber = default(double);

                        //check if the parsing of user entered data (numbers and decimal number separator) is successfull
                    if (Double.TryParse(inputText.Substring(inputText.LastIndexOf('_') + 1, inputText.LastIndexOf("|") - inputText.LastIndexOf('_') - 1), out alreadyInpNumber))
                    {
                            //check if parsed data is not a double.NaN or double.Infinity
                        if (!double.IsNaN(alreadyInpNumber) && !double.IsInfinity(alreadyInpNumber))
                        {
                                //check if current math operation in not a Math.Pow
                            if (OpProvider.LastOperation.Key != SimpleCalculator.MathOperation.Exponentiation)
                            {

                                string resultAsString = OpProvider.Result.ToString();
                                resultAsString += ((resultAsString.IndexOf(',') < 0 && resultAsString.IndexOf(',') < 0) ? ",0" : string.Empty);

                                string alreadyInpNumberAsString = alreadyInpNumber.ToString();
                                alreadyInpNumberAsString += ((alreadyInpNumberAsString.IndexOf(',') < 0 && alreadyInpNumberAsString.IndexOf(',') < 0) ? ",0" : string.Empty);

                                if (!OpProvider.LockResult)
                                    result = Evaluate(string.Format("({0}{1}{2})", resultAsString, OpProvider.LastOperation.Value.ToString(), alreadyInpNumberAsString));
                                else
                                    result = Evaluate(string.Format("({0}{1}{2})", alreadyInpNumberAsString, OpProvider.LastOperation.Value.ToString(), resultAsString));
                            }
                            else //current math operation is a Math.Pow
                            {
                                    //check if this is a first time when enter key has been pressed in current session
                                if (!OpProvider.LockResult)
                                {
                                    //if yes than get the specified number (in buffer) raised to the specified power (last entered value)
                                    result = Math.Pow(OpProvider.Result, alreadyInpNumber);
                                }
                                else
                                {
                                    //if yes than get the specified number (that is being shown to user) raised to the specified power (value in buffer)
                                    result = Math.Pow(alreadyInpNumber, OpProvider.Result);
                                }
                            }
                        }

                            //clear 
                        OpProvider.BufferNumber.Clear();
                            //try to set a new value to the result variable in buffer
                            //but if the OpProvider.LockResult == true
                            //then a new value won't be setted to the result variable
                            //--------------------------------
                            //it used for saving the last user entered number 
                            //on which current shown value will be (+, -, *, /, e)
                        OpProvider.Result = alreadyInpNumber;
                            //lock current result in buffer for changing
                        OpProvider.LockResult = true;

                        string buffer = "|" + new string(' ', EmptySpaceCalcUI.Length) + "|";
                        string resultOfOperation = string.Empty;

                        if (UndelineSpaceCalcUI.Length >= result.ToString().Length)
                            resultOfOperation = "|" + new string('_', UndelineSpaceCalcUI.Length - result.ToString().Length) + result + "|";
                        else
                            resultOfOperation = "|_" + result.ToString("#").Substring(0, UndelineSpaceCalcUI.Length - 1) + "|";
                           
                            //replace the row with the previous math operations in the buffer 
                        Draw(buffer, 0, RowBufferOperation);
                            //replace the row with the current user input value in the buffer 
                        Draw(resultOfOperation, 0, RowInputData);
                            //this flag will be showing that the last operation was the math operation (+, -, *, /) 
                        _wasAlreadyCalcOp = true;
                    }
                }
                    //clear console 
                Console.Clear();
                    //print the buffer to the console screen.
                Print();
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }
        #endregion
    }
}
