using System;
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
       
        
     
        

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_collectable))
            {
                other.tag = _collected;
                StackSignals.Instance.onInteractionWithCollectable?.Invoke(other.transform.parent.gameObject);
            }

            if  (other.CompareTag(_gate))
            {
               CoreGameSignals.Instance.onInteractionWithGate?.Invoke(other.transform.parent.gameObject);
                
            }
            
        }

       
    }
    
    
}