using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static UnwarrantedTools.Locations;

namespace UnwarrantedTools
{
    public static class Tools
    {
        public enum MapLocations { Home, Center, Library, Shop, Alley, Docks, Square, Tavern, Kog, Feri, Rutherian, Seren, Market, Hideout, TrainA, TrainB, TrainC }

        public static Random rng = new Random();

        // For user input/menu selections
        public static int userInputInt;
        public static char userInputChar;

        // For things reflecting gameplay (money, map progress, current state)
        public static int money = 50; //Player always starts a game with 50 Muns
        public static bool discoveredMarket;
        public static bool discoveredHideout;
        public static bool inBattle = false;

        // For interrogations; stores input that changes the flow of the conversation
        public static int interrogationLine = 999;
        public static string interrogationPresent;

        // For in-game time
        public static int timeDays = 1;
        public static int timeHours = 0;
        public static int timeMinutes = 0;
        public static bool timeUp = false;

        // For file paths
        private static string notebookPath = "Player/notebook.txt";
        private static string saveFilePath = "Player/save.txt";
        private static string inventoryPath = "Player/inventory.txt";

        private static string line;

        // Methods Involving NPC Dialogue and Interrogations -----------------------------------------------------------------------------
        /// <summary>
        /// Begins the intterogation loop with the specified NPC. Has all of the interrogation commands.
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        /// <param name="startingPoint">Line in the text file to begin the interrogation from.</param>
        /// <param name="durration">How many lines to display from the text file.</param>
        /// <param name="convoState">Which state the interrogation is. 1 for start, 2 for continue, 3 for end.</param>
        /// <param name="NPCName">Name of the NPC being interrogated.</param>
        public static void Interrogation(string NPCDialoguePath, int startingPoint, int durration, int convoState, string NPCName)
        {
            Console.WriteLine($"{NPCName}:");
            for (int i = startingPoint; i < startingPoint + durration; i++)
            {
                Console.WriteLine($"[{i}] {File.ReadLines(NPCDialoguePath).ElementAt(i)}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            // Commands are needed as follows: 
            // State 1: Record, Clarify, Silent, Present (x2), Leave
            // State 2: Record, Clarify (+alt text), Silent (+alt text)
            // State 3: (+alt text), Record, Leave
            if (convoState == 3) Console.WriteLine("+ J a c k p o t +\n");
            Console.WriteLine("[R]ecord line");
            if (convoState != 3) Console.Write("[C]larify");
            if (convoState == 2) Console.Write(", then back to main");
            if (convoState != 3) Console.Write("\n[S]tay silent");
            if (convoState == 2) Console.Write(" (back to main)");
            if (File.ReadLines(notebookPath).Count() > 0 && convoState == 1) Console.WriteLine("\nPresent [W]ritten evidence");
            if (convoState == 1) Console.WriteLine("Present [P]hysical evidence");
            if (convoState != 2) Console.WriteLine("[L]eave conversation\n");

            bool okToGo = false;
            while (!okToGo)
            {
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        Console.Write("\nEnter the number of the line you wish to copy to your notebook> ");
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
                    case 'c':
                        if (convoState != 3)
                        {
                            Console.Write("\nEnter the number of the line you wish to ask clarification about> ");
                            int.TryParse(Console.ReadLine(), out userInputInt);
                            if (userInputInt >= startingPoint && userInputInt < startingPoint + durration)
                            {
                                InterrogationInquire(NPCDialoguePath, userInputInt);
                                interrogationPresent = ".";
                                okToGo = true;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case 's':
                        if (convoState != 3)
                        {
                            interrogationPresent = ",";
                            okToGo = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input!");
                        }
                        break;
                    case 'w':
                        if (convoState == 1)
                        {
                            InterrogationPresentWriting();
                            interrogationLine = 999;
                            okToGo = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case 'p':
                        if (convoState == 1)
                        {
                            //TODO
                            //Console.WriteLine("\nEnter the number of the item you wish to present.> ");
                            //int.TryParse(Console.ReadLine(), out userInputInt);
                            InterrogationPresentItem();
                            interrogationLine = 999;
                            okToGo = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case 'l':
                        if (convoState == 1 || convoState == 3)
                        {
                            Console.WriteLine("\nAre you sure you want to leave? [Y]es || [Any Key] No");
                            userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                            if (userInputChar == 'y')
                            {
                                interrogationLine = 998;
                            }
                            interrogationPresent = ".";
                            okToGo = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    default:
                        Console.WriteLine("\nInvalid command!");
                        break;
                }
            }
        }
        
        /// <summary>
        /// Starts an interrogation loop intended for objects and books which cannot reply. Only has 1 command (record) plus the added "leave".
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        /// <param name="startingPoint">Line in the text file to begin the interrogation from.</param>
        /// <param name="durration">How many lines to display from the text file.</param>
        /// <param name="itemPickup">Whether or not the item can be added into your inventory.</param>
        /// <param name="itemName">Name of the item being interrogated.</param>
        public static void Interrogation(string NPCDialoguePath, int startingPoint, int durration, bool itemPickup, string itemName)
        {
            for (int i = startingPoint; i < startingPoint + durration; i++)
            {
                Console.WriteLine($"[{i}] {File.ReadLines(NPCDialoguePath).ElementAt(i)}");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("[R]ecord line");
            if (itemPickup) Console.WriteLine("[T]ake Item");
            if (!itemPickup) Console.WriteLine("[L]eave");

            bool okToGo = false;
            while (!okToGo)
            {
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        Console.Write("Enter the number of the line you wish to copy to your notebook> ");
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
                    case 't':
                        if (itemPickup)
                        {
                            File.AppendAllText(inventoryPath, itemName + Environment.NewLine);
                            Console.WriteLine($"\nYou pickup the {itemName}");
                            Console.WriteLine("[Any Key] Continue...");
                            Console.ReadKey();
                            Console.Clear();
                            okToGo = true;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid Input!");
                        }
                        break;
                    case 'l':
                        Console.WriteLine("You've got everything you need from here.");
                        Console.WriteLine("[Any Key] Continue...");
                        Console.ReadKey();
                        Console.Clear();
                        okToGo = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid command!");
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
                bool alreadyCopied = false;

                // Checks all of the Notebook against the line you selected to be copied. 
                // If a match is found, change "alreadyCopied" and leave the loop.
                using (StreamReader reader = new StreamReader(notebookPath))
                {
                    while (reader.EndOfStream == false)
                    {
                        if (reader.ReadLine().Equals(File.ReadLines(NPCDialoguePath).ElementAt(entryNumber)))
                        {
                            alreadyCopied = true;
                            break;
                        }
                    }
                }

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
            Console.WriteLine("\nInventory machine [B]roke. Placeholder instead.");
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
            Console.WriteLine("\nYou open your notebook for reference first. Close it to continue on to your selection.");
            EntryDisplay();
            while (true)
            {
                Console.Write("\nEnter the number of the line from your notebook to present> ");
                int.TryParse(Console.ReadLine(), out userInputInt);
                try
                {
                    line = File.ReadLines(notebookPath).ElementAt(userInputInt);
                    Console.WriteLine($"You present \"{line}\" forward as evidence.\n"); // TODO? placeholder?
                    interrogationPresent = line;
                    return interrogationPresent;
                }
                catch
                {
                    Console.WriteLine("Invalid input!");
                    return "";
                }
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

        /// <summary>
        /// Displays the time in a way readable by the player.
        /// </summary>
        public static void TimeDisplay()
        {
            Console.WriteLine($"Day {timeDays}, {timeHours}:{timeMinutes}0");
        }

        /// <summary>
        /// Game over sequence. Allows player to reload old save, start a new save, or quit.
        /// </summary>
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

        // Methods for Inventory Management ----------------------------------------------------------------------------------------------
        ///<summary>
        /// TODO
        /// </summary>
        public static void OpenInventory()
        {
            Console.WriteLine("\n+++ OPENING INVENTORY +++");
            TimeDisplay();
            Console.WriteLine($"Money: {money} Muns");
            bool okToGo = false;
            Console.WriteLine("\n[N]otebook\n[K]ey Items\n[S]pell Stones\n\n[B]ack to Game");
            while (!okToGo)
            {
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'n':
                        EntryDisplay();
                        break;
                    case 'k':
                        ItemDisplay();
                        break;
                    case 's':
                        // Placeholder
                        Console.WriteLine("\nPlaceholder.... Battle Items");
                        break;
                    case 'b':
                        Console.WriteLine("\n+++ CLOSING INVENTORY +++\n");
                        okToGo = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        break;
                }
            }
        }

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
                    if (File.ReadLines(notebookPath).Count() == 0)
                    {
                        Console.WriteLine("\nYour notebook is empty...");
                        break;
                    }
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
                    if (!(userInputChar == 'n') || (File.ReadLines(notebookPath).Count() - l) <= 0)
                    {
                        Console.WriteLine("You closed your notebook.");
                        break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Opperation failed!");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Displays everything in the player's inventory.
        /// </summary>
        public static void ItemDisplay()
        {
            Console.WriteLine();
            try
            {
                Console.WriteLine("You're holding the following items:");
                using (StreamReader reader = new StreamReader(inventoryPath))
                {
                    while (reader.EndOfStream == false)
                    {
                        line = reader.ReadLine();
                        Console.WriteLine(line);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Opperation failed!");
            }
        }

        /// <summary>
        /// Goes through the player's inventory to determine if an item has already been picked up or not.
        /// </summary>
        /// <param name="itemName">Item name to compare to the inventory.</param>
        /// <returns></returns>
        public static bool ItemTaken(string itemName)
        {
            bool alreadyTaken = false;

            try
            {
                using (StreamReader reader = new StreamReader(inventoryPath))
                {
                    while (reader.EndOfStream == false)
                    {
                        if (reader.ReadLine().Equals(itemName))
                        {
                            alreadyTaken = true;
                            break;
                        }
                        else
                        {
                            alreadyTaken = false;
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Opperation failed! Returning false.");
                return false;
            }

            return alreadyTaken;
        }

        // Methods for Saving and Loading ------------------------------------------------------------------------------------------------
        // Items saved to the save.txt should be in this order: Days, Hours, Minutes, Money, Masked Market found?, Hideout found?
        /// <summary>
        /// Clears all save, notebook, and inventory data.
        /// </summary>
        public static void NewSave()
        {
            StreamWriter writer = new StreamWriter(saveFilePath);
            writer.WriteLine(1);
            writer.WriteLine(0);
            writer.WriteLine(0);
            writer.WriteLine(50);
            writer.WriteLine(false);
            writer.WriteLine(false);
            writer.Close();
            File.WriteAllText(notebookPath, "To-do List: ????" + Environment.NewLine);
            File.WriteAllText(inventoryPath, "");
            Console.Clear();
            Console.WriteLine("Your name is Jack Montenegro.\nYou were a highly respected detective in the citadel of Lux. But after someone you helped put away repayed the favor... ");
            Console.WriteLine("\nYou let go of everything.");
            Console.WriteLine("\nAll you have now is an invalid badge, meger funds consisting of pocket change,\n...and probably something nearing alchohol poisoning.");
            Console.WriteLine("\nMaybe it's time to change something...");
            Console.WriteLine("[Any Key] Continue...");
            Console.ReadKey();
            Load();
        }

        /// <summary>
        /// Save the current Day, Hour and Minute to the save file. 
        /// If no save is found, create a new one; if one is found, override it.
        /// TODO: When you have free time further down the line, add more flavortext options for sleeping.
        /// TODO: Some other things are going to need to be saved and loaded down the line, like endings already obtained.
        /// </summary>
        public static void Save()
        {
            switch (rng.Next(4))
            {
                case 0:
                default:
                    Console.WriteLine("\nYou sleep for a few restless hours. You wake up completely unprepared to face your day.");
                    break;
                case 1:
                    Console.WriteLine("\nYou over-sleep and someone else finishes the case, stealing your glory. At least that happens in your dream.");
                    break;
                case 2:
                    Console.WriteLine("\nYou have a dream about being anywhere else. It was okay.");
                    break;
                case 3:
                    Console.WriteLine("\nYou actually got some quality sleep this time- and still only wasted 4 hours.");
                    break;
            }
            for (int i = 0; i < 8; i++)
            {
                TimeAdvance();
            }
            StreamWriter writer = new StreamWriter(saveFilePath);
            writer.WriteLine(timeDays);
            writer.WriteLine(timeHours);
            writer.WriteLine(timeMinutes);
            writer.WriteLine(money);
            writer.WriteLine(discoveredMarket);
            writer.WriteLine(discoveredHideout);
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
                int.TryParse(File.ReadLines(saveFilePath).ElementAt(3), out money);
                discoveredMarket = Convert.ToBoolean(File.ReadLines(saveFilePath).ElementAt(4));
                discoveredHideout = Convert.ToBoolean(File.ReadLines(saveFilePath).ElementAt(5));
                SetLocation(MapLocations.Home);
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
