using System;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private short _stackScore;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
           CoreGameSignals.Instance.onSetCollectableScore += OnSetCollectableScore;
           CoreGameSignals.Instance.onSendCollectableScore += OnSendCollectableScore;
        }

        private short OnSendCollectableScore()
        {
            return _stackScore;
        }

        private void OnSetCollectableScore(short stackScore)
        {
            _stackScore = stackScore;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onSetCollectableScore -= OnSetCollectableScore;
            CoreGameSignals.Instance.onSendCollectableScore -= OnSendCollectableScore;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}