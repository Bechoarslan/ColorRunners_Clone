using System;
using System.Threading.Tasks;
using Runtime.Commands.Pool;
using Runtime.Controllers.Collectable;
using Runtime.Data.UnityObject;
using Runtime.Enums.Pool;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;
        [SerializeField] private Transform poolManagerG;
        #endregion

        #region Private Variables

        private CD_Pool _poolData;
        private GameObject _emptyGameObject;
        private PoolGenerateCommand _poolGenerateCommand;
        private RestartPoolCommand _restartPoolCommand;
        
        private readonly string _poolDataPath = "Data/CD_Pool";

        #endregion

        #endregion

        private void Awake()
        {
            _poolData = GetPoolData();
            Init();
            StartPool();
        }

       

        private void StartPool()
        {
            _poolGenerateCommand.Execute();
        }

        private void Init()
        {
            _poolGenerateCommand = new PoolGenerateCommand(ref _poolData, ref poolManagerG, ref _emptyGameObject);
            _restartPoolCommand = new RestartPoolCommand(ref _poolData, ref poolManagerG, ref levelHolder);
        }

        private CD_Pool GetPoolData() => Resources.Load<CD_Pool>(_poolDataPath);

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            PoolSignals.Instance.onSendPool += OnSendPool;
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
        }

        private GameObject OnGetPoolObject(PoolType poolType)
        {
            var parent = transform.GetChild((int)poolType);
            var obj = parent.childCount !=0 ? parent.transform.GetChild(0).gameObject : Instantiate(_poolData.PoolList[(int)poolType].Pref,Vector3.zero,Quaternion.identity,parent);
            return obj;

        }

        private void OnSendPool(GameObject CollectableObject, PoolType poolType)
        {
            
        }


        private async void RestartPool()
        {
            await Task.Delay(500);
            _restartPoolCommand.Execute();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            PoolSignals.Instance.onSendPool -= OnSendPool;
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void OnReset()
        {
            RestartPool();
        }
    }
}