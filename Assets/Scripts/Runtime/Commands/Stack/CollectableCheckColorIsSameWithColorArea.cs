using Runtime.Managers;
using UnityEngine;

namespace Runtime.Commands.Stack
{
    public class CollectableCheckColorIsSameWithColorArea
    {
        public bool Execute(GameObject collectableObj, Transform colorAreaObj)
        {
            var collectableColorType = collectableObj.GetComponent<CollectableManager>().SendColorType();
            var colorAreaColorType = colorAreaObj.GetComponentInParent<ColorAreaManager>().SendColorType();
            Debug.LogWarning("CollectableColorType: " + collectableColorType + " ColorAreaColorType:" + colorAreaColorType);

            return collectableColorType == colorAreaColorType;
        }
    }
}