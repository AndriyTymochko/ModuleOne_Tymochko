using System.Text;
using System.Collections.Generic;

namespace Lab_1_3_Operators.OperationType.SimpleCalculator.Buffer
{
    internal struct LastOp
    {
        /// <summary>
        /// flag that show if lock Result variable can be change
        /// </summary>
        public bool LockResult;

        /// <summary>
        /// to store the result of previous math operations
        /// </summary>
        private double _result;
        public double Result
        {
            get
            {
                return _result;
            }
            set
            {
                if (!LockResult)
                    _result = double.IsNaN(value) ? 0 : value;
            }
        }

        /// <summary>
        /// to store the last math operation 
        /// </summary>
        private KeyValuePair<MathOperation, char> _lastOperation;
        public KeyValuePair<MathOperation, char> LastOperation
        {
            get
            {
                return _lastOperation;
            }
            set
            {
                LockResult = false;
                _lastOperation = value;
            }
        }

        /// <summary>
        /// to store the string that represents a previous calc operations
        /// </summary>
        private StringBuilder _bufferNumber;
        public StringBuilder BufferNumber
        {
            get
            {
                if (_bufferNumber == default(StringBuilder))
                    _bufferNumber = new StringBuilder();
                return _bufferNumber;
            }
            set
            {
                LockResult = false;
                    //if the 'value' variable is double.NaN than set the buffer to deffault value ('0') 
                _bufferNumber = value.IndexOf('N') >= 0 ? new StringBuilder("0") : value;
            }
        }
    }
}
