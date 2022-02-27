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
using Oasys.SDK.Tools;
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

    internal class _CoreEvents
    {
        internal static List<Hero> enemies => UnitManager.EnemyChampions;
        internal static List<CanKillClass> IsKillable = new();
        internal static int Ticks = 0;
        internal static int LastTick = 0;
        internal static Task MainTick()
        {
            Ticks += 10;
            if (!MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(Modules.KillSteal.KillStealer).IsOn)
                CoreEvents.OnCoreMainTick -= KillSteal.KillstealerAndAutoCast;
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
                        Logger.Log($"Q: {isKillableQ}");
                    }
                    if (champ.IsKillable(SpellSlot.W))
                    {
                        isKillableW = true;
                        Logger.Log($"W: {isKillableW}");
                    }
                    if (champ.IsKillable(SpellSlot.E))
                    {
                        isKillableE = true;
                        Logger.Log($"E: {isKillableE}");
                    }
                    if (champ.IsKillable(SpellSlot.R))
                    {
                        isKillableR = true;
                        Logger.Log($"R: {isKillableR}");
                    }
                    if (champ.IsKillable(SpellSlot.BasicAttack))
                    {
                        isKillableAA = true;
                        Logger.Log($"AA: {isKillableAA}");
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
                if (MenuManager.GetTab("OKMaw - Settings").GetItem<Switch>(Modules.KillSteal.KillStealer).IsOn)
                {
                    CoreEvents.OnCoreMainTick += KillSteal.KillstealerAndAutoCast;
                }
            }
            return Task.FromResult(0);
        }
    }
}
