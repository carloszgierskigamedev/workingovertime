using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneDetrigger : MonoBehaviour
{
    AudioSource _audioSource = null;
    bool _alreadyTriggered = false;

    void Start()
    {
        _audioSource = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!_alreadyTriggered && other.CompareTag("Player") && GameObject.FindObjectOfType<BaseTriggerController>().AlreadyTriggered)
        {
            _alreadyTriggered = true;
            _audioSource.loop = false;
        }
    }
}
