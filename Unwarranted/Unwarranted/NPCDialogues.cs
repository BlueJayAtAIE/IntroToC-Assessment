using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static UnwarrantedTools.Tools;
using static UnwarrantedTools.Locations;

namespace UnwarrantedTools
{
    public static class NPCDialogues
    {
        private static bool okToGo = false;
        private static bool firstLoop = true;
        private static bool fear = false;
        private static bool angry = false;

        private static string ObjectTextPath = "NPC Dialogues/objectFlavortext.txt";

        private static string TestNPCPath = "NPC Dialogues/testTalk.txt";
        private static string MaridethPath = "NPC Dialogues/MaridethTalk.txt";
        private static string MinorNPCTalkPath = "NPC Dialogues/MinorNPCsTalk.txt";

        //TODO: all conversations with Prime Suspects.
        //TODO: some NPCS, including:
        //          Rat, Rubi, Sprocket, Dock Worker, Beak-Mask
        //TODO: NPCS that don't even have dialogue
        //          Lab Assistant, Bodyguard, Random Citizen (stretch goal)

        /// <summary>
        /// Test conversation that most all interrogations will be based on. Reference txt file for conversation flow.
        /// </summary>
        public static void TestCharacterInterrogation()
        {
            Console.Clear();
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                // The firstLoop, fear, and angry bools all allow for the main information given to be changed.
                // firstLoop is for greetings, while fear and angry are two general purpose ones that can be used anywhere.
                // Not shown in this example, but theoretically you can even use them to change replies. Use where needed.
                if (!firstLoop) Interrogation(TestNPCPath, 1, 4, 1, "Mr.Test");
                if (firstLoop) Interrogation(TestNPCPath, 0, 5, 1, "Mr.Test");
                ReplyCheck();
            
                // Reply check will run through the present and line variables, and depending on what they are, dialogue will
                // change. Present is to be checked first, then line (only if the player isn't silent that round).
                void ReplyCheck()
                {
                    bool silent = false;

                    // Checks presented line against whatever parameter. Can be NPC's own dialogue, someone else's, or even an object.
                    if (interrogationPresent.Equals(File.ReadLines(TestNPCPath).ElementAt(9)))
                    {
                        Interrogation(TestNPCPath, 17, 1, 2, "Mr.Test");
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(File.ReadLines(TestNPCPath).ElementAt(17)))
                    {
                        Interrogation(TestNPCPath, 20, 2, 3, "Mr.Test");
                        Console.WriteLine("debug message: heckin gottem");
                        okToGo = true;
                    }
                    else if (interrogationPresent.Equals(","))
                    {
                        Console.WriteLine("debug message: honk");
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {
                        // Purposefully empty statement. This is so else doesn't catch a non-presenting turn as an invalid present.
                    }
                    else
                    {
                        //In here goes spoken dialogue for when an NPC has no reply to a shown line/object. This phrase cannot be recorded- so it's just a writeline instead.
                        Console.WriteLine("That means nothing to me.");
                    }

                    // Checks if selected line has additional "inqury" text.
                    if (!silent)
                    {
                        switch (interrogationLine)
                        {
                            case 1:
                                Interrogation(TestNPCPath, 7, 1, 2, "Mr.Test");
                                ReplyCheck();
                                break;
                            case 2:
                                Interrogation(TestNPCPath, 9, 2, 2, "Mr.Test");
                                ReplyCheck();
                                break;
                            case 4:
                                Interrogation(TestNPCPath, 12, 1, 2, "Mr.Test");
                                ReplyCheck();
                                break;
                            case 998:
                                okToGo = true;
                                break;
                            case 999:
                                //Purposefully empty statement. This is so default doesn't catch a non-inqury turn as an invalid inqury.
                                break;
                            default:
                                //In here goes spoken dialogue for when an NPC has no inqury reply. This phrase cannot be recorded- so it's just a writeline instead.
                                Console.WriteLine("I can't explain anything more about that...");
                                break;
                        }
                    }
                    Console.WriteLine();
                    firstLoop = false;
                }
            }
        }

        // Methods for Character interrogation ------------------------------------------------------------------------------------
        public static void MaridethOpeningInterrogation()
        {
            Console.Clear();
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Interrogation(MaridethPath, 0, 8, 1, "Marideth");
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;

                    if (interrogationPresent.Equals("To-do List: ????"))
                    {
                        Interrogation(MaridethPath, 33, 2, 2, "Marideth");
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(File.ReadLines(MaridethPath).ElementAt(7)))
                    {
                        Interrogation(MaridethPath, 28, 3, 3, "Marideth");
                        okToGo = true;
                        Console.WriteLine("[Any Key] Continue...");
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(","))
                    {
                        Console.WriteLine("\n\nCome on Jack stop messing around.");
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {

                    }
                    else
                    {
                        Console.WriteLine("That doesn't really mean anything to me.");
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 3:
                                Interrogation(MaridethPath, 10, 3, 2, "Marideth");
                                ReplyCheck();
                                break;
                            case 4:
                                Interrogation(MaridethPath, 14, 3, 2, "Marideth");
                                ReplyCheck();
                                break;
                            case 6:
                                Interrogation(MaridethPath, 18, 2, 2, "Marideth");
                                ReplyCheck();
                                break;
                            case 7:
                                Interrogation(MaridethPath, 21, 5, 2, "Marideth");
                                ReplyCheck();
                                break;
                            case 998:
                                Console.WriteLine("\nWhere do you think you're going? You better know what you're doing Jack...");
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.WriteLine("I have no idea what to tell you.");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void MaridethInterrogation()
        {
            Console.Clear();
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine();
                Interrogation(MaridethPath, 38, 4, 1, "Marideth");
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;

                    if (interrogationPresent.Equals("To-do List: ????"))
                    {
                        Interrogation(MaridethPath, 33, 2, 2, "Marideth");
                        //ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(MaridethPath, 44, 4, 3, "Marideth");
                        okToGo = true;
                        Console.WriteLine("[Any Key] Continue...");
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Marideth");
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(","))
                    {
                        Console.WriteLine("\n\nCome on Jack stop messing around.");
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {

                    }
                    else
                    {
                        Console.WriteLine("That doesn't really mean anything to me.");
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 998:
                                Console.WriteLine("\nCya later Jack. I'm counting on you to finish this case. You owe me. Money. Rent's due in a week.");
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.WriteLine("I have no idea what to tell you.");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void CerisInterrogation()
        {
            Console.Clear();
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine();
                Interrogation(MinorNPCTalkPath, 2, 4, 1, "Ceris");
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;

                    if (interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(62)) || interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(63)) || interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(64)) || interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(65)) || interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(73)))
                    {
                        Interrogation(MinorNPCTalkPath, 25, 5, 2, "Ceris");
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(MinorNPCTalkPath, 18, 5, 2, "Ceris");
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Ceris");
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(","))
                    {
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {

                    }
                    else
                    {
                        Console.WriteLine("I've no idea what you're goin on about.");
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 3:
                                Interrogation(MinorNPCTalkPath, 8, 4, 2, "Ceris");
                                Console.ReadKey();
                                break;
                            case 5:
                                Interrogation(MinorNPCTalkPath, 14, 2, 2, "Ceris");
                                Console.ReadKey();
                                break;
                            case 998:
                                Console.WriteLine("\nBye Jack. I know you'll be back.");
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.WriteLine("Not super sure what to say.");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void ArthurInterrogation()
        {
            Console.Clear();
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine();
                
                if (GetLocation() == MapLocations.Library) Interrogation(MinorNPCTalkPath, 33, 6, 1, "Arthur");
                if (GetLocation() == MapLocations.Square) Interrogation(MinorNPCTalkPath, 66, 3, 1, "Arthur");
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;

                    if (interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(15)))
                    {
                        Interrogation(MinorNPCTalkPath, 57, 2, 2, "Arthur");
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(MinorNPCTalkPath, 61, 3, 2, "Arthur");
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Arthur");
                    }
                    else if (interrogationPresent.Equals(","))
                    {
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {

                    }
                    else
                    {
                        Console.WriteLine("Forgive me, but I do not understand what you are presenting to me.");
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 35:
                                Interrogation(MinorNPCTalkPath, 41, 2, 2, "Arthur");
                                Console.ReadKey();
                                break;
                            case 38:
                                Interrogation(MinorNPCTalkPath, 45, 10, 2, "Arthur");
                                Console.ReadKey();
                                break;
                            case 998:
                                Console.WriteLine("\nI may see you later today- to you- it could be any time for me. We shall see!");
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.WriteLine("Pardon, I cannot offer you anything more from that sentnce");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void SeleneInterrogation()
        {
            Console.Clear();
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine();

                Interrogation(MinorNPCTalkPath, 74, 4, 1, "Selene");
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;

                    if (interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(15)))
                    {
                        Interrogation(MinorNPCTalkPath, 91, 4, 2, "Selene");
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(MinorNPCTalkPath, 86, 3, 2, "Selene");
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Selene");
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(","))
                    {
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {

                    }
                    else
                    {
                        Console.WriteLine("I don't... really understand how this is relevant??");
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 77:
                                Interrogation(MinorNPCTalkPath, 80, 4, 2, "Selene");
                                Console.ReadKey();
                                break;
                            case 998:
                                Console.WriteLine("\nBye Jack!! Good luck!~");
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.WriteLine("I don't think I can tell you anything more about that?");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        // Methods for Object interrogation ---------------------------------------------------------------------------------------
        // TODO: lead up to interactions.
        public static void ItemInspect(int itemID)
        {
            Console.Clear();
            switch (itemID)
            {
                case 0:
                    Console.WriteLine("On the table lies a Missing Persons flyer. You note a few important details:");
                    break;
                case 1:
                    Console.WriteLine();
                    break;
                case 2:
                    Console.WriteLine();
                    break;
                case 3:
                    Console.WriteLine();
                    break;
                case 4:
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine("Error. The game is about to crash. You may not even see this text before it does.");
                    break;
            }
            Interrogation(ObjectTextPath, ((KeyItem)keyItems[itemID]).textStart, ((KeyItem)keyItems[itemID]).textDurration, ((KeyItem)keyItems[itemID]).canPickup, ((KeyItem)keyItems[itemID]).name, itemID);
        }
    }
}

//When interrogationLine is 999 always treat it as being blank.
//When interrogationLine is 998 always treat it as leaving the conversation.
//When interrogationPresent is "." always treat it as being blank.
//When interrogationPresent is "," always treat it as being silent for one round.