using Oasys.Common.Enums.GameEnums;
using Oasys.Common.GameObject.Clients;
using Oasys.Common.Menu;
using Oasys.Common.Menu.ItemComponents;
using Oasys.SDK;
using Oasys.SDK.Menu;
using Oasys.SDK.SpellCasting;
using Ok_Maw.Modules.Spells;
using Ok_Maw.OasysPrediction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ok_Maw.Modules
{
    internal static class Autocast
    {
        public static int Tick = 0;
        public static int LastActionTick = 0;

        public static int AutoCaster;
        public static int AutoCasterQ;
        public static int AutoCasterW;
        public static int AutoCasterE;
        public static int AutoCasterR;
        public static int AutoCasterMode;

        public static Task Autocaster()
        {
            Tick += 10;

            // Def Settings Tab
            Tab KogSettingsTab = MenuManager.GetTab("OKMaw - Settings");
            // Check if AutoCaster is turned on in the menu
            // If the last action was done <= XXX Ticks ago => Skip
            if ((Tick - LastActionTick) <= 50)
                return Task.CompletedTask;
            KogMaw champ = new KogMaw();
            if (KogSettingsTab.SwitchItemOn(AutoCaster) && Use.Me.IsAlive)
            {

                // Q Auto cast if turned on
                if (KogSettingsTab.SwitchItemOn(AutoCasterQ) && Use.Me.SpellReady(SpellSlot.Q))
                {
                    if (Use.AnyOneInRange(SpellSlot.Q))
                    {
                        foreach (AIHeroClient Hero in UnitManager.EnemyChampions)
                        {
                            if (Hero.IsInRange(Use.Me.CastRange(SpellSlot.Q)) && Use.Me.SpellReady(SpellSlot.Q))
                            {
                                var pred = Prediction.MenuSelected.GetPrediction(Prediction.MenuSelected.PredictionType.Line, Hero, Use.Me.CastRange(SpellSlot.Q), Use.Me.SpellRadius(SpellSlot.Q), -2, Use.Me.SpellMissileSpeed(SpellSlot.Q), true);
                                if (pred.EnoughHitChance() && !pred.MoreCollisionsThan(0))
                                {
                                    SpellCastProvider.CastSpell(CastSlot.Q, Hero.Position);
                                    LastActionTick = Tick;
                                    break;
                                }
                            }
                        }
                    }
                }

                // W Auto cast if turned on
                if (KogSettingsTab.SwitchItemOn(AutoCasterW) && Use.Me.SpellReady(SpellSlot.W))
                {
                    if (Use.AnyOneInRange(champ.CurrentRange))
                    {
                        bool _ = (Use.Me.SpellReady(SpellSlot.W));
                        if (_)
                        {
                            SpellCastProvider.CastSpell(CastSlot.W);
                            LastActionTick = Tick;
                        }
                    }
                }

                // E Auto cast if turned on
                if (KogSettingsTab.SwitchItemOn(AutoCasterE) && Use.Me.SpellReady(SpellSlot.E))
                {
                    if (Use.AnyOneInRange(SpellSlot.E))
                    {
                        foreach (AIHeroClient Hero in UnitManager.EnemyChampions)
                        {
                            var pred = Prediction.MenuSelected.GetPrediction(Prediction.MenuSelected.PredictionType.Line, Hero, Use.Me.CastRange(SpellSlot.E), Use.Me.SpellRadius(SpellSlot.E), -2, Use.Me.SpellMissileSpeed(SpellSlot.E), false);
                            if (pred.EnoughHitChance())
                            {
                                SpellCastProvider.CastSpell(CastSlot.Q, Hero.Position);
                                LastActionTick = Tick;
                                break;
                            }
                        }
                    }
                }

                // R Auto cast if turned on (Not recommended)
                if (KogSettingsTab.SwitchItemOn(AutoCasterR) && Use.Me.SpellReady(SpellSlot.R))
                {
                    if (Use.AnyOneInRange(SpellSlot.R))
                    {
                        foreach (AIHeroClient Hero in UnitManager.EnemyChampions)
                        {
                            var pred = Prediction.MenuSelected.GetPrediction(Prediction.MenuSelected.PredictionType.Line, Hero, Use.Me.CastRange(SpellSlot.E), Use.Me.SpellRadius(SpellSlot.E), -2, Use.Me.SpellMissileSpeed(SpellSlot.E), false);
                            if (pred.EnoughHitChance())
                            {
                                SpellCastProvider.CastSpell(CastSlot.Q, Hero.Position);
                                LastActionTick = Tick;
                                break;
                            }
                        }
                    }
                }
            }
            return Task.FromResult(0);
        }
    }
}
