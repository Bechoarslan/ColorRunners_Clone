using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.ColorCheckArea
{
    public class ColorCheckAreaCheckColor
    {
        
        public void Execute(GameObject colorAreaGameObject)
        {
            var colorAreaManager = colorAreaGameObject.GetComponent<ColorCheckAreaManager>();
            if (colorAreaManager.SendColorType() == PlayerSignals.Instance.onGetPlayerColor?.Invoke())
            {
                CollectableSignals.Instance.onIsPlayerColorSame?.Invoke(true);
                Debug.LogWarning("Execute ==> Color are Same");
            }
            else
            {
                CollectableSignals.Instance.onIsPlayerColorSame?.Invoke(false);
                Debug.LogWarning("Execute ===> Color  are not same ");
            }
            

        }
    }
}