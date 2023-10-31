using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Signals;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Managers
{
    public class EnvironmentManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        [SerializeField] private MeshRenderer[] _objMeshRenderer;
        [ShowInInspector] private float _rateTime;
        [SerializeField] private float _interval = 0.1f;
        private static readonly int Saturation = Shader.PropertyToID("_Saturation");

        #endregion

        #endregion
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EnvironmentSignals.Instance.onPlayerStayInteractWithEnvironment += OnPlayerStayInteractWithEnvironment;
            EnvironmentSignals.Instance.onPlayerInteractWithEnvironment += OnPlayerInteractWithEnvironment;
            EnvironmentSignals.Instance.onPlayerExitInteractWithEnvironment += OnPlayerExitInteractWithEnvironment;
        }

        private void OnPlayerExitInteractWithEnvironment()
        {
            _objMeshRenderer = null;
        }

        private void OnPlayerInteractWithEnvironment(GameObject buildingObj)
        {
            _objMeshRenderer = buildingObj.GetComponentsInChildren<MeshRenderer>();
            
        }

        private void OnPlayerStayInteractWithEnvironment(GameObject buildingObj)
        {


            for (var i = 0; i < _objMeshRenderer.Length - 1; i++)
            {
                if (!(Time.time - _rateTime >= _interval)) continue;
                var score = CoreGameSignals.Instance.onSendCollectableScore?.Invoke();
                if (score <= 0) return;
                var satFloat = _objMeshRenderer[i].material.GetFloat(Saturation);

                if (satFloat >= 1)
                {
                    continue;
                }
                
                EnvironmentSignals.Instance.onPlayerPaintEnvironment?.Invoke();
                _objMeshRenderer[i].material.DOFloat(satFloat + 0.10f, "_Saturation", 0);
                _rateTime = Time.time;

            }



        }

        private void UnSubscribeEvents()
        {
            EnvironmentSignals.Instance.onPlayerStayInteractWithEnvironment -= OnPlayerStayInteractWithEnvironment;
            EnvironmentSignals.Instance.onPlayerInteractWithEnvironment -= OnPlayerInteractWithEnvironment;
            EnvironmentSignals.Instance.onPlayerExitInteractWithEnvironment -= OnPlayerExitInteractWithEnvironment;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}