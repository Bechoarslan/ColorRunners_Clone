
using Runtime.Enums.Collectable;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Collectable
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Private Variables

        private readonly string _collectable = "Collectable";
        private readonly string _gate = "Gate";

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                other.tag = "Collected";
                var parent = other.transform.parent;
                CollectableSignals.Instance.onCheckCollectablesColors?.Invoke(parent.gameObject,
                    transform.parent.gameObject);
                CollectableSignals.Instance.onCollectableInteractWithCollectable?.Invoke(parent.gameObject);

            }

            if (other.CompareTag(_gate))
            {
                CollectableSignals.Instance.onCollectableInteractWithGate?.Invoke(other.transform.parent.gameObject);
                
            }
        }
        
    }
}