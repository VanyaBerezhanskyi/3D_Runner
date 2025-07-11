using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed = 5;
    [SerializeField] private float _sideSpeed = 10;
    [SerializeField] private float _jumpHeight = 10;
    [SerializeField] private float _gravity = 20;

    private CharacterController _characterController;
    private float _directionY = 0;
    private Animator _animator;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float directionX = Input.GetAxis("Horizontal") * _sideSpeed;
        float directionZ = _forwardSpeed;
        Vector3 direction = new Vector3(directionX, _directionY, directionZ);

        if (Input.GetButtonDown("Jump"))
        {
            _directionY = _jumpHeight;
        }

        _directionY -= _gravity * Time.deltaTime; // Реалізація гравітації

        _characterController.Move(direction * Time.deltaTime);

        if (_characterController.isGrounded)
        {
            _animator.SetBool("isGrounded", true);
        }
        else
        {
            _animator.SetBool("isGrounded", false);
        }
    }
}
