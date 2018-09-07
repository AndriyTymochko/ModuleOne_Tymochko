using System;
using System.Collections.Generic;

namespace Lab_1_3_Operators.OperationType.SimpleCalculator
{
    internal interface ICalc<TKey, TValue>
    {
        void AddNewNumeric(char number);
        void DeleteLastNumeric();
        void MathOperation(KeyValuePair<TKey, TValue> operation);
        void ClearBufferAndShowResult();
    }
}
