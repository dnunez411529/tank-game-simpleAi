using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    public GameObject tank;
    public string team;
    private void OnTriggerEnter(Collider hit)
    {
        Debug.Log("hit: " + hit.gameObject.name);
        Debug.Log("current gameobject: " + gameObject.name);
        /*tank.SetActive(false);   //no longer needed for respawning
        yield return new WaitForSeconds(2);
        tank.SetActive(true);*/
        if (!hit.gameObject.name.Contains("Bullet"))
        {
            if (!hit.gameObject.name.Contains(team))
            {

                if (hit.gameObject.name.Contains("1"))
                {
                    Destroy(hit.gameObject);
                }

                Destroy(gameObject);
            }

        }

    }

}
