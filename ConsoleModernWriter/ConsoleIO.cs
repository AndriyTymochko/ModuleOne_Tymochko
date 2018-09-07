using System;
using System.Text;
using System.Runtime.InteropServices;

namespace ConsoleModernWriter
{
    public static class ConsoleIO
    {
        #region Fields & Properties
        public static readonly string ConsoleRowSeparator;
        private static readonly string _console_WaitingForKeyPressing;
        private static readonly string _console_WaitingForExit;
        private static readonly string _console_WaitingForGoToMainMenu;
        private static readonly string _console_Error;
        private static readonly string _console_InvalidInputData;
        private static readonly string _console_InvalidData_AttemptCount;
        #endregion

        #region Constructors
        static ConsoleIO()
        {
            ConsoleRowSeparator = new string('-', 52);
            _console_WaitingForKeyPressing = "Press any key to continue...";
            _console_WaitingForExit = "Press any key to exit...";
            _console_WaitingForGoToMainMenu = "Press any key for exit to the Main Menu...";
            _console_Error = "Some unhandled exception has been occurred!\nErrorDescription: {0}\nStackTrace:{1}";
            _console_InvalidInputData = "Invalid Input Data!";
            _console_InvalidData_AttemptCount = "Attempt count: {0}";
        }
        #endregion

        #region Methods
        public static void ResizeConsoleWindow(int width, int height)
        {
            Console.WindowHeight = height < 10 ? 10 : height;
            Console.WindowWidth = width < 10 ? 10 : width;
        }

        #region Informational region
        public static void Console_ShowErrorDescription(_Exception ex)
        {
            WriteColorTextNewLine(string.Format(_console_Error, ex.Message, ex.StackTrace), ConsoleColor.Red);
            Console_WaitingForKeyPressing(string.Format(_console_WaitingForExit, ex.Message));
        }

        public static void Console_ToMainMenu(ConsoleColor cc = ConsoleColor.White)
        {
            Console_WaitingForKeyPressing(_console_WaitingForGoToMainMenu);
        }


        public static void Console_WaitingForKeyPressing(ConsoleColor cc = ConsoleColor.White)
        {
            Console_WaitingForKeyPressing(_console_WaitingForKeyPressing);
        }

        public static void Console_WaitingForKeyPressing(string message, ConsoleColor cc = ConsoleColor.White)
        {
            WriteColorTextNewLine(message);
            Console.ReadKey();
        }
        #endregion

        #region Write without new line
        public static void WriteColorText(int number)
        {
            WriteColorText(number.ToString(), ConsoleColor.White);
        }

        public static void WriteColorText(string text)
        {
            WriteColorText(text, ConsoleColor.White);
        }

        public static void WriteColorText(StringBuilder text)
        {
            WriteColorText(text.ToString(), ConsoleColor.White);
        }

        public static void WriteColorText(int number, ConsoleColor cc)
        {
            WriteColorText(number.ToString(), cc);
        }

        public static void WriteColorText(string text, ConsoleColor cc)
        {
            WriteColorText(text, cc, Console.BackgroundColor);
        }

        public static void WriteColorText(string text, ConsoleColor cc, ConsoleColor backgroundColor)
        {
            ConsoleColor oldTextColor = Console.ForegroundColor;
            ConsoleColor oldBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = Enum.IsDefined(typeof(ConsoleColor), cc) ? cc : ConsoleColor.White;
            Console.BackgroundColor = Enum.IsDefined(typeof(ConsoleColor), backgroundColor) ? backgroundColor : ConsoleColor.Black;
           
            Console.Write(text, cc);
            
            Console.ForegroundColor = oldTextColor;
            Console.BackgroundColor = oldBackgroundColor;
        }

        public static void WriteColorText(string text, ConsoleColor textColor, int textAsNumberInAnotherColor, ConsoleColor anotherTextColor)
        {
            WriteColorText(text, textColor, textAsNumberInAnotherColor.ToString(), anotherTextColor, Console.BackgroundColor);
        }

        public static void WriteColorText(string text, ConsoleColor textColor, string anotherText, ConsoleColor anotherTextColor)
        {
            WriteColorText(text, textColor, anotherText, anotherTextColor, Console.BackgroundColor);
        }

        public static void WriteColorText(string text, ConsoleColor textColor, string anotherText, ConsoleColor anotherTextColor, ConsoleColor anotherTextBackgroundColor)
        {
            if (text.IndexOf(anotherText) < 0)
                WriteColorText(text, textColor);
            else
            {
                int indexOf = default(int);
                string tmp = text;
                do
                {
                    indexOf = tmp.IndexOf(anotherText);
                    if (indexOf < 0)
                    {
                        WriteColorText(tmp, textColor);
                        break;
                    }
                    else
                    {
                        WriteColorText(tmp.Substring(0, indexOf), textColor);
                        WriteColorText(tmp.Substring(indexOf, anotherText.Length), anotherTextColor, anotherTextBackgroundColor);
                    }

                    tmp = tmp.Substring(indexOf + anotherText.Length);
                } while (tmp.Length > 0);
            }
        }
        #endregion

        #region Write with new line
        public static void WriteColorTextNewLine()
        {
            WriteColorTextNewLine(string.Empty, ConsoleColor.White);
        }

        public static void WriteColorTextNewLine(string text)
        {
            WriteColorTextNewLine(text, ConsoleColor.White);
        }

        public static void WriteColorTextNewLine(StringBuilder text)
        {
            WriteColorTextNewLine(text, ConsoleColor.White);
        }


        public static void WriteColorTextNewLine(int number, ConsoleColor cc)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = Enum.IsDefined(typeof(ConsoleColor), cc) ? cc : ConsoleColor.White;
            Console.WriteLine(number.ToString(), cc);
        }

        public static void WriteColorTextNewLine(StringBuilder text, ConsoleColor cc)
        {
            WriteColorTextNewLine(text.ToString(), cc);
        }

        public static void WriteColorTextNewLine(string text, ConsoleColor cc)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = Enum.IsDefined(typeof(ConsoleColor), cc) ? cc : ConsoleColor.White;
            Console.WriteLine(text, cc);
        }

        public static void WriteColorTextNewLine(string text, ConsoleColor textColor, int textAsNumberInAnotherColor, ConsoleColor anotherTextColor)
        {
            WriteColorTextNewLine(text, textColor, textAsNumberInAnotherColor.ToString(), anotherTextColor);
        }

        public static void WriteColorTextNewLine(string text, ConsoleColor textColor, string anotherText, ConsoleColor anotherTextColor)
        {
            if (string.IsNullOrWhiteSpace(anotherText) || text.IndexOf(anotherText) < 0)
            {
                WriteColorTextNewLine(text, textColor);
            }
            else
            {
                WriteColorText(text, textColor, anotherText, anotherTextColor);
                WriteColorTextNewLine(string.Empty);
            }
        }


        #region Show Warning
        public static void ShowIncorrectUserInputDataWarning()
        {
            ShowIncorrectUserInputDataWarning(0);
        }

        public static void ShowIncorrectUserInputDataWarning(int attemptCount)
        {
            WriteColorTextNewLine(_console_InvalidInputData, ConsoleColor.Red);
            if (attemptCount > 0)
                WriteColorTextNewLine(string.Format(_console_InvalidData_AttemptCount, attemptCount), ConsoleColor.White, attemptCount.ToString(), ConsoleColor.Red);
        }
        #endregion

        //public void WriteEveryCharInColorBackground(
        //    string text, ConsoleColor textColor, ConsoleColor backgroundColor,
        //    string[] exceptions, ConsoleColor exceptionColor, ConsoleColor exceptionBackgroundColor)
        //{
        //    if (text.IndexOf(anotherText) < 0)
        //        WriteColorText(text, textColor);
        //    else
        //    {
        //        int indexOf = default(int);
        //        string tmp = text;
        //        do
        //        {
        //            indexOf = tmp.IndexOf(anotherText);
        //            if (indexOf < 0)
        //            {
        //                WriteColorText(tmp, textColor);
        //                break;
        //            }
        //            else
        //            {
        //                WriteColorText(tmp.Substring(0, indexOf), textColor);
        //                WriteColorText(tmp.Substring(indexOf, anotherText.Length), anotherTextColor);
        //            }

        //            tmp = tmp.Substring(indexOf + anotherText.Length);
        //        } while (tmp.Length > 0);
        //    }
        //}
        #endregion

        #endregion
    }
}

