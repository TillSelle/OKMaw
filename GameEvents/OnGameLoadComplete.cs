using Oasys.SDK;
using Oasys.SDK.Events;
using Oasys.SDK.Tools;
using Oasys.SDK.Menu;
using Oasys.Common.Menu.ItemComponents;

namespace Ok_Maw
{
    internal partial class _GameEvents
    {
        internal static Task OnGameLoadComplete()
        {
            Logger.Log($"{UnitManager.MyChampion.ModelName}");
            if (UnitManager.MyChampion.ModelName == "KogMaw")
            {
                var tabindex = MenuManager.AddTab(new() { Title = "OKMaw - Settings" });
                var KillStealer = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = true, TabName = "OKMaw - Settings", Title = "KillStealer" });
                var KillStealerQ = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Kill steal with Q" });
                var KillStealerE = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Kill steal with E" });
                var KillStealerR = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Kill steal with R" });
                var KillStealerAA = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Kill steal with AA" });
                var KillStealOn = MenuManager.GetTab(tabindex).AddItem(new ModeDisplay() { ModeNames = new() { "InCombo", "InRange" }, SelectedModeName = "InRange", TabName = "OKMaw - Settings", Title = "Killsteal Mode" });
                var AutoCaster = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Autocaster" });
                var AutoCasterQ = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Auto cast Q" });
                var AutoCasterQMode = MenuManager.GetTab(tabindex).AddItem(new ModeDisplay() { TabName = "OKMaw - Settings", Title = "Q Cast Mode", SelectedModeName = "InCombo", ModeNames = new() { "InCombo", "InRange" } });
                var AutoCasterQPred = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = true, TabName = "OKMaw - Settings", Title = "Q Use Prediction" });
                var AutoCasterW = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Auto cast W" });
                var AutoCasterWMode = MenuManager.GetTab(tabindex).AddItem(new ModeDisplay() { TabName = "OKMaw - Settings", Title = "W Cast Mode", SelectedModeName = "InCombo", ModeNames = new() { "InCombo","WhenReady" } });
                var AutoCasterE = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Auto cast E" });
                var AutoCasterEMode = MenuManager.GetTab(tabindex).AddItem(new ModeDisplay() { TabName = "OKMaw - Settings", Title = "E Cast Mode", SelectedModeName = "InCombo", ModeNames = new() { "InCombo", "InRange" } });
                var AutoCasterEPred = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = true, TabName = "OKMaw - Settings", Title = "E Use Prediction" });
                var AutoCasterR = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = false, TabName = "OKMaw - Settings", Title = "Auto cast R" });
                var AutoCasterRMode = MenuManager.GetTab(tabindex).AddItem(new ModeDisplay() { TabName = "OKMaw - Settings", Title = "R Cast Mode", SelectedModeName = "InCombo", ModeNames = new() { "InCombo", "InRange" } });
                var AutoCasterRPred = MenuManager.GetTab(tabindex).AddItem(new Switch() { IsOn = true, TabName = "OKMaw - Settings", Title = "R Use Prediction" });
                Modules.KillSteal.KillStealer = KillStealer;
                Modules.KillSteal.KillStealerQ = KillStealerQ;
                Modules.KillSteal.KillStealerAA = KillStealerAA;
                Modules.KillSteal.KillStealerE = KillStealerE;
                Modules.KillSteal.KillStealerR = KillStealerR;
                Modules.KillSteal.KillStealOn = KillStealOn;
                Modules.KillSteal.AutoCaster = AutoCaster;
                Modules.KillSteal.AutoCasterE = AutoCasterE;
                Modules.KillSteal.AutoCasterEMode = AutoCasterEMode;
                Modules.KillSteal.AutoCasterEPred = AutoCasterEPred;
                Modules.KillSteal.AutoCasterQ = AutoCasterQ;
                Modules.KillSteal.AutoCasterQMode = AutoCasterQMode;
                Modules.KillSteal.AutoCasterQPred = AutoCasterQPred;
                Modules.KillSteal.AutoCasterR = AutoCasterR;
                Modules.KillSteal.AutoCasterRMode = AutoCasterRMode;
                Modules.KillSteal.AutoCasterRPred = AutoCasterRPred;
                Modules.KillSteal.AutoCasterW = AutoCasterW;
                Modules.KillSteal.AutoCasterWMode = AutoCasterWMode;
                CoreEvents.OnCoreMainTick += _CoreEvents.MainTick;
                Logger.Log("You are playing KogMaw!");
            }
            return Task.FromResult(0);
        }
    }
}
