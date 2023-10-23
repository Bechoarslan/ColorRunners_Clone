using System;
using System.Collections.Generic;
using DG.Tweening;
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

        [SerializeField] private int collectableStackNumber;

        #endregion

        #region Private Variables

        [ShowInInspector] private MiniGameType _miniGameType;
        [ShowInInspector] private List<GameObject> _collectableList = new List<GameObject>();
        [ShowInInspector] private StackData _stackData;
        private readonly string _stackDataPath = "Data/CD_Stack";
        private Transform _playerManagerTransform;
        private CollectableAdderCommand _collectableAdderCommand;
        private CollectableLerpMovementCommand _collectableLerpMovementCommand;
        private CollectableSetAnimationOnPlayCommand _collectableSetAnimationOnPlayCommand;
        private CollectableChangeParentCommand _collectableChangeParentCommand;
        private CollectableMoveToColorAreaHolderCommand _collectableMoveToColorAreaHolderCommand;
        private CollectableSetVisibleUnVisibleCollectableCommand _collectableSetVisibleUnVisibleCollectableCommand;
        private CollectableCheckColorIsSameWithColorArea _collectableCheckColorIsSameWithColorArea;
        private CollectableSetColliderEnable _collectableSetColliderEnable;
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
            _collectableAdderCommand = new CollectableAdderCommand(ref _stackData, ref _collectableList, this);
            _collectableLerpMovementCommand = new CollectableLerpMovementCommand(ref _stackData, ref _collectableList);
            _collectableSetAnimationOnPlayCommand = new CollectableSetAnimationOnPlayCommand();
            _collectableChangeParentCommand = new CollectableChangeParentCommand(ref _collectableList, this.transform);
            _collectableMoveToColorAreaHolderCommand = new CollectableMoveToColorAreaHolderCommand(ref _collectableList); 
            _collectableSetVisibleUnVisibleCollectableCommand = new CollectableSetVisibleUnVisibleCollectableCommand(ref _collectableList, ref _stackData);
            _collectableCheckColorIsSameWithColorArea = new CollectableCheckColorIsSameWithColorArea();
            _collectableSetColliderEnable = new CollectableSetColliderEnable(ref _collectableList);



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
            CollectableSignals.Instance.onSetUnVisibleCollectableToVisible += _collectableSetVisibleUnVisibleCollectableCommand.Execute;
            MiniGameSignals.Instance.onMiniGameAreaSendToMiniGameTypeToListeners += OnMiniGameAreaSendToMiniGameTypeToListeners;
            MiniGameSignals.Instance.onCollectableInteractWithCollectableArea +=
                OnCollectableInteractWithCollectableArea;
            MiniGameSignals.Instance.onCollectableExitInteractWithColorArea += OnCollectableExitInteractWithColorArea;
            MiniGameSignals.Instance.onPlayerExitInteractWithMiniGameArea += OnPlayerExitInteractWithMiniGameArea;
            MiniGameSignals.Instance.onCheckCollectableListIsEmpty += OnCheckIsNull;
            
        }

        private void OnCheckIsNull()
        {
            if (_collectableList.Count <= 0)
            {
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
            }
        }


        private void OnPlayerExitInteractWithMiniGameArea()
        {
            if (_miniGameType != MiniGameType.Turret) return;
            for (int i = 0; i < 10; i++)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Collectable);
                obj.SetActive(true);
                _collectableAdderCommand.Execute(obj);
            }
        }

        private void OnCollectableExitInteractWithColorArea(GameObject collectableObj)
        {
            CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(collectableObj,CollectableAnimationStates.Run);
        }

        private void OnMiniGameAreaSendToMiniGameTypeToListeners(MiniGameType miniGameType)
        {
            _miniGameType = miniGameType;
            if (_miniGameType != MiniGameType.Turret) return;
                _collectableSetColliderEnable.Execute();
            
            
        }  
           
       

        private void OnCollectableInteractWithCollectableArea(GameObject collectableObj, Transform colorAreaObj)
        {
            switch (_miniGameType)
            {
                case MiniGameType.Drone:
                   var isColorsSame = _collectableCheckColorIsSameWithColorArea.Execute(collectableObj, colorAreaObj);
                    _collectableMoveToColorAreaHolderCommand.Execute(collectableObj,colorAreaObj);
                   _collectableChangeParentCommand.Execute(collectableObj,colorAreaObj,isColorsSame);
                    break;
                
                case MiniGameType.Turret:
                    CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(collectableObj,CollectableAnimationStates.HideWalk);
                    var isColorSame = _collectableCheckColorIsSameWithColorArea.Execute(collectableObj, colorAreaObj);
                    MiniGameSignals.Instance.onTurretMiniGamePlay?.Invoke(_collectableList,isColorSame);
                    break;
                
            }
        }
        

        private void OnSendIsSameColorCondition(bool condition) => _isCollectableColorSame = condition;

        private void OnCollectableInteractWithCollectable(GameObject collectableObject)
        {
            if (_isCollectableColorSame)
            {
                _collectableAdderCommand.Execute(collectableObject);
                CollectableSignals.Instance.onSetCollectableAnimation?.Invoke(collectableObject,
                    CollectableAnimationStates.Run);
            }
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CollectableSignals.Instance.onCollectableInteractWithCollectable -= OnCollectableInteractWithCollectable;
            CollectableSignals.Instance.onSendIsSameColorCondition -= OnSendIsSameColorCondition;
            CollectableSignals.Instance.onSetUnVisibleCollectableToVisible -= _collectableSetVisibleUnVisibleCollectableCommand.Execute;
            MiniGameSignals.Instance.onMiniGameAreaSendToMiniGameTypeToListeners -= OnMiniGameAreaSendToMiniGameTypeToListeners;
            MiniGameSignals.Instance.onCollectableInteractWithCollectableArea -=
                OnCollectableInteractWithCollectableArea;
            MiniGameSignals.Instance.onCollectableExitInteractWithColorArea -= OnCollectableExitInteractWithColorArea;
            MiniGameSignals.Instance.onPlayerExitInteractWithMiniGameArea -= OnPlayerExitInteractWithMiniGameArea;
            MiniGameSignals.Instance.onCheckCollectableListIsEmpty -= OnCheckIsNull;
           
           



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
            for (int i = 0; i < 10; i++)
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
            if (!_playerManagerTransform) _playerManagerTransform = FindObjectOfType<PlayerManager>().transform;
        }

        private void Update()
        {
            if (!_playerManagerTransform) return;
            _collectableLerpMovementCommand.Execute(ref _playerManagerTransform);
        }

        private void OnReset()
        {
            throw new NotImplementedException();
        }
    }
}