using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableCheckColorSameCommand
    {
        
        public void Execute(GameObject collectableGameObject)
        {
            var collectableManager = collectableGameObject.GetComponent<CollectableManager>();
            collectableManager.OnGetCollectableColor(PlayerSignals.Instance.onGetPlayerColor.Invoke());
        }
    }
}
