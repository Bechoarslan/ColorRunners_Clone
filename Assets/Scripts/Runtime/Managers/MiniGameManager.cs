using System;
using Runtime.Enums.MiniGame;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MiniGameType miniGameType;

        #endregion
        

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onCollectableInteractWithMiniGameArea += OnCollectableInteractWithMiniGameArea;
            MiniGameSignals.Instance.onCollectableIsNotSameColorWithColorArea += OnCollectableIsNotSameColorWithColorArea;
            
        }
        

        private void OnCollectableIsNotSameColorWithColorArea(GameObject collectableObject, GameObject miniGameManager)
        {
            if (gameObject.GetInstanceID() != miniGameManager.GetInstanceID()) return;
            if (miniGameType == MiniGameType.Drone)
            {
                CollectableSignals.Instance.onDestroyCollectableObject?.Invoke(collectableObject);
            }
            else
            {
                Debug.LogWarning("Executed ===> Turret Targeted Player");
            }
                
            
            
            
        }

        private void OnCollectableInteractWithMiniGameArea(GameObject miniGameObject)
        {
            if (miniGameObject.GetInstanceID() != gameObject.GetInstanceID()) return;
                MiniGameSignals.Instance.onSendMiniGameAreaType?.Invoke(miniGameType);
        
        }

        private void UnSubscribeEvents()
        {
            CollectableSignals.Instance.onCollectableInteractWithMiniGameArea -= OnCollectableInteractWithMiniGameArea;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}