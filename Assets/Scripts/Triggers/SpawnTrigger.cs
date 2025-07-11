using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Messenger.Broadcast(GameEvent.SPAWN_TRIGGER_ENTER);

            Destroy(transform.parent.gameObject); // Знищуємо тайл, який не показує камера, для економії пам'яті
        }
    }
}
