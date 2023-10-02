using System;
using Runtime.Controllers.Collectable;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    
    public class CollectableManager : MonoBehaviour
    {
        [SerializeField] private CollectableAnimationController collectableAnimationController;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onCheckCollectableIsCurrent += OnCheckCollectableIsCurrent;
        }

        private void UnSubscribeEvents()
        {
            CollectableSignals.Instance.onCheckCollectableIsCurrent -= OnCheckCollectableIsCurrent;
        }

        private void OnCheckCollectableIsCurrent(GameObject collectableGameObject)
        {
            
            if(collectableGameObject != gameObject)
                return;
    
            collectableAnimationController.SetAnimationState(PlayerAnimationStates.Run);


        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}