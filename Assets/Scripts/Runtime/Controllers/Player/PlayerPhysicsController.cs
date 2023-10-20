using System;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MiniGameArea"))
            {
                MiniGameSignals.Instance.onMiniGameAreaInteractWithCollectable?.Invoke(other.transform.parent.gameObject);
            }
        }
    }
}