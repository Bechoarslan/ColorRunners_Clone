using System;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                other.tag = "Collected";
                StackSignals.Instance.onInteractionWithCollectable?.Invoke(other.transform.parent.gameObject);
            }

            if  (other.CompareTag("Gate"))
            {
               CoreGameSignals.Instance.onInteractionWithGate?.Invoke(other.transform.parent.gameObject);
                
            }

      

           
            
        }
    }
}