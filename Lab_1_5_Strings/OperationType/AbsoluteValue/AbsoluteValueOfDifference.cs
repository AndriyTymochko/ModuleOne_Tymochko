using System;
using System.Text;
using System.Linq;
using System.Threading;

using ConsoleModernWriter;

namespace Lab_1_5_Strings.OperationType.AbsoluteValue
{
    internal class AbsoluteValueOfDifference : Rules, IOperationType
    {
        #region Fields & Properties 
        private readonly string _header;
        #endregion

        #region Constructors 
        public AbsoluteValueOfDifference() : this(500) { }
        public AbsoluteValueOfDifference(ushort delayBetweenTyping) : base(delayBetweenTyping)
        {
            _header = "Get Absolute value of the difference between two numbers.";
        }
        #endregion

        #region Methods 

        public virtual bool RunAndContinue()
        {
            try
            {
                StringBuilder consoleLog = new StringBuilder();
                
                Console.Clear();
                ConsoleIO.WriteColorTextNewLine(_header, ConsoleColor.Cyan);
                consoleLog.AppendLine(_header);

                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);
                consoleLog.AppendLine(string.Format("{0}{1}{0}", ConsoleIO.ConsoleRowSeparator, "\n"));

                string answer = string.Format("{0}{1}{2}",
                    new string(ReplaceToSymbol, GetTypedValue("first", ref consoleLog)),
                    Delimiter,
                    new string(ReplaceToSymbol, GetTypedValue("second", ref consoleLog)));

                Console.Clear();
                ConsoleIO.WriteColorText(consoleLog);
                ConsoleIO.WriteColorTextNewLine("\n-!Well Done! You've made a great thing! Now your PC is trying to process it!-\n", ConsoleColor.Green);

                DoMathOperation(answer);//, ExpressionType.SubtractChecked);

                ConsoleIO.Console_ToMainMenu();
                return true;
            }
            catch(Exception ex)
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return false;
        }


        protected virtual int GetTypedValue(string valueName, ref StringBuilder consoleLog)
        {
            try
            {
                int value = 0, attemptCount = 0;
                string typedValue = string.Empty;
                string showMessage = string.Format("Enter {0} value (it must be integer and in range [{1};{2}]): -> ", valueName, MinTypingValue, MaxTypingValue);

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
                } while (!int.TryParse(typedValue, out value) || value < MinTypingValue || value > MaxTypingValue);
                consoleLog.AppendLine(showMessage + typedValue + "\n" + ConsoleIO.ConsoleRowSeparator);

                return value;
            }
            catch (Exception ex)
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return 0;
        }


        protected virtual void DoMathOperation(string answer)
        {
            try
            {
                int firstArg = 0, secondArg = 0;
                int answerLength = answer.Length;

                string emptySpace = string.Empty;
                string endingVar1 = string.Concat(ReplaceToSymbol, Delimiter);
                string endingVar2 = string.Concat(Delimiter, ReplaceToSymbol);
                string replaceFrom = string.Concat(ReplaceToSymbol, Delimiter, ReplaceToSymbol);

                while (true)
                {
                    Thread.Sleep(DelayBetweenTypingInMillisec);
                    ConsoleIO.WriteColorText(answer, ConsoleColor.White, Delimiter.ToString(), ConsoleColor.Red);

                    firstArg = answer.Substring(0, answer.IndexOf(Delimiter)).Count(x => x == ReplaceToSymbol);
                    secondArg = answer.Substring(answer.IndexOf(Delimiter)).Count(x => x == ReplaceToSymbol);
                    ConsoleIO.WriteColorTextNewLine(string.Concat(new string(' ', 3 + answerLength - answer.Length), "|   ", firstArg , " - ", secondArg, " = ",(firstArg - secondArg)));

                    if (answer.IndexOf(endingVar1) < 0 || answer.IndexOf(endingVar2) < 0)
                        return;

                    //"↓▼"
                    emptySpace = new string(' ', 3 + answerLength);
                    ConsoleIO.WriteColorTextNewLine(string.Format("{0}{1}{0}", emptySpace, "↓"), ConsoleColor.DarkGreen);
                    answer = answer.Replace(replaceFrom, Delimiter.ToString());
                }
            }
            catch (Exception ex)
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }


        public override string ToString()
        {
            return _header;
        }
        #endregion
    }
}

