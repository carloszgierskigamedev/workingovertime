using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = default;
    [Header("Speed")]
    [SerializeField] private float _forwardWalkSpeed = 6f;
    [SerializeField] private float _forwardRunSpeed = 10f;
    [SerializeField] private float _backwardMoveSpeed = 3.5f;
    [SerializeField] private float _gravity = -9.81f;
    private Vector3 _velocity;
    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck = default;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask = default;
    [Header("Stamina/Run system")]
    [SerializeField] private float _totalStamina = 100f;
    [SerializeField] private float _staminaRunCost = 30;
    [SerializeField] private float _staminaRecoverAmount = 15;
    [SerializeField] private float _currentStamina = default;
    private bool _isGrounded;
    private bool _isRunning = false;
    private bool _canRun = true;

    void Start()
    {
        _currentStamina = _totalStamina;
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * horizontalMovement + transform.forward * verticalMovement;

        float forwardSpeed = _isRunning ? _forwardRunSpeed : _forwardWalkSpeed;
        float moveSpeedToUse = (verticalMovement >= 0) ? forwardSpeed : _backwardMoveSpeed;
        _characterController.Move(movement * moveSpeedToUse * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);

        RunCheck();
        StaminaDrainCalculation();

    }

    private void RunCheck()
    {
        if (_canRun)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                StartRunning();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || _currentStamina <= 0)
            {
                StopRunning();
            }
        }
    }

    private void StartRunning()
    {
        if (_currentStamina <= 0) return;
        _isRunning = true;
    }

    private void StopRunning()
    {
        StartCoroutine(RunningCooldown());
        _isRunning = false;
    }

    private void StaminaDrainCalculation()
    {
        if (_isRunning)
        {
            _currentStamina -= _staminaRunCost * Time.deltaTime;
        }
        else
        {
            if (_currentStamina < _totalStamina)
            {
                _currentStamina += _staminaRecoverAmount * Time.deltaTime;
            }
        }
    }

    IEnumerator RunningCooldown()
    {
        _canRun = false;
        yield return new WaitForSeconds(2.5f);
        _canRun = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.gameObject.CompareTag("Door"))
        {
            hit.rigidbody.AddForceAtPosition(-hit.normal * 10f, hit.point);
        }
    }

}
