using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTriggerController : MonoBehaviour
{
    AudioSource _audioSource = null;
    bool _alreadyTriggered = false;
    public bool AlreadyTriggered => _alreadyTriggered;

    void Start()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!_alreadyTriggered && other.CompareTag("Player"))
        {
            _alreadyTriggered = true;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
