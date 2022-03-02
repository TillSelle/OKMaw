using Oasys.SDK;
using Oasys.SDK.Events;


namespace Ok_Maw
{
    internal partial class _GameEvents
    {
        internal static Task OnGameMatchComplete()
        {
            if (UnitManager.MyChampion.ModelName == "KogMaw")
            {
                CoreEvents.OnCoreMainTick -= _CoreEvents.MainTick;
                CoreEvents.OnCoreMainInputAsync -= _CoreEvents.MainInput;
                CoreEvents.OnCoreMainInputRelease -= _CoreEvents.MainInputRelease;
                CoreEvents.OnCoreRender -= _CoreEvents.OnCoreRender;
            }
            return Task.FromResult(0);
        }
    }
}
