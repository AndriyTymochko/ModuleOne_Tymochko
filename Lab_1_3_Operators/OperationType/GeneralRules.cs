
namespace Lab_1_3_Operators.OperationType
{
    internal class GeneralRules
    {
        #region Fields & Properties 
        /// <summary>
        /// store the max possible user input value 
        /// </summary>
        private int _minTypingValue;
        public int MinTypingValue
        {
            get
            {
                return _minTypingValue;
            }
            private set
            {
                _minTypingValue = value;
            }
        }

        /// <summary>
        /// store the max possible user input value
        /// </summary>
        private int _maxTypingValue;
        public int MaxTypingValue
        {
            get
            {
                return _maxTypingValue;
            }
            private set
            {
                _maxTypingValue = value;
            }
        }
        #endregion

        #region Constructors 
        public GeneralRules(int minTypingValue, int maxTypingValue)
        {
            if (minTypingValue < maxTypingValue && minTypingValue >= 0)
            {
                MinTypingValue = minTypingValue;
                MaxTypingValue = maxTypingValue;
            }
            else
            {
                _minTypingValue = byte.MinValue;
                MaxTypingValue = byte.MaxValue;
            }
        }
        #endregion
    }
}

