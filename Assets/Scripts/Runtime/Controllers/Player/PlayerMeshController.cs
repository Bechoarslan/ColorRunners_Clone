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

        #region Private Variables

        [ShowInInspector] private short _startCollectableScore = 50;

        #endregion

        #endregion
        

        public void SetCollectableScore(short score)
        {
            scoreText.text = score.ToString();
        }
    }
}