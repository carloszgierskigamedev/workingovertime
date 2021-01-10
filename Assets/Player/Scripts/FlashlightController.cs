using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private bool _flashlightEnabled = false;
    [SerializeField] private GameObject _lightSource; 
    private bool _flashlightCooldown = false;
    private AudioSource _audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        _lightSource.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FlashlightInput"))
        {
            if(!_flashlightCooldown)
            {
                if (!_flashlightEnabled)
                {
                    _audioSource.Play();
                    _lightSource.SetActive(true);
                    _flashlightEnabled = true;
                }
                else
                {
                    _audioSource.Play();
                    _lightSource.SetActive(false);
                    _flashlightEnabled = false;
                }

                StartCoroutine(FlashlightCooldown());
            }
        }
    }

    IEnumerator FlashlightCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        _flashlightCooldown = false;
    }
}
