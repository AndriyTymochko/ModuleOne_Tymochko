using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab_1_5_Strings.OperationType;
using ConsoleModernWriter;

namespace Lab_1_5_Strings
{
    class Program
    {
        #region Fields & Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        static void Main(string[] args)
        {
            ShowMainMenu();
        }


        private static void ShowMainMenu()
        {
            try
            {
                ushort delay = 3000;
                byte attemptCount = 0;
                IOperationType selectedOperation = null;

                StringBuilder attemptStr = new StringBuilder();
                ConsoleKeyInfo keyInfo = default(ConsoleKeyInfo);

                string mainMemu = Properties.Settings.Default.MainMenu;

                int consoleWindowWidth = Console.WindowWidth, consoleWindowHeight = Console.WindowHeight;
                do
                {
                    //Resize console window size to default width and height
                    ConsoleIO.ResizeConsoleWindow(consoleWindowWidth, consoleWindowHeight);

                    attemptCount = 0; attemptStr.Clear();
                    do
                    {
                        if (attemptCount < 5)
                        {
                            Console.Clear();
                            ConsoleIO.WriteColorTextNewLine(string.Format(mainMemu, "\t", "\n", ConsoleIO.ConsoleRowSeparator));

                            if (attemptCount > 0)
                            {
                                ConsoleIO.WriteColorTextNewLine("Invalid input data!", ConsoleColor.Red);
                                ConsoleIO.WriteColorText("Attempt count: ");
                                ConsoleIO.WriteColorTextNewLine(attemptCount.ToString(), ConsoleColor.Red);

                                ConsoleIO.WriteColorTextNewLine(string.Format("Entered keys: {0}{1}{2}", attemptStr, "\n", ConsoleIO.ConsoleRowSeparator));
                            }
                            else
                                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);

                            keyInfo = Console.ReadKey();
                            if (keyInfo.Key == ConsoleKey.Q)
                                return;
                            selectedOperation = OperationFactory.GetSelectedOperation(keyInfo.Key);

                            if (selectedOperation == default(IOperationType))
                                Console.Beep();

                            attemptStr.AppendFormat((attemptCount > 0 ? ", " : string.Empty) + "'{0}'", keyInfo.KeyChar);
                            attemptCount++;
                        }
                        else
                        {
                            ConsoleIO.WriteColorTextNewLine("\nIt looks like that you don't know what are you doing or maybe you are an idiot!", ConsoleColor.Red);
                            Console.ReadKey();
                            return;
                        }
                    } while (selectedOperation == default(IOperationType));

                    ConsoleIO.WriteColorTextNewLine(string.Format("\nNice choice! \nYou'll be redirected to the {0} after '{1}'sec", selectedOperation.ToString(), delay / 1000), ConsoleColor.Green);
                    System.Threading.Thread.Sleep(delay);

                } while (selectedOperation.RunAndContinue());
            }
            catch (Exception ex)
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }
        #endregion
    }
}
