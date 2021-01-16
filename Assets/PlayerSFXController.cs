using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _footstepsSfx = default;
    AudioSource _audioSource = default;
    CharacterController _characterController = default;
    PlayerMovement _playerMovement = default;
    private Vector3 lastPosition;
    private float totalMoved;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _characterController = GetComponent<CharacterController>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        FootstepsSoundEffects();
    }

    void FootstepsSoundEffects()
    {
        if (_characterController.velocity.magnitude > 0 && _characterController.isGrounded && !_audioSource.isPlaying)
        {
            float moveFromLastPosition = (lastPosition - _characterController.transform.position).magnitude;
            lastPosition = _characterController.transform.position;
            totalMoved += moveFromLastPosition;

            if (totalMoved >= 4f && !_playerMovement.IsRunning)
            {
                _audioSource.PlayOneShot(GetRandomFootstepClip());
                totalMoved = 0f;
            }
            else 
            if (totalMoved >=3f && _playerMovement.IsRunning)
            {
                _audioSource.PlayOneShot(GetRandomFootstepClip());
                totalMoved = 0f;
            }
        }
    }

    AudioClip GetRandomFootstepClip()
    {
        return _footstepsSfx[Random.Range(0, _footstepsSfx.Length)];
    }
}
