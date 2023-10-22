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
        [SerializeField] private Transform holder;
        
        private readonly string _collected = "Collected";
        private readonly string _player = "Player";
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_collected))
            {
                MiniGameSignals.Instance.onCollectableInteractWithCollectableArea.Invoke(other.transform.parent.gameObject,holder);
            }
           
            
        }

        private void OnTriggerExit(Collider other)
        {
            
            
            
        }
        
    }
}