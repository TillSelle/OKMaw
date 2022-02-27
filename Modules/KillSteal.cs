using Oasys.Common.Enums.GameEnums;
using Oasys.Common.GameObject.Clients;
using Oasys.Common.Menu.ItemComponents;
using Oasys.SDK;
using Oasys.SDK.Menu;
using Ok_Maw.Modules.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasys.Common.Extensions;
using Oasys.SDK.SpellCasting;
using Oasys.Common.Tools.Devices;
using Oasys.SDK.InputProviders;
using Oasys.Common.GameObject;
using static Oasys.Common.Logic.LS.HitChance;

namespace Ok_Maw.Modules
{
    internal static class KillSteal
    {

        public static int KillStealer;
        public static int KillStealerQ;
        public static int KillStealerE;
        public static int KillStealerR;
        public static int KillStealerAA;
        public static int KillStealOn;
        public static int AutoCaster;
        public static int AutoCasterQ;
        public static int AutoCasterQMode;
        public static int AutoCasterQPred;
        public static int AutoCasterW;
        public static int AutoCasterWMode;
        public static int AutoCasterE;
        public static int AutoCasterEMode;
        public static int AutoCasterEPred;
        public static int AutoCasterR;
        public static int AutoCasterRMode;
        public static int AutoCasterRPred;

        public static bool IsKillable(this AIHeroClient target, SpellSlot spellslot)
        {
            float TotalHealth = target.TotalShieldPlusHealth();
            if (!target.IsTargetable || !target.IsEnemy || !target.IsAlive || !target.IsVisible  || target.HasUndyingBuff())
                return false;
            if (target.ModelName == "Blitzcrank" && !target.BuffManager.HasBuff("BlitzcrankManaBarrierCD") && !target.BuffManager.HasBuff("ManaBarrier"))
                TotalHealth += target.Mana / 2;
            KogMaw kogMaw = new KogMaw();
            return kogMaw.CalculateActualDamage(target, spellslot) >= TotalHealth;
        }

        public static Task KillstealerAndAutoCast()
        {
            if (MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(KillStealer).IsOn)
            {
                foreach (CanKillClass enemie in _CoreEvents.IsKillable)
                {
                    if (!enemie.Target.IsAlive)
                        continue;
                    if (enemie.IsKillable)
                    {
                        float[] RRange = new float[4] {0,1300,1550,1800};
                        if (MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(KillStealerR).IsOn && enemie.IsKillableR && enemie.Target.IsInRange(RRange[UnitManager.MyChampion.GetSpellBook().GetSpellClass(SpellSlot.R).Level]))
                        {
                            Orbwalker.AllowAttacking = false;

                            var pred = Prediction.MenuSelected.GetPrediction(Prediction.MenuSelected.PredictionType.Circle, enemie.Target, RRange[UnitManager.MyChampion.GetSpellBook().GetSpellClass(SpellSlot.R).Level], 240, 0.6F, 900000, false);
                            if (pred.HitChance == Prediction.MenuSelected.HitChance.High || pred.HitChance == Prediction.MenuSelected.HitChance.VeryHigh || pred.HitChance == Prediction.MenuSelected.HitChance.Immobile)
                            {
                                SpellCastProvider.CastSpell(CastSlot.R, pred.CastPosition, 0.25F);
                            }
                            /*var pred = Prediction.LS.GetPrediction(enemie.Target, 0.6F, 240, 10000);
                            if (pred.Hitchance == VeryHigh || pred.Hitchance == High || pred.Hitchance == Immobile && UnitManager.MyChampion.GetSpellBook().GetSpellClass(SpellSlot.R).IsSpellReady)
                            {
                                SpellCastProvider.CastSpell(CastSlot.R, pred.CastPosition, 0.25F);
                            }*/
                            Orbwalker.AllowAttacking = true;
                        }
                        if (enemie.IsKillableAA && enemie.Target.IsInRange(UnitManager.MyChampion.TrueAttackRange) && MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(KillStealerAA).IsOn)
                        {
                            Orbwalker.SelectedTarget = enemie.Target;
                            Orbwalker.SelectedHero = enemie.Target;
                        }
                        if (enemie.IsKillableQ && MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(KillStealerQ).IsOn && UnitManager.MyChampion.GetSpellBook().GetSpellClass(SpellSlot.Q).IsSpellReady)
                        {
                            Orbwalker.AllowAttacking = false;

                            var pred = Prediction.MenuSelected.GetPrediction(Prediction.MenuSelected.PredictionType.Line, enemie.Target, 1360, 240, 0.25F, 1400, true);
                            if (pred.HitChance == Prediction.MenuSelected.HitChance.High || pred.HitChance == Prediction.MenuSelected.HitChance.VeryHigh || pred.HitChance == Prediction.MenuSelected.HitChance.Immobile && !pred.Collision)
                            {
                                SpellCastProvider.CastSpell(CastSlot.Q, pred.CastPosition, 0.25F);
                            }

                            Orbwalker.AllowAttacking = true;
                        }
                        if (enemie.IsKillableE && enemie.Target.IsInRange(1360) && MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(KillStealerE).IsOn)
                        {
                            Orbwalker.AllowAttacking = false;

                            var pred = Prediction.MenuSelected.GetPrediction(Prediction.MenuSelected.PredictionType.Line, enemie.Target, 1360, 240, 0.25F, 1400, false);
                            if (pred.HitChance == Prediction.MenuSelected.HitChance.High || pred.HitChance == Prediction.MenuSelected.HitChance.VeryHigh || pred.HitChance == Prediction.MenuSelected.HitChance.Immobile)
                            {
                                SpellCastProvider.CastSpell(CastSlot.E, pred.CastPosition, 0.25F);
                            }

                            /*var pred = Prediction.LS.GetPrediction(enemie.Target, 0.25F, 240, 1400);
                            if (pred.Hitchance == VeryHigh || pred.Hitchance == High || pred.Hitchance == Immobile && UnitManager.MyChampion.GetSpellBook().GetSpellClass(SpellSlot.E).IsSpellReady)
                            {
                                SpellCastProvider.CastSpell(CastSlot.E, enemie.Target.Position, 0.25F);
                            }*/
                            Orbwalker.AllowAttacking = true;
                        }
                    }
                }
            }
            return Task.FromResult(0);
        }
    }
}
