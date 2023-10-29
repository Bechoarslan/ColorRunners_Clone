using System;
using DG.Tweening;
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
        #endregion

        #endregion
        

        public void SetCollectableScore(short score)
        {
            scoreText.text = score.ToString();
        }

        
    }
}