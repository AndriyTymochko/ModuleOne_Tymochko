using System;
using System.Text;

namespace Lab_1_3_Operators
{
    public static class StringBuilderSearching
    {
        #region Methods

        /// <summary>
        /// zero-based index of the first occurrence of the specified
        /// character in this StringBuilder
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <returns>
        /// zero-based index position of value if that char is found, or -1 if it is not
        /// </returns>
        public static int IndexOf(this StringBuilder haystack, char needle)
        {
            if (haystack == null)
                throw new ArgumentNullException();

            for (int idx = 0; idx < haystack.Length; idx++)
            {
                if (haystack[idx] == needle)
                    return idx;
            } 
            return -1;
        }

        /// <summary>
        /// zero-based index of the last occurrence of the specified
        /// character in this StringBuilder
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <returns>
        /// zero-based index position of value if that character is found, or -1 if it is not.
        /// </returns>
        public static int LastIndexOf(this StringBuilder haystack, char needle)
        {
            if (haystack == null)
                throw new ArgumentNullException();

            for (int idx = haystack.Length - 1; idx >= 0; idx--)
            {
                if (haystack[idx] == needle)
                    return idx;
            }
            return -1;
        }

        #endregion
    }
}
