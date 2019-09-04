using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Unwarranted
{
    public class Tools
    {
        public static bool gameOn = true;

        public static Random rng = new Random();

        public string notebookPath = "Player/notebook.txt";
        public string saveFilePath = "Player/save.txt";
        public string inventoryPath = "Player/inventory.txt";

        public static int userInputInt;
        public static char userInputChar;

        public static int timeDays = 1;
        public static int timeHours = 0;
        public static int timeMinutes = 0;

        string line;

        // TODO: Establish loop in which the player has the encounter in.
        /// <summary>
        /// Begins the intterogation loop with the specified NPC. WIP, may split into more methods. ===============================================================================
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        public void BeginInterrogation(string NPCDialoguePath)
        {
            for (int i = 0; i < File.ReadLines(NPCDialoguePath).Count(); i++)
            {
                Console.WriteLine($"[{i}] {File.ReadLines(NPCDialoguePath).ElementAt(i)}");
            }
            CopyToEntry(NPCDialoguePath);
        }

        //Methods Pertaining to In-Game Time Operations -------------------------------------------------------------------------------------
        /// <summary>
        /// Advances the game-time forward by one increment. To be used after player actions (aside from inventory management).
        /// </summary>
        public void AdvanceTime()
        {
            timeMinutes += 3;
            if (timeMinutes == 6)
            {
                timeHours++;
                timeMinutes = 0;
            }
            if (timeHours == 24)
            {
                timeHours = 0;
                timeDays++;
            }
            if (timeDays == 8)
            {
                //oh nooooo the game is ooooooover ================================================================================================================================
                gameOn = false;
            }

            //Displays the time. Will be in a separate method later- the one to check inventory. ======================================================================================
            Console.WriteLine($"Day {Tools.timeDays}, {Tools.timeHours}:{Tools.timeMinutes}0");
        }

        //Methods Pertaining to Notebook Operations -------------------------------------------------------------------------------------
        /// <summary>
        /// Copies the user defined line of the NPC dialogue to the Notebook.
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        public void CopyToEntry(string NPCDialoguePath)
        {            
            //Very important to keep all text editing inside a try/catch, in the case of the text files being edited, moved, or otherwise corrupted.
            int.TryParse(Console.ReadLine(), out userInputInt);
            try
            {
                StreamReader reader = new StreamReader(notebookPath);
                bool alreadyCopied = false;
                while (reader.EndOfStream == false)
                {
                    if (reader.ReadLine().Equals(File.ReadLines(NPCDialoguePath).ElementAt(userInputInt)))
                    {
                        alreadyCopied = true;
                    }
                }
                reader.Close();
                if (!alreadyCopied)
                {
                    File.AppendAllText(notebookPath, File.ReadLines(NPCDialoguePath).ElementAt(userInputInt) + Environment.NewLine);
                    line = File.ReadLines(notebookPath).LastOrDefault();
                    Console.WriteLine($"Line copied: {line}");
                }
                else
                {
                    Console.WriteLine("You've already written down that line.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input!");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// TODO: Should display 10 entries of the notebook. Additional inputs display 10 more. =================================================================================
        /// </summary>
        public void DisplayEntriesTemp()
        {
            Console.WriteLine();
            try
            {
                if (File.ReadLines(notebookPath).Count() <= 10)
                {
                    Console.WriteLine("Showing entries:");
                    for (int i = 0; i < File.ReadLines(notebookPath).Count(); i++)
                    {
                        Console.WriteLine($"[{i}] {File.ReadLines(notebookPath).ElementAt(i)}");
                    }
                }
                else if (File.ReadLines(notebookPath).Count() > 10)
                {
                    Console.WriteLine("Showing Page 1 of Entries");
                    for (int i = 0; i <= 10; i++)
                    {
                        Console.WriteLine($"[{i}] {File.ReadLines(notebookPath).ElementAt(i)}");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Invalid input!");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Clears all entries in the player's Notebook.
        /// </summary>
        public void ClearAllEntries()
        {
            Console.WriteLine("Are you sure you want to clear your notebook? Type \"Y\" to continue, any other button to cancel.");
            userInputChar = char.ToLower(Console.ReadKey(true).KeyChar);
            if (userInputChar == 'y')
            {
                File.WriteAllText(notebookPath, "");
                Console.WriteLine("Notebook cleared.");
            }
            else
            {
                Console.WriteLine("Operation Canceled.");
            }
            Console.WriteLine();
        }

        //Methods for Saving and Loading
        /// <summary>
        /// Save the current Day, Hour and Minute to the save file. If no save is found, create a new one- if one is found, override it.
        /// </summary>
        public void Save()
        {
            File.WriteAllText(saveFilePath, "");
            StreamWriter writer = new StreamWriter(saveFilePath);
            writer.WriteLine(timeDays);
            writer.WriteLine(timeHours);
            writer.WriteLine(timeMinutes);
            writer.Close();
        }

        /// <summary>
        /// Load the Day, Hour, and Minute from the save file.
        /// </summary>
        public void Load()
        {
            int.TryParse(File.ReadLines(saveFilePath).ElementAt(0), out timeDays);
            int.TryParse(File.ReadLines(saveFilePath).ElementAt(1), out timeHours);
            int.TryParse(File.ReadLines(saveFilePath).ElementAt(2), out timeMinutes);
        }
    }
}
