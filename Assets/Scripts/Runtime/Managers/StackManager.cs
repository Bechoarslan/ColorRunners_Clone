
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Commands.Collectable;
using Runtime.Commands.Stack;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Runtime.Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

        [SerializeField] private GameObject collectableObject;
        [SerializeField] private float waitForSecond;

        #endregion

        #region Private Variables

        [ShowInInspector]private List<GameObject> collectableList = new List<GameObject>();
        private StackData _stackData;
        private readonly string _stackDataPath = "Data/CD_Stack";

        private StackMoverCommand _stackMoverCommand;
        private AdderOnStackCommand _adderOnStackCommand;
        private StackScoreUpdaterCommand _stackScoreUpdaterCommand;
        private CollectableAnimationCommand _collectableAnimationCommand;
         private CollectableCheckColorSameCommand _collectableCheckColorSameCommand;
         private RemoverOnStackCommand _removerOnStackCommand;
         private StackAdderAnimationCommand _stackAdderAnimationCommand;
         
         [ShowInInspector] private int _stackScore;
         [ShowInInspector] private bool _isColorSame;
         [ShowInInspector] private short _collectableAddScore = 1;
        
        

        #endregion

        #endregion


        private void Awake()
        {
            _stackData = GetStackData();
            Init();
        }

        private void Init()
        {
            _stackMoverCommand = new StackMoverCommand(ref _stackData);
            _adderOnStackCommand = new AdderOnStackCommand(this, ref collectableList, ref _stackData);
            _stackScoreUpdaterCommand = new StackScoreUpdaterCommand(ref collectableList);
            _collectableAnimationCommand = new CollectableAnimationCommand();
            _collectableCheckColorSameCommand = new CollectableCheckColorSameCommand();
            _removerOnStackCommand = new RemoverOnStackCommand(ref collectableList);
            _stackAdderAnimationCommand = new StackAdderAnimationCommand(ref collectableList, ref _stackData);

        }

        private StackData GetStackData() => Resources.Load<CD_Stack>(_stackDataPath).Data;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onInteractionWithCollectable += OnInteractionWithCollectable;
            StackSignals.Instance.onStackFollowPlayer += OnStackFollowPlayer;
            CollectableSignals.Instance.onIsPlayerColorSame += OnIsPlayerColorSame;
            
        }

        private void OnIsPlayerColorSame(bool condition) => _isColorSame = condition;
        
        private void OnStackFollowPlayer(Vector2 direction)
        {
            transform.localPosition = new Vector3(0, collectableObject.transform.position.y, direction.y - 1.2f);
            if (collectableList.Count > 0)
            {
                _stackMoverCommand.Execute(direction.x, collectableList);
            }
        }

        private void OnInteractionWithCollectable(GameObject collectableGameObject)
        {
                _collectableCheckColorSameCommand.Execute(collectableGameObject);
                if (_isColorSame)
                {
                    StartCoroutine(_stackAdderAnimationCommand.Execute());
                    _adderOnStackCommand.Execute(collectableGameObject);
                    _stackScoreUpdaterCommand.Execute(_collectableAddScore);
                    _collectableAnimationCommand.Execute(ref collectableGameObject,PlayerAnimationStates.Run);
                }
                else
                {
                    _removerOnStackCommand.Execute(collectableGameObject, _collectableAnimationCommand);
                    _stackScoreUpdaterCommand.Execute(_collectableAddScore);

                }
               
                
            
        }


        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onInteractionWithCollectable -= OnInteractionWithCollectable;
            StackSignals.Instance.onStackFollowPlayer -= OnStackFollowPlayer;
           
        }
        
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}