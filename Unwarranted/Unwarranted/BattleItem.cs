using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static UnwarrantedTools.Tools;

namespace UnwarrantedTools
{
    public class BattleItem : Item
    {
        public int attackDamage;
        public AttackEffect attackEffect;
        public bool equipt = false;

        public BattleItem(string itemName, int itemIDNumber, int damage, AttackEffect effect)
        {
            name = itemName;
            itemID = itemIDNumber;
            attackDamage = damage;
            attackEffect = effect;          
        }
    }
}

// Attack Effect info:
// Burn: Burns for 2 turns, dealing attackDamage at the start of each of the two turns.
// Sap: Deals damage than heals by half the damage dealt (rounded up).
// Static: 50% chance to stun for 1 turn, effectively skipping the turn of the opponent.
// Restore: Deals no damage, instead heals user for attackDamage.
