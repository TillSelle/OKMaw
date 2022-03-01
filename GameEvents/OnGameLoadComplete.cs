using Oasys.SDK;
using Oasys.SDK.Events;
using Ok_Maw.Modules;
using Oasys.SDK.Menu;
using Oasys.Common.Menu.ItemComponents;
using Oasys.Common.Menu;

namespace Ok_Maw
{
    internal partial class _GameEvents
    {
        internal static Task OnGameLoadComplete()
        {
            //Oasys.SDK.Tools.Logger.Log($"{UnitManager.MyChampion.ModelName}");
            if (UnitManager.MyChampion.ModelName == "KogMaw")
            {

                // Adding Tabs for debugging
                int? DebugIndex = null;
                // If does not already exist create it!
                if (MenuManager.GetTab("OKDebug") == null)
                     DebugIndex = MenuManager.AddTab(new() { Title = "OKDebug" });
                var DebugTab = MenuManager.GetTab("OKDebug");
                var DebugItemIndex = DebugTab.AddItem(new Switch() { IsOn = false, TabName = "OKDebug", Title = "Debug KogMaw" });

                // Adding Tabs for basic Module Usage
                var tabindex = MenuManager.AddTab(new() { Title = "OKMaw - Settings" });
                var KillStealer = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = true, TabName = "OKMaw - Settings", Title = "KillStealer" });
                var KillStealerQ = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Kill steal with Q" });
                var KillStealerE = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Kill steal with E" });
                var KillStealerR = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Kill steal with R" });
                var KillStealerAA = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Kill steal with AA" });
                var KillStealerMode = MenuManager.GetTab(tabindex).AddItem(new ModeDisplay() { ModeNames = new() { "InCombo", "InRange" }, SelectedModeName = "InCombo", TabName = "OKMaw - Settings", Title = "Killsteal Mode" });
                var AutoCaster = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Autocaster" });
                var AutoCasterQ = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Auto cast Q" });
                var AutoCasterW = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Auto cast W" });
                var AutoCasterE = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Auto cast E" });
                var AutoCasterR = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Auto cast R" });
                var AutoCasterMode = MenuManager.GetTab(tabindex).AddItem(new ModeDisplay() { ModeNames = new() { "InCombo", "InRange" }, SelectedModeName = "InCombo", TabName = "OKMaw - Settings", Title = "Autocast Mode" });

                // Setting "Environmental" vars
                LoggerTabs.DebuggerTabIndex = DebugIndex;
                LoggerTabs.DebuggerTab = DebugTab;
                LoggerTabs.DebugItemIndex = DebugItemIndex;

                KillSteal.BasicKogMawTab = tabindex;
                KillSteal.KillStealer = KillStealer;
                KillSteal.KillStealerQ = KillStealerQ;
                KillSteal.KillStealerAA = KillStealerAA;
                KillSteal.KillStealerE = KillStealerE;
                KillSteal.KillStealerR = KillStealerR;
                KillSteal.KillStealMode = KillStealerMode;

                Autocast.AutoCaster = AutoCaster;
                Autocast.AutoCasterE = AutoCasterE;
                Autocast.AutoCasterQ = AutoCasterQ;
                Autocast.AutoCasterR = AutoCasterR;
                Autocast.AutoCasterW = AutoCasterW;
                Autocast.AutoCasterMode = AutoCasterMode;

                CoreEvents.OnCoreMainTick += _CoreEvents.MainTick;
                CoreEvents.OnCoreMainInputAsync += _CoreEvents.MainInput;
                CoreEvents.OnCoreMainInputRelease += _CoreEvents.MainInputRelease;
            }
            return Task.FromResult(0);
        }
    }
}
