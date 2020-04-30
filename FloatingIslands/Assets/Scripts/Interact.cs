using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interact : MonoBehaviour
{
    [SerializeField] private float radius = 5f;


    public void TryInteract(GameObject interacter)
    {
        var dist = Vector3.Distance(interacter.transform.position, transform.position);
        if(dist <= radius)
        {
            Interaction();
        }
    }

    virtual protected void Interaction()
    {
        Debug.Log("ouch");
    }

}
