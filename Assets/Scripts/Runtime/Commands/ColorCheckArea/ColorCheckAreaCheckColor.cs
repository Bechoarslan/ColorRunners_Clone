using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.ColorCheckArea
{
    public class ColorCheckAreaCheckColor
    {
        
        public void Execute(GameObject colorAreaGameObject, Transform colorCheckHolder)
        {
            var colorAreaManager = colorAreaGameObject.GetComponent<ColorCheckAreaManager>();
            CollectableSignals.Instance.onIsPlayerColorSame?.Invoke(colorAreaManager.SendColorType() ==
                                                                    PlayerSignals.Instance.onGetPlayerColor?.Invoke());
            Debug.LogWarning(colorCheckHolder);
            ColorCheckSignals.Instance.onSendCheckAreaHolderTransform?.Invoke(colorAreaManager.SendHolderTransform());
        }

        
    }
}