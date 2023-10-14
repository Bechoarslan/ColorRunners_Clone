using Runtime.Enums.Color;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Collectable
{
    public class CollectableCheckColorCommand
    {
        public void Execute(GameObject collectableObj, GameObject collectableManager)
        {
            var colMan = collectableManager.GetComponent<CollectableManager>().SendColorType();
            var colMan2 = collectableObj.GetComponent<CollectableManager>().SendColorType();

            CollectableSignals.Instance.onSendIsSameColorCondition?.Invoke(colMan == colMan2);
        }
    }
}