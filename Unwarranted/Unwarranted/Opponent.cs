using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static UnwarrantedTools.Tools;

namespace UnwarrantedTools
{
    public class Opponent
    {
        public string opponentName;
        public int opponentHP;
        public BattleItem primaryAttack;
        public BattleItem secondaryAttack;
        public BattleItem tertiaryAttack;

        public Opponent(string name)
        {
            switch (name)
            {
                case "Thug":
                    ThugBattle();
                    break;
                case "Kog":
                    KogBattle();
                    break;
                case "Seren":
                    SerenBattle();
                    break;
                case "Spectre1":
                    SpectreBattleRoundOne();
                    break;
                case "Spectre2":
                    SpectreBattleRoundTwo();
                    break;
                case "Rutherian":
                    RutherianBattleRoundOne();
                    break;
                case "RutherianX":
                    RutherianBattleRoundTwo();
                    break;
            }
        }

        void ThugBattle()
        {
            opponentName = "The Thug";
            opponentHP = 8;
            primaryAttack = (BattleItem)battleRunes[0]; //Nothing
            secondaryAttack = (BattleItem)battleRunes[1]; //Basic
            tertiaryAttack = (BattleItem)battleRunes[3]; //Stun
        }

        void KogBattle()
        {
            opponentName = "Kog";
            opponentHP = 15;
            primaryAttack = (BattleItem)battleRunes[3]; //Stun
            secondaryAttack = (BattleItem)battleRunes[4]; //Shock
            tertiaryAttack = (BattleItem)battleRunes[7]; //Smite
        }

        void SerenBattle()
        {
            opponentName = "Seren";
            opponentHP = 17;
            primaryAttack = (BattleItem)battleRunes[2]; //Flame
            secondaryAttack = (BattleItem)battleRunes[4]; //Shock
            tertiaryAttack = (BattleItem)battleRunes[5]; //Restore
        }

        void SpectreBattleRoundOne()
        {
            opponentName = "The Spectre";
            opponentHP = 12;
            secondaryAttack = (BattleItem)battleRunes[3]; //Stun
            tertiaryAttack = (BattleItem)battleRunes[6]; //Drain
            primaryAttack = (BattleItem)battleRunes[0]; //Nothing
        }

        void SpectreBattleRoundTwo()
        {
            opponentName = "The Spectre";
            opponentHP = 16;
            primaryAttack = (BattleItem)battleRunes[2]; //Flame
            secondaryAttack = (BattleItem)battleRunes[6]; //Drain
            tertiaryAttack = (BattleItem)battleRunes[7]; //Smite
        }

        void RutherianBattleRoundOne()
        {
            opponentName = "Rutherian";
            opponentHP = 12;
            primaryAttack = (BattleItem)battleRunes[3]; //Stun
            secondaryAttack = (BattleItem)battleRunes[6]; //Drain
            tertiaryAttack = (BattleItem)battleRunes[8]; //Charge
        }

        void RutherianBattleRoundTwo()
        {
            opponentName = "Corrupt Rutherian";
            opponentHP = 20;
            primaryAttack = (BattleItem)battleRunes[0]; //Nothing
            secondaryAttack = (BattleItem)battleRunes[7]; //Smite
            tertiaryAttack = (BattleItem)battleRunes[9]; //Leech
        }

        // >in tools
        // function for calling this constructor
        // define a turn
        // use rng.next a lot
        // check for Death
        // advance if both are alive
        // if You Perish, send back to last save
    }
}
