using System.Collections.Generic;
using DG.Tweening;
using Runtime.Controllers.Turret;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.MiniGame
{
    public class MiniGamePlayTurretCommand
    {
        private TurretController _turretController;
        public MiniGamePlayTurretCommand(ref TurretController turretController)
        {
            _turretController = turretController;
        }

        public void Execute(bool isPlayerExited, bool condition, List<GameObject> collectableObj)
        {
            if (isPlayerExited) return;
            if (condition) return;
            _turretController.SetTarget(collectableObj[0].transform);
            _turretController.isPlayerTargeted = true;
            DOVirtual.DelayedCall(0.2f, () =>
            {
                if (MiniGameSignals.Instance.onCheckColorAgainForTurretMiniGame?.Invoke() == true) return;
                _turretController.Shoot();
                var destroyedCollectableObject = collectableObj[^1].gameObject;
                collectableObj.Remove(destroyedCollectableObject);
                destroyedCollectableObject.SetActive(false);
                var pool = Object.FindObjectOfType<PoolManager>().GetComponentInChildren<Transform>();
                destroyedCollectableObject.transform.parent = pool;
                collectableObj.TrimExcess();
                CoreGameSignals.Instance.onSetCollectableScore?.Invoke((short)collectableObj.Count);
                

            });

        }
    }
}