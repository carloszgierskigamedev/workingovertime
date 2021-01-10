using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour
{
    [SerializeField] protected float radius = default;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);    
    }

    public virtual void OnInteract() 
    {
        Debug.Log("Used base interactable");
    }
}
