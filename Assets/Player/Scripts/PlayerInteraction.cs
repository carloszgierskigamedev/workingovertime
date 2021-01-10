using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Camera cam = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    void Interact()
    {
        //If E is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 15, Color.red, 2f);

            if (Physics.Raycast(ray, out hit, 15))
            {
                BaseInteractable interactable = hit.collider.GetComponent<BaseInteractable>();
                if (interactable != null)
                {
                    interactable.OnInteract();
                }
            }
        }
    }
}
