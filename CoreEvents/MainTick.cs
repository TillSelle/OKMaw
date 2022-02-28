using Oasys.Common.GameObject.Clients;
using Oasys.Common.GameObject.ObjectClass;
using Oasys.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ok_Maw.Modules;
using Oasys.Common.Enums.GameEnums;
using Oasys.SDK.Rendering;
using Oasys.SDK.Menu;
using Oasys.Common.Menu.ItemComponents;
using Oasys.SDK.Events;

namespace Ok_Maw
{

    internal class CanKillClass
    {
        internal AIHeroClient Attacker { get; set; }
        internal Hero Target { get; set; }
        internal bool IsKillableQ { get; set; }
        internal bool IsKillableW { get; set; }
        internal bool IsKillableE { get; set; }
        internal bool IsKillableR { get; set; }
        internal bool IsKillableAA { get; set; }
        internal bool IsKillable => (IsKillableQ == true)? true : (IsKillableW == true)? true : (IsKillableE == true)? true : (IsKillableR == true)? true : (IsKillableAA == true)? true : false;
    }

    internal static partial class _CoreEvents
    {
        internal static List<Hero> enemies => UnitManager.EnemyChampions;
        internal static List<CanKillClass> IsKillable = new();
        internal static bool KSIsOn;
        internal static bool ACIsOn;
        internal static int Ticks = 0;
        internal static int LastTick = 0;
        internal static Task MainTick()
        {
            Ticks += 10;

            if (MenuManager.GetTab("OKMaw - Settings").ModeSelected(KillSteal.KillStealMode, "InCombo")) {
                CoreEvents.OnCoreMainTick -= KillSteal.Killstealer;
                KSIsOn = false;
            }

            if (MenuManager.GetTab("OKMaw - Settings").ModeSelected(Autocast.AutoCasterMode, "InCombo")) {
                CoreEvents.OnCoreMainTick -= Autocast.Autocaster;
                ACIsOn = false;
            }

            // Checks if the KillStealer is activated in the tab and on
            if (!MenuManager.GetTab("OKMaw - Settings").SwitchItemOn(Modules.KillSteal.KillStealer) && KSIsOn) 
            {
                CoreEvents.OnCoreMainTick -= KillSteal.Killstealer;
                KSIsOn = false;
            }

            // Checks if the AutoCaster is activated in the tab and on
            if (!MenuManager.GetTab("OkMaw - Settings").SwitchItemOn(Modules.Autocast.AutoCaster) && ACIsOn)
            {
                CoreEvents.OnCoreMainTick -= Autocast.Autocaster;
                ACIsOn = false;
            }
            if (Ticks - LastTick >= 100)
            {
                IsKillable.Clear();
                LastTick = Ticks;
                foreach (Hero champ in enemies)
                {
                    bool isKillableQ = false;
                    bool isKillableW = false;
                    bool isKillableE = false;
                    bool isKillableR = false;
                    bool isKillableAA = false;
                    if (champ.IsKillable(SpellSlot.Q))
                    {
                        isKillableQ = true;
                        //Oasys.SDK.Tools.Logger.Log($"Q: {isKillableQ}");
                    }
                    if (champ.IsKillable(SpellSlot.W))
                    {
                        isKillableW = true;
                        //Oasys.SDK.Tools.Logger.Log($"W: {isKillableW}");
                    }
                    if (champ.IsKillable(SpellSlot.E))
                    {
                        isKillableE = true;
                        //Oasys.SDK.Tools.Logger.Log($"E: {isKillableE}");
                    }
                    if (champ.IsKillable(SpellSlot.R))
                    {
                        isKillableR = true;
                        //Oasys.SDK.Tools.Logger.Log($"R: {isKillableR}");
                    }
                    if (champ.IsKillable(SpellSlot.BasicAttack))
                    {
                        isKillableAA = true;
                        //Oasys.SDK.Tools.Logger.Log($"AA: {isKillableAA}");
                    }
                    IsKillable.Add(new()
                    {
                        IsKillableAA = isKillableAA,
                        IsKillableE = isKillableE,
                        IsKillableQ = isKillableQ,
                        IsKillableR = isKillableR,
                        IsKillableW = isKillableW,
                        Attacker = UnitManager.MyChampion,
                        Target = champ
                    });

                }
                if (MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(KillSteal.KillStealer).IsOn && !KSIsOn)
                {
                    if (MenuManager.GetTab("OKMaw - Settings").ModeSelected(KillSteal.KillStealMode, "InRange"))
                    {
                        CoreEvents.OnCoreMainTick += KillSteal.Killstealer;
                        KSIsOn = true;
                    }
                }
                if (MenuManager.GetTab("OKMaw - Settings").SwitchItemOn(Autocast.AutoCaster) && !ACIsOn)
                {
                    if (MenuManager.GetTab("OKMaw - Settings").ModeSelected(Autocast.AutoCasterMode, "InRange"))
                    {
                        CoreEvents.OnCoreMainTick += Autocast.Autocaster;
                        ACIsOn = true;
                    }
                }
            }
            return Task.FromResult(0);
        }
    }
}
