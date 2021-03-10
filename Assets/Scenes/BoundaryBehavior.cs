using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryBehavior : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
