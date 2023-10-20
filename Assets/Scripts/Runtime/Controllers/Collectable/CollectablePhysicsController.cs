
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Collectable
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        
        #region Private Variables

        private readonly string _collectable = "Collectable";
        private readonly string _gate = "Gate";
        private readonly string _collected = "Collected";
        private readonly string _miniGameArea = "MiniGameArea";
        private readonly string _colorArea = "ColorCheckArea";

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_collectable))
            {
                other.tag = _collected;
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