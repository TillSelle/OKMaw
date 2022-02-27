using Oasys.SDK;
using Oasys.SDK.Events;

namespace Ok_Maw
{
    public class EntryPoint
    {
        [OasysModuleEntryPoint]
        public static void Main()
        {
            GameEvents.OnGameLoadComplete += _GameEvents.OnGameLoadComplete;
            GameEvents.OnGameMatchComplete += _GameEvents.OnGameMatchComplete;
        }

    }
}