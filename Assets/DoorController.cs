using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    
    [SerializeField] private bool _closedDoor = false;
    [SerializeField] private AudioClip[] _doorSfx = default;
    HingeJoint _doorHingeJoint = default;
    Rigidbody _rigidBody = default;
    private AudioSource _audioSource = default;
    private bool _alreadyPlayedDoorHingeSFX = false;
    private float _yRotation = default;
    private float _velocityBeforeClosing = default;
    public bool IsDoorLocked => _rigidBody.isKinematic;
    public bool ClosedDoor => _closedDoor;

    void Start()
    {
        _closedDoor = true;
        _doorHingeJoint = GetComponent<HingeJoint>();
        _rigidBody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {  
        JointLimits hingeJointLimits = _doorHingeJoint.limits;
        _yRotation = transform.rotation.eulerAngles.y;

        if (Mathf.Abs(_doorHingeJoint.velocity) > 1)
        {
            _velocityBeforeClosing = Mathf.Abs(_doorHingeJoint.velocity);
        }

        OpenAndCloseCycle(_yRotation, _doorHingeJoint, hingeJointLimits);
        LockDoorOnPositionZero(_yRotation, _doorHingeJoint, hingeJointLimits);
    }

    void OpenAndCloseCycle(float yRotation, HingeJoint hingeJoint, JointLimits hingeJointLimits)
    {
        if (Mathf.Abs(_doorHingeJoint.velocity) > 10f && !_alreadyPlayedDoorHingeSFX)
        {
            if (Mathf.Abs(_doorHingeJoint.velocity) < 45f) 
            {
                _audioSource.PlayOneShot(_doorSfx[1], 0.5f);
            }

            _alreadyPlayedDoorHingeSFX = true;
            StartCoroutine(doorHingeSfxCooldown());
        }

        if (_closedDoor && (yRotation > 5f && yRotation <= 90f))
        {
            hingeJointLimits.min = 0f;
            hingeJointLimits.max = 90f;
            hingeJoint.limits = hingeJointLimits;
            _closedDoor = false;
        }
        else if (_closedDoor && (yRotation >= 270f && yRotation < 355))
        {
            hingeJointLimits.min = -90f;
            hingeJointLimits.max = 0f;
            hingeJoint.limits = hingeJointLimits;
            _closedDoor = false;
        } 
    }

    void LockDoorOnPositionZero(float yRotation, HingeJoint hingeJoint, JointLimits hingeJointLimits)
    {
        if (!_closedDoor && (yRotation <= 4f || yRotation >= 356f))
        {
            hingeJointLimits.max = 0f;
            hingeJointLimits.min = 0f;
            hingeJoint.limits = hingeJointLimits;            
            
            yRotation = Mathf.Round(yRotation);

            if (yRotation == 0f || yRotation == 360f)
            {
                if (_velocityBeforeClosing < 80f)
                {
                    _audioSource.PlayOneShot(_doorSfx[0], 0.3f);
                }
                else 
                {
                    _audioSource.PlayOneShot(_doorSfx[2]);
                }

                hingeJointLimits.max = 90f;
                hingeJointLimits.min = -90f;
                hingeJoint.limits = hingeJointLimits;
                _closedDoor = true;
            }
        }
    }

    IEnumerator doorHingeSfxCooldown()
    {
        yield return new WaitUntil(() => Mathf.Abs(_doorHingeJoint.velocity) < 10f);
        _alreadyPlayedDoorHingeSFX = false;
    }

    public void LockDoor()
    {
        StartCoroutine(LockDoorCoroutine());
    }

    public void UnlockDoor(float waitTime = 0f)
    {
        StartCoroutine(UnlockDoorCoroutine(waitTime));
    }

    private IEnumerator LockDoorCoroutine()
    {
        if (_closedDoor && !IsDoorLocked)
        {
            _rigidBody.isKinematic = true;
            yield return new WaitForSeconds(1.5f);
            _audioSource.PlayOneShot(_doorSfx[3]);
        }
    }

    private IEnumerator UnlockDoorCoroutine(float waitTime = 0f)
    {
        yield return new WaitForSeconds(waitTime);
        _rigidBody.isKinematic = false;
        _audioSource.PlayOneShot(_doorSfx[3]);
    }
}