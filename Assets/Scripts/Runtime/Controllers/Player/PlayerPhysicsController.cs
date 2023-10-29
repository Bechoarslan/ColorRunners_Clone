using System;
using Runtime.Enums.MiniGame;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private readonly string _minigameAreaTag = "MiniGameArea";
        private readonly string _endAreaTag = "EndArea";
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_minigameAreaTag))
            {
                MiniGameSignals.Instance.onPlayerInteractWithMiniGameArea?.Invoke(other.transform.parent.gameObject);
                
            }

            if (other.CompareTag(_endAreaTag))
            {
                CoreGameSignals.Instance.onPlayerInteractWithEndArea?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_minigameAreaTag))
            {
                MiniGameSignals.Instance.onPlayerExitInteractWithMiniGameArea?.Invoke();
                
            }

            if (other.CompareTag(_endAreaTag))
            {
                CoreGameSignals.Instance.onPlayerExitInteractWithEndArea?.Invoke();
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
}