using System;
using System.Diagnostics;

using ConsoleModernWriter;


namespace Lab_1_3_Operators.OperationType.GuessNumber
{
    class GuessNumberOperation : IOperationType
    {

        #region Fields & Properties
        /// <summary>
        /// to store all restrictions and permisions that are connected with the guessing a number task
        /// </summary>
        private readonly Rules _rulesProvider;
        #endregion

        #region Constructors 
        public GuessNumberOperation()
        {
            //4 - lides count;
            //1 - min value to guess;
            //25  max value to guess  
            _rulesProvider = new Rules(4, 1, 25);
        }
        #endregion

        #region Methods
        public virtual bool RunAndContinue()
        {
            try
            {
                    //clear console 
                Console.Clear();
                    //show current task name
                ConsoleIO.WriteColorTextNewLine(this.ToString(), ConsoleColor.Cyan);
                    //add current rues description
                ConsoleIO.WriteColorTextNewLine(_rulesProvider.RulesDescription);
                 
                    //for getting the time of opeartiona
                Stopwatch sw = new Stopwatch();
                    // start the stopwatch
                sw.Start();
                    //play game
                bool IsAWinner = TryToGuessTheNumber();
                    // stop the stopwatch
                sw.Stop();

                //check the result
                if (!IsAWinner)
                {
                    ConsoleIO.WriteColorTextNewLine("\n-!-!-!-!-!-!-!GAME OVER!-!-!-!-!-!-!-\n", ConsoleColor.Red);
                }
                else //user find the right answer
                {
                    ConsoleIO.WriteColorTextNewLine("\n-!-!-!-!-!-!-!Well Done!-!-!-!-!-!-!-\n", ConsoleColor.Green);
                    ConsoleIO.WriteColorTextNewLine(string.Format("{0}!, you are a winner! Your Time is: '{1}' sec\n", Environment.UserName, sw.Elapsed.Seconds), ConsoleColor.Green);
                }
                    
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
        /// guess number algorithm 
        /// </summary>
        /// <returns>
        /// true - if user find the correct answer 
        /// false - if user didn't find the correct answer 
        /// </returns>
        protected virtual bool TryToGuessTheNumber()
        {
            try
            {
                Random rndObj = new Random();

                int lastMinValue = _rulesProvider.MinTypingValue;
                int lastMaxValue = _rulesProvider.MaxTypingValue;
                //generate a random value that user must guess
                int rndValue = rndObj.Next(lastMinValue, lastMaxValue);

                int enteredValue = 0, attemptCount = 0;
                string typedValue = string.Empty;
                string showMessage = "LifesCount: '{0}'\nGuessed number is at range [{1};{2}]. Enter your guess: -> ";

                do
                {
                    //check if it's not a first loop
                    if (attemptCount != 0)
                    {
                        //show the info message that current input value was too low or too high
                        ConsoleIO.WriteColorTextNewLine(
                            string.Format("{0} - Too {1}!", enteredValue, (enteredValue < rndValue ? "low" : "high")),
                            (ConsoleColor)rndObj.Next((int)ConsoleColor.DarkBlue, (int)ConsoleColor.White));
                        ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);
                    }

                    //check if entered value is in range of min and max possible values
                    if (enteredValue >= _rulesProvider.MinTypingValue && enteredValue > lastMinValue
                        && enteredValue <= _rulesProvider.MaxTypingValue && enteredValue <= lastMaxValue)
                    {
                        lastMinValue = enteredValue < rndValue ? enteredValue : lastMinValue;
                        lastMaxValue = enteredValue > rndValue ? enteredValue : lastMaxValue;
                    }

                    ConsoleIO.WriteColorText(string.Format(showMessage, _rulesProvider.MaxAttemptsCount - attemptCount, lastMinValue, lastMaxValue));
                    typedValue = Console.ReadLine();
                    attemptCount++;
                } while ((!int.TryParse(typedValue, out enteredValue) || enteredValue != rndValue) && attemptCount < _rulesProvider.MaxAttemptsCount);
                    
                //show text separator (by default: ------------)
                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);
                    //check if user answer number >= MAX attempts
                return attemptCount >= _rulesProvider.MaxAttemptsCount ? false : true;
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return false;
        }

        public override string ToString()
        {
            return "'Guess the number'";
        }
        #endregion
    }
}
