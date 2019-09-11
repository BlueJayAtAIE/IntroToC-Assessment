using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static UnwarrantedTools.Tools;

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

        //TODO: all conversations with Prime Suspects.

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
                    else if (interrogationPresent.Equals(".")) ;
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

        // Methods for Object interrogation ---------------------------------------------------------------------------------------
        public static void MissingPosterInspect()
        {
            Console.Clear();
            Console.WriteLine("On the table lies a Missing Persons flyer. You note a few important details:");
            Interrogation(ObjectTextPath, 1, 3, true, "Missing Person Poster");
        }
    }
}

//When interrogationLine is 999 always treat it as being blank.
//When interrogationLine is 998 always treat it as leaving the conversation.
//When interrogationPresent is "." always treat it as being blank.
//When interrogationPresent is "," always treat it as being silent for one round.