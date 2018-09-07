using System;
using System.Text;
using System.Diagnostics;

using ConsoleModernWriter;


namespace Lab_1_3_Operators.OperationType.Factorial
{
    class FactorialOperation : IOperationType
    {
        #region Fields & Properties
        /// <summary>
        /// to store all restrictions and permisions that are connected with the factorial operation
        /// </summary>
        private readonly GeneralRules _rulesProvider;
        #endregion


        #region Constructors 
        public FactorialOperation()
        {
                //[byte.MinValue; 170] it's a posible range for user input value  
            _rulesProvider = new GeneralRules(byte.MinValue, 170);
        }
        #endregion


        #region Methods
        public virtual bool RunAndContinue()
        {
            try
            {
                    //resize console window 
                ConsoleIO.ResizeConsoleWindow(Console.WindowWidth + 20, Console.WindowHeight);
                    //to store current operation log (for redrawing in console)
                StringBuilder consoleLog = new StringBuilder();
                    //clear console 
                Console.Clear();
                    //show current task name
                ConsoleIO.WriteColorTextNewLine(this.ToString(), ConsoleColor.Cyan);
                    //add current task name to gen log
                consoleLog.AppendLine(this.ToString());
                    //show text separator (by default: ------------)
                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);
                    //add text separator to gen log
                consoleLog.AppendLine(string.Format("{0}{1}{0}", ConsoleIO.ConsoleRowSeparator, "\n"));
                 
                //get user answer
                int answer = GetTypedValue(ref consoleLog);

                    //clear console 
                Console.Clear();
                    //show all log info
                ConsoleIO.WriteColorText(consoleLog);
                    //show the congrats message 
                ConsoleIO.WriteColorTextNewLine("\n-!Well Done! You've made a great thing! Now your PC is trying to process it!-\n", ConsoleColor.Green);

                    //for getting the time of opeartiona
                Stopwatch sw = new Stopwatch();
                sw.Start(); // start the stopwatch
                double factorial = GetFactorial(answer);
                sw.Stop(); // stop the stopwatch

                    //show the generic output info (facotrail for -> result -> time of operation)
                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);
                ConsoleIO.WriteColorText(string.Format("The factorail of '{0}' -> ", answer), ConsoleColor.White, answer.ToString(), ConsoleColor.Red);
                ConsoleIO.WriteColorTextNewLine(string.Format("'{0}'", factorial), ConsoleColor.White, factorial.ToString(), ConsoleColor.Green);
                ConsoleIO.WriteColorTextNewLine(string.Format("Time of calculation -> '{0}' milliseconds", sw.Elapsed.TotalMilliseconds), ConsoleColor.White, sw.Elapsed.TotalMilliseconds.ToString(), ConsoleColor.Yellow);
                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator + "\n");

                    //show the suggestion of pressing any key to return to the main menu
                ConsoleIO.Console_ToMainMenu();
                return true;
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return false;
        }

        /// <summary>
        /// Try to get a user input value (number) for calc the factorial
        /// </summary>
        /// <param name="consoleLog"></param>
        /// <returns></returns>
        protected virtual int GetTypedValue(ref StringBuilder consoleLog)
        {
            try
            {
                int value = 0, attemptCount = 0;
                string typedValue = string.Empty;
                string showMessage = string.Format("Enter a value to find the factorial (it must be integer and in range [{0};{1}]): -> ", _rulesProvider.MinTypingValue, _rulesProvider.MaxTypingValue);

                do
                {
                    Console.Clear();
                    ConsoleIO.WriteColorTextNewLine(consoleLog);

                    if (attemptCount > 0)
                    {
                        ConsoleIO.WriteColorTextNewLine("Invalid input data!", ConsoleColor.Red);
                        ConsoleIO.WriteColorText("Attempt count: ");
                        ConsoleIO.WriteColorTextNewLine(attemptCount.ToString(), ConsoleColor.Red);
                        ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);
                    }

                    ConsoleIO.WriteColorText(showMessage);
                    typedValue = Console.ReadLine();
                    attemptCount++;
                } while (!int.TryParse(typedValue, out value) || value < _rulesProvider.MinTypingValue || value > _rulesProvider.MaxTypingValue);
                consoleLog.AppendLine(showMessage + typedValue + "\n" + ConsoleIO.ConsoleRowSeparator);

                return value;
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return 0;
        }

        /// <summary>
        /// Calculate factorial by using the recursion
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        protected virtual double GetFactorial(int number)
        {
            try
            {
                return number > 1 ? number * GetFactorial(number - 1) : 1;
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return 0;
        }
        

        public override string ToString()
        {
            return "'Factorial'";
        }
        #endregion
    }
}
