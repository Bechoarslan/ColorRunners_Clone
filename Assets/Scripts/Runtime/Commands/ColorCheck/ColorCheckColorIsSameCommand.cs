using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.ColorCheck
{
    public class ColorCheckColorIsSameCommand
    {
        public void Execute(GameObject collectableObject, GameObject colorAreaManager)
        {
            var collectable = collectableObject.GetComponent<CollectableManager>().SendColorType();
            var manager = colorAreaManager.GetComponent<ColorAreaManager>().SendColorType();
            var miniGameManager = colorAreaManager.transform.parent.gameObject; 

            if (collectable == manager) return;
            
            MiniGameSignals.Instance.onCollectableIsNotSameColorWithColorArea?.Invoke(collectableObject,miniGameManager);
        }
    }
}