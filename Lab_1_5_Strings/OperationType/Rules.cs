using System;
using Security;

namespace Lab_1_5_Strings.OperationType
{
    internal class Rules
    {
        #region Fieds & Properties
        private readonly int _minTypingValue;
        protected int MinTypingValue
        {
            get
            {
                return _minTypingValue;
            }
        }


        private readonly int _maxTypingValue;
        protected int MaxTypingValue
        {
            get
            {
                return _maxTypingValue;
            }
        }


        private readonly char _replaceToSymbol;
        protected char ReplaceToSymbol
        {
            get
            {
                return _replaceToSymbol == default(char) ? '#' : _replaceToSymbol;
            }
        }

        private readonly char _delimiter;
        protected char Delimiter
        {
            get
            {
                return _delimiter == default(char) ? '#' : _delimiter;
            }
        }


        private readonly ushort _DelayBetweenTypingInMillisec;
        protected ushort DelayBetweenTypingInMillisec
        {
            get
            {
                return _DelayBetweenTypingInMillisec == default(ushort) ? (ushort)500 : _DelayBetweenTypingInMillisec;
            }
        }
        
        #endregion

        #region Constructors 
        public Rules()
        {
           if(!Int32.TryParse(SecurityHelper.UnprotectConfigParameter(Properties.Settings.Default.AbsoluteValue_MinTypingValue.ToString()), out _minTypingValue))
               _minTypingValue = 1;
           if(!Int32.TryParse(SecurityHelper.UnprotectConfigParameter(Properties.Settings.Default.AbsoluteValue_MaxTypingValue.ToString()), out _maxTypingValue))
               _maxTypingValue = 15;
           if(!Char.TryParse(SecurityHelper.UnprotectConfigParameter(Properties.Settings.Default.AbsoluteValue_ReplaceToSymbol.ToString()), out _replaceToSymbol))
               _replaceToSymbol = '1';
           if (!Char.TryParse(SecurityHelper.UnprotectConfigParameter(Properties.Settings.Default.AbsoluteValue_Delimiter.ToString()), out _delimiter))
               _delimiter = '#';
        }

        public Rules(int minTypingValue, int maxTypingValue) : this(minTypingValue, maxTypingValue, 500) { }
        public Rules(int minTypingValue, int maxTypingValue, ushort DelayBetweenTypingInMillisec)
        {
            _minTypingValue = minTypingValue;
            _maxTypingValue = maxTypingValue;
            _DelayBetweenTypingInMillisec = DelayBetweenTypingInMillisec;
        }

        public Rules(ushort DelayBetweenTypingInMillisec) : this() 
        {
            _DelayBetweenTypingInMillisec = DelayBetweenTypingInMillisec;
        }
        #endregion
    }
}
