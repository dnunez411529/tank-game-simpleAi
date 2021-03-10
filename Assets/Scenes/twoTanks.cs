using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoTanks : MonoBehaviour
{
    public GameObject[] removeTank;

    public void removeTanks()
    {
        for (int i = 0; i < 3; i++)
        {
            removeTank[i].gameObject.SetActive(false);
        }
        
    }
}
