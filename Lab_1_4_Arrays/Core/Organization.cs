using System;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;

using ConsoleModernWriter;
using Lab_1_4_Arrays.Core.Hardware;

namespace Lab_1_4_Arrays.Core
{
    internal class Organization : IOrganization
    {
        #region Fields & Properties 
        /// <summary>
        /// To store the reference to the jagged array with all organization departments and all hardware of that departments
        /// </summary>
        private readonly object[][] _organizationHardwaresArray;
        public object[][] OrganizationHardwaresArray
        { 
            get
            {
                return _organizationHardwaresArray;
            }
        }
        #endregion

        #region Constructors
        public Organization()
        {
                //to initialize the jagged aray with the default values
            _organizationHardwaresArray = InitDepartmentsArray();
        }
        #endregion

        #region Methods
        /// <summary>
        /// To generate the jagged array with the default values 
        /// for all organization departments and hardware of this departments
        /// </summary>
        /// <returns></returns>
        protected virtual object[][] InitDepartmentsArray()
        {
                //first dimension of jagged array - 4 departments
            return new object[4][]
            {
                //second dimension of jagged array - another jagged array. 
                //first dimension - 3 unique computers types (Desktop, Laptop, Server)
                new object[3][] {
                    //second dimension - single dimension array with 2 desktop computers
                    new DesktopBuilder[] {
                        new DesktopBuilder(),
                        new DesktopBuilder()
                    },
                    //second dimension - single dimension array with 2 laptops
                    new LaptopBuilder[] {
                        new LaptopBuilder(),
                        new LaptopBuilder()
                    },
                    //second dimension - single dimension array with 1 server
                    new ServerBuilder[] {
                        new ServerBuilder()
                    }
                },
                //second dimension of jagged array - another jagged array. 
                //first dimension - 1 unique computer type (Laptop)
                new object[1][] {
                    //second dimension - single dimension array with 3 laptops
                    new LaptopBuilder[] {
                        new LaptopBuilder(),
                        new LaptopBuilder(),
                        new LaptopBuilder()
                    },
                },
                //first dimension - 2 unique computers types (Desktop, Laptop)
                new object[2][] {
                    //second dimension - single dimension array with 3 desktop computers
                    new DesktopBuilder[] {
                        new DesktopBuilder(),
                        new DesktopBuilder(),
                        new DesktopBuilder()
                    },
                    //second dimension - single dimension array with 2 laptops
                    new LaptopBuilder[] {
                        new LaptopBuilder(),
                        new LaptopBuilder()
                    },
                },
                //first dimension - 3 unique computers types (Desktop, Laptop, ServerBuilder)
                new object[3][] {
                    //second dimension - single dimension array with 1 desktop computer
                    new DesktopBuilder[] {
                        new DesktopBuilder()
                    },
                    //second dimension - single dimension array with 1 laptop
                    new LaptopBuilder[] {
                        new LaptopBuilder()
                    },
                    //second dimension - single dimension array with 2 servers
                    new ServerBuilder[] {
                        new ServerBuilder(),
                        new ServerBuilder()
                    }
                }
            };
        }

        /// <summary>
        /// To show the full current/actual status of organisation hardware 
        /// </summary>
        public virtual void ShowFullStatusOfOrganization()
        {
            try
            {
                for (int dep = OrganizationHardwaresArray.GetLowerBound(0); dep <= OrganizationHardwaresArray.GetUpperBound(0); dep++)
                {
                    ConsoleIO.WriteColorTextNewLine(string.Format("Department №: '{0}'", dep + 1), ConsoleColor.Green);
                    for (int compType = OrganizationHardwaresArray[dep].GetLowerBound(0); compType <= OrganizationHardwaresArray[dep].GetUpperBound(0); compType++)
                    {
                        Computer[] comp = (OrganizationHardwaresArray[dep][compType] as Computer[]);
                        ConsoleIO.WriteColorTextNewLine(string.Format("ComputerType: '{0}'", comp[0].ToString()), ConsoleColor.Cyan);
                        for (int i = 0; i < comp.Length; i++)
                        {
                            ConsoleIO.WriteColorTextNewLine(string.Format("ArrayIndex: '[{0}][[{1}][{2}]]', RAM: '{3}', CPU: '{4}', HDD: '{5}'",
                                dep, compType, i,
                                comp[i].RAM, comp[i].CPU, comp[i].HDD));
                        }
                    }
                    ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator + "\n" + ConsoleIO.ConsoleRowSeparator);
                }
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// To count and show the number of unique hardware types in each departments
        /// </summary>
        public virtual void ShowNumberOfEachHardwareTypes()
        {
            try
            {
                Random rndObj = new Random();
                //ConsoleIO.WriteColorTextNewLine("Organization consist of 4 department. Every department has several computers of different types:", ConsoleColor.Green);
                for (int dep = OrganizationHardwaresArray.GetLowerBound(0); dep <= OrganizationHardwaresArray.GetUpperBound(0); dep++)
                {
                    ConsoleIO.WriteColorText(string.Format("- {0} department:", dep + 1), ConsoleColor.White, dep + 1, ConsoleColor.Green);
                    //   (ConsoleColor)rndObj.Next((int)ConsoleColor.DarkBlue, (int)ConsoleColor.White));

                    for (int compType = OrganizationHardwaresArray[dep].GetLowerBound(0); compType <= OrganizationHardwaresArray[dep].GetUpperBound(0); compType++)
                    {
                        Computer[] comp = (OrganizationHardwaresArray[dep][compType] as Computer[]);
                        if (comp.Length >= 1)
                        {
                            ConsoleIO.WriteColorText(string.Format("{0} {1} {2}{3}",
                                (compType != 0 ? "," : string.Empty),
                                comp.Length,
                                comp[0].ToString(),
                                (comp.Length > 1 ? "s" : string.Empty)));
                        }
                    }
                    ConsoleIO.WriteColorTextNewLine();
                }
                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator + "\n");
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// To count and show the number of selected (T) hardware type
        /// </summary>
        /// <typeparam name="T">the type of hardware that should be inspected</typeparam>
        public virtual void ShowNumberOfHardwareType<T>()
        {
            try
            {
                int compCount = 0;
                string nameAsStringRep = string.Empty;
                for (int dep = OrganizationHardwaresArray.GetLowerBound(0); dep <= OrganizationHardwaresArray.GetUpperBound(0); dep++)
                {
                    for (int compType = OrganizationHardwaresArray[dep].GetLowerBound(0); compType <= OrganizationHardwaresArray[dep].GetUpperBound(0); compType++)
                    {
                        Computer[] comp = (OrganizationHardwaresArray[dep][compType] as Computer[]);
                        if (comp != null && comp.Length > 0)
                        {
                            if (comp[0] is T)
                            {
                                compCount += comp.Length;
                                if (string.IsNullOrEmpty(nameAsStringRep))
                                    nameAsStringRep = ((Computer)comp[0]).ToString();
                            }
                        }
                    }
                }
                ConsoleIO.WriteColorTextNewLine(string.Format("Number of '{0}': '{1}'", (typeof(T) != typeof(Computer) ? nameAsStringRep : "all computers"), compCount), ConsoleColor.White, compCount.ToString(), ConsoleColor.Green);
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// To show the selected (T) hardware type with the largest parameter (fieldName) value
        /// </summary>
        /// <typeparam name="T">the type of hardware that should be inspected</typeparam>
        /// <param name="fieldName">the field that is used to compare the hardware</param>
        public virtual void ShowHardwareWithTheLargestParameterData<T>(string fieldName)
        {
            try
            {
                double maxValue = 0;
                StringBuilder largestCapacity = new StringBuilder();
                FieldInfo field = typeof(T).GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (field != default(FieldInfo))
                {
                    for (int dep = OrganizationHardwaresArray.GetLowerBound(0); dep <= OrganizationHardwaresArray.GetUpperBound(0); dep++)
                    {
                        for (int compType = OrganizationHardwaresArray[dep].GetLowerBound(0); compType <= OrganizationHardwaresArray[dep].GetUpperBound(0); compType++)
                        {
                            Computer[] comp = (OrganizationHardwaresArray[dep][compType] as Computer[]);
                            for (int i = 0; i < comp.Length; i++)
                            {
                                double curValue = 0;
                                object fieldValue = field.GetValue(comp[i]);
                                if (double.TryParse(Regex.Match(fieldValue.ToString(), @"\d+").Value, out curValue))
                                {
                                    if (maxValue == curValue)
                                    {
                                        largestCapacity.AppendFormat("\nDepartment №: '{0}', ComputerType: '{1}', ArrayIndex: '[{2}] [[{3}][{4}]]', {5}: '{6}'",
                                            dep + 1, comp[i].ToString(),
                                            dep, compType, i,
                                            fieldName, fieldValue.ToString());
                                    }
                                    else if (maxValue < curValue)
                                    {
                                        largestCapacity.Clear();
                                        largestCapacity.AppendFormat("Department №: '{0}', ComputerType: '{1}', ArrayIndex: '[{2}] [[{3}][{4}]]', {5}: '{6}'",
                                            dep + 1, comp[i].ToString(),
                                            dep, compType, i,
                                            fieldName, fieldValue.ToString());
                                        maxValue = curValue;
                                    }
                                }
                            }
                        }
                    }
                }
                ConsoleIO.WriteColorTextNewLine(largestCapacity);
                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator + "\n");
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// To show the selected (T) hardware type with the lowest parameter (fieldName) value
        /// </summary>
        /// <typeparam name="T">the type of hardware that should be inspected</typeparam>
        /// <param name="fieldName">the field that is used to compare the hardware</param>
        public virtual void ShowHardwareWithTheLowestParameterData<T>(string fieldName)
        {
            try
            {
                double minValue = double.MaxValue;
                StringBuilder lowestCapacity = new StringBuilder();
                FieldInfo field = typeof(T).GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (field != default(FieldInfo))
                {
                    for (int dep = OrganizationHardwaresArray.GetLowerBound(0); dep <= OrganizationHardwaresArray.GetUpperBound(0); dep++)
                    {
                        for (int compType = OrganizationHardwaresArray[dep].GetLowerBound(0); compType <= OrganizationHardwaresArray[dep].GetUpperBound(0); compType++)
                        {
                            Computer[] comp = (OrganizationHardwaresArray[dep][compType] as Computer[]);
                            for (int i = 0; i < comp.Length; i++)
                            {
                                double curValue = 0;
                                object fieldValue = field.GetValue(comp[i]);
                                if (double.TryParse(Regex.Match(fieldValue.ToString(), @"\d+").Value, out curValue))
                                {
                                    if (minValue == curValue)
                                    {
                                        lowestCapacity.AppendFormat("\nDepartment №: '{0}', ComputerType: '{1}', ArrayIndex: '[{2}] [[{3}][{4}]]', {5}: '{6}'",
                                            dep + 1, comp[i].ToString(),
                                            dep, compType, i,
                                            fieldName, fieldValue.ToString());
                                    }
                                    else if (minValue > curValue)
                                    {
                                        lowestCapacity.Clear();
                                        lowestCapacity.AppendFormat("\nDepartment №: '{0}', ComputerType: '{1}', ArrayIndex: '[{2}] [[{3}][{4}]]', {5}: '{6}'",
                                            dep + 1, comp[i].ToString(),
                                            dep, compType, i,
                                            fieldName, fieldValue.ToString());
                                        minValue = curValue;
                                    }
                                }
                            }
                        }
                    }
                }
                ConsoleIO.WriteColorTextNewLine(lowestCapacity);
                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator + "\n");
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Update the selected (T1) hardware parameter value (fieldName)
        /// </summary>
        /// <typeparam name="T1">the type of hardware that should be modified</typeparam>
        /// <typeparam name="T2">the type of hardware parameter new value</typeparam>
        /// <param name="fieldName">the field (hardware parameter name) that should be modified</param>
        /// <param name="newValue">new parameter value</param>
        public virtual void UpdateFieldValue<T1, T2>(string fieldName, T2 newValue)
        {
            try
            {
                FieldInfo field = typeof(T1).GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (field != default(FieldInfo))
                {
                    for (int dep = OrganizationHardwaresArray.GetLowerBound(0); dep <= OrganizationHardwaresArray.GetUpperBound(0); dep++)
                    {
                        for (int compType = OrganizationHardwaresArray[dep].GetLowerBound(0); compType <= OrganizationHardwaresArray[dep].GetUpperBound(0); compType++)
                        {
                            Computer[] comp = (OrganizationHardwaresArray[dep][compType] as Computer[]);
                            for (int i = 0; i < comp.Length; i++)
                            {
                                if (comp[i] is T1)
                                {
                                    field.SetValue(comp[i], newValue);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) //if error has been occured than show the error description and stack trace
            {
                ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
        }
        #endregion
    }
}
