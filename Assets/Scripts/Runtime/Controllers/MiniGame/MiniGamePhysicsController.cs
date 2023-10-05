using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.MiniGame
{
    public class MiniGamePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private string _collectable = "Collected";

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_collectable))
            {
                Debug.LogWarning("Collectable Entered Trigger");
                MiniGameSignals.Instance.onInteractionWithCollectable?.Invoke(other.transform.parent.gameObject);
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_collectable))
            {
                MiniGameSignals.Instance.onExitInteractionWithCollectable?.Invoke(other.transform.parent.gameObject);
            }
        }
    }
}