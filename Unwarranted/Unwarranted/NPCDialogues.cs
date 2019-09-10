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

        private static string TestNPCPath = "NPC Dialogues/testTalk.txt";

        // Currently, interrogations go on for as long as until the player has discovered the right path to the end. They cannot
        // leave the interrogation loop until then, and once they do hit the end they have only the option to leave.
        // This is likely to change- I'll reformate some things as to not possibly trap the player in an encounter they
        // cant possibly finish with the information they have. So.... TODO
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
                if (!firstLoop) InterrogationStart(TestNPCPath, 1, 4);
                if (firstLoop) InterrogationStart(TestNPCPath, 0, 5);
                ReplyCheck();
            
                // Reply check will run through the present and line variables, and depending on what they are, dialogue will
                // change. Present is to be checked first, then line (only if the player isn't silent that round).
                void ReplyCheck()
                {
                    bool silent = false;

                    // Checks presented line against whatever parameter. Can be NPC's own dialogue, someone else's, or even an object.
                    if (interrogationPresent.Equals(File.ReadLines(TestNPCPath).ElementAt(9)))
                    {
                        InterrogationContinue(TestNPCPath, 17, 1);
                        ReplyCheck();
                    }
                    else if (interrogationPresent.Equals(File.ReadLines(TestNPCPath).ElementAt(17)))
                    {
                        InterrogationEnd(TestNPCPath, 20, 2);
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
                                InterrogationContinue(TestNPCPath, 7, 1);
                                ReplyCheck();
                                break;
                            case 2:
                                InterrogationContinue(TestNPCPath, 9, 2);
                                ReplyCheck();
                                break;
                            case 4:
                                InterrogationContinue(TestNPCPath, 12, 1);
                                ReplyCheck();
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

        public static void H()
        {
            // Put some stuff in here. Make some more conversations.
        }
    }
}

//When interrogationLine is 999 always treat it as being blank.
//When interrogationPresent is "." always treat it as being blank.
//When interrogationPresent is "," always treat it as being silent for one round.