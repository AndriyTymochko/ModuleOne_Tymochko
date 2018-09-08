using System;
using ConsoleModernWriter;

using Lab_1_4_Arrays.Core;
using Lab_1_4_Arrays.Core.Hardware;


namespace Lab_1_4_Arrays
{
    class Program
    {
        #region Methods
        static void Main(string[] args)
        {
            ShowAndSolveTasks();
        }

        private static void ShowAndSolveTasks()
        {
            try
            {
                    //to create and store the reference to object that represents all methods 
                    //with the manipulation of organization hardware (creation/modifying/inspection)
                IOrganization orgProvider = new Organization();
                //resize the console window 
                /*
                 * Збільшувати розмір консолі, завчасно не дізнавшись який максимальний допустимий на комп'ютері користувача не бажано, 
                 * буде помилка (T:System.ArgumentOutOfRangeException). Наприклад, якщо в мене вже стоїть макс розмір консолі, 
                 * а в коді "Console.WindowWidth + 30"
                 * його збільшують ще більше, такі нюанси зачасту прописані в описі до методу чи властивостей (Console.WindowHeight).
                */
                ConsoleIO.ResizeConsoleWindow(100, 40);

                    //to show the full status of organisation hardware  
                ConsoleIO.WriteColorTextNewLine("Organization hardware status", ConsoleColor.Yellow);
                orgProvider.ShowFullStatusOfOrganization();
                Console.ReadKey(true);

                ConsoleIO.WriteColorTextNewLine(
                    "Task 1: Organization consist of 4 department. Every department has several computers of different types:", ConsoleColor.Cyan, 
                    "Task 1:", ConsoleColor.Red);
                    //show the number of unique hardware types in each departments
                orgProvider.ShowNumberOfEachHardwareTypes();
                Console.ReadKey(true);

                ConsoleIO.WriteColorTextNewLine(
                    "Task 2: Count total number of all computers and every type", ConsoleColor.Cyan, 
                    "Task 2:", ConsoleColor.Red);
                    //show the number of all computers in whole organization
                orgProvider.ShowNumberOfHardwareType<Computer>();
                    //show the number of Desktop computers in whole organization
                orgProvider.ShowNumberOfHardwareType<DesktopBuilder>();
                    //show the number of Laptop computers in whole organization
                orgProvider.ShowNumberOfHardwareType<LaptopBuilder>();
                    //show the number of Server computers in whole organization
                orgProvider.ShowNumberOfHardwareType<ServerBuilder>();
                ConsoleIO.WriteColorTextNewLine(ConsoleIO.ConsoleRowSeparator + "\n");
                Console.ReadKey(true);


                ConsoleIO.WriteColorTextNewLine(
                    "Task 3: Find computer with the largest (HDD):", ConsoleColor.Cyan, 
                    "Task 3:", ConsoleColor.Red);
                orgProvider.ShowHardwareWithTheLargestParameterData<Computer>("HDD");
                Console.ReadKey(true);

                ConsoleIO.WriteColorTextNewLine(
                    "Task 4: Find computer with the lowest (RAM):", ConsoleColor.Cyan, 
                    "Task 4:", ConsoleColor.Red);
                orgProvider.ShowHardwareWithTheLowestParameterData<Computer>("RAM");
                Console.ReadKey(true);

                ConsoleIO.WriteColorTextNewLine(
                    "Task 5: Find computer with the largest number of cores (CPU):", ConsoleColor.Cyan, 
                    "Task 5:", ConsoleColor.Red);
                orgProvider.ShowHardwareWithTheLargestParameterData<Computer>("CPU");
                Console.ReadKey(true);

                ConsoleIO.WriteColorTextNewLine(
                    "Task 6: Make desktop upgrade: set RAM up to 8GB", ConsoleColor.Cyan, 
                    "Task 6:", ConsoleColor.Red);
                ConsoleIO.WriteColorTextNewLine("Organization new hardware status", ConsoleColor.Yellow);
                orgProvider.UpdateFieldValue<DesktopBuilder, string>("RAM", "8 GB");
                orgProvider.ShowFullStatusOfOrganization();
                Console.ReadKey(true);

                ConsoleIO.WriteColorTextNewLine(
                    "Task 7: Make laptop upgrade: set HDD up to 1024 GB", ConsoleColor.Cyan, 
                    "Task 7:", ConsoleColor.Red);
                orgProvider.UpdateFieldValue<LaptopBuilder, string>("HDD", "1024 GB");
                Console.ReadKey(true);

                    //to show the full status of organisation hardware  
                ConsoleIO.WriteColorTextNewLine("Organization new hardware status", ConsoleColor.Yellow);
                orgProvider.ShowFullStatusOfOrganization();
                Console.ReadKey(true);
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
