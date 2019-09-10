using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static UnwarrantedTools.Tools;
using static UnwarrantedTools.Locations;

namespace Unwarranted
{
    class Program
    {
        static void Main(string[] args)
        {
            NPCDialogues diaHolder = new NPCDialogues();

            Console.WriteLine("               UNWARRANTED v0.0.0.0.4");
            Console.WriteLine("\n[N]ew game                             [L]oad game");
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
                        break;
                    case 'l':
                        Load();
                        break;

                }
            }

            // This is the main game loop
            while (!timeUp)
            {
                if (timeDays == 8 && !inBattle)
                {
                    TimeUp();
                    if (timeUp)
                    {
                        break;
                    }
                }

                //OpenInventory();

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
                    case MapLocations.Sprocket:
                        Sprocket();
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
                    case MapLocations.TrainA:
                        TrainA();
                        TimeAdvance();
                        break;
                    case MapLocations.TrainB:
                        TrainB();
                        TimeAdvance();
                        break;
                    case MapLocations.TrainC:
                        TrainC();
                        TimeAdvance();
                        break;
                }

                //diaHolder.TestCharacterInterrogation();

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
//
//
//
//