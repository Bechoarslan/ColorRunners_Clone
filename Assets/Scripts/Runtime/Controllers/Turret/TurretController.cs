using System;
using System.Collections.Generic;
using DG.Tweening;
using RootMotion.FinalIK;
using Sirenix.OdinInspector.Editor.TypeSearch;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace Runtime.Controllers.Turret
{
    public class TurretController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public bool isPlayerTargeted;
        public Transform turretTransform;
        #endregion

        #region Serialized Variables

        
        [SerializeField] private Part[] turretParts;
        [SerializeField] private List<ParticleSystem> particlePart;
        

        #endregion

        #region Private Variables

        [SerializeField] Transform _playerTransform;

        #endregion

        #endregion

        private void Start()
        {
            InvokeRepeating("MoveTurretHead",0.5f,1);
        }


        private void MoveTurretHead()
        {
                var xValue = Random.Range(-1.5f, 1.5f);
                var yValue = Random.Range(-0.5f, 0.5f);
                var zValue = Random.Range(0f, 2f);
                turretTransform.DOLocalMove(new Vector3(xValue, yValue, zValue),1f);
            
        }

        public void Shoot()
        {
            for (int i = 0; i < particlePart.Count; i++)
            {
                particlePart[i].Play();
            }
        }

        private void Update()
        {
            if (isPlayerTargeted)
            {
                foreach (var part in turretParts) part.AimAt(_playerTransform);
                return;
                
            }
            foreach(var part in turretParts) part.AimAt(turretTransform);
        }

        public void SetTarget(Transform target)
        {
            Debug.LogWarning("Executed ===> Set Target");
            _playerTransform = target;
        }
 

        [Serializable]
        public class Part
        {
            public Transform transform;
            private RotationLimit _rotationLimit;

            public void AimAt(Transform target)
            {
                transform.LookAt(new Vector3(target.position.x,target.position.y + 0.3f,target.position.z),transform.up);
                if (_rotationLimit == null)
                {
                    _rotationLimit = transform.GetComponent<RotationLimit>();
                    _rotationLimit.Disable();
                }

                _rotationLimit.Apply();
            }
         }
    }
}