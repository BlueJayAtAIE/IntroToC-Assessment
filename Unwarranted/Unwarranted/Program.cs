using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static UnwarrantedTools.Tools;
using static UnwarrantedTools.Locations;
using static UnwarrantedTools.NPCDialogues;

namespace Unwarranted
{
    class Program
    {
        static void Main(string[] args)
        {
            // Game is preloaded to allow for checking for achievements
            Load();
            Console.WriteLine("                -= UNWARRANTED =-");
            Console.WriteLine("\n[N]ew game                            [L]oad game");

            // Achievements checking
            int achievementCount = 0;
            foreach (bool b in endingsObtained)
            {
                if (b == true)
                {
                    achievementCount++;
                }
            }
            Console.WriteLine($"\n\nAchievements: {achievementCount}/{endingsObtained.Count()}");
            if (endingsObtained[0]) Console.WriteLine("\tSquashed - Exposed Kog as the perpetrator.");
            if (endingsObtained[1]) Console.WriteLine("\tLove to Hate You - Discovered Feri to be the culprit.");
            if (endingsObtained[2]) Console.WriteLine("\tDragon Slayer - Proved Seren to be the transgressor.");
            if (endingsObtained[3]) Console.WriteLine("\tUnder the Guise - Exposed Rutherian as the mastermind behind the kidnappings.");

            // Check for user input.
            while (userInputChar != 'n' && userInputChar != 'l')
            userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
            {
                switch (userInputChar)
                {
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                    case 'n':
                        NewSave();
                        MaridethOpeningInterrogation();
                        break;
                    case 'l':
                        Console.Clear();
                        break;

                }
            }


            // This is the main game loop.
            while (true)
            {
                // Checks the time to see if the final day has hit. If it has, the game over state will start.
                if (timeDays >= 8 && !inBattle)
                {
                    Console.WriteLine("\nThe top hour of the final day has commenced... your time is up.");
                    GameEnd();
                    if (timeUp)
                    {
                        break;
                    }
                }

                Console.Clear();
                TimeDisplay();

                // Big scary switch for navigation.
                switch (GetLocation())
                {
                    default:
                    case MapLocations.Home:
                        Home();
                        TimeAdvance();
                        break;
                    case MapLocations.Center:
                        Center();
                        TimeAdvance();
                        break;
                    case MapLocations.Library:
                        Library();
                        TimeAdvance();
                        break;
                    case MapLocations.Shop:
                        Shop();
                        TimeAdvance();
                        break;
                    case MapLocations.Alley:
                        Alley();
                        TimeAdvance();
                        break;
                    case MapLocations.Docks:
                        Docks();
                        TimeAdvance();
                        break;
                    case MapLocations.Square:
                        Square();
                        TimeAdvance();
                        break;
                    case MapLocations.Tavern:
                        Tavern();
                        TimeAdvance();
                        break;                    
                    case MapLocations.Kog:
                        Kog();
                        TimeAdvance();
                        break;
                    case MapLocations.Feri:
                        Feri();
                        TimeAdvance();
                        break;
                    case MapLocations.Rutherian:
                        Rutherian();
                        TimeAdvance();
                        break;
                    case MapLocations.Seren:
                        Seren();
                        TimeAdvance();
                        break;
                    case MapLocations.Market:
                        Market();
                        TimeAdvance();
                        break;
                    case MapLocations.Hideout:
                        Hideout();
                        TimeAdvance();
                        break;
                    // Taking any of the trains actually doesn't take an action- they're faster than "walking" from place to place.
                    case MapLocations.TrainA:
                        TrainA();
                        break;
                    case MapLocations.TrainB:
                        TrainB();
                        break;
                    case MapLocations.TrainC:
                        TrainC();
                        break;
                }

                // PLEASE KEEP COMMENTED.
                //Console.WriteLine("End of Debug. Press the any key.");
                //break;
            }
        }
    }
}

// Hello, these are some notes to myself to help understand where to go next.
//
//Console.WriteLine("THIS IS A UNICODE CHARACTER TEST. DELETE THIS LATER FUTURE SELF.");
//Console.WriteLine("\nPIPES: ═ ║ ╒ ╓ ╔ ╕ ╖ ╗ ╘ ╙ ╚ ╛ ╜ ╝ ╞ ╟ ╠ ╡ ╢ ╣ ╤ ╥ ╦ ╧ ╨ ╩ ╪ ╫ ╬ \n\nBOXES: ▀ ▄ █ ▌ ▐ ░ ▒ ▓ ■");
//
// TODO: find Seren a better name. I thought of one forever ago but forgot to write it down... It's too similar to Selene
//
//
//