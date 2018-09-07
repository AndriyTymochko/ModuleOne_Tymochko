using System;
using System.Collections.Generic;

namespace Lab_1_3_Operators.OperationType.Result
{
    interface IResult
    {
        bool IsUserResultAlreadySaved(string resultListName, string userName);
        IList<T> GetUserResults<T>(string resultListName, string userName);
        bool AddNewResult<T>(string resultListName, string userName, T res);
    }
}
