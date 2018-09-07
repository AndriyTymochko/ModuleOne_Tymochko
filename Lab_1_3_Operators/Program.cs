using System;
using System.Text;

using Lab_1_3_Operators.OperationType;
using ConsoleModernWriter;

namespace Lab_1_3_Operators
{
    class Program
    {
        #region Methods
        static void Main(string[] args)
        {
            ShowMainMenu();
        }
        

        private static void ShowMainMenu()
        {
            try
            {
                    //pause (in millisec) after user correct answer, before redirecting to selected operation
                ushort delay = 3000;
                    //to store the user wrong answers count in main menu
                byte attemptCount = 0;
                    //to store the number of MAX posible wrong answers in main menu
                byte maxAttemptCount = 0;
                    //try parse unprotected value of MAX posible wrong answers in main menu from config/settings file
                if (!byte.TryParse(Security.SecurityHelper.ProtectConfigParameter(Properties.Settings.Default.MainMenu_MaxIncorectInputCount), out maxAttemptCount))
                    maxAttemptCount = 5; //if the parsing was unsuccessful than set the default value

                    //to store the instance of a class/struct that is implementing an interface IOperationType
                IOperationType selectedOperation = default(IOperationType);
                    //all user wrong answers
                StringBuilder attemptStr = new StringBuilder();
                //to store user input key
                ConsoleKeyInfo keyInfo = default(ConsoleKeyInfo);
                    //get menu description from setting file
                string mainMemu = Properties.Settings.Default.MainMenu;
                    //to store default  console window size (width and height)
                int consoleWindowWidth = Console.WindowWidth, consoleWindowHeight = Console.WindowHeight;

                do // while user wanna running the program (for default  - always:) )
                {
                    //Resize console window size to default width and height
                    ConsoleIO.ResizeConsoleWindow(consoleWindowWidth, consoleWindowHeight);
                    attemptCount = 0; attemptStr.Clear(); //clear console 
                    do // while user is entering invalid data
                    {
                        if (attemptCount < 5) // check the number of incorrect user input in sequence
                        {
                                //clear console 
                            Console.Clear();  
                                //show main menu
                            ConsoleIO.WriteColorTextNewLine(string.Format(mainMemu, "\t", "\n", ConsoleIO.ConsoleRowSeparator));

                            if (attemptCount > 0) //check if invalid input data have been entered
                            {
                                //show the number of incorrect input data entering
                                ConsoleIO.ShowIncorrectUserInputDataWarning(attemptCount);
                                    //show all wrong answers in sequence
                                ConsoleIO.WriteColorTextNewLine(string.Format("Entered keys: {0}{1}{2}", attemptStr, "\n", ConsoleIO.ConsoleRowSeparator));
                            }
                            else // if it's a first program running, show the row separator (for default '-----------')
                            {
                                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);
                            }

                                //read user input
                            keyInfo = Console.ReadKey(true);
                            if (keyInfo.Key == ConsoleKey.Q) //if user entered 'q' or 'Q' ->  exit
                                return;

                                //get an instance of a class that is implementing an interface IOperationType (depends on user answer)
                            selectedOperation = OperationFactory.GetSelectedOperation(keyInfo.Key);

                                //add user incorrect answers (if it was)
                            attemptStr.AppendFormat((attemptCount > 0 ? ", " : string.Empty) + "'{0}'", keyInfo.KeyChar);
                            attemptCount++;
                        }
                        else //if number of user incorrect answers >= MAX 
                        {
                            ConsoleIO.WriteColorTextNewLine("\nIt looks like that you don't know what are you doing or maybe you are an idiot!", ConsoleColor.Red);
                            Console.ReadKey();
                            return; 
                        }
                    } while (selectedOperation == default(IOperationType)); // while user is entering invalid data

                    ConsoleIO.WriteColorTextNewLine(string.Format("\nNice choice! \nYou'll be redirected to the {0} after '{1}'sec", selectedOperation.ToString(), delay / 1000), ConsoleColor.Green);
                    System.Threading.Thread.Sleep(delay);

                } while (selectedOperation.RunAndContinue());
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
