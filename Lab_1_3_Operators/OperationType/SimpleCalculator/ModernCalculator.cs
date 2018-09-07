using System;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using ConsoleModernWriter;
using Lab_1_3_Operators.OperationType.SimpleCalculator.Buffer;


namespace Lab_1_3_Operators.OperationType.SimpleCalculator
{
    internal class ModernCalculator : IOperationType
    {
        #region Fields & Properties 
        /// <summary>
        /// to store a reference to the implementation of abstract class 'Calc<MathOperation, char>'
        /// </summary>
        private readonly Calc<MathOperation, char> _calcProvider;
        
        /// <summary>
        /// to store all possible math operations (dictionary with unique math operations) in this implementation of calculator
        /// </summary>
        private static IDictionary<MathOperation, char> _calcOperationType;
        #endregion

        #region Constructors 
        public ModernCalculator()
        {
                //to initialise the implementation of the abstract class 'Calc<MathOperation, char>'
            _calcProvider = new CalcOperation();
        }

        static ModernCalculator()
        {
                //to initialise the dictionary with unique math operations
                //key as enum (posible math operations)
                //value as char (for compare with the user input key (math operation) and for showing it in calc 'display')
            _calcOperationType = new Dictionary<MathOperation, char>()
                { 
                    { MathOperation.Multiplication, '*' },
                    { MathOperation.Divide, '/' },
                    { MathOperation.Addition, '+' },
                    { MathOperation.Subtraction, '-' },
                    { MathOperation.Exponentiation, 'e' }
                };
        }
        #endregion


        #region Methods 
        public virtual bool RunAndContinue()
        {
            try
            {
                    //to store the user last pressed key info
                ConsoleKeyInfo keyInfo = default(ConsoleKeyInfo);
                    //if user press any button whitch is deffined in _calcOperationType dictionary (math operation)
                    //this variable will be used to store a referene to the struct KeyValuePair<MathOperation, char>
                    //which will be show the current math operation (+, -, *, .....) 
                KeyValuePair<MathOperation, char> operation = default(KeyValuePair<MathOperation, char>);
                    //show calculator initial screen 
                _calcProvider.ClearBufferAndShowInitScreen();
                do // until user presses on the key 'q' or 'Q'
                {
                        // get the last pressed key info
                    keyInfo = Console.ReadKey(true);
                    switch (GetKeyType(keyInfo, ref operation))
                    {
                        //if numeric / point / comma key has been pressed 
                        case KeyType.Numeric:
                            char number = keyInfo.KeyChar == '.' ? _calcProvider.NumberDecimalSeparator : keyInfo.KeyChar;
                            _calcProvider.AddNewNumeric(number);
                            break;
                        //if any calc operation key (+, -, *, /, e ...) has been pressed than try to do entered math operation
                        case KeyType.CalcOperation:
                            _calcProvider.MathOperation(operation);
                            break;
                        //if 'c' button has been pressed, than delete last entered symbol
                        case KeyType.DeleteLast:
                            _calcProvider.DeleteLastNumeric();
                            break;
                        //if 'Enter' button has been pressed then clear calc upper row (buffer) and show the result of operation    
                        case KeyType.ClearBufferAndShowResult:
                            _calcProvider.ClearBufferAndShowResult();
                            break;
                        //if 'c' button has been pressed than clear calculator buffer and show initial screen
                        case KeyType.ClearBufferAndShowInitScreen:
                            _calcProvider.ClearBufferAndShowInitScreen();
                            break;
                        //if 'q' or 'Q' button has been pressed than exit from calculator    
                        case KeyType.Exit:
                            return true;
                    }
                } while (true); 
            }
            catch(Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return false;
        }

        /// <summary>
        /// depending on the user input (pressed key) in calculator, 
        /// return the type of operation which must be done 
        /// </summary>
        /// <param name="keyInfo"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public virtual KeyType GetKeyType(ConsoleKeyInfo keyInfo, ref KeyValuePair<MathOperation, char> operation)
        {
            try
            {
               int number = 0;

               //if numeric / point / comma key has been pressed 
               if (int.TryParse(keyInfo.KeyChar.ToString(), out number) || keyInfo.KeyChar == ',' || keyInfo.KeyChar == '.')
                   return KeyType.Numeric;
               //if backspace '<-' button has been pressed 
               else if(keyInfo.KeyChar == '\b')
                   return KeyType.DeleteLast;
               //if 'c' button has been pressed 
               else if (keyInfo.KeyChar.ToString().ToLower() == "c") 
                   return KeyType.ClearBufferAndShowInitScreen;
               //if 'Enter' button has been pressed    
               else if (keyInfo.KeyChar.ToString().ToLower() == "\r") 
                   return KeyType.ClearBufferAndShowResult;
               //if 'q' or 'Q' button has been pressed    
               else if (keyInfo.KeyChar.ToString().ToLower() == "q")
                   return KeyType.Exit;
               else
               {
                   //if any calc operation key (+, -, *, /, e ...) has been pressed    
                   operation = _calcOperationType.FirstOrDefault(x => x.Value == keyInfo.KeyChar);
                   if(Enum.IsDefined(typeof(MathOperation), operation.Key))
                       return KeyType.CalcOperation;
                   else
                       return KeyType.Na;
               }
            }
            catch(Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return KeyType.Na;
        }

        public override string ToString()
        {
            return "'Simple calculator'";
        }
        #endregion
    }
}