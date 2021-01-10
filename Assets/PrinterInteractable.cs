using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterInteractable : BaseInteractable
{
    private bool _alreadyInteracted = false;
    AudioSource _audioSource = null;
    void Start()
    {
        _alreadyInteracted = false;
        _audioSource = GetComponent<AudioSource>();
    }

    public override void OnInteract() 
    {
        if (!_alreadyInteracted)
        {
            _alreadyInteracted = true;
            GameObject.FindObjectOfType<TaskController>().PrinterTaskCompleted();
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}
