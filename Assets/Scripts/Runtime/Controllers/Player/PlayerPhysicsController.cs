using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private readonly string _collectable = "Collectable";
        private readonly string _gate = "Gate";
        private readonly string _collected = "Collected";
        private readonly string _colorCheckArea = "ColorCheckArea";
        private readonly string _miniGameArea = "MiniGameArea";
        #endregion
        #endregion

        private void OnTriggerEnter(Collider other)
        {
          
            if (other.CompareTag(_collectable))
            { 
                other.tag = _collected;
                StackSignals.Instance.onInteractionWithCollectable?.Invoke(other.transform.parent.gameObject);
            }

            if (other.CompareTag(_gate))
            {
                CoreGameSignals.Instance.onPlayerInteractionWithGate?.Invoke(other.transform.parent.gameObject);
            }

            if (other.CompareTag(_colorCheckArea))
            {
                CoreGameSignals.Instance.onPlayerInteractionWithColorCheckArea?.Invoke(other.transform.parent.gameObject);
            }
        }
    }
}