using Oasys.Common.Enums.GameEnums;
using Oasys.Common.GameObject.Clients;
using Oasys.Common.Menu.ItemComponents;
using Oasys.SDK;
using Oasys.SDK.Menu;
using Ok_Maw.Modules.Spells;
using Oasys.Common.Extensions;
using Oasys.SDK.SpellCasting;
using Oasys.Common.Tools.Devices;
using Oasys.SDK.InputProviders;
using static Oasys.Common.Logic.LS.HitChance;

namespace Ok_Maw.Modules
{
    internal static class KillSteal
    {
        public static int Tick = 0;
        public static int LastAATick = 0;

        public static int BasicKogMawTab;
        public static int KillStealer;
        public static int KillStealDrawings;
        public static int KillStealerQ;
        public static int KillStealerE;
        public static int KillStealerR;
        public static int KillStealerAA;
        public static int KillStealMode;
        

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

        public static Task Killstealer()
        {
            
            Tick += 10;
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
                            KogMaw kog = new KogMaw();
                            if (Use.Me.Mana - 40 >= kog.CurrentManaCost && enemie.Target.IsVisible && enemie.Target.Position.IsOnScreen())
                            {
                                Orbwalker.AllowAttacking = false;

                                var pred = Prediction.MenuSelected.GetPrediction(Prediction.MenuSelected.PredictionType.Circle, enemie.Target, RRange[UnitManager.MyChampion.GetSpellBook().GetSpellClass(SpellSlot.R).Level], 240, 0, 600, false);
                                if (pred.HitChance == Prediction.MenuSelected.HitChance.High || pred.HitChance == Prediction.MenuSelected.HitChance.VeryHigh || pred.HitChance == Prediction.MenuSelected.HitChance.Immobile)
                                {
                                    SpellCastProvider.CastSpell(CastSlot.R, pred.CastPosition, 0.25F);
                                }
                                Orbwalker.AllowAttacking = true;
                            }
                        }
                        
                        if ((Tick - LastAATick) >= 800)
                        {
                            if (enemie.IsKillableAA || enemie.IsKillableW && enemie.Target.IsInRange(UnitManager.MyChampion.TrueAttackRange) && MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(KillStealerAA).IsOn)
                            {
                                if (Orbwalker.CanBasicAttack && enemie.Target.IsVisible && enemie.Target.Position.IsOnScreen())
                                {
                                    
                                    var MPos = GameEngine.ScreenMousePosition;
                                    
                                    //Oasys.SDK.Tools.Logger.Log($"{MPos.X}, {MPos.Y}");
                                    MouseProvider.SetCursor(enemie.Target.Position.ToW2S());
                                    //Oasys.SDK.Tools.Logger.Log($"{MPos.X}, {MPos.Y}");
                                    Mouse.LeftClick();
                                    MouseProvider.SetCursor(MPos);

                                    LastAATick = Tick;
                                }
                            }
                        }
                        if (enemie.IsKillableQ && MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(KillStealerQ).IsOn && UnitManager.MyChampion.GetSpellBook().GetSpellClass(SpellSlot.Q).IsSpellReady)
                        {
                            if (Use.Me.ManaLimit(160) && enemie.Target.IsVisible && enemie.Target.Position.IsOnScreen())
                            {
                                Orbwalker.AllowAttacking = false;

                                var pred = Prediction.MenuSelected.GetPrediction(Prediction.MenuSelected.PredictionType.Line, enemie.Target, 1360, 240, 0.25F, 1400, true);
                                if (pred.HitChance == Prediction.MenuSelected.HitChance.High || pred.HitChance == Prediction.MenuSelected.HitChance.VeryHigh || pred.HitChance == Prediction.MenuSelected.HitChance.Immobile && !pred.Collision)
                                {
                                    SpellCastProvider.CastSpell(CastSlot.Q, pred.CastPosition, 0.25F);
                                }

                                Orbwalker.AllowAttacking = true;
                            }
                        }
                        if (enemie.IsKillableE && enemie.Target.IsInRange(1360) && MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(KillStealerE).IsOn)
                        {
                            if (Use.Me.ManaLimit(200) && enemie.Target.IsVisible && enemie.Target.Position.IsOnScreen())
                            {
                                Orbwalker.AllowAttacking = false;

                                var pred = Prediction.MenuSelected.GetPrediction(Prediction.MenuSelected.PredictionType.Line, enemie.Target, 1360, 240, 0.25F, 1400, false);
                                if (pred.HitChance == Prediction.MenuSelected.HitChance.High || pred.HitChance == Prediction.MenuSelected.HitChance.VeryHigh || pred.HitChance == Prediction.MenuSelected.HitChance.Immobile)
                                {
                                    SpellCastProvider.CastSpell(CastSlot.E, pred.CastPosition, 0.25F);
                                }
                                Orbwalker.AllowAttacking = true;
                            }
                        }
                    }
                }
            }
            return Task.FromResult(0);
        }
    }
}
