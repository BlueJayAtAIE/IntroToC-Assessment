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

        public enum AttackEffect { None, Burn, Sap, Static, Restore };

        public static Random rng = new Random();

        // For user input/menu selections
        public static int userInputInt;
        public static char userInputChar;

        // For things reflecting gameplay (money, map progress, current state, HP)
        public static int money = 50; // Player always starts a game with 50 Muns
        public static bool discoveredMarket;
        public static bool discoveredHideout;
        public static bool givenFreeMoney = false;
        public static bool stoleFreeMoney = false;
        public static bool inBattle = false;
        public static int HP = 25; // Max HP is 25
        static bool chargeTurn = false;
        static AttackEffect playerStatus;
        static AttackEffect opponentStatus;

        // For interrogations; stores input that changes the flow of the conversation
        public static int interrogationLine = 999;
        public static string interrogationPresent = ".";

        // For in-game time
        public static int timeDays = 1;
        public static int timeHours = 0;
        public static int timeMinutes = 0;
        public static bool timeUp = false;

        // Item related
        public static Item[] battleRunes = new Item[]
        {
            new BattleItem("[Blank Slot]", 0, 0, AttackEffect.None), new BattleItem("Basic", 1, 4, AttackEffect.None),
            new BattleItem("Flame", 2, 2, AttackEffect.Burn), new BattleItem("Stun", 3, 0, AttackEffect.Static),
            new BattleItem("Shock", 4, 3, AttackEffect.Static), new BattleItem("Restore", 5, 4, AttackEffect.Restore),
            new BattleItem("Drain", 6, 3, AttackEffect.Sap), new BattleItem("Smite", 7, 7, AttackEffect.None),
            new BattleItem("Charge", 8, 10, AttackEffect.None), new BattleItem("Leech", 9, 7, AttackEffect.Sap)
        };

        public static Item[] keyItems = new Item[]
        {
            new KeyItem("Missing Person Poster", 0, 1, 3, true), new KeyItem("Bird Mask", 1, 6, 3, true), new KeyItem("Stained Dagger", 2, 11, 3, true),
            new KeyItem("Pure Etheris Vial", 3, 16, 3, true), new KeyItem("Free Sample of Cram", 4, 21, 3, true),  new KeyItem("Broken Bottle", 5, 0, 0, true)
        };

        // Endings: 1: Kog, 2: Feri, 3: Seren, 4: Rutherian
        public static bool[] endingsObtained = new bool[4];

        // For file paths
        private static string notebookPath = "Player/notebook.txt";
        private static string saveFilePath = "Player/save.txt";
        public static string libraryBooksPath = "NPC Dialogues/libraryBooks.txt";

        private static string line;

        // Very Important Bool Do Not Touch. /s
        private static bool rutherianIsEvil = true;

        // Methods Involving NPC Dialogue and Interrogations -----------------------------------------------------------------------------
        /// <summary>
        /// Begins the intterogation loop with the specified NPC. Has all of the interrogation commands.
        /// </summary>
        /// <param name="NPCDialoguePath">Directory path that leads to the file the dialogue is located in.</param>
        /// <param name="startingPoint">Line in the text file to begin the interrogation from.</param>
        /// <param name="durration">How many lines to display from the text file.</param>
        /// <param name="convoState">Which state the interrogation is. 1 for start, 2 for continue, 3 for end.</param>
        /// <param name="NPCName">Name of the NPC being interrogated.</param>
        /// <param name="textColor">Color the NPC uses for their dialogue.</param>
        public static void Interrogation(string NPCDialoguePath, int startingPoint, int durration, int convoState, string NPCName, ConsoleColor textColor)
        {
            // Display's character's Name-Tag
            Console.Write("[");
            Console.ForegroundColor = textColor;
            Console.Write(NPCName);
            Console.ResetColor();
            Console.WriteLine("]:");

            // Loop through the dialogue
            Console.ForegroundColor = textColor;
            for (int i = startingPoint; i < startingPoint + durration; i++)
            {
                Console.WriteLine($"[{i}] {File.ReadLines(NPCDialoguePath).ElementAt(i)}");
            }
            Console.ResetColor();

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            // Commands are needed as follows: 
            // State 1: Record, Clarify, Silent, Present (x2), Leave
            // State 2: Record, Silent (+alt text)
            // State 3: (+alt text), Record, Leave
            if (convoState == 3) Console.WriteLine("+ J a c k p o t +\n");
            Console.WriteLine("[R]ecord line");
            if (convoState == 1) Console.WriteLine("[C]larify");
            if (convoState != 3) Console.Write("[S]tay silent");
            if (convoState == 2) Console.Write(" (back to main)");
            if (File.ReadLines(notebookPath).Count() > 0 && convoState == 1) Console.WriteLine("\nPresent [W]ritten evidence");
            if (convoState == 1 && KeyItemCheck()) Console.WriteLine("Present [P]hysical evidence");
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
                            Console.WriteLine("\nInvalid input!");
                        }
                        break;
                    case 'c':
                        if (convoState == 1)
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
                                Console.WriteLine("\nInvalid input!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
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
                            Console.WriteLine("\nInvalid Input!");
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
                            Console.WriteLine("\nInvalid input!");
                        }
                        break;
                    case 'p':
                        if (convoState == 1 && KeyItemCheck())
                        {
                            InterrogationPresentItem();
                            interrogationLine = 999;
                            okToGo = true;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
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
                            Console.WriteLine("\nInvalid input!");
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
        /// <param name="itemID">ID of the item from the keyItems array.</param>
        public static void Interrogation(string NPCDialoguePath, int startingPoint, int durration, bool itemPickup, string itemName, int itemID)
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
                        Console.Write("\nEnter the number of the line you wish to copy to your notebook> ");
                        int.TryParse(Console.ReadLine(), out userInputInt);
                        if (userInputInt >= startingPoint && userInputInt < startingPoint + durration)
                        {
                            InterrogationCopyEntry(NPCDialoguePath, userInputInt);
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
                        }
                        break;
                    case 't':
                        if (itemPickup)
                        {
                            keyItems[itemID].playerObtained = true;
                            Save();
                            Console.WriteLine($"\nYou pickup the {itemName}");
                            Console.WriteLine("[Any Key] Continue...");
                            Console.ReadKey(true);
                            Console.Clear();
                            okToGo = true;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid Input!");
                        }
                        break;
                    case 'l':
                        if (!itemPickup)
                        {
                            Console.WriteLine("\nYou've got everything you need from here.");
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
        /// Opens your inventory, allowing you to show items from your Key Items to the current NPC.
        /// </summary>
        /// <returns></returns>
        public static string InterrogationPresentItem()
        {
            Console.WriteLine("\nYou open your inventory for reference first.");
            ItemDisplay();
            while (true)
            {
                Console.Write("\nEnter the number of the item from your inventory to present> ");
                int.TryParse(Console.ReadLine(), out userInputInt);
                try
                {
                    if (keyItems[userInputInt].playerObtained)
                    {
                        line = keyItems[userInputInt].name;
                        Console.WriteLine($"You present the {line} forward as evidence.\n");
                        interrogationPresent = line;
                        return interrogationPresent;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
                        interrogationPresent = ".";
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input!");
                    interrogationPresent = ".";
                }
            }
        }

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
                    Console.WriteLine($"You present \"{line}\" forward as evidence.\n");
                    interrogationPresent = line;
                    return interrogationPresent;
                }
                catch
                {
                    Console.WriteLine("Invalid input!");
                    interrogationPresent = ".";
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
            Console.Write($"Day {timeDays}, {timeHours}:{timeMinutes}0 ");

            if (timeHours == 0 && timeMinutes == 0) Console.WriteLine("- Midnight");
            else if (timeHours < 6 || timeHours > 18) Console.WriteLine("- Night");
            else if (timeHours < 12) Console.WriteLine("- Morning");
            else if (timeHours == 12) Console.WriteLine("- Noon");
            else Console.WriteLine("- Day");
        }

        /// <summary>
        /// Game over sequence. Allows player to reload old save, start a new save, or quit.
        /// </summary>
        public static void GameEnd()
        {
            // Aw beans.
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
        /// Opens the player's inventory, allowing them to look through their notebook and item lists.
        /// </summary>
        public static void OpenInventory()
        {
            Console.WriteLine("\n+++ OPENING INVENTORY +++");
            TimeDisplay();
            Console.WriteLine($"Money: {money} Muns");
            bool okToGo = false;
            Console.WriteLine("\n[N]otebook\n[K]ey Items\n[S]pell Stones\n\n[X] Back to Game");
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
                        AttackRearrange();
                        break;
                    case 'x':
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
            Console.WriteLine("\nYou're holding the following items:");
            foreach (KeyItem k in keyItems)
            {
                if (k.playerObtained)
                {
                    Console.WriteLine($"[{k.itemID}] {k.name}");
                }
            }
        }

        /// <summary>
        ///  Check to see if the playee actually has any Key Items in their inventories.
        /// </summary>
        /// <returns></returns>
        public static bool KeyItemCheck()
        {
            bool anyItems = false;
            foreach (KeyItem k in keyItems)
            {
                if (k.playerObtained == true)
                    anyItems = true;
            }
            return anyItems;
        }

        /// <summary>
        /// Displays the player's CURRENTLY EQUIPT attack runes.
        /// </summary>
        public static void AttackDisplay()
        {
            foreach (BattleItem b in battleRunes)
            {
                if (b.equipt && b.name != "[Blank Slot]")
                {
                    Console.WriteLine($"[{b.itemID}] {b.name}");
                }
            }
        }

        /// <summary>
        /// Allows the player to view and rearrange the attacks they bring into battle.
        /// </summary>
        public static void AttackRearrange()
        {
            Console.WriteLine("\nCurrently equipt attcks:");
            AttackDisplay();
            Console.WriteLine("\nRearrange spell configuration? [Y]es || [Any Key] No");
            userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
            if (userInputChar == 'y')
            {
                foreach (BattleItem b in battleRunes)
                {
                    b.equipt = false;
                    if (b.playerObtained)
                    {
                        Console.WriteLine($"[{b.itemID}] {b.name}");
                    }
                }
                Console.WriteLine("\nChoose three attacks from above to equipt. Only 1 blank slot allowed.");
                for (int i = 0; i < 3; i++)
                {
                    while (true)
                    {
                        Console.Write($"\nChoose an attack for slot {i + 1}> ");
                        int.TryParse(Console.ReadLine(), out userInputInt);
                        if (battleRunes[userInputInt].playerObtained && !((BattleItem)battleRunes[userInputInt]).equipt)
                        {
                            ((BattleItem)battleRunes[userInputInt]).equipt = true;
                            Console.WriteLine($"Equipt {battleRunes[userInputInt].name} to slot {i + 1}.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Action cancled. Returning to main inventory page.");
            }
            Console.WriteLine("Returning to main inventory page.");
        }

        /// <summary>
        /// Buy the specified item, adding it to your possible list of attacks to choose from.
        /// </summary>
        /// <param name="attackID">Number of the entry in the battleItem array to check against and buy.</param>
        /// <param name="cost">Muns required to purchase.</param>
        /// <param name="attackName">Name of the attack.</param>
        public static void BuyItem(int attackID, int cost, string attackName)
        {
            if (!battleRunes[attackID].playerObtained)
            {
                if (money >= cost)
                {
                    Console.WriteLine($"\nAre you sure you want to purchase the {attackName} Spellstone? [Y]es || [Any Key] No");
                    userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                    if (userInputChar == 'y')
                    {
                        money -= cost;
                        battleRunes[attackID].playerObtained = true;
                        Console.WriteLine($"You have purchased the {attackName} Spellstone for {cost} Muns!");
                    }
                    else
                    {
                        Console.WriteLine("Purchase cancled.");
                    }
                }
                else
                {
                    Console.WriteLine("You don't have enough Muns to buy this...");
                }
            }
            else
            {
                Console.WriteLine("\nYou already bought this!");
            }
        }

        // Methods for BATTLE ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Begins a battle loop.
        /// When the player's HP is 0, the battle ends kicking them back to their last save.
        /// When the opponent's HP is 0, the battle ends with the player winning.
        /// TODO: Right now, the battle ends restoring the player's HP. This should be done elsewhere, as after the 
        /// battle concludes the location it's called in will check the hp to determine what happens next.
        /// </summary>
        /// <param name="battleName">Name of the battle to call. Reference Opponent.cs for opponent names.</param>
        public static void Battle(string battleName)
        {
            Console.Clear();
            Opponent opponent = new Opponent(battleName);
            while (HP > 0 && opponent.opponentHP > 0)
            {
                // Display health.
                Console.WriteLine($"{opponent.opponentName}: {opponent.opponentHP} HP Remaining\t\tYou: {HP} HP Remaining");

                // Check to make sure the player actually has any attacks equipt.
                bool hasEquipment = false;
                foreach (BattleItem b in battleRunes)
                {
                    if (b.equipt)
                    {
                        hasEquipment = true;
                    }
                }
                if (!hasEquipment)
                {
                    HP = 0;
                    Console.WriteLine("\nYou have no attacks equipt!");
                }

                // Player's Turn.
                if (HP > 0 && hasEquipment) PlayerAttack(opponent);

                // If the opponent lives, it's their turn.
                if (opponent.opponentHP > 0 && hasEquipment) OpponentAttack(opponent);
            }

            if (opponent.opponentHP <= 0)
            {
                Console.WriteLine($"{opponent.opponentName} lies before you inpacacitated.");
                if (opponent.opponentName == "The Thug" && !stoleFreeMoney)
                {
                    Console.WriteLine("You quietly pickpocket the thug for 50 Muns. What? He stole it anyways...");
                    money += 50;
                    stoleFreeMoney = true;
                }
                Console.WriteLine("[Any Key] Continue...");
                Console.ReadKey();
            }

            if (HP <= 0)
            {
                Console.WriteLine("\nThe world fades to black...");
                Console.WriteLine("\"Jack! Wake up!\"");
                Console.WriteLine("You open your eyes again. It's almost as if no time has past since you last slept.");
                Console.WriteLine("[Any Key] Continue...");
                Console.ReadKey();
                Load();
                TimeDisplay();
            }
        }

        /// <summary>
        /// Player's turn to attack.
        /// </summary>
        /// <param name="op"></param>
        public static void PlayerAttack(Opponent op)
        {
            // Check for and roll for possible skip turn
            if (playerStatus == AttackEffect.Static)
            {
                if (rng.Next(0, 1) == 1)
                {
                    Console.WriteLine("\nThe static status effect caused you to be unable to move!");
                    playerStatus = AttackEffect.None;
                    return;
                }
            }

            // Let player choose attack
            AttackDisplay();
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nChoose an attack> ");
                int.TryParse(Console.ReadLine(), out userInputInt);
                if (((BattleItem)battleRunes[userInputInt]).equipt)
                {
                    Console.WriteLine();
                    switch (userInputInt)
                    {
                        default:
                        case 0:
                            Console.WriteLine("Invalid input!");
                            okToGo = false;
                            break;
                        case 1:
                            Console.WriteLine("You use your Basic attack, which deals 4 damage!");
                            op.opponentHP -= 4;
                            break;
                        case 2:
                            Console.WriteLine("You use a Flame attack, which deals 2 damage! The opponent is burned!");
                            op.opponentHP -= 2;
                            opponentStatus = AttackEffect.Burn;
                            break;
                        case 3:
                            Console.WriteLine("You use a Stun attack! The opponent is stunned!");
                            opponentStatus = AttackEffect.Static;
                            break;
                        case 4:
                            Console.WriteLine("You use a Shock attack, which deals 3 damage! The opponent is stunned!");
                            op.opponentHP -= 3;
                            opponentStatus = AttackEffect.Static;
                            break;
                        case 5:
                            Console.WriteLine("You use a Restoration spell, which heals 5 damage!");
                            HP += 5;
                            break;
                        case 6:
                            Console.WriteLine("You use a Draining spell, which deals 3 damage, and heals you for 2!");
                            op.opponentHP -= 3;
                            HP += 2;
                            break;
                        case 7:
                            Console.WriteLine("You use a Smiting spell, which deals a massive 7 damage!");
                            op.opponentHP -= 7;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                    okToGo = false;
                }
            }

            // Check for and inflict burn damage
            if (playerStatus == AttackEffect.Burn)
            {
                Console.WriteLine("\nBefore your turn ends, you take 2 damage from the burn!");
                HP -= 2;
                playerStatus = AttackEffect.None;
            }
        }

        /// <summary>
        /// Opponents turn to attack.
        /// </summary>
        /// <param name="op"></param>
        public static void OpponentAttack(Opponent op)
        {
            // Check for and roll for possible skip turn
            if (opponentStatus == AttackEffect.Static)
            {
                if (rng.Next(0, 1) == 1)
                {
                    Console.WriteLine($"\nThe static status effect caused {op.opponentName} to be unable to move!");
                    opponentStatus = AttackEffect.None;
                    return;
                }
            }

            // Roll Attack
            Console.WriteLine();
            bool okToGo = false;
            while (!okToGo)
            {
                // A Charged attack stored from the last turn will always go next.
                if (chargeTurn == true)
                {
                    Console.WriteLine($"{op.opponentName} unleashes a Charged attack, which deals a massive 10 damage!");
                    HP -= 10;
                    chargeTurn = false;
                    break;
                }

                // Rest of the Attacks
                switch (rng.Next(0, 9))
                {
                    default:
                    case 0:
                        if (op.primaryAttack == (BattleItem)battleRunes[0] || op.secondaryAttack == (BattleItem)battleRunes[0] || op.tertiaryAttack == (BattleItem)battleRunes[0])
                        {
                            Console.WriteLine($"{op.opponentName} does nothing for one turn.");
                            okToGo = true;
                        }
                        break;
                    case 1:
                        if (op.primaryAttack == (BattleItem)battleRunes[1] || op.secondaryAttack == (BattleItem)battleRunes[1] || op.tertiaryAttack == (BattleItem)battleRunes[1])
                        {
                            Console.WriteLine($"{op.opponentName} uses a Basic attack, which deals 4 damage!");
                            HP -= 4;
                            okToGo = true;
                        }
                        break;
                    case 2:
                        if (op.primaryAttack == (BattleItem)battleRunes[2] || op.secondaryAttack == (BattleItem)battleRunes[2] || op.tertiaryAttack == (BattleItem)battleRunes[2])
                        {
                            Console.WriteLine($"{op.opponentName} uses a Flame attack, which deals 2 damage! You are burned!");
                            HP -= 2;
                            playerStatus = AttackEffect.Burn;
                            okToGo = true;
                        }
                        break;
                    case 3:
                        if (op.primaryAttack == (BattleItem)battleRunes[3] || op.secondaryAttack == (BattleItem)battleRunes[3] || op.tertiaryAttack == (BattleItem)battleRunes[3])
                        {
                            Console.WriteLine($"{op.opponentName} uses a Stun attack! You are stunned!");
                            playerStatus = AttackEffect.Static;
                            okToGo = true;
                        }
                        break;
                    case 4:
                        if (op.primaryAttack == (BattleItem)battleRunes[4] || op.secondaryAttack == (BattleItem)battleRunes[4] || op.tertiaryAttack == (BattleItem)battleRunes[4])
                        {
                            Console.WriteLine($"{op.opponentName} uses a Shock attack, which deals 3 damage! You are stunned!");
                            HP -= 3;
                            playerStatus = AttackEffect.Static;
                            okToGo = true;
                        }
                        break;
                    case 5:
                        if (op.primaryAttack == (BattleItem)battleRunes[5] || op.secondaryAttack == (BattleItem)battleRunes[5] || op.tertiaryAttack == (BattleItem)battleRunes[5])
                        {
                            Console.WriteLine($"{op.opponentName} uses a Restoration spell, which heals 5 damage!");
                            op.opponentHP += 5;
                            okToGo = true;
                        }
                        break;
                    case 6:
                        if (op.primaryAttack == (BattleItem)battleRunes[6] || op.secondaryAttack == (BattleItem)battleRunes[6] || op.tertiaryAttack == (BattleItem)battleRunes[6])
                        {
                            Console.WriteLine($"{op.opponentName} uses a Draining spell, which deals 3 damage, and heals them for 2!");
                            HP -= 3;
                            op.opponentHP += 2;
                            okToGo = true;
                        }
                        break;
                    case 7:
                        if (op.primaryAttack == (BattleItem)battleRunes[7] || op.secondaryAttack == (BattleItem)battleRunes[7] || op.tertiaryAttack == (BattleItem)battleRunes[7])
                        {
                            Console.WriteLine($"{op.opponentName} uses a Smiting spell, which deals a massive 7 damage!");
                            HP -= 7;
                            okToGo = true;
                        }
                        break;
                    case 8:
                        if (op.primaryAttack == (BattleItem)battleRunes[8] || op.secondaryAttack == (BattleItem)battleRunes[8] || op.tertiaryAttack == (BattleItem)battleRunes[8])
                        {
                            if (chargeTurn == false)
                            {
                                Console.WriteLine($"{op.opponentName} begins charging an attack...");
                                chargeTurn = true;
                                okToGo = true;
                            }
                        }
                        break;
                    case 9:
                        if (op.primaryAttack == (BattleItem)battleRunes[9] || op.secondaryAttack == (BattleItem)battleRunes[9] || op.tertiaryAttack == (BattleItem)battleRunes[9])
                        {
                            Console.WriteLine($"{op.opponentName} uses a Leeching spell, which deals a massive 7 damage, and heals them for 4!");
                            HP -= 7;
                            op.opponentHP += 4;
                            okToGo = true;
                        }
                        break;
                }
            }

            // Check for and inflict burn damage
            if (opponentStatus == AttackEffect.Burn)
            {
                Console.WriteLine($"\nBefore their turn ends, {op.opponentName} takes 2 damage from the burn!");
                HP -= 2;
                opponentStatus = AttackEffect.None;
            }
        }

        // Methods for Saving and Loading ------------------------------------------------------------------------------------------------

        // Items saved to the save.txt should be in this order: 
        // Days, Hours, Minutes, Money, Masked Market found, Hideout found, Money sources found, Ending progress, Battle Item progress, Key Item progress

        /// <summary>
        /// Clears all save, notebook, and inventory data.
        /// </summary>
        public static void NewSave()
        {
            File.WriteAllText(saveFilePath, "");
            timeDays = 1;
            timeHours = 0;
            timeMinutes = 0;
            money = 50;
            discoveredMarket = false;
            discoveredHideout = false;
            givenFreeMoney = false;
            stoleFreeMoney = false;
            foreach (BattleItem b in battleRunes)
            {
                b.playerObtained = false;
            }
            foreach (KeyItem k in keyItems)
            {
                k.playerObtained = false;
            }

            // Player always starts with Empty, Basic and Stun attacks
            battleRunes[0].playerObtained = true;
            ((BattleItem)battleRunes[0]).equipt = true;
            battleRunes[1].playerObtained = true;
            ((BattleItem)battleRunes[1]).equipt = true;
            battleRunes[3].playerObtained = true;
            ((BattleItem)battleRunes[3]).equipt = true;

            Save();

            File.WriteAllText(notebookPath, "To-do List: ????" + Environment.NewLine);
            Console.Clear();
            Console.WriteLine("Your name is Jack Montenegro.\nYou were a highly respected detective in the citadel of Lux. But after someone you helped put away repayed the favor... ");
            Console.WriteLine("\nYou let go of everything.");
            Console.WriteLine("\nAll you have now is an invalid badge, meger funds consisting of pocket change,\n...and probably something nearing alchohol poisoning.");
            Console.WriteLine("\nMaybe it's time to change something...");
            Console.WriteLine("[Any Key] Continue...");
            Console.ReadKey(true);

            Load();
        }

        /// <summary>
        /// Save to the save file.
        /// If no save is found, create a new one; if one is found, override it.
        /// </summary>
        public static void Save()
        {
            StreamWriter writer = new StreamWriter(saveFilePath);
            writer.WriteLine(timeDays);
            writer.WriteLine(timeHours);
            writer.WriteLine(timeMinutes);
            writer.WriteLine(money);
            writer.WriteLine(discoveredMarket);
            writer.WriteLine(discoveredHideout);
            writer.WriteLine(givenFreeMoney);
            writer.WriteLine(stoleFreeMoney);

            foreach (bool b in endingsObtained)
            {
                writer.WriteLine(b);
            }

            foreach (BattleItem b in battleRunes)
            {
                writer.WriteLine(b.playerObtained);
            }

            foreach (KeyItem k in keyItems)
            {
                writer.WriteLine(k.playerObtained);
            }

            writer.Close();
            Console.WriteLine();
        }

        /// <summary>
        /// Load from the save file.
        /// </summary>
        public static void Load()
        {
            try
            {
                StreamReader reader = new StreamReader(saveFilePath);
                int.TryParse(reader.ReadLine(), out timeDays);
                int.TryParse(reader.ReadLine(), out timeHours);
                int.TryParse(reader.ReadLine(), out timeMinutes);
                int.TryParse(reader.ReadLine(), out money);
                discoveredMarket = Convert.ToBoolean(reader.ReadLine());
                discoveredHideout = Convert.ToBoolean(reader.ReadLine());
                givenFreeMoney = Convert.ToBoolean(reader.ReadLine());
                stoleFreeMoney = Convert.ToBoolean(reader.ReadLine());

                while (reader.EndOfStream == false)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        endingsObtained[i] = Convert.ToBoolean(reader.ReadLine());
                    }

                    foreach (BattleItem b in battleRunes)
                    {
                        b.playerObtained = Convert.ToBoolean(reader.ReadLine());
                    }

                    foreach (KeyItem k in keyItems)
                    {
                        k.playerObtained = Convert.ToBoolean(reader.ReadLine());
                    }
                }

                reader.Close();
                SetLocation(MapLocations.Home);
                HP = 25;
                Console.Clear();
            }
            catch
            {
                Console.WriteLine("Cannot read save file! Please start a new save.");
            }
        }

    }
}
