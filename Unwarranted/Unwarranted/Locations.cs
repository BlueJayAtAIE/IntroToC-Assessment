using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnwarrantedTools.Tools;


namespace UnwarrantedTools
{
    public static class Locations
    {
        static MapLocations location = MapLocations.Home;

        public static MapLocations GetLocation()
        {
            return location;
        }

        public static void Home()
        {
            // TODO: make some interactions with mari so your house isnt so barren
            Console.WriteLine("\nHome is home. It barely fits you and your company, but rent's cheap.\nPapers are scattered on tables and the floor. All your pegboards are filled with old cases.\nYou should clean up soon. Especially since Rat keeps coming over to trash the place even more.");
            Console.WriteLine("\nYou can sleep in your bed for a while, or you can talk to Marideth.\nFrom here you can walk to a few places relatively quickly, including a few of the busier parts of town.");
            Console.WriteLine("\n[R]est (Save), [L]uxxian Great Library, [T]rain Station (Midday Station), [S]unrise Center, [B]right Sqaure");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        Save();
                        break;
                    case 'l':
                        location = MapLocations.Library;
                        break;
                    case 't':
                        location = MapLocations.TrainB;
                        break;
                    case 's':
                        location = MapLocations.Center;
                        break;
                    case 'b':
                        location = MapLocations.Square;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Center()
        {
            Console.WriteLine("[H]ome, [L]uxxian Great Library, [S]hop, Light [A]lley, [D]ocks");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'h':
                        location = MapLocations.Home;
                        break;
                    case 'l':
                        location = MapLocations.Library;
                        break;
                    case 's':
                        location = MapLocations.Shop;
                        break;
                    case 'a':
                        location = MapLocations.Alley;
                        break;
                    case 'd':
                        location = MapLocations.Docks;
                        break;                  
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Library()
        {
            // TODO: Time to write MASSIVE AMOUNTS OF LORE FILLED FLAVOR TEXT :^) t a s t y
            Console.WriteLine("[H]ome, [C]enter");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'h':
                        location = MapLocations.Home;
                        break;
                    case 'c':
                        location = MapLocations.Center;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Shop()
        {
            Console.WriteLine("[C]enter");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'c':
                        location = MapLocations.Center;
                        break;                   
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Alley()
        {
            Console.WriteLine("An entirely ironic name, Light Alley is probably the darkest place in all of the citadel.\nA place for shifty creatures of all kind to do buissiness behind the back of the law.\nThis is the stomping grounds of one of your informants, Rat. Hardly a friend; more of a liability.");
            // TODO: make some possible interactions for this area.
            // Placeholder. If you have knowledge of the Masked Market, you can make your way there.
            if (true)
            {
                Console.WriteLine("\nFrom here, you've only one direction to go- back to Sunrise Center.");
                Console.WriteLine("[S]unrise Center");
            }
            if (false)
            {
                Console.WriteLine("\nFrom here, you can go back to Sunrise Center, or if you feel like walking into your death, stroll the Masked Market.");
                Console.WriteLine("[S]unrise Center, [M]asked Market");
            }
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 's':
                        location = MapLocations.Center;
                        break;
                    case 'm':
                        if (false)
                        {
                            location = MapLocations.Market;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                            okToGo = false;
                        }
                        break;                   
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Docks()
        {
            Console.WriteLine("[T]rain, [C]enter, [K]og");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 't':
                        location = MapLocations.TrainA;
                        break;
                    case 'c':
                        location = MapLocations.Center;
                        break;
                    case 'k':
                        location = MapLocations.Kog;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Square()
        {
            Console.WriteLine("[H]ome, [T]avern, [F]eri, [S]procket, [K]og");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'h':
                        location = MapLocations.Home;
                        break;
                    case 't':
                        location = MapLocations.Tavern;
                        break;
                    case 'f':
                        location = MapLocations.Feri;
                        break;
                    case 's':
                        location = MapLocations.Sprocket;
                        break;
                    case 'k':
                        location = MapLocations.Kog;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Tavern()
        {
            Console.WriteLine("[S]quare, [F]eri");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 's':
                        location = MapLocations.Square;
                        break;
                    case 'f':
                        location = MapLocations.Feri;
                        break;                          
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Sprocket()
        {
            Console.WriteLine("[S]quare");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 's':
                        location = MapLocations.Square;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Kog()
        {
            Console.WriteLine("[D]ocks, [B]right Square, [M]asked Market");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'd':
                        location = MapLocations.Docks;
                        break;
                    case 's':
                        location = MapLocations.Square;
                        break;
                    case 'm':
                        location = MapLocations.Market;
                        break;                    
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Feri()
        {
            Console.WriteLine("[B]right Square, [P]en & Dragon Tavern");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 's':
                        location = MapLocations.Square;
                        break;
                    case 't':
                        location = MapLocations.Tavern;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Rutherian()
        {
            Console.WriteLine("[T]rain Station (Dawn Station), [H]ideout");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 't':
                        location = MapLocations.TrainC;
                        break;
                    case 'h':
                        location = MapLocations.Hideout;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Seren()
        {
            Console.WriteLine("[T]rain Station (Dawn Station), [H]ideout");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 't':
                        location = MapLocations.TrainC;
                        break;
                    case 'h':
                        location = MapLocations.Hideout;
                        break;                    
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Market()
        {
            Console.WriteLine("[L]ight Alley, [K]og's Workshop");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'a':
                        location = MapLocations.Alley;
                        break;
                    case 'k':
                        location = MapLocations.Kog;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Hideout()
        {
            Console.WriteLine("[R]utherian's Estate, [S]eren's Lab");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        location = MapLocations.Rutherian;
                        break;
                    case 's':
                        location = MapLocations.Seren;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void TrainA()
        {
            Console.WriteLine("[D]ocks, [T]ake Train to Midday Station");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'd':
                        location = MapLocations.Docks;
                        break;
                    case 't':
                        location = MapLocations.TrainB;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void TrainB()
        {
            Console.WriteLine("\nYou arrive at Midday Station. This is the train station closest to your house.\nTaking the train to Dusk Station will bring you to the docks, while the Dawn Station train takes you to the Inner Ring.");
            Console.WriteLine("\n[H]ome, [T]ake Train to Dusk Station, [R]ide Train to Dawn Station");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'h':
                        location = MapLocations.Home;
                        break;
                    case 't':
                        location = MapLocations.TrainA;
                        break;
                    case 'r':
                        location = MapLocations.TrainC;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void TrainC()
        {
            Console.WriteLine("\nYou arrive at Dawn Station. This is the train station in the Inner Ring.\nTaking the train to Midday Station will bring you back close to your house.");
            Console.WriteLine("\nThe Inner Ring is home to the aristocrats of Lux, seperated from the Outter Ring by a giant wall.\nFor common folk, theres practically nothing to do here but sight-see and talk to the locals.");
            Console.WriteLine("\n[R]utherian's Estate, [S]eren's Lab, [T]ake Train to Midday Station");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        location = MapLocations.Rutherian;
                        break;
                    case 's':
                        location = MapLocations.Seren;
                        break;
                    case 't':
                        location = MapLocations.TrainB;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }
    }
}
