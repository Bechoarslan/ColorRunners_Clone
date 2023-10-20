using System;
using System.Collections.Generic;
using Runtime.Enums.Collectable;
using Runtime.Enums.MiniGame;
using Runtime.Managers;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Controllers.ColorCheck
{
    public class ColorAreaPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

         

        #endregion

        #region Serialized Variables
        [SerializeField] private Transform colorCheckHolder;
        #endregion
        
        #region Private Variables

        private readonly string _collected = "Collected";
        private readonly string _player = "Player";
        [ShowInInspector]private MiniGameType _miniGameType;

        #endregion
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_player))
            {
                MiniGameSignals.Instance.onColorAreaInteractWithPlayerManager?.Invoke();
            }

            if (other.CompareTag(_collected))
            {
                var parent = other.transform.parent;
                var collectableManager = parent.gameObject.GetComponent<CollectableManager>().SendColorType();

                switch (_miniGameType)
                {
                    case MiniGameType.Drone:
                        var collision = other.gameObject.GetComponent<CapsuleCollider>();
                       MiniGameSignals.Instance.onCheckColorCollectableForColorArea?.Invoke(collectableManager,transform.parent.gameObject,parent.gameObject);
                        MiniGameSignals.Instance.onDroneColorAreaSendCollectableToHolder?.Invoke(parent.gameObject, colorCheckHolder);
                        collision.enabled = false;
                        
                        
                        
                        break;
                    case MiniGameType.Turret:
                        
                        CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(parent.gameObject,CollectableAnimationStates.HideWalk);
                        MiniGameSignals.Instance.onCheckColorCollectableForColorArea?.Invoke(collectableManager,transform.parent.gameObject,parent.gameObject);
                        
                        break;
                        
                        
                }
                
              
                
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_collected))
            {
                CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(other.transform.parent.gameObject,CollectableAnimationStates.Run);
            }
        }


        public void GetMiniGameType(MiniGameType miniGameType)
        {
            _miniGameType = miniGameType;
        }
    }
}