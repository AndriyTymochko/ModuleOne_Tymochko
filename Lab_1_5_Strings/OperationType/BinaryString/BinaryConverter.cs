using System;
using System.Text;
using System.Linq;
using System.Threading;

using ConsoleModernWriter;

namespace Lab_1_5_Strings.OperationType.BinaryString
{
    internal class BinaryConverter : Rules, IOperationType
    {
        #region Fields & Properties 
        private readonly string _header;
        #endregion

        #region Constructors 
        public BinaryConverter() : this(500) { }
        public BinaryConverter(ushort delayBetweenTyping) : base(ushort.MinValue + 1, ushort.MaxValue, delayBetweenTyping)
        {
            _header = "Convert number to binary string.";
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

                ConsoleIO.ResizeConsoleWindow(Console.WindowWidth + 20, Console.WindowHeight);

                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);
                consoleLog.AppendLine(string.Format("{0}{1}{0}", ConsoleIO.ConsoleRowSeparator, "\n"));

                int answer = GetTypedValue(ref consoleLog);

                Console.Clear();
                ConsoleIO.WriteColorText(consoleLog);
                ConsoleIO.WriteColorTextNewLine("\n-!Well Done! You've made a great thing! Now your PC is trying to process it!-\n", ConsoleColor.Green);

                Convert(answer);//, ExpressionType.SubtractChecked);

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


        protected virtual int GetTypedValue(ref StringBuilder consoleLog)
        {
            try
            {
                int value = 0, attemptCount = 0;
                string typedValue = string.Empty;
                string showMessage = string.Format("Enter a positive integer value (it must be in range [{0};{1}]): -> ", MinTypingValue, MaxTypingValue);

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


        protected virtual void Convert(int answer)
        {
            try
            {
                int tmp = 0; 
                StringBuilder result = new StringBuilder();

                ConsoleIO.WriteColorText(string.Format("Converted value: {0} decimal format ({1:N2}) {0} binary format {0} ", "->", answer), ConsoleColor.White, "->", ConsoleColor.Red);
                while (answer > 0)
                {
                    tmp = answer % 2;
                    result.Insert(0, tmp);
                    answer /= 2;
                }

                if (result.Length % 4 != 0)
                    result.Insert(0, "0", 4 - result.Length % 4);

                while (result.Length > 0)
                {
                    Thread.Sleep(DelayBetweenTypingInMillisec);
                    ConsoleIO.WriteColorText(result[0].ToString(), result[0].ToString() == "0" ? ConsoleColor.Red : ConsoleColor.Green);
                    result.Remove(0, 1);
                    if (result.Length % 4 == 0) ConsoleIO.WriteColorText(" ");
                }
                ConsoleIO.WriteColorTextNewLine();
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


