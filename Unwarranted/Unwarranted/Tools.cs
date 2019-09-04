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

        private string line;

        // Methods Involving NPC Dialogue and Interrogations -----------------------------------------------------------------------------
        /// <summary>
        /// Begins the conversation loop with the specified NPC. WIP, displays all dialogue at once. 
        /// Conversations do not allow for recording information.
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        public void ConversationStart(string NPCDialoguePath)
        {
            for (int i = 0; i < File.ReadLines(NPCDialoguePath).Count(); i++)
            {
                Console.WriteLine($"[{i}] {File.ReadLines(NPCDialoguePath).ElementAt(i)}");
            }           
        }

        /// <summary>
        /// Begins the intterogation loop with the specified NPC.
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        /// <param name="startingPoint">Line in the text file to begin the interrogation from.</param>
        /// <param name="durration">How many lines to display the interrogation for.</param>
        public void InterrogationStart(string NPCDialoguePath, int startingPoint, int durration)
        {
            for (int i = startingPoint; i < durration; i++)
            {
                Console.WriteLine($"[{i}] {File.ReadLines(NPCDialoguePath).ElementAt(i)}");
            }
            //begin a loop here- OOOOH MAKE THIS ENTIRE LOOP ITS OWN METHOD. split up "interrogationstart" and make a "interrogationcontinue" method- you'll see why
            int.TryParse(Console.ReadLine(), out userInputInt); //TODO: Make a check to make sure the user inputs a currently displaying line (in the case some are hidden)
            //!!ASK THE PLAYER TO INPUT A KEY CORRESPONDING TO SEVERAL COMMANDS!!
            InterrogationInquire(NPCDialoguePath, userInputInt);//(command: inquire)
            //stall for a turn- the opposing npc may say more to possibly record (command: stay silent)
            InterrogationCopyEntry(NPCDialoguePath, userInputInt); //(command: record line)
            InterrogationPresentItem(); //(command: present physical evidence)
            InterrogationPresentWriting(); //(command: present written evidence)
            //opens your Notebook, allows you to select two lines and compare them (command: jackpot) !!PRESENTED AT THE END OF INTERROGATION CYCLES!!
        }

        /// <summary>
        /// Select a line spoken by the NPC to have them go into more detail.
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        /// <param name="lineNumber">Line number of NPC Dialogue which is being inspected.</param>
        public int InterrogationInquire(string NPCDialoguePath, int lineNumber)
        {
            try
            {               
                line = File.ReadLines(NPCDialoguePath).ElementAt(lineNumber);
                Console.WriteLine($"\"Can you please elaborate on the statement: \"{line}\"?\"\n");
                return lineNumber;
            }
            catch
            {
                Console.WriteLine("Invalid input! Please type the number in the brackets to copy the corresponding line.\n");
                return 0;
            }
        }

        /// <summary>
        /// Copies the user defined line of the NPC dialogue to the Notebook.
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        /// <param name="entryNumber">Line number of the NPC Dialogue txt file to copy over.</param>
        public void InterrogationCopyEntry(string NPCDialoguePath, int entryNumber)
        {
            try
            {
                StreamReader reader = new StreamReader(notebookPath);
                bool alreadyCopied = false;

                // Checks all of the Notebook against the line you selected to be copied. 
                //If a match is found, change "alreadyCopied" and leave the loop.
                while (reader.EndOfStream == false)
                {
                    if (reader.ReadLine().Equals(File.ReadLines(NPCDialoguePath).ElementAt(entryNumber)))
                    {
                        alreadyCopied = true;
                        break;
                    }
                }
                reader.Close();

                if (!alreadyCopied)
                {
                    File.AppendAllText(notebookPath, File.ReadLines(NPCDialoguePath).ElementAt(entryNumber) + Environment.NewLine);
                    line = File.ReadLines(notebookPath).LastOrDefault();
                    Console.WriteLine($"You write down \"{line}\"");
                }
                else
                {
                    Console.WriteLine("You've already written down that line.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input! Please type the number in the brackets to copy the corresponding line.");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Opens your inventory, allowing you to show items from your Key Items to the current NPC.
        /// </summary>
        /// <returns></returns>
        public string InterrogationPresentItem()
        {           
            return "placeholder";
        }

        // I would love to expand this to check line against the NPC's dialogue file to see if they said it or someone else did.
        // The only reason I'd do this is for flavortext- "Do you remember saying {thing}?" vs "We heard {thing} from someone else."
        // Tiny polish please consider.
        /// <summary>
        /// Opens your Notebook, allowing you to show recorded lines to the current NPC.
        /// </summary>
        /// <returns></returns>
        public string InterrogationPresentWriting()
        {
            EntryDisplay();
            int.TryParse(Console.ReadLine(), out userInputInt);
            try
            {
                line = File.ReadLines(notebookPath).ElementAt(userInputInt);
                Console.WriteLine(line); //placeholder
                return line;
            }
            catch
            {
                Console.WriteLine("Invalid input! Please type the number in the brackets to copy the corresponding line.\n");
                return "";
            }
        }

        // Methods Pertaining to In-Game Time Operations ---------------------------------------------------------------------------------
        /// <summary>
        /// Advances the game-time forward by one increment. 
        /// To be used after player actions (aside from inventory management).
        /// </summary>
        public void TimeAdvance()
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
        }

        public void TimeDisplay()
        {
            Console.WriteLine($"Day {Tools.timeDays}, {Tools.timeHours}:{Tools.timeMinutes}0");
        }

        // Methods Pertaining to Notebook Operations -------------------------------------------------------------------------------------
        /// <summary>
        /// Display 10 entries of the notebook at a time. 
        /// An additional input displays 10 more.
        /// </summary>
        public void EntryDisplay()
        {
            Console.WriteLine();
            try
            {
                int i = 0; // Used in the various loops and for the entry count.
                int l = 0; // Used for checking for more pages and for the entry count.
                int j = 1; // Used for displaying the page number.

                while (true)
                {
                    // This top block runs if the Notebook has 10 or less entries.
                    if (File.ReadLines(notebookPath).Count() <= 10)
                    {
                        Console.WriteLine("Showing entries:");
                        for (i = 0; i < File.ReadLines(notebookPath).Count(); i++)
                        {
                            Console.WriteLine($"[{i}] {File.ReadLines(notebookPath).ElementAt(i)}");
                        }
                    }
                    // And this part for 11 or more.
                    else if (File.ReadLines(notebookPath).Count() > 10)
                    {
                        Console.WriteLine($"Showing Page {j} of Entries");
                        if ((File.ReadLines(notebookPath).Count() - l) > 10)
                        {
                            for (i = 0; i < 10; i++)
                            {
                                Console.WriteLine($"[{i + l}] {File.ReadLines(notebookPath).ElementAt(i + l)}");
                            }
                        }
                        else if ((File.ReadLines(notebookPath).Count() - l) < 10)
                        {
                            for (i = 0; i < (File.ReadLines(notebookPath).Count() - l); i++)
                            {
                                Console.WriteLine($"[{i + l}] {File.ReadLines(notebookPath).ElementAt(i + l)}");
                            }
                        }
                        l += 10;
                        j++;
                    }
                    // "Next Page" only displays with more than 0 entries left to show after the previous "pages" are factored in.
                    Console.WriteLine("\n[Any Key] Close Notebook");
                    if ((File.ReadLines(notebookPath).Count() - l) > 0) Console.WriteLine("[N]ext Page\n");

                    // If no more pages remain, n will also close the Notebook.
                    userInputChar = char.ToLower(Console.ReadKey(true).KeyChar);
                    if (!(userInputChar == 'n') || (File.ReadLines(notebookPath).Count() - l) <= 0) break;
                }
            }
            catch
            {
                Console.WriteLine("You don't have anything in your notebook.");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Clears all entries in the player's Notebook.
        /// </summary>
        public void EntryClearAll()
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

        // Methods for Saving and Loading ------------------------------------------------------------------------------------------------
        /// <summary>
        /// Save the current Day, Hour and Minute to the save file. 
        /// If no save is found, create a new one; if one is found, override it.
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
            try
            {
                int.TryParse(File.ReadLines(saveFilePath).ElementAt(0), out timeDays);
                int.TryParse(File.ReadLines(saveFilePath).ElementAt(1), out timeHours);
                int.TryParse(File.ReadLines(saveFilePath).ElementAt(2), out timeMinutes);
            }
            catch
            {
                Console.WriteLine("Cannot read save file! Please start a new save.");
            }
        }
    }
}
