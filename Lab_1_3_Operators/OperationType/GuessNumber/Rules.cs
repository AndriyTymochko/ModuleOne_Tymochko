
namespace Lab_1_3_Operators.OperationType.GuessNumber
{
    internal class Rules : GeneralRules
    {
        #region Fields & Properties 
        /// <summary>
        /// to store the task rules description
        /// </summary>
        private string _rulesDescription;
        internal string RulesDescription
        {
            get
            {
                return _rulesDescription;
            }
        }

        /// <summary>
        ///  max lifes (max user wrong answers) before game over
        /// </summary>
        private int _maxAttemptsCount = 0;
        internal int MaxAttemptsCount
        {
            get
            {
                if (_maxAttemptsCount <= 0) _maxAttemptsCount = 3;
                return _maxAttemptsCount;
            }
            private set
            {
                _maxAttemptsCount = value > 0 ? value : 3;
            }
        }
        #endregion

        #region Constructors 
        public Rules(int maxAttemptsCount, int minTypingValue, int maxTypingValue) : base(minTypingValue, maxTypingValue)
        {
            _rulesDescription = 
                string.Format("   -You must guess the Number between '{0}' and '{1}' that was generated randomly by program{2}"
                        + "   -The program will help you with advices if your entered number is too low or too high{2}{3}",
                          minTypingValue, maxTypingValue, "\n", ConsoleModernWriter.ConsoleIO.ConsoleRowSeparator);
            MaxAttemptsCount = maxAttemptsCount;
        }
        #endregion
    }
}
