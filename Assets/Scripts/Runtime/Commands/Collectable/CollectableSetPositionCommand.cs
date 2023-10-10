using DG.Tweening;
using Runtime.Enums;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;


namespace Runtime.Commands.Collectable
{
    public class CollectableSetPositionCommand
    {
        public void Execute(GameObject arg0, Transform miniGameHolder)
        {
            var randomValue = Random.Range(-1.2f, 1.2f);
            arg0.transform.DOMove(new Vector3(miniGameHolder.transform.position.x,arg0.transform.position.y,miniGameHolder.transform.position.z + randomValue),2f).OnComplete( () =>
            {
                
            });
            arg0.transform.DORotate(Vector3.zero, 0.1f);
        }
    }
}