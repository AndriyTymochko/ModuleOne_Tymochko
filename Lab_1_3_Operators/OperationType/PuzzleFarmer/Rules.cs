using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using Security;

namespace Lab_1_3_Operators.OperationType.PuzzleFarmer
{
    internal class Rules
    {
        #region Fields & Properties 
        /// <summary>
        /// symbol that shows a correct answer 
        /// </summary>
        internal const char CorrectAnswerLabel = '+';
        /// <summary>
        /// for storing user wrong answers count
        /// </summary>
        internal int CurrenAttempt { get; private set; }

        /// <summary>
        /// max lifes (max user wrong answers) before game over
        /// </summary>
        private int _maxAttemptsCount = 0;
        internal int MaxAttemptsCount
        { 
            get
            {
                if (_maxAttemptsCount <=0) _maxAttemptsCount = 3;
                return _maxAttemptsCount;
            }
            private set 
            {
                _maxAttemptsCount = value > 0 ? value : 3;
            }
        }

        /// <summary>
        /// to store the task rules description
        /// </summary>
        private static string _rulesDescription;
        internal string RulesDescription
        {
            get
            {
                return _rulesDescription;
            }
        }

        /// <summary>
        /// to store the posible correct keys 
        /// </summary>
        private static readonly IList<string> _keysList;
        internal IList<string> KeysList 
        {
            get
            {
                return _keysList;
            }
        }

        /// <summary>
        /// to store all posible answers
        /// </summary>
        private static readonly IDictionary<FarmerPazle_Answers, string> _answersDict;
        internal IDictionary<FarmerPazle_Answers, string> AnswersDict
        {
            get
            {
                return _answersDict;
            }
        }
        #endregion

        #region Constructors 
        static Rules()
        {
                //to initialise a new set of posible keys 
            _keysList = new List<string> {
                 SecurityHelper.UnprotectConfigParameter(Properties.Settings.Default.PuzzleFarmerKey1),
                 SecurityHelper.UnprotectConfigParameter(Properties.Settings.Default.PuzzleFarmerKey2)
            };

                //to initialise a new set of posible user answers
            _answersDict = new Dictionary<FarmerPazle_Answers, string> { 
                { FarmerPazle_Answers.There_farmer_and_wolf, " {0} 1) There -> farmer and wolf" },
                { FarmerPazle_Answers.There_farmer_and_cabbage, " {0} 2) There -> farmer and cabbage" },
                { FarmerPazle_Answers.There_farmer_and_goat, " {0} 3) There -> farmer and goat" },
                { FarmerPazle_Answers.There_farmer, " {0} 4) There -> farmer" },
                { FarmerPazle_Answers.Back_farmer_and_wolf, " {0} 5) Back <- farmer and wolf" },
                { FarmerPazle_Answers.Back_farmer_and_cabbage, " {0} 6) Back <- farmer and cabbage" },
                { FarmerPazle_Answers.Back_farmer_and_goat, " {0} 7) Back <- farmer and goat" },
                { FarmerPazle_Answers.Back_farmer, " {0} 8) Back <- farmer" },
            };

            _rulesDescription = string.Format("  -From one bank to another should carry a wolf, goat and cabbage{0}"
                                + "  -At the same time can neither carry nor leave together on the banks of a wolf    and a goat, a goat and cabbage{0}"
                                + "  -You can only carry a wolf with cabbage or as each passenger separately{0}"
                                + "  -You can do whatever how many flights{0}"
                                + "  -How to transport the wolf, goat and cabbage that all went well?{0}"
                                + "{2}{0}"
                                + "Please, code the whole sequence of increasing numbers starting from 1 by {3} variables that will map to the following options:", 
                                "\n", "\t", ConsoleModernWriter.ConsoleIO.ConsoleRowSeparator, _answersDict.Count);
        }

        public Rules() : this(Properties.Settings.Default.PuzzleFarmerMaxAttemptsCount) { }
        
        public Rules(string maxAttempsCount)
        {
            if (!Int32.TryParse(SecurityHelper.UnprotectConfigParameter(maxAttempsCount), out _maxAttemptsCount))
                _maxAttemptsCount = 3;
        }

        public Rules(int maxAttempsCount)
        {
            MaxAttemptsCount = maxAttempsCount;
        }
        #endregion
    
        #region Methods 
        internal void PlusAttempt()
        {
            CurrenAttempt++;
        }

        /// <summary>
        /// check if max lifes (max wrong user answers) <= curren attempt (game over or no)
        /// </summary>
        /// <returns></returns>
        internal bool IsMaxAttempt()
        {
            return _maxAttemptsCount <= CurrenAttempt;
        }

        /// <summary>
        /// check if all user answers (one string) == any suitable key 
        /// </summary>
        /// <param name="alreadyEntCorrectAnsw"></param>
        /// <returns></returns>
        public virtual bool IsAlreadyWin(StringBuilder alreadyEntCorrectAnsw)
        {
            if (alreadyEntCorrectAnsw.Length == 0 || KeysList.Count == 0)
                return false;
            return KeysList.Any(k => k == alreadyEntCorrectAnsw.ToString());
        }

        
        /// <summary>
        /// Get the foreground color of the label that shows the number of user current attempt (depends on the 'CurrenAttempt/ MaxAttemptsCount')
        /// </summary>
        /// <returns></returns>
        internal virtual ConsoleColor GetCurrentAttemptColor()
        { 
            if(MaxAttemptsCount == 0)
                return ConsoleColor.White;
            double percOfAttempts = Math.Abs((double)CurrenAttempt / (double)MaxAttemptsCount);

            if (percOfAttempts >= 0 && percOfAttempts < 0.33)
                return ConsoleColor.Green;
            if (percOfAttempts >= 0.33 && percOfAttempts < 0.66)
                return ConsoleColor.Blue;
            if (percOfAttempts >= 0.66)
                return ConsoleColor.Red;
            else
                return ConsoleColor.White;
        }
        #endregion
    }
}
