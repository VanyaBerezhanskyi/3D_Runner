using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float _speed = 10;

    private Vector3 _direction;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();

        float directionX = Random.Range(0, 2) == 0 ? -1 : 1; // Вибираємо випадковий напрям або по осі z, або проти
        _direction.x = directionX;
        _direction = transform.TransformDirection(_direction);
    }

    private void FixedUpdate()
    {
        _rigidBody.linearVelocity = _direction * _speed;
    }

    private void OnCollisionEnter(Collision collison)
    {
        if (collison.gameObject.CompareTag("Wall"))
        {
            _direction = -_direction;
        }
        else if (collison.gameObject.CompareTag("Player"))
        {
            Messenger.Broadcast(GameEvent.PLAYER_DEAD);
        }
    }
}
