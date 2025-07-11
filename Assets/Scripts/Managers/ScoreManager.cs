using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ObjectScore _objectScore;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.COIN_COLLECTED, OnCoinCollected);
        Messenger.AddListener(GameEvent.RETRY_BUTTON_PRESSED, ResetScore);

        ResetScore();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.COIN_COLLECTED, OnCoinCollected);
        Messenger.RemoveListener(GameEvent.RETRY_BUTTON_PRESSED, ResetScore);
    }

    private void OnCoinCollected() => _objectScore.score++;

    private void ResetScore() => _objectScore.score = 0; 
}
