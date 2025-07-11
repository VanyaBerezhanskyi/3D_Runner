using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Messenger.Broadcast(GameEvent.COIN_COLLECTED);

            Destroy(gameObject);
        }
    }
}
