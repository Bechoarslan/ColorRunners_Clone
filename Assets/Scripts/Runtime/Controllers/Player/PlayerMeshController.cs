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

        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private Animator playerAnimator;
        #endregion

        #endregion
        

        internal void SetCollectableScore(short score)
        {
            scoreText.text = score.ToString();
        }
        
        internal void SetPlayerAnimation(bool condition)
        {
            playerAnimator.SetBool("Run",condition);
        }

        
    }
}