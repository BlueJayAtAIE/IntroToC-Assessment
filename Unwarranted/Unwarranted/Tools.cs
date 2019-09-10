using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UnwarrantedTools
{
    public static class Tools
    {
        public enum MapLocations { Home, Center, Library, Shop, Alley, Docks, Square, Tavern, Sprocket, Kog, Feri, Rutherian, Seren, Market, Hideout, TrainA, TrainB, TrainC }

        public static Random rng = new Random();

        public static int userInputInt;
        public static char userInputChar;

        public static int money = 50; //Player always starts a game with 50 Muns

        public static int interrogationLine = 999;
        public static string interrogationPresent;

        public static int timeDays = 1;
        public static int timeHours = 0;
        public static int timeMinutes = 0;
        public static bool timeUp = false;

        public static bool inBattle = false;

        private static string notebookPath = "Player/notebook.txt";
        private static string saveFilePath = "Player/save.txt";
        private static string inventoryPath = "Player/inventory.txt";

        private static string line;

        // Methods Involving NPC Dialogue and Interrogations -----------------------------------------------------------------------------
        /// <summary>
        /// Starts an interrogation loop intended for objects and books which cannot reply. Only has 1 command (record) plus the added "leave".
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        /// <param name="startingPoint">Line in the text file to begin the interrogation from.</param>
        /// <param name="durration">How many lines to display from the text file.</param>
        public static void InterrogationObject(string NPCDialoguePath, int startingPoint, int durration)
        {
            for (int i = startingPoint; i < startingPoint + durration; i++)
            {
                Console.WriteLine($"[{i}] {File.ReadLines(NPCDialoguePath).ElementAt(i)}");
            }
            Console.WriteLine("\nChoose a command from below:");
            Console.WriteLine("[R]ecord line");
            Console.WriteLine("[L]eave");

            bool okToGo = false;
            while (!okToGo)
            {
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                    case 'r':
                        Console.WriteLine("Enter the number of the line you wish to copy to your notebook.");
                        int.TryParse(Console.ReadLine(), out userInputInt);
                        if (userInputInt >= startingPoint && userInputInt < startingPoint + durration)
                        {
                            InterrogationCopyEntry(NPCDialoguePath, userInputInt);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case 'l':
                        Console.WriteLine("You've got everything you need from here.");
                        okToGo = true;
                        break;
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Begins the intterogation loop with the specified NPC. Has all of the interrogation commands (record, inquire, stall, present x2).
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        /// <param name="startingPoint">Line in the text file to begin the interrogation from.</param>
        /// <param name="durration">How many lines to display from the text file.</param>
        public static void InterrogationStart(string NPCDialoguePath, int startingPoint, int durration)
        {
            for (int i = startingPoint; i < startingPoint + durration; i++)
            {
                Console.WriteLine($"[{i}] {File.ReadLines(NPCDialoguePath).ElementAt(i)}");
            }
            Console.WriteLine("\nChoose a command from below:");
            Console.WriteLine("[R]ecord line");
            Console.WriteLine("[I]nquire");
            Console.WriteLine("[S]tay silent");
            if (File.ReadLines(notebookPath).Count() > 0) Console.WriteLine("Present [W]ritten evidence");
            Console.WriteLine("Present [P]hysical evidence\n");

            bool okToGo = false;
            while (!okToGo)
            {
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        Console.WriteLine("Enter the number of the line you wish to copy to your notebook.");
                        int.TryParse(Console.ReadLine(), out userInputInt);
                        if (userInputInt >= startingPoint && userInputInt < startingPoint + durration)
                        {
                            InterrogationCopyEntry(NPCDialoguePath, userInputInt);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case 'i':
                        Console.WriteLine("Enter the number of the line you wish to inquire about.");
                        int.TryParse(Console.ReadLine(), out userInputInt);
                        if (userInputInt >= startingPoint && userInputInt < startingPoint + durration)
                        {
                            InterrogationInquire(NPCDialoguePath, userInputInt);
                            interrogationPresent = "";
                            okToGo = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case 's':
                        interrogationPresent = ",";
                        okToGo = true;
                        break;
                    case 'w':
                        InterrogationPresentWriting();
                        interrogationLine = 999;
                        okToGo = true;
                        break;
                    case 'p':
                        //Console.WriteLine("Enter the number of the item you wish to present.");
                        //int.TryParse(Console.ReadLine(), out userInputInt);
                        InterrogationPresentItem();
                        interrogationLine = 999;
                        okToGo = true;
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            }
        }

        /// <summary>
        /// To be used for replies to inquiries and presented information. Has only 3 of the commands (record, inquire, stall).
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        /// <param name="startingPoint">Line in the text file to begin the interrogation from.</param>
        /// <param name="durration">How many lines to display from the text file.</param>
        public static void InterrogationContinue(string NPCDialoguePath, int startingPoint, int durration)
        {
            for (int i = startingPoint; i < startingPoint + durration; i++)
            {
                Console.WriteLine($"[{i}] {File.ReadLines(NPCDialoguePath).ElementAt(i)}");
            }
            Console.WriteLine("\nChoose a command from below:");
            Console.WriteLine("[R]ecord line");
            Console.WriteLine("[I]nquire");
            Console.WriteLine("[S]tay silent (back to main)");

            bool okToGo = false;
            while (!okToGo)
            {
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        Console.WriteLine("Enter the number of the line you wish to copy to your notebook.");
                        int.TryParse(Console.ReadLine(), out userInputInt);
                        if (userInputInt >= startingPoint && userInputInt < startingPoint + durration)
                        {
                            InterrogationCopyEntry(NPCDialoguePath, userInputInt);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case 'i':
                        Console.WriteLine("Enter the number of the line you wish to inquire about.");
                        int.TryParse(Console.ReadLine(), out userInputInt);
                        if (userInputInt >= startingPoint && userInputInt < startingPoint + durration)
                        {
                            InterrogationInquire(NPCDialoguePath, userInputInt);
                            interrogationPresent = "";
                            okToGo = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case 's':
                        interrogationPresent = ",";
                        okToGo = true;
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// For use at the end of interrogation interactions. Has only 1 of the commands (record) plus the added "leave".
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        /// <param name="startingPoint">Line in the text file to begin the interrogation from.</param>
        /// <param name="durration">How many lines to display from the text file.</param>
        public static void InterrogationEnd(string NPCDialoguePath, int startingPoint, int durration)
        {
            for (int i = startingPoint; i < startingPoint + durration; i++)
            {
                Console.WriteLine($"[{i}] {File.ReadLines(NPCDialoguePath).ElementAt(i)}");
            }
            Console.WriteLine("J a c k p o t");
            Console.WriteLine("\nChoose a command from below:");
            Console.WriteLine("[R]ecord line");
            Console.WriteLine("[L]eave conversation");

            bool okToGo = false;
            while (!okToGo)
            {
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        Console.WriteLine("Enter the number of the line you wish to copy to your notebook.");
                        int.TryParse(Console.ReadLine(), out userInputInt);
                        if (userInputInt >= startingPoint && userInputInt < startingPoint + durration)
                        {
                            InterrogationCopyEntry(NPCDialoguePath, userInputInt);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case 'l':
                        Console.WriteLine("You've got everything you need from here.");
                        okToGo = true;
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Select a line spoken by the NPC to have them go into more detail.
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        /// <param name="lineNumber">Line number of NPC Dialogue which is being inspected.</param>
        public static int InterrogationInquire(string NPCDialoguePath, int lineNumber)
        {
            try
            {
                line = File.ReadLines(NPCDialoguePath).ElementAt(lineNumber);
                Console.WriteLine($"\"Can you please elaborate on the statement: \"{line}\"?\"\n");
                interrogationLine = lineNumber;
                return interrogationLine;
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
        public static void InterrogationCopyEntry(string NPCDialoguePath, int entryNumber)
        {
            try
            {
                StreamReader reader = new StreamReader(notebookPath);
                bool alreadyCopied = false;

                // Checks all of the Notebook against the line you selected to be copied. 
                // If a match is found, change "alreadyCopied" and leave the loop.
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
        /// TODO: Opens your inventory, allowing you to show items from your Key Items to the current NPC.
        /// </summary>
        /// <returns></returns>
        public static string InterrogationPresentItem()
        {
            Console.WriteLine("Inventory machine [B]roke. Placeholder instead.");
            interrogationPresent = "";
            return interrogationPresent;
        }

        // TODO: I would love to expand this to check line against the NPC's dialogue file to see if they said it or someone else did.
        // The only reason I'd do this is for flavortext- "Do you remember saying {thing}?" vs "We heard {thing} from someone else."
        // Tiny polish please consider.
        /// <summary>
        /// Opens your Notebook, allowing you to show recorded lines to the current NPC.
        /// </summary>
        /// <returns></returns>
        public static string InterrogationPresentWriting()
        {
            Console.WriteLine("You open your notebook for reference first. Close it to continue on to your selection.");
            EntryDisplay();
            Console.WriteLine("Enter the number of the line from your notebook to present.");
            int.TryParse(Console.ReadLine(), out userInputInt);

            try
            {
                line = File.ReadLines(notebookPath).ElementAt(userInputInt);
                Console.WriteLine($"You present \"{line}\" forward as evidence."); // TODO? placeholder
                interrogationPresent = line;
                return interrogationPresent;
            }
            catch
            {
                Console.WriteLine("Invalid input!");
                return "";
            }
        }

        // Methods Pertaining to In-Game Time Operations ---------------------------------------------------------------------------------
        /// <summary>
        /// Advances the game-time forward by one increment. 
        /// To be used after player actions (aside from inventory management).
        /// </summary>
        public static void TimeAdvance()
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
        }

        public static void TimeDisplay()
        {
            Console.WriteLine($"Day {timeDays}, {timeHours}:{timeMinutes}0");
        }

        public static void TimeUp()
        {
            // Aw beans.
            Console.WriteLine("\nThe top hour of the final day has commenced... your time is up.");
            Console.WriteLine("You can [R]eload a previous save, [S]tart a new save, or [Q]uit.");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 's':
                        NewSave();
                        break;
                    case 'r':
                        Load();
                        break;
                    case 'q':
                        timeUp = true;
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        okToGo = false;
                        break;
                }
            }
        }

        // Methods Pertaining to Notebook Operations -------------------------------------------------------------------------------------
        /// <summary>
        /// Display 10 entries of the notebook at a time. 
        /// An additional input displays 10 more.
        /// </summary>
        public static void EntryDisplay()
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
                    if ((File.ReadLines(notebookPath).Count() - l) > 0 && File.ReadLines(notebookPath).Count() > 10) Console.WriteLine("[N]ext Page\n");

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

        // FOR NOW IM GETTING RID OF THE CLEAR MECHANIC. It's way too destructive of an action, and may lead to some endings being unobtainable.
        /// <summary>
        /// Clears all entries in the player's Notebook.
        /// </summary>
        //public void EntryClearAll()
        //{
        //    Console.WriteLine("Are you sure you want to clear your notebook? Type \"Y\" to continue, any other button to cancel.");
        //    userInputChar = char.ToLower(Console.ReadKey(true).KeyChar);
        //    if (userInputChar == 'y')
        //    {
        //        File.WriteAllText(notebookPath, "");
        //        Console.WriteLine("Notebook cleared.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Operation Canceled.");
        //    }
        //    Console.WriteLine();
        //}

        // Methods for Inventory Management ----------------------------------------------------------------------------------------------

        ///<summary>
        /// TODO
        /// </summary>
        public static void OpenInventory()
        {
            Console.WriteLine("\n+++ OPENING INVENTORY +++");
            TimeDisplay();
            bool okToGo = false;
            Console.WriteLine("\n[N]otebook\n[K]ey Items\n[S]pell Stones\n\n[B]ack to Game");
            while (!okToGo)
            {
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'n':
                        EntryDisplay();
                        break;
                    case 'k':
                        //placeholder
                        Console.WriteLine("Placeholder");
                        break;
                    case 's':
                        //placeholder
                        Console.WriteLine("Placeholder");
                        break;
                    case 'b':
                        Console.WriteLine("+++ CLOSING INVENTORY +++\n");
                        okToGo = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }

        // Methods for Saving and Loading ------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clears all save, notebook, and inventory data. TODO: sets location to your home and replays intro monologue (when thats actually written).
        /// </summary>
        public static void NewSave()
        {
            StreamWriter writer = new StreamWriter(saveFilePath);
            writer.WriteLine(1);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.Close();
            File.WriteAllText(notebookPath, "");
            File.WriteAllText(inventoryPath, "");
            Console.WriteLine("\nNew game monologue goes here.");
            Console.WriteLine("[Any Key] Continue...");
            Console.ReadKey();
            Load();
        }

        /// <summary>
        /// Save the current Day, Hour and Minute to the save file. 
        /// If no save is found, create a new one; if one is found, override it.
        /// TODO: When you have free time further down the line, add more flavortext options for sleeping.
        /// TODO: Some other things are going to need to be saved and loaded down the line, like endings
        /// already obtained, bools denotating whether the Masked Market and Hideout have been found, and money.
        /// </summary>
        public static void Save()
        {
            Console.WriteLine("\nYou sleep for a few restless hours. You wake up completely unprepared to face your day.");
            for (int i = 0; i < 8; i++)
            {
                TimeAdvance();
            }
            StreamWriter writer = new StreamWriter(saveFilePath);
            writer.WriteLine(timeDays);
            writer.WriteLine(timeHours);
            writer.WriteLine(timeMinutes);
            writer.Close();
            Console.WriteLine("(Your game has been saved.)");
            TimeDisplay();
            Console.WriteLine();
        }

        /// <summary>
        /// Load the Day, Hour, and Minute from the save file.
        /// </summary>
        public static void Load()
        {
            try
            {
                int.TryParse(File.ReadLines(saveFilePath).ElementAt(0), out timeDays);
                int.TryParse(File.ReadLines(saveFilePath).ElementAt(1), out timeHours);
                int.TryParse(File.ReadLines(saveFilePath).ElementAt(2), out timeMinutes);
                Console.Clear();
                TimeDisplay();
                Console.WriteLine();
            }
            catch
            {
                Console.WriteLine("Cannot read save file! Please start a new save.");
            }
        }
    }
}
