
using Runtime.Signals;
using UnityEngine;


namespace Runtime.Managers
{
    public class GateManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Renderer renderer;
        [SerializeField] private Color32 gateColor;

        #endregion

        #endregion
        

        private void OnEnable()
        {
            SubscribeEvents();

        }

        private void SubscribeEvents()
        {
            GateSignals.Instance.onGetGateColor += OnGetGateColor;
            CoreGameSignals.Instance.onPlay += () => GateSignals.Instance.onSetGateColor?.Invoke(gateColor);
        }

        private Color32 OnGetGateColor()
        {
            return renderer.material.color;
        }

        private void UnSubscribeEvents()
        {
            GateSignals.Instance.onGetGateColor -= OnGetGateColor;
            CoreGameSignals.Instance.onPlay -= () => GateSignals.Instance.onSetGateColor?.Invoke(gateColor);
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}