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
        // Checks if the enemy is in any way Killable. (Condition)? (whatever should happen when true) : (whatever should happen when false)
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

            // Should not run every Tick since this could impact the performance too much so we take the LastTick var that gets updated every 10th tick with the current Tick var so we can check if it has executed 10 ticks already
            if (Ticks - LastTick >= 100)
            {
                // "Clear" the list so in the foreach inside of Killstealer() we don't loop through the same char twice (I assign a new one so in the case that this var is accessed at "the same time" in Killstealer and MainTick the lists values inside of Killstealer aren't getting overwritten/deleted)
                IsKillable = new();

                LastTick = Ticks;
                
                foreach (Hero champ in enemies)
                {
                    // IsKillAble Check the rest should be self explanatory 
                    bool isKillableQ = false;
                    bool isKillableW = false;
                    bool isKillableE = false;
                    bool isKillableR = false;
                    bool isKillableAA = false;
                    if (champ.IsKillable(SpellSlot.Q))
                    {
                        isKillableQ = true;
                    }
                    if (champ.IsKillable(SpellSlot.W))
                    {
                        isKillableW = true;
                    }
                    if (champ.IsKillable(SpellSlot.E))
                    {
                        isKillableE = true;
                    }
                    if (champ.IsKillable(SpellSlot.R))
                    {
                        isKillableR = true;
                    }
                    if (champ.IsKillable(SpellSlot.BasicAttack))
                    {
                        isKillableAA = true;
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

                // Add killstealer as OnCoreMainTick subscriber when the killstealer isnt on and its turned on inside of the menu
                if (MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(KillSteal.KillStealer).IsOn && !KSIsOn)
                {
                    // Check if the Killstealer is actually set to "InRange"
                    if (MenuManager.GetTab("OKMaw - Settings").ModeSelected(KillSteal.KillStealMode, "InRange"))
                    {
                        CoreEvents.OnCoreMainTick += KillSteal.Killstealer;
                        KSIsOn = true;
                    }
                }

                // Add Autocaster as OnCoreMainTick subscriber when the autocaster isnt on and its turned on inside of the menu
                if (MenuManager.GetTab("OKMaw - Settings").SwitchItemOn(Autocast.AutoCaster) && !ACIsOn)
                {
                    // Check if the Autocaster is actually set to "InRange"
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
