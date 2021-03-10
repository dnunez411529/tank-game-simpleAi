using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
