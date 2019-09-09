using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Unwarranted
{
    class Program
    {
        static void Main(string[] args)
        {
            Tools tl = new Tools();
            NPCDialogues dialogueHolder = new NPCDialogues();

            // This is the main game loop
            while (true)
            {
                if (Tools.timeDays == 8)
                {
                    // Inform the player time is up; offer to load previous save or quit entirely.
                    break;
                }

                dialogueHolder.TestCharacterInterrogation();

                Console.WriteLine("End of Debug. Press the any key.");
                Console.ReadKey();
                break;
            }
        }
    }
}

// Hello, these are some notes to myself to help understand where to go next.
//
// Now how do interrogations actually work? the InterrogationStart will start the encounter (duh). Another
// method for the overall interaction will be made that loops the player through several times (InterrogationContinue). There will be a switch on the userIntputInt and userIntputChar  
// variables which will direct the flow to the next part of the interrogation- basically just running StartInterrogation again with new parameters.
// While this does mean making methods for all important interrogations with multiple branching paths, it also means we can store the NPC dialogue from
// one character ALL in one document- as long as we keep track of where to start and end dialogue chunks.
// Thankfully, smaller interactions like inspecting books and objects need only use StartInterrogation once, although they need a modified version without
// some of the commands.
//
//
//
//