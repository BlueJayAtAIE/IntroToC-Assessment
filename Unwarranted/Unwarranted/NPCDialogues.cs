using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static UnwarrantedTools.Tools;

namespace Unwarranted
{
    public class NPCDialogues
    {
        string TestNPCPath = "NPC Dialogues/testTalk.txt";

        private bool okToGo = false;
        private bool firstLoop = true;
        private bool fear = false;
        private bool angry = false;

        // Currently, interrogations go on for as long as until the player has discovered the right path to the end. They cannot
        // leave the interrogation loop until then, and once they do hit the end they have only the option to leave.
        // This is likely to change- I'll reformate some things as to not possibly trap the player in an encounter they
        // cant possibly finish with the information they have.
        /// <summary>
        /// Test conversation that most all interrogations will be based on. Reference txt file for conversation flow.
        /// </summary>
        public void TestCharacterInterrogation()
        {
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
                    if (interrogationPresent.Equals(File.ReadLines(TestNPCPath).ElementAt(17)))
                    {
                        InterrogationEnd(TestNPCPath, 20, 2);
                        Console.WriteLine("debug message: heckin gottem");
                        okToGo = true;
                    }
                    if (interrogationPresent.Equals(","))
                    {
                        Console.WriteLine("debug message: honk");
                        silent = true;
                    }
                    if (interrogationPresent.Equals("")) ; // Purposefully blank- this prevents the else below from picking up blank as a reply option.
                    else
                    {
                        Console.WriteLine("This means nothing to me.");
                    }

                    if (!silent)
                    {
                        switch (interrogationLine)
                        {
                            default:
                                Console.WriteLine("I can't explain anything more about that...");
                                break;
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
                                break;
                        }
                    }
                    Console.WriteLine();
                    firstLoop = false;
                }
            }
        }
    }
}

//When interrogationLine is 999 always treat it as being blank.
//When interrogationPresent is "," always treat it as being silent for one round.