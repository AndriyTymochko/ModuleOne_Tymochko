using System;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using ConsoleModernWriter;


namespace Lab_1_3_Operators.OperationType.PuzzleFarmer
{
    internal class PuzzleFarmerOperation : IOperationType
    {
        #region Fields & Properties 
        /// <summary>
        /// to store all data and behavior that is connected with pazzle rules
        /// </summary>
        private readonly Rules _rulesProvider;
        
        /// <summary>
        /// to store all data and behavior that is connected with object/persons/animals transferring from one river bank to another 
        /// </summary>
        private readonly Transfer _transferProvider;
        #endregion

        #region Constructors 
        public PuzzleFarmerOperation()
        {
            _rulesProvider = new Rules();
            _transferProvider = new Transfer();
        }
        #endregion

        #region Methods 
        public virtual bool RunAndContinue()
        {
            try
            {
                    //to store last user answer
                string lastAnswer = string.Empty;
                    //all posible answers
                var userAnswersDict = new Dictionary<FarmerPazle_Answers, string>(_rulesProvider.AnswersDict);
                    //to store all correct answers as string 
                StringBuilder enteredCorrectAnsw = new StringBuilder();
                    //get last pressed key info
                ConsoleKeyInfo keyInfo = default(ConsoleKeyInfo);
                    //resize console window 
                ConsoleIO.ResizeConsoleWindow(Console.WindowWidth, Console.WindowHeight + 10);
                    
                    //for getting the time of user game
                Stopwatch sw = new Stopwatch();
                    // start the stopwatch
                sw.Start();
                do // while user answer number < MAX attempts And until user find the right answer / key
                {
                        //Clear console and show task header and rules description  
                    Console.Clear();
                    ConsoleIO.WriteColorTextNewLine(this.ToString(), ConsoleColor.Cyan);
                    ConsoleIO.WriteColorTextNewLine(_rulesProvider.RulesDescription);

                        //Show all posible answers 
                    userAnswersDict.ToList().ForEach(x => ConsoleIO.WriteColorTextNewLine(String.Format(x.Value, string.Empty), ConsoleColor.White, Rules.CorrectAnswerLabel.ToString(), ConsoleColor.DarkGreen));
                    ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);
                    
                        //Check and show the object that must be tranfer to the other bank of river and which have been already transferred there
                    _transferProvider.ShowCurrentTransferStatus(lastAnswer);
                     
                        //check if last answer was incorrect
                    if (_rulesProvider.CurrenAttempt > 0 && String.IsNullOrWhiteSpace(lastAnswer))
                            ConsoleIO.WriteColorTextNewLine("Incorrect entered value!", ConsoleColor.Red);
                        
                        //show how many lifes user has
                    ConsoleIO.WriteColorText("Lifes count: ", ConsoleColor.White);
                    ConsoleIO.WriteColorTextNewLine(_rulesProvider.MaxAttemptsCount - _rulesProvider.CurrenAttempt, _rulesProvider.GetCurrentAttemptColor());

                        //show already enered correct answers / keys
                    ConsoleIO.WriteColorText("Entered correct keys: ", ConsoleColor.White);
                    ConsoleIO.WriteColorTextNewLine(enteredCorrectAnsw.ToString(), ConsoleColor.Green);

                       //show last enered key char
                    if (keyInfo != default(ConsoleKeyInfo))
                        ConsoleIO.WriteColorTextNewLine(string.Format("Last entered key: '{0}", keyInfo.KeyChar));
                    
                        //show text separator (by default: ------------)
                    ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator);

                    //chech if user has already found the right answer / key. If Yes then exit from do {...}while(...);
                    if (_rulesProvider.IsAlreadyWin(enteredCorrectAnsw))
                        break;

                       //read key 
                    keyInfo = Console.ReadKey();
                       //check if current answer is correct (compare with the dictionary of keys)
                    lastAnswer = GetCorrectAnswer(keyInfo.KeyChar.ToString(), userAnswersDict, ref enteredCorrectAnsw);
                } while (!_rulesProvider.IsMaxAttempt()); //check if user answer number < MAX attempts
                sw.Stop(); // stop the stopwatch

                if (_rulesProvider.IsMaxAttempt()) //check if user answer number >= MAX attempts
                    ConsoleIO.WriteColorTextNewLine("\n-!-!-!-!-!-!-!GAME OVER!-!-!-!-!-!-!-\n", ConsoleColor.Red);
                else //user find the right answer / key
                {
                    ConsoleIO.WriteColorTextNewLine("\n-!-!-!-!-!-!-!Well Done!-!-!-!-!-!-!-\n", ConsoleColor.Green);
                    ConsoleIO.WriteColorTextNewLine(string.Format("{0}!, you are a winner! Your Time is: '{1}' sec\n", Environment.UserName, sw.Elapsed.Seconds), ConsoleColor.Green);
                }
                    //show the suggestion of pressing any key to return to the main menu
                ConsoleIO.Console_ToMainMenu();
                return true;
            }
            catch(Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return false;
        }

        /// <summary>
        /// check if current answer is correct (compare with the dictionary of keys and previous answers)
        /// </summary>
        /// <param name="enteredKeyChar"></param>
        /// <param name="availableAnsw"></param>
        /// <param name="alreadyEntCorrectAnsw"></param>
        /// <returns></returns>
        protected virtual string GetCorrectAnswer(string enteredKeyChar, IDictionary<FarmerPazle_Answers, string> availableAnsw, ref StringBuilder alreadyEntCorrectAnsw)
        {
            try
            {
                    //to store all previous correct answers
                StringBuilder correctAnsw = alreadyEntCorrectAnsw;
                byte enteredVal = 0;
                    //try to parse enterd key char. if successfully -> check if such value is check if such value is defined in FarmerPazle_Answers enum (if it's suitable) in FarmerPazle_Answers enum 
                if (Byte.TryParse(enteredKeyChar, out enteredVal) && Enum.IsDefined(typeof(FarmerPazle_Answers), enteredVal))
                {
                    string position = string.Empty;
                    if (correctAnsw.Length > 0) //check if it's a first correct enter
                            //get the first suitable key
                        position = _rulesProvider.KeysList.FirstOrDefault(k => 
                               k.Substring(0, correctAnsw.Length) == correctAnsw.ToString()
                            && k.Substring(correctAnsw.Length, 1) == enteredKeyChar);
                    else //it's first correct enter
                        position = _rulesProvider.KeysList.FirstOrDefault(k => k.Substring(0, 1) == enteredKeyChar);

                    if (!String.IsNullOrEmpty(position)) //if there is any suitable key
                    {
                            /*get the KeyValuePair<FarmerPazle_Answers, string> where last entered value (ets: 1, 2, 3...) 
                             * == key from all posible answers (from dictionary 'FarmerPazle_Answers')*/
                        var selectedDictValue = availableAnsw
                            .Where(x => x.Key == (FarmerPazle_Answers)Enum.Parse(typeof(FarmerPazle_Answers), enteredVal.ToString()))
                            .Select(k => new KeyValuePair<FarmerPazle_Answers, string>(k.Key, k.Value))
                            .FirstOrDefault();
                            //add '+' char as a sign of correct answer
                        string selectedValue = string.Format(selectedDictValue.Value, Rules.CorrectAnswerLabel + "{0}");
                        availableAnsw[selectedDictValue.Key] = selectedValue;
                            //save last correct answer (key char)
                        correctAnsw.Append(enteredVal);
                        alreadyEntCorrectAnsw = correctAnsw;

                        return selectedValue;
                    }
                }
                    //if answer was incorrect than user life - 1
                _rulesProvider.PlusAttempt();
                return string.Empty;
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return string.Empty;
        }


        public override string ToString()
        {
            return "Puzzle 'Farmer, wolf, goat and cabbage'";
        }
        #endregion
    }
}
