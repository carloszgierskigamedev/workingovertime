using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : BaseInteractable
{
    private float _interactCooldown = 3f;
    private bool _canInteract = false;
    private DoorController _doorController = default;
    
    private void Start() 
    {
        _canInteract = true;
        _doorController = GetComponent<DoorController>();   
    }
    
    public override void OnInteract() 
    {
        if (_canInteract)
        {
            StartCoroutine(InteractLockedDoorCoolingDown(_interactCooldown));
        }
    }

    IEnumerator InteractLockedDoorCoolingDown(float interactCooldown)
    {
        _canInteract = false;
        
        if (_doorController.IsDoorLocked)
        {
            GameObject.FindObjectOfType<TaskController>().IntentionalLockedDoorInteraction();
        }
        
        yield return new WaitForSeconds(interactCooldown);
        _canInteract = true;
    }
}
