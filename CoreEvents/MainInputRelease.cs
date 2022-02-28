﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasys.Common.Menu.ItemComponents;
using Oasys.SDK.Events;
using Oasys.SDK.Menu;
using Ok_Maw.Modules;

namespace Ok_Maw
{
    internal static partial class _CoreEvents
    {
        public static async Task MainInputRelease()
        {
            if (MenuManager.GetTab(KillSteal.BasicKogMawTab).GetItem<Switch>(KillSteal.KillStealer).IsOn)
            {
                if (MenuManager.GetTab(KillSteal.BasicKogMawTab).GetItem<ModeDisplay>(KillSteal.KillStealMode).SelectedModeName == "InCombo" && KSIsOn)
                {
                    CoreEvents.OnCoreMainTick -= KillSteal.Killstealer;
                    KSIsOn = false;
                }
            }

            if (MenuManager.GetTab(KillSteal.BasicKogMawTab).SwitchItemOn(Autocast.AutoCaster))
            {
                if (MenuManager.GetTab(KillSteal.BasicKogMawTab).ModeSelected(Autocast.AutoCasterMode, "InCombo") && ACIsOn)
                {
                    CoreEvents.OnCoreMainTick -= Autocast.Autocaster;
                    ACIsOn = false;
                }
            }
        }
    }
}
