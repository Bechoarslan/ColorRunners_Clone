using System;
using System.Collections.Generic;
using Runtime.Commands.Collectable;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
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
        private Transform playerManager;
        
        

        #endregion

        #region Private Variables

        [ShowInInspector] private List<GameObject> _collectableList = new List<GameObject>();
       
        [ShowInInspector] private StackData _stackData;
        private readonly string _stackDataPath = "Data/CD_Stack";
        private CollectableAdderCommand _collectableAdderCommand;
        private CollectableLerpMovementCommand _collectableLerpMovementCommand;

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
            
        }

        private void OnCollectableInteractWithCollectable(GameObject collectableObject)
        {
            _collectableAdderCommand.Execute(collectableGameObject);
        }


        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CollectableSignals.Instance.onCollectableInteractWithCollectable -= OnCollectableInteractWithCollectable;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void OnPlay()
        {
            _collectableAdderCommand.Execute(collectableGameObject);
            GetPlayerTransform();
        }
        
        private void GetPlayerTransform()
        { 
            if(!playerManager) playerManager = FindObjectOfType<PlayerManager>().transform;
        }
        
        private void Update()
        {
            if (!playerManager) return;
            _collectableLerpMovementCommand.Execute(ref playerManager);
        }

        private void OnReset()
        {
            throw new NotImplementedException();
        }
    }
}