using Oasys.Common.GameObject.Clients;
using Oasys.SDK;
using Oasys.Common.Enums.GameEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ok_Maw
{
    internal static class Use
    {
        public static AIHeroClient Me => UnitManager.MyChampion;

        public static bool AnyOneInRange(SpellSlot spellslot)
        {
            return AnyOneInRange(Me.CastRange(spellslot));
        }
        public static bool AnyOneInRange(float spellCastRange)
        {
            bool someoneInRange = false;
            foreach (AIHeroClient champ in UnitManager.EnemyChampions)
            {
                someoneInRange = (champ.IsInRange(spellCastRange)) ? true : (someoneInRange == true) ? true : false;
            }
            return someoneInRange;
        }
    }
}
