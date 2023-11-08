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
        private readonly string _environmentTag = "BuildArea";
        private int _timer;
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

            if (other.CompareTag(_environmentTag))
            {
                EnvironmentSignals.Instance.onPlayerInteractWithEnvironment?.Invoke(other.transform.parent.gameObject);
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
                CoreGameSignals.Instance.onSetPlayerScale?.Invoke();
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            }

            if (other.CompareTag(_environmentTag))
            {
                EnvironmentSignals.Instance.onPlayerExitInteractWithEnvironment?.Invoke();
            }
            
            
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(_environmentTag))
            {
                if (_timer >= 20)
                {
                    EnvironmentSignals.Instance.onPlayerStayInteractWithEnvironment?.Invoke();
                }
                else
                {
                    _timer++;
                }
                
                
            }
        }
    }
}