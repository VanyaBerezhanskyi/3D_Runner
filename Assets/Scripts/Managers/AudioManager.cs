using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_DEAD, StopMusic);

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, StopMusic);
    }

    private void StopMusic() => _audioSource.Stop();
}
