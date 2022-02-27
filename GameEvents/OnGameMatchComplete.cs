using Oasys.SDK;
using Oasys.SDK.Events;
using Oasys.SDK.Tools;


namespace Ok_Maw
{
    internal partial class _GameEvents
    {
        internal static Task OnGameMatchComplete()
        {
            if (UnitManager.MyChampion.ModelName == "KogMaw")
            {
                CoreEvents.OnCoreMainTick -= _CoreEvents.MainTick;
            }
            return Task.FromResult(0);
        }
    }
}
