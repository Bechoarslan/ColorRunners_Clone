using System;
using System.Collections.Generic;
using Runtime.Commands.Collectable;
using Runtime.Commands.Stack;
using Runtime.Controllers.Collectable;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Collectable;
using Runtime.Enums.MiniGame;
using Runtime.Enums.Pool;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject collectableGameObject;
        private Transform playerManagerTransform;
        [SerializeField] private int collectableStackNumber;
        
        

        #endregion

        #region Private Variables

        [ShowInInspector] private List<GameObject> _collectableList = new List<GameObject>();
        private MiniGameType _miniGameType;
       
        [ShowInInspector] private StackData _stackData;
        private readonly string _stackDataPath = "Data/CD_Stack";
        private CollectableAdderCommand _collectableAdderCommand;
        private CollectableLerpMovementCommand _collectableLerpMovementCommand;
        private CollectableSetAnimationStateCommand _collectableSetAnimationStateCommand;
        private CollectableSetAnimationOnPlayCommand _collectableSetAnimationOnPlayCommand;
        [ShowInInspector] private bool _isCollectableColorSame;

        #endregion

        #endregion

        private void Awake()
        {
            
            _stackData = GetStackData();
            Init();
        }
        
        

        private void Init()
        {
            _collectableAdderCommand = new CollectableAdderCommand(ref _stackData, ref _collectableList,this);
            _collectableLerpMovementCommand = new CollectableLerpMovementCommand(ref _stackData, ref _collectableList);
            _collectableSetAnimationStateCommand = new CollectableSetAnimationStateCommand();
            _collectableSetAnimationOnPlayCommand = new CollectableSetAnimationOnPlayCommand(ref _collectableSetAnimationStateCommand);
            
        }

        private StackData GetStackData() => Resources.Load<CD_Stack>(_stackDataPath).Data;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CollectableSignals.Instance.onCollectableInteractWithCollectable += OnCollectableInteractWithCollectable;
            CollectableSignals.Instance.onSendIsSameColorCondition += OnSendIsSameColorCondition;
            CollectableSignals.Instance.onDestroyCollectableObject += OnDestroyCollectableObject;
            MiniGameSignals.Instance.onSendMiniGameAreaType += OnSendMiniGameAreaType;
            MiniGameSignals.Instance.onCollectableInteractWithColorCheckArea += OnCollectableInteractWithColorCheckArea;
            MiniGameSignals.Instance.onCollectableExitFromColorCheckArea += OnCollectableExitFromColorCheckArea;
           
            
        }

        private void OnDestroyCollectableObject(GameObject collectableObject)
        {
            Debug.LogWarning("Executed ====> Destroyed Collectable Object " + collectableObject.gameObject.name);
            Destroy(collectableObject);
        }


        private void OnSendIsSameColorCondition(bool condition) => _isCollectableColorSame = condition;
        
        private void OnCollectableInteractWithCollectable(GameObject collectableObject)
        {
            if (_isCollectableColorSame)
            {
                _collectableAdderCommand.Execute(collectableObject);
                _collectableSetAnimationStateCommand.Execute(collectableObject,CollectableAnimationStates.Run);

            }
            
        }
        private void OnSendMiniGameAreaType(MiniGameType miniGameType) => _miniGameType = miniGameType;
        private void OnCollectableInteractWithColorCheckArea(GameObject collectableObject)
        {
            if (_miniGameType == MiniGameType.Turret)
            {
                
                _collectableSetAnimationStateCommand.Execute(collectableObject,CollectableAnimationStates.HideWalk);
            }
            else
            {
               
            }
        }
        private void OnCollectableExitFromColorCheckArea(GameObject collectableObject)
        {
            _collectableSetAnimationStateCommand.Execute(collectableObject,CollectableAnimationStates.Run);
        }

        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CollectableSignals.Instance.onCollectableInteractWithCollectable -= OnCollectableInteractWithCollectable;
            CollectableSignals.Instance.onSendIsSameColorCondition -= OnSendIsSameColorCondition;
            MiniGameSignals.Instance.onSendMiniGameAreaType -= OnSendMiniGameAreaType;
            MiniGameSignals.Instance.onCollectableInteractWithColorCheckArea -= OnCollectableInteractWithColorCheckArea;
            MiniGameSignals.Instance.onCollectableExitFromColorCheckArea -= OnCollectableExitFromColorCheckArea;
       
        }
        
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            InitiliazedObject();
        }
        
        private void InitiliazedObject()
        {
            for (int i = 0; i < 6; i++)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Collectable);
                obj.SetActive(true);
                _collectableAdderCommand.Execute(obj);
                
            }
        }

        private void OnPlay()
        {
            GetPlayerTransform();
            _collectableSetAnimationOnPlayCommand.Execute(_collectableList);


        }
        
        private void GetPlayerTransform()
        { 
            if(!playerManagerTransform) playerManagerTransform = FindObjectOfType<PlayerManager>().transform;
        }
        
        private void Update()
        {
            if (!playerManagerTransform) return;
            _collectableLerpMovementCommand.Execute(ref playerManagerTransform);
        }

        private void OnReset()
        {
            throw new NotImplementedException();
        }
    }
}