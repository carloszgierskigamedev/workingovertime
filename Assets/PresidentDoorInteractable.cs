using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresidentDoorInteractable : BaseInteractable
{
    private float interactCooldown = 3f;
    private bool canInteract = false;
    
    private void Start() 
    {
        canInteract = true;    
    }
    
    public override void OnInteract() 
    {
        if (canInteract)
        {
            StartCoroutine(InteractCoolingDown(interactCooldown));
        }
    }

    IEnumerator InteractCoolingDown(float interactCooldown)
    {
        canInteract = false;
        GameObject.FindObjectOfType<TaskController>().PresidentDoorTask();
        yield return new WaitForSeconds(interactCooldown);
        canInteract = true;
    }
}
