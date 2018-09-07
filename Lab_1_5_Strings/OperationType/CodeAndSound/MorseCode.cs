using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

using ConsoleModernWriter;

namespace Lab_1_5_Strings.OperationType.CodeAndSound
{
    class MorseCode : Rules, IOperationType
    {
        #region Fields & Properties
        private readonly string _header;
        private static readonly IDictionary<char, string> _morseAlphabetDictionary;
        #endregion

        #region Constructors
        public MorseCode() : this(500) { }
        public MorseCode(ushort delayBetweenTyping)
            : base(delayBetweenTyping)
        {
            _header = "Morse code as sounds.";
        }

        static MorseCode()
        {
            _morseAlphabetDictionary = new Dictionary<char, string>()
                                   {
                                       {'a', ".-"},
                                       {'b', "-..."},
                                       {'c', "-.-."},
                                       {'d', "-.."},
                                       {'e', "."},
                                       {'f', "..-."},
                                       {'g', "--."},
                                       {'h', "...."},
                                       {'i', ".."},
                                       {'j', ".---"},
                                       {'k', "-.-"},
                                       {'l', ".-.."},
                                       {'m', "--"},
                                       {'n', "-."},
                                       {'o', "---"},
                                       {'p', ".--."},
                                       {'q', "--.-"},
                                       {'r', ".-."},
                                       {'s', "..."},
                                       {'t', "-"},
                                       {'u', "..-"},
                                       {'v', "...-"},
                                       {'w', ".--"},
                                       {'x', "-..-"},
                                       {'y', "-.--"},
                                       {'z', "--.."},
                                       {'0', "-----"},
                                       {'1', ".----"},
                                       {'2', "..---"},
                                       {'3', "...--"},
                                       {'4', "....-"},
                                       {'5', "....."},
                                       {'6', "-...."},
                                       {'7', "--..."},
                                       {'8', "---.."},
                                       {'9', "----."}
                                   };

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

                string answer = GetTypedValue(ref consoleLog);

                Console.Clear();
                ConsoleIO.WriteColorText(consoleLog);
                ConsoleIO.WriteColorTextNewLine("\n-!Well Done! You've made a great thing! Now your PC is trying to process it!-\n", ConsoleColor.Green);

                Convert(TranslateToMorse(answer));//, ExpressionType.SubtractChecked);

                ConsoleIO.Console_ToMainMenu();
                return true;
            }
            catch (Exception ex)
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return false;
        }


        protected virtual string GetTypedValue(ref StringBuilder consoleLog)
        {
            try
            {
                int attemptCount = 0;
                string typedValue = string.Empty;
                string showMessage = "Enter string for converting: -> ";

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
                } while (string.IsNullOrWhiteSpace(typedValue));
                consoleLog.AppendLine(showMessage + typedValue + "\n" + ConsoleIO.ConsoleRowSeparator);

                return typedValue;
            }
            catch (Exception ex)
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return string.Empty;
        }


        protected virtual string TranslateToMorse(string answer)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (char character in answer)
                {
                    if (_morseAlphabetDictionary.ContainsKey(character))
                    {
                        stringBuilder.Append(_morseAlphabetDictionary[character] + " ");
                    }
                    else if (character == ' ')
                    {
                        stringBuilder.Append("/ ");
                    }
                    else
                    {
                        stringBuilder.Append(character + " ");
                    }
                }
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return string.Empty;
        }


        protected virtual void Convert(string answer)
        {
            try
            {
                answer.ToList().ForEach(c =>
                        {
                            if (c == '.')
                                Console.Beep(2000, 500);
                            else if (c == '-')
                                Console.Beep(2000, 1200);
                            Thread.Sleep(500);
                        });
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