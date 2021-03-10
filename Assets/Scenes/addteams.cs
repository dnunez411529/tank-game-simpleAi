using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addteams : MonoBehaviour
{
    public GameObject[] removeTeams;

    public void threeTeams()
    {
        for (int i = 0; i < 3; i++)
        {
            removeTeams[i].gameObject.SetActive(true);
        }
    }
}