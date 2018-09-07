using System.Data;
using System.Collections.Generic;

using Lab_1_3_Operators.OperationType.SimpleCalculator.Buffer;

namespace Lab_1_3_Operators.OperationType.SimpleCalculator
{
    internal abstract class Calc<TKey, TValue> : CalcModernBuffer, ICalc<TKey, TValue>
    {
        #region Fields & Properties 
        /// <summary>
        /// symbol that shows a decimal number separator 
        /// </summary>
        private char _numberDecimalSeparator = ',';
        public virtual char NumberDecimalSeparator
        {
            get
            {
                return _numberDecimalSeparator;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// if numeric key (1, 2, 3) and decimal key separator ('.', ',') has been pressed
        /// marked as abstract and must be overridden in the nested class/es
        /// </summary>
        /// <param name="number"></param>
        public abstract void AddNewNumeric(char number);
        /// <summary>
        /// if calulation operation (+, -, *, /, pow) has been pressed
        /// marked as abstract and must be overridden in the nested class/es
        /// </summary>
        public abstract void DeleteLastNumeric();
        /// <summary>
        /// if (+, -, *, /, pow) key has been pressed
        /// marked as abstract and must be overridden in the nested class/es
        /// </summary>
        /// <param name="operation"></param>
        public abstract void MathOperation(KeyValuePair<TKey, TValue> operation);
        /// <summary>
        /// when enter key will be pressed but with only one operand
        ///for example: '5' -> '+' -> 'Enter' -> result '10' -> 'Enter' -> result '15' -> and so on
        ///marked as abstract and must be overridden in the nested class/es
        /// </summary>
        public abstract void ClearBufferAndShowResult();

        /// <summary>
        /// for making the math operation by:
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected virtual double Evaluate(string expression)
        {
            try
            {
                    //replace deff decimal number separator to '.'
                expression = expression.Replace(NumberDecimalSeparator, '.');
                    //creating a temperary datatable 
                DataTable table = new DataTable();
                    //add column to it (datatable) with the double data type and expression that must be calculated
                table.Columns.Add("expression", typeof(double), expression);
                    //add new row
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                    //try to parse the already calc data in new data table row. 
                    //if the parsing was successful, return the result
                    //else - return double.NaN (0.0 / 0.0)
                double outputValue = default(double);
                return double.TryParse(row["expression"].ToString(), out outputValue)
                    ? outputValue
                    : double.NaN;
            }
            catch //if any error has been occured than return double.NaN (0.0 / 0.0)
            {
                return double.NaN;
            }
        }
        #endregion
    }
}
