using System;
using Runtime.Enums.MiniGame;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private readonly string _minigameAreaTag = "MiniGameArea";
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_minigameAreaTag))
            {
                MiniGameSignals.Instance.onPlayerInteractWithMiniGameArea?.Invoke(other.transform.parent.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_minigameAreaTag))
            {
                MiniGameSignals.Instance.onPlayerExitInteractWithMiniGameArea?.Invoke();
            }
        }
    }
}