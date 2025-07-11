using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ObjectScore _objectScore;
    [SerializeField] private GameObject _finishScreen;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.COIN_COLLECTED, OnCoinCollected);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, ShowRetryButton);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.COIN_COLLECTED, OnCoinCollected);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, ShowRetryButton);
    }

    private void OnCoinCollected() => _scoreText.text = "Score: " + _objectScore.score.ToString();

    private void ShowRetryButton() => _finishScreen.gameObject.SetActive(true);

    public void OnClickRetryButton() => Messenger.Broadcast(GameEvent.RETRY_BUTTON_PRESSED);
}
