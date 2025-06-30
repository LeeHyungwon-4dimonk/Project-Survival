using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHWPlayerInteract : MonoBehaviour
{  

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(2, 2, 0));
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Interactable Object") && Input.GetKey(KeyCode.E))
        {
            other.GetComponent<IInteractable>().Interact();
            Debug.Log("chest");
        }
    }
}
