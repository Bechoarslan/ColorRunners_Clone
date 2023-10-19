using System;
using System.Collections.Generic;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.ColorCheck
{
    public class ColorAreaPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public List<GameObject> newListOfCollectables = new List<GameObject>();

        #endregion

        #region Serialized Variables
        [SerializeField] private Transform colorCheckHolder;
        #endregion
        
        #region Private Variables

        private readonly string _collected = "Collected";
        private readonly string _player = "Player";

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
                MiniGameSignals.Instance.onColorAreaInteractWithCollectable?.Invoke(other.transform.parent.gameObject, newListOfCollectables,gameObject.transform);
                MiniGameSignals.Instance.onColorAreaSendCollectableToHolder?.Invoke(other.transform.parent.gameObject, colorCheckHolder);
            }
            
        }
    }
}