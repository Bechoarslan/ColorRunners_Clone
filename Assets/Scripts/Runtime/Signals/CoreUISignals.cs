using Runtime.Extentions;
using Runtime.Enums;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreUISignals : MonoSingleton<CoreUISignals>
    {
        public UnityAction<UIPanelTypes, int> onOpenPanel = delegate { };
        public UnityAction<int> onClosePanel = delegate { };
        public UnityAction onCloseAllPanels = delegate { };
    }
}