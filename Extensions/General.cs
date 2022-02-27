using Oasys.Common.GameObject;
using Oasys.Common.GameObject.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ok_Maw
{
    public static partial class Extensions
    {
        public static float TotalShieldPlusHealth(this GameObjectBase entity) => (float)(entity.Health + entity.NeutralShield + entity.MagicalShield + entity.PhysicalShield);

        public static bool HasUndyingBuff(this AIHeroClient target)
        {
            if (target.BuffManager.GetBuffList().Any(
                b => b.IsActive &&
                    (b.Name == "ChronoShift" ||
                     b.Name == "FioraW" ||
                     b.Name == "BardRStasis" ||
                     b.Name == "JudicatorIntervention" ||
                     b.Name == "UndyingRage"
                    )
               ))
                return true;
            return !target.IsTargetable;
        }

        public static bool IsInRange(this AIHeroClient targHero, float range)
        {
            return targHero.Distance <= range + targHero.UnitComponentInfo.UnitBoundingRadius + 5;
        }
    }
}
