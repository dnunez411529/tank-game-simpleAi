using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoteams : MonoBehaviour
{
    public GameObject[] removeTeams;
    private tankManage _tankManage; 
    public void twoTeams()
    {
        //_tankManage.
        for (int i = 0; i < 3; i++)
        {
            removeTeams[i].gameObject.SetActive(false);
        }
    }
}
