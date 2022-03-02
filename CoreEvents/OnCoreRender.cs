using Oasys.SDK.Menu;
using Oasys.SDK.Rendering;
using Oasys.Common.Extensions;
using Ok_Maw.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ok_Maw
{
    internal static partial class _CoreEvents
    {
        public static void OnCoreRender()
        {
            if (MenuManager.GetTab(KillSteal.BasicKogMawTab).SwitchItemOn(KillSteal.KillStealDrawings))
            {
                foreach (CanKillClass canKillObj in IsKillable)
                {
                    if (canKillObj.Target.IsVisible && canKillObj.Target.Position.IsOnScreen())
                    {
                        if (canKillObj.IsKillable)
                        {
                            string KillableThrough = $"{(canKillObj.IsKillableQ ? (canKillObj.IsKillableE || canKillObj.IsKillableR ? "Q," : "Q") : String.Empty)}{(canKillObj.IsKillableE ? (canKillObj.IsKillableR ? "E," : "E") : String.Empty)}{(canKillObj.IsKillableR ? "R" : String.Empty)}";
                            RenderFactory.DrawText($"Killable {KillableThrough}", 13, canKillObj.Target.Position.ToW2S(), SharpDX.Color.DodgerBlue, true);
                        }
                    }
                }
            }
        }
    }
}
