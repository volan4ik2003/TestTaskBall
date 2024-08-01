using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontTrigger : MonoBehaviour
{
    public bool moveAllowed = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Cell"))
        {
            if (other.gameObject.GetComponent<Cell>() && !other.gameObject.GetComponent<Cell>().IsPainted)
            {
                moveAllowed = true;            
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cell"))
        {
            moveAllowed = false;
        }
    }
}
