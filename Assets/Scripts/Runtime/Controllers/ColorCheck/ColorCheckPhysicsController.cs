using System;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.ColorCheck
{
    public class ColorCheckPhysicsController : MonoBehaviour
    {
        #region Private Variables

        private readonly string _collected = "Collected";

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_collected))
            {
                MiniGameSignals.Instance.onCollectableInteractWithColorCheckArea?.Invoke(other.transform.parent.gameObject);
                MiniGameSignals.Instance.onCheckColorTypes?.Invoke(other.transform.parent.gameObject,transform.parent.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_collected))
            {
                MiniGameSignals.Instance.onCollectableExitFromColorCheckArea?.Invoke(other.transform.parent.gameObject);
            }
        }
    }
}