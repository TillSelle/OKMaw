using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Oasys.SDK.Prediction.MenuSelected;

namespace Ok_Maw.OasysPrediction
{
    internal static class Extensions
    {
        public static bool EnoughHitChance(this PredictionOutput predOut)
        {
            return (predOut.HitChance == HitChance.VeryHigh || predOut.HitChance == HitChance.High || predOut.HitChance == HitChance.Immobile);
        }

        public static float CollisionNumber(this PredictionOutput predOut)
        {
            return predOut.CollisionObjects.Count;
        }

        public static bool MoreCollisionsThan(this PredictionOutput predOut, int MaxNumberOfCollisions)
        {
            return (predOut.CollisionNumber() >= MaxNumberOfCollisions);
        }
    }
}
