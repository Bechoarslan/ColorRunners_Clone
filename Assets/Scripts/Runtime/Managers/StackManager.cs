
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Commands.Stack;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;



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
        [ShowInInspector] private int _stackScore;
        private Transform _miniGameHolder;
        

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
            ColorAreaSignals.Instance.onSendMiniGameHolder += OnSendMiniGameHolder;
            PlayerSignals.Instance.onPlayerSettledToMiniGameArea += OnPlayerSettledToMiniGameArea;
            PlayerSignals.Instance.onPlayerExitMiniGameArea += OnPlayerExitMiniGameArea;
            
        }

        private void OnPlayerExitMiniGameArea()
        {
            StartCoroutine(SetPosition());
        }

        private void OnPlayerSettledToMiniGameArea()
        {
            StartCoroutine(MoveToMiniGameHolder(1f));
        }

        private void OnSendMiniGameHolder(Transform miniGameHolder) => _miniGameHolder = miniGameHolder;
        
        private IEnumerator MoveToMiniGameHolder(float f)
        {
            for (int i = 6; i < collectableList.Count; i++)
            {
                var playerPos = collectableList[i].transform.position;
                var holderPos = collectableList[Random.Range(1, 4)].transform.position;
                var targetPosition = new Vector3(holderPos.x, playerPos.y, holderPos.z);
                Tweener tweener = collectableList[i].transform.DOMove(targetPosition, f);
                yield return tweener.WaitForCompletion();
                



            }
           
           


        }

        [Button]
        private IEnumerator SetPosition()
        {
            for (int i = 6; i < collectableList.Count; i++)
            {
                Vector3 newPos = collectableList[i - 1].transform.localPosition;
                newPos.z -= 0.8f;
               Tweener tween = collectableList[i].transform.DOLocalMove(newPos, 0.2f);
               yield return tween.WaitForCompletion();
               
            }
            
            
            
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