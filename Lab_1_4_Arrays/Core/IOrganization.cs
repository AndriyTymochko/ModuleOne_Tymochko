
namespace Lab_1_4_Arrays.Core
{
    interface IOrganization
    {
        /// <summary>
        /// To store the reference to the array with all organization departments and all hardware of that departments
        /// </summary>
        object[][] OrganizationHardwaresArray { get; }

        /// <summary>
        /// To show the full current/actual status of organisation hardware 
        /// </summary>
        void ShowFullStatusOfOrganization();

        /// <summary>
        /// To count and show the number of unique hardware types in each departments
        /// </summary>
        void ShowNumberOfEachHardwareTypes();

        /// <summary>
        /// To count and show the number of selected (T) hardware type
        /// </summary>
        /// <typeparam name="T">the type of hardware that should be inspected</typeparam>
        void ShowNumberOfHardwareType<T>();

        /// <summary>
        /// To show the selected (T) hardware type with the largest parameter (fieldName) value
        /// </summary>
        /// <typeparam name="T">the type of hardware that should be inspected</typeparam>
        /// <param name="fieldName">the field that is used to compare the hardware</param>
        void ShowHardwareWithTheLargestParameterData<T>(string fieldName);

        /// <summary>
        /// To show the selected (T) hardware type with the lowest parameter (fieldName) value
        /// </summary>
        /// <typeparam name="T">the type of hardware that should be inspected</typeparam>
        /// <param name="fieldName">the field that is used to compare the hardware</param>
        void ShowHardwareWithTheLowestParameterData<T>(string fieldName);

        /// <summary>
        /// Update the selected (T1) hardware parameter value (fieldName)
        /// </summary>
        /// <typeparam name="T1">the type of hardware that should be modified</typeparam>
        /// <typeparam name="T2">the type of hardware parameter new value</typeparam>
        /// <param name="fieldName">the field (hardware parameter name) that should be modified</param>
        /// <param name="newValue">new parameter value</param>
        void UpdateFieldValue<T1, T2>(string fieldName, T2 newValue);
    }
}
