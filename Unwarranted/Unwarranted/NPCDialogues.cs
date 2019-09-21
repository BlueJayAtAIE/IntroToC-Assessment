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

        private static ConsoleColor textColor;

        private static string ObjectTextPath = "NPC Dialogues/objectFlavortext.txt";

        private static string TestNPCPath = "NPC Dialogues/testTalk.txt";
        private static string MaridethPath = "NPC Dialogues/MaridethTalk.txt";
        private static string KogPath = "NPC Dialogues/KogTalk.txt";
        private static string FeriPath = "NPC Dialogues/FeriTalk.txt";
        private static string SerenPath = "NPC Dialogues/SerenTalk.txt";
        private static string RutherianPath = "NPC Dialogues/RutherianTalk.txt";
        private static string MinorNPCTalkPath = "NPC Dialogues/MinorNPCsTalk.txt";

        /// <summary>
        /// Test conversation that most all interrogations will be based on. Reference txt file for conversation flow.
        /// </summary>
        public static void TestCharacterInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.Blue;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                // The firstLoop, fear, and angry bools all allow for the main information given to be changed.
                // firstLoop is for greetings, while fear and angry are two general purpose ones that can be used anywhere.
                // Not shown in this example, but theoretically you can even use them to change replies. Use where needed.
                if (!firstLoop) Interrogation(TestNPCPath, 1, 4, 1, "Mr.Test", textColor);
                if (firstLoop) Interrogation(TestNPCPath, 0, 5, 1, "Mr.Test", textColor);
                ReplyCheck();
            
                // Reply check will run through the present and line variables, and depending on what they are, dialogue will
                // change. Present is to be checked first, then line (only if the player isn't silent that round).
                void ReplyCheck()
                {
                    bool silent = false;

                    // Checks presented line against whatever parameter. Can be NPC's own dialogue, someone else's, or even an object.
                    if (interrogationPresent.Equals(File.ReadLines(TestNPCPath).ElementAt(9)))
                    {
                        Interrogation(TestNPCPath, 17, 1, 2, "Mr.Test", textColor);
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(File.ReadLines(TestNPCPath).ElementAt(17)))
                    {
                        Interrogation(TestNPCPath, 20, 2, 3, "Mr.Test", textColor);
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
                                Interrogation(TestNPCPath, 7, 1, 2, "Mr.Test", textColor);
                                ReplyCheck();
                                break;
                            case 2:
                                Interrogation(TestNPCPath, 9, 2, 2, "Mr.Test", textColor);
                                ReplyCheck();
                                break;
                            case 4:
                                Interrogation(TestNPCPath, 12, 1, 2, "Mr.Test", textColor);
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

        // Methods for Minor Character Interrogation ------------------------------------------------------------------------------------
        public static void MaridethOpeningInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.Blue;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Interrogation(MaridethPath, 0, 8, 1, "Marideth", textColor);
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals("To-do List: ????"))
                    {
                        Interrogation(MaridethPath, 33, 2, 2, "Marideth", textColor);
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(File.ReadLines(MaridethPath).ElementAt(7)))
                    {
                        Interrogation(MaridethPath, 28, 3, 3, "Marideth", textColor);
                        okToGo = true;
                        Console.WriteLine("[Any Key] Continue...");
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(","))
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("\n\nCome on Jack stop messing around.");
                        Console.ResetColor();
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {

                    }
                    else
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("That doesn't really mean anything to me.");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 3:
                                Interrogation(MaridethPath, 10, 3, 2, "Marideth", textColor);
                                ReplyCheck();
                                break;
                            case 4:
                                Interrogation(MaridethPath, 14, 3, 2, "Marideth", textColor);
                                ReplyCheck();
                                break;
                            case 6:
                                Interrogation(MaridethPath, 18, 2, 2, "Marideth", textColor);
                                ReplyCheck();
                                break;
                            case 7:
                                Interrogation(MaridethPath, 21, 5, 2, "Marideth", textColor);
                                ReplyCheck();
                                break;
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nWhere do you think you're going? You better know what you're doing Jack...");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("I have no idea what to tell you.");
                                Console.ResetColor();
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
            textColor = ConsoleColor.Blue;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Interrogation(MaridethPath, 38, 4, 1, "Marideth", textColor);
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals("To-do List: ????"))
                    {
                        Interrogation(MaridethPath, 33, 2, 2, "Marideth", textColor);
                        //ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(MaridethPath, 44, 4, 3, "Marideth", textColor);
                        okToGo = true;
                        Console.WriteLine("[Any Key] Continue...");
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Marideth", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(","))
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("\n\nCome on Jack stop messing around.");
                        Console.ResetColor();
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {

                    }
                    else
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("That doesn't really mean anything to me.");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nCya later Jack. I'm counting on you to finish this case. You owe me. Money. Rent's due in a week.");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("I have no idea what to tell you.");
                                Console.ResetColor();
                                break;
                        }
                    }
                }
            }
            Console.Clear();
        }

        public static void CerisInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.Green;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Interrogation(MinorNPCTalkPath, 2, 4, 1, "Ceris", textColor);
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(62)) || interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(63)) || interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(64)) || interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(65)) || interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(73)))
                    {
                        Interrogation(MinorNPCTalkPath, 25, 5, 2, "Ceris", textColor);
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(MinorNPCTalkPath, 18, 5, 2, "Ceris", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Ceris", textColor);
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
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("I've no idea what you're goin on about.");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 3:
                                Interrogation(MinorNPCTalkPath, 8, 4, 2, "Ceris", textColor);
                                Console.ReadKey();
                                break;
                            case 5:
                                Interrogation(MinorNPCTalkPath, 14, 2, 2, "Ceris", textColor);
                                Console.ReadKey();
                                break;
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nBye Jack. I know you'll be back.");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("Not super sure what to say.");
                                Console.ResetColor();
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
            textColor = ConsoleColor.DarkRed;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                if (GetLocation() == MapLocations.Library) Interrogation(MinorNPCTalkPath, 33, 6, 1, "Arthur", textColor);
                if (GetLocation() == MapLocations.Square) Interrogation(MinorNPCTalkPath, 66, 3, 1, "Arthur", textColor);
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(15)))
                    {
                        Interrogation(MinorNPCTalkPath, 57, 2, 2, "Arthur", textColor);
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(MinorNPCTalkPath, 61, 3, 2, "Arthur", textColor);
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Arthur", textColor);
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
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("Forgive me, but I do not understand what you are presenting to me.");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 35:
                                Interrogation(MinorNPCTalkPath, 41, 2, 2, "Arthur", textColor);
                                Console.ReadKey();
                                break;
                            case 38:
                            case 67:
                                Interrogation(MinorNPCTalkPath, 45, 10, 2, "Arthur", textColor);
                                Console.ReadKey();
                                break;
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nI may see you later today- from your view- it could be any time for me. We shall see!");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("Pardon, I cannot offer you anything more from that sentence.");
                                Console.ResetColor();
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
            textColor = ConsoleColor.Cyan;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Interrogation(MinorNPCTalkPath, 74, 4, 1, "Selene", textColor);
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(File.ReadLines(libraryBooksPath).ElementAt(15)))
                    {
                        Interrogation(MinorNPCTalkPath, 91, 4, 2, "Selene", textColor);
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(MinorNPCTalkPath, 86, 3, 2, "Selene", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Selene", textColor);
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
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("I don't... really understand how this is relevant??");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 77:
                                Interrogation(MinorNPCTalkPath, 80, 4, 2, "Selene", textColor);
                                Console.ReadKey();
                                break;
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nBye Jack!! Good luck!~");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("I don't think I can tell you anything more about that?");
                                Console.ResetColor();
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void SprocketInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.Yellow;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Interrogation(MinorNPCTalkPath, 97, 5, 1, "Sprocket", textColor);
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(","))
                    {

                        Console.ForegroundColor = textColor;
                        Console.WriteLine("Uh- inspector? Why'd you go quiet?...");
                        Console.ResetColor();
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {

                    }
                    else
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("Hm? Whats the point of showing me this?");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 99:
                            case 101:
                                Interrogation(MinorNPCTalkPath, 104, 6, 2, "Sprocket", textColor);
                                Console.ReadKey();
                                break;
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nSo long inspector!");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("Not really sure what else to say...");
                                Console.ResetColor();
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void RatInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.DarkMagenta;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Interrogation(MinorNPCTalkPath, 112, 4, 1, "Rat", textColor);
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(3)))
                    {
                        Interrogation(MinorNPCTalkPath, 122, 9, 3, "Rat", textColor);
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Rat", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(","))
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("Come on Jacky why're you so silent?~");
                        Console.ResetColor();
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {

                    }
                    else
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("What're you trying to get at Jacky?");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 115:
                                Interrogation(MinorNPCTalkPath, 118, 2, 2, "Rat", textColor);
                                Console.ReadKey();
                                break;
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nLater Jacky~");
                                Console.WriteLine("Have fun playing detective with all your little friends~");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("Eh? I can't say anything more.");
                                Console.ResetColor();
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void RubiniaInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.Red;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Interrogation(MinorNPCTalkPath, 133, 3, 1, "Rubinia", textColor);
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(MinorNPCTalkPath, 138, 3, 2, "Rubinia", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[2].name) || interrogationPresent.Equals(keyItems[3].name))
                    {
                        Interrogation(MinorNPCTalkPath, 143, 2, 2, "Rubinia", textColor);
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Rubinia", textColor);
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
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("I do not know what to tell you after being presented this information.");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nGoodbye inspector.");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("I'm sorry... I cannot offer you more.");
                                Console.ResetColor();
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void BeakInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.White;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Interrogation(MinorNPCTalkPath, 157, 2, 1, "Beak-Mask", textColor);
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(","))
                    {
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {

                    }
                    else
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("The question confuses me.");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 157:
                                Interrogation(MinorNPCTalkPath, 161, 7, 2, "Beak-Mask", textColor);
                                Console.ReadKey();
                                break;
                            case 158:
                                Interrogation(MinorNPCTalkPath, 170, 11, 2, "Beak-Mask", textColor);
                                Console.ReadKey();
                                break;
                            case 998:
                                Console.ForegroundColor = textColor;
                                if (!keyItems[4].playerObtained)
                                {
                                    ItemInspect(4);
                                }
                                else
                                {
                                    Console.WriteLine("Good bye FRIEND...");
                                    Console.WriteLine("[Any Key] Continue...");
                                    Console.ReadKey();
                                }
                                Console.ResetColor();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("The question confuses me.");
                                Console.ResetColor();
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void DocksInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.White;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                Interrogation(MinorNPCTalkPath, 147, 1, 1, "Docks Worker", textColor);

                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(MinorNPCTalkPath, 150, 5, 3, "Docks Worker", textColor);
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Docks Worker", textColor);
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
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("You're wastin' my time kid.");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nHmph. Bye.");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("What don't you get about \"scram\" kid?");
                                Console.ResetColor();
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        // Methods for Prime Suspect interrogation --------------------------------------------------------------------------------------

        public static void FeriInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.DarkCyan;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                if (angry && fear)
                {
                    angry = false;
                    fear = false;
                    Interrogation(FeriPath, 29, 5, 3, "Feri", textColor);
                    Console.WriteLine("\nYou have a choice to make now... Arrest and Report Feri?");
                    Console.WriteLine("[Y]es || [Any Key] No");
                    userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                    if (userInputChar == 'y')
                    {
                        endingsObtained[1] = true;
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("\nOh hun this is SO far from over... you'll be hearing from ALL my lawyers!");
                        Console.ResetColor();
                        Console.WriteLine("\nBut you never did hear from those lawyers. After presenting the evidence to the police they started to look into Feri.");
                        Console.WriteLine("The police found little ties to the missing Chimeric- but they did find the casino was a money laundering front.");
                        Console.WriteLine("Feri went to jail for a very different reason. But you still can't help but wonder...");
                        Console.WriteLine("\nWhatever did happen to those Chimerics?\n");
                        Save();
                        GameEnd();
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("You're doing the right thing hun... I like to know the killer isn't me~");
                        Console.ResetColor();
                        Console.WriteLine("[Any Key] Continue...");
                        Console.ReadKey();
                        break;
                    }
                }

                Interrogation(FeriPath, 1, 1, 1, "Feri", textColor);

                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(105)) || interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(108)) || interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(109)))
                    {
                        Interrogation(FeriPath, 13, 3, 2, "Feri", textColor);
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(File.ReadLines(FeriPath).ElementAt(15)))
                    {
                        Interrogation(FeriPath, 18, 2, 2, "Feri", textColor);
                        fear = true;
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(FeriPath, 4, 7, 2, "Feri", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[5].name))
                    {
                        Interrogation(FeriPath, 23, 3, 2, "Feri", textColor);
                        angry = true;
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Feri", textColor);
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
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("Hun you're making no sense right about now.");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nBye-bye inspector~.");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("Not sure how to spin it more clear for ya buttercup.");
                                Console.ResetColor();
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void SerenInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.DarkGreen;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                if (angry && fear)
                {
                    angry = false;
                    fear = false;
                    Interrogation(SerenPath, 33, 7, 3, "Seren", textColor);

                    Battle("Seren");
                    if (HP <= 0)
                    {
                        HP = 25;
                        break;
                    }

                    Console.WriteLine("\nYou have a choice to make now... Arrest and Report Seren?");
                    Console.WriteLine("[Y]es || [Any Key] No");
                    userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                    if (userInputChar == 'y')
                    {
                        endingsObtained[2] = true;
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("\nYou're making a mistake Jack... It's him... it's.....");
                        Console.ResetColor();
                        Console.WriteLine("\nBut you never did hear who Seren was blaming it on.\nAfter presenting the evidence to the police they started to look into Seren.");
                        Console.WriteLine("The police found little ties to the missing Chimeric-\nbut they did find Seren was hiding under a fake identity. A few actually.");
                        Console.WriteLine("Seren went to jail for a very different reason. But you still can't help but wonder...");
                        Console.WriteLine("\nWhatever did happen to those Chimerics?\n");
                        Save();
                        GameEnd();
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("Th... thank... you... please find the Spectre... it'll all make sense...");
                        Console.ResetColor();
                        Console.WriteLine("[Any Key] Continue...");
                        Console.ReadKey();
                        break;
                    }
                }

                Interrogation(SerenPath, 1, 1, 1, "Seren", textColor);

                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(152)) || interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(153)))
                    {
                        Interrogation(SerenPath, 7, 2, 2, "Seren", textColor);
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(File.ReadLines(RutherianPath).ElementAt(34)))
                    {
                        Interrogation(SerenPath, 16, 3, 2, "Seren", textColor);
                    }
                    else if (interrogationPresent.Equals(File.ReadLines(SerenPath).ElementAt(12)) && fear)
                    {
                        Interrogation(SerenPath, 27, 3, 2, "Seren", textColor);
                        angry = true;
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(SerenPath, 4, 1, 2, "Seren", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[1].name))
                    {
                        Interrogation(SerenPath, 21, 2, 2, "Seren", textColor);
                        fear = true;
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[3].name))
                    {
                        Interrogation(SerenPath, 11, 3, 2, "Seren", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Seren", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(","))
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("\nP-please don't get quiet like that- it's freaking me out.");
                        Console.ResetColor();
                        silent = true;
                    }
                    else if (interrogationPresent.Equals("."))
                    {

                    }
                    else
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("I-I-I don't understand what this is supposed to mean.");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nFarewell inspector!");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("Um... I'll do my best to help- we're reaaally busy though...");
                                Console.ResetColor();
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void KogInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.Magenta;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                if (angry)
                {
                    angry = false;
                    fear = false;
                    Interrogation(KogPath, 15, 4, 3, "Kog", textColor);

                    Battle("Kog");
                    if (HP <= 0)
                    {
                        HP = 25;
                        break;
                    }

                    Console.WriteLine("\nYou have a choice to make now... Arrest and Report Kog?");
                    Console.WriteLine("Hell [Y]es || [Any Key] No");
                    userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                    if (userInputChar == 'y')
                    {
                        endingsObtained[0] = true;
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("\nEh it was a good run.");
                        Console.ResetColor();
                        Console.WriteLine("\nBut you never did think Kog was a redeemable person.\nAfter presenting the evidence to the police they started to look into Kog.");
                        Console.WriteLine("The police found little ties to the missing Chimeric- but they did find literally everything else.");
                        Console.WriteLine("Kog went to jail for a very different reason. A few. But you still can't help but wonder...");
                        Console.WriteLine("\nWhatever did happen to those Chimerics?\n");
                        Save();
                        GameEnd();
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("Ha. Thats what I thought. Just keep your mouth shut Monte.");
                        Console.ResetColor();
                        Console.WriteLine("[Any Key] Continue...");
                        Console.ReadKey();
                        break;
                    }
                }

                Interrogation(KogPath, 1, 2, 1, "Kog", textColor);

                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(125)) || interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(126)) || interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(127)) || interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(128)) || interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(129)))
                    {
                        Interrogation(KogPath, 8, 5, 3, "Kog", textColor);

                        if (!keyItems[1].playerObtained)
                        {
                            ItemInspect(1);
                        }

                        okToGo = true;
                    }
                    else if (interrogationPresent.Equals(File.ReadLines(SerenPath).ElementAt(21)) || interrogationPresent.Equals(File.ReadLines(SerenPath).ElementAt(22)))
                    {
                        angry = true;
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        Interrogation(KogPath, 5, 1, 2, "Seren", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Seren", textColor);
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
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("The hell is this?");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nGet outta my sight Monte.");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("Eh? Are you deaf?");
                                Console.ResetColor();
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        public static void RutherianInterrogation()
        {
            Console.Clear();
            textColor = ConsoleColor.DarkYellow;
            okToGo = false;
            firstLoop = true;
            while (!okToGo)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                if (timeDays <= 3) Interrogation(RutherianPath, 1, 3, 1, "Rutherian", textColor);
                if (timeDays > 3) Interrogation(RutherianPath, 22, 3, 1, "Rutherian", textColor);
                
                ReplyCheck();

                void ReplyCheck()
                {
                    bool silent = false;
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    if (interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(163)) || interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(164)) || interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(165)) || interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(166)))
                    {
                        Interrogation(RutherianPath, 45, 2, 2, "Rutherian", textColor);
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(File.ReadLines(MinorNPCTalkPath).ElementAt(167)))
                    {
                        Interrogation(RutherianPath, 49, 5, 2, "Rutherian", textColor);
                    }
                    else if (interrogationPresent.Equals(keyItems[0].name))
                    {
                        if (!givenFreeMoney)
                        {
                            Interrogation(RutherianPath, 6, 11, 2, "Rutherian", textColor);
                            givenFreeMoney = true;
                            money += 150;
                            Console.ReadKey();
                        }
                        else
                        {
                            Interrogation(RutherianPath, 10, 5, 2, "Rutherian", textColor);
                            Console.ReadKey();
                        }
                    }
                    else if (interrogationPresent.Equals(keyItems[2].name))
                    {
                        Interrogation(RutherianPath, 39, 4, 2, "Rutherian", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[3].name))
                    {
                        Interrogation(RutherianPath, 33, 4, 2, "Rutherian", textColor);
                        Console.ReadKey();
                    }
                    else if (interrogationPresent.Equals(keyItems[4].name))
                    {
                        Interrogation(MinorNPCTalkPath, 184, 1, 2, "Rutherian", textColor);
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
                        Console.ForegroundColor = textColor;
                        Console.WriteLine("I'm not sure what to say given this information.");
                        Console.ResetColor();
                    }

                    if (!silent && !okToGo)
                    {
                        switch (interrogationLine)
                        {
                            case 24:
                                Interrogation(RutherianPath, 27, 4, 2, "Rutherian", textColor);
                                Console.ReadKey();
                                break;
                            case 998:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("\nPlease be safe inspector!");
                                Console.ResetColor();
                                Console.WriteLine("[Any Key] Continue...");
                                Console.ReadKey();
                                okToGo = true;
                                break;
                            case 999:
                                break;
                            default:
                                Console.ForegroundColor = textColor;
                                Console.WriteLine("I'm not certain how much more I can elaborate?");
                                Console.ResetColor();
                                break;
                        }
                    }
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        // Methods for Object interrogation ---------------------------------------------------------------------------------------------
        public static void ItemInspect(int itemID)
        {
            Console.Clear();
            switch (itemID)
            {
                case 0:
                    Console.WriteLine("On the table lies a Missing Persons flyer. You note a few important details:");
                    break;
                case 1:
                    Console.WriteLine("Your ticket into the Masked Market lays in front of you.");
                    break;
                case 2:
                    Console.WriteLine("Once you recover from your pounding head, you take the offered item and inspect it. Its...");
                    break;
                case 3:
                    Console.WriteLine("While Seren has his back turned you sneak one of the mysterous vials into your pocket.");
                    break;
                case 4:
                    Console.WriteLine("\"Wait WAIT Friend before you go... take this and advertize my wares...\"");
                    Console.WriteLine("The Beak-Mask attempts to hand you a can. The lid is hanging off so you can see whats inside.");
                    break;
                case 5:
                    Console.WriteLine("You notice something tucked under the bar. You quickly grab it before any notices.");
                    break;
                default:
                    Console.WriteLine("Error. The game is about to crash. You may not even see this text before it does.");
                    Console.WriteLine("If it doesnt- thats cool too. Please report this text if you see it in gameplay.");
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