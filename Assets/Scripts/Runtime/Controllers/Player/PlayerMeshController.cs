using System;
using DG.Tweening;
using Runtime.Enums.Collectable;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private Animator playerAnimator;
        #endregion

        #endregion
        
        
        internal void SetPlayerAnimation(bool condition)
        {
            playerAnimator.SetBool("Run",condition);
        }

        
    }
}