using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Unwarranted
{
    public class NPCDialogues
    {
        Tools tl = new Tools();

        string TestNPCPath = "NPC Dialogues/testTalk.txt";

        public void TestCharacterInterrogation()
        {
            tl.InterrogationStart(TestNPCPath, 0, 5);
            switch (Tools.interrogationLine)
            {
                case 999:
                    break;
            }
            if (Tools.interrogationPresent.Equals(File.ReadLines(TestNPCPath).ElementAt(1)))

            {
                Console.WriteLine("CAN I GET AN \"H\" IN THE CHAT");
            }
        }
    }
}

//When interrogationLine is 999 always treat it as being blank.
//When interrogationPresent is "," always treat it as being silent for one round.