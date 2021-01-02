using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private bool flashlightEnabled = false;
    [SerializeField] private GameObject lightSource; 
    private bool flashlightCooldown = false;
    //[SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        lightSource.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FlashlightInput"))
        {
            if(!flashlightCooldown)
            {
                if (!flashlightEnabled)
                {
                    lightSource.SetActive(true);
                    flashlightEnabled = true;
                }
                else
                {
                    lightSource.SetActive(false);
                    flashlightEnabled = false;
                }

                StartCoroutine(FlashlightCooldown());
            }
        }
    }

    IEnumerator FlashlightCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        flashlightCooldown = false;
    }
}
