using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenInteractable : BaseInteractable
{
    private bool _alreadyInteracted = false;
    [SerializeField] private GameObject _doorGameObject = default;
    [SerializeField] private float _unlockDoorTime = default;
    Rigidbody _doorRigidbody = default;
    HingeJoint _doorHingeJoint = default;
    DoorController _doorController = default;

    void Start()
    {
        _alreadyInteracted = false;
        _doorRigidbody = _doorGameObject.GetComponent<Rigidbody>();
        _doorHingeJoint = _doorGameObject.GetComponent<HingeJoint>();
        _doorController = _doorGameObject.GetComponent<DoorController>();
    }

    void Update() 
    {   
        Debug.DrawRay(_doorHingeJoint.transform.position, _doorHingeJoint.transform.forward * 10f, Color.magenta, 10f);
    }
    public override void OnInteract() 
    {
        if (!_alreadyInteracted)
        {
            _alreadyInteracted = true;
            SlamDoor();
            GameObject.FindObjectOfType<TaskController>().OvenTaskCompleted();
        }
    }

    public void SlamDoor()
    {
        if (Mathf.Round(_doorGameObject.transform.eulerAngles.y) > 0 && Mathf.Round(_doorGameObject.transform.eulerAngles.y) <= 91)
        {
            _doorRigidbody.AddForce(_doorHingeJoint.transform.forward * 10000f);
            StartCoroutine(DoorLockerCaller());
        }
        else 
        if (Mathf.Round(_doorGameObject.transform.eulerAngles.y) > 91 && Mathf.Round(_doorGameObject.transform.eulerAngles.y) < 271)
        {
            _doorRigidbody.AddForce(-_doorHingeJoint.transform.forward * 10000f);
            StartCoroutine(DoorLockerCaller());
        }
    }

    IEnumerator DoorLockerCaller()
    {
        yield return new WaitUntil(() => _doorController.ClosedDoor);
        _doorController.LockDoor();
        GameObject.FindObjectOfType<TaskController>().IntentionalLockedDoorReaction();
        _doorController.UnlockDoor(_unlockDoorTime);
    }
}
