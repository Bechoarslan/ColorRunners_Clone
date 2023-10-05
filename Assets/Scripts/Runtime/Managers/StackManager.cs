using System;
using System.Collections.Generic;
using Runtime.Commands.Stack;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinValidator.Editor;
using UnityEngine;

namespace Runtime.Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

        [SerializeField] private GameObject collectableObject;

        #endregion

        #region Private Variables

        private List<GameObject> collectableList = new List<GameObject>();
        private StackData _stackData;
        private readonly string _stackDataPath = "Data/CD_Stack";

        private StackMoverCommand _stackMoverCommand;
        private AdderOnStackCommand _adderOnStackCommand;
        private StackScoreUpdaterCommand _stackScoreUpdaterCommand;
        
      

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
            
           
           
        }

       

        private void OnStackFollowPlayer(Vector2 direction)
        {
            transform.position = new Vector3(0, collectableObject.transform.position.y, direction.y - 1.2f);
            if (collectableList.Count > 0)
            {
                _stackMoverCommand.Execute(direction.x, collectableList);
            }
        }

        private void OnInteractionWithCollectable(GameObject collectableObject)
        {
            
            _adderOnStackCommand.Execute(collectableObject);
            _stackScoreUpdaterCommand.Execute();
            

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