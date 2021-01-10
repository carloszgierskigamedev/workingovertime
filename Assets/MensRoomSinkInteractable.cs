using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensRoomSinkInteractable : BaseInteractable
{
    private bool _alreadyInteracted = false;
    void Start()
    {
        _alreadyInteracted = false;
    }

    public override void OnInteract() 
    {
        if (!_alreadyInteracted)
        {
            _alreadyInteracted = true;
            GameObject.FindObjectOfType<TaskController>().MensRoomTaskCompleted();
        }
    }
}
