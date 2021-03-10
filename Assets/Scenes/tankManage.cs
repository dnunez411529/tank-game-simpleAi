using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class tankManage : MonoBehaviour
{
    public GameObject[] buttons;
    private GameObject[] tanks;
    //tank prefabs
    public GameObject green;
    public GameObject purple;
    public GameObject blue;
    //team size and team amounts
    [Range(1,2)]
    public int teamSize;
    [Range(2,3)]
    public int teams;

    private GameObject[] delete;

    void Start()
    {
        tanks = GameObject.FindGameObjectsWithTag("Player");
        if (teamSize > 1)
        {
            addTanks();
            buttons[3].GetComponent<Button>().interactable = false;
            buttons[3].GetComponent<Image>().color = Color.grey;
        }
        else
        {
            buttons[2].GetComponent<Button>().interactable = false;
            buttons[2].GetComponent<Image>().color = Color.grey;
        }

        if (teamSize > 2)
        {
            buttons[3].GetComponent<Button>().interactable = true;
            buttons[3].GetComponent<Image>().color = Color.white;
            buttons[2].GetComponent<Button>().interactable = true;
            buttons[2].GetComponent<Image>().color = Color.white;
        }
        if (teams == 2)
        {
            print("two teams, disabling green");
            disableGreen();
            buttons[0].GetComponent<Button>().interactable = false;
            buttons[0].GetComponent<Image>().color = Color.grey;
        }
        else
        {
            buttons[1].GetComponent<Button>().interactable = false;
            buttons[1].GetComponent<Image>().color = Color.grey;
        }
        var randpos = new Random();
        blue.transform.position = new Vector3(4 * randpos.Next(2, 3), 1, 28 - (1* randpos.Next(4,6)));
        green.transform.position = new Vector3(4 * randpos.Next(3, 4), 1, 15 - (1* randpos.Next(2,3)));
        purple.transform.position = new Vector3(4 * randpos.Next(5, 6), 1, 4 - (1* randpos.Next(2,3)));
    }

    // unpauses game aka start game
    public void beginGame()
    {
        
        Time.timeScale = 1;
        for (int i = 0; i < 4; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
            buttons[i].GetComponent<Image>().color = Color.grey;
        }
        //addTanks();
    }

    public void restGame()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
    
    public void addTanks()
    {
        GameObject[] curTanks = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < curTanks.Length; i++)
        {
            if (curTanks[i].gameObject.name.Contains("Clone"))
            {
                Destroy(curTanks[i]);
            }
        }
        /*for (int i = 0; i < 3; i++)
        {
            thirdTanks[i].gameObject.SetActive(true);
        }*/
        if (teamSize > 1)
        {
            for (int i = 0; i < teamSize - 1; i++)
            {
                addBlue(i);
                addPurple(i);
                if (teams > 2)
                {
                    addGreen(i);
                }
            }
        }
    }

    public void enableTNum2()
    {
        buttons[0].GetComponent<Button>().interactable = true;
        buttons[0].GetComponent<Image>().color = Color.white;
        buttons[1].GetComponent<Button>().interactable = false;
        buttons[1].GetComponent<Image>().color = Color.grey;
    }

    public void enableTNum3()
    {
        buttons[1].GetComponent<Button>().interactable = true;
        buttons[1].GetComponent<Image>().color = Color.white;
        buttons[0].GetComponent<Button>().interactable = false;
        buttons[0].GetComponent<Image>().color = Color.grey;
    }

    public void enable1Tanks()
    {
        buttons[2].GetComponent<Button>().interactable = true;
        buttons[2].GetComponent<Image>().color = Color.white;
        buttons[3].GetComponent<Button>().interactable = false;
        buttons[3].GetComponent<Image>().color = Color.grey;
    }

    public void enable2Tanks()
    {
        buttons[3].GetComponent<Button>().interactable = true;
        buttons[3].GetComponent<Image>().color = Color.white;
        buttons[2].GetComponent<Button>().interactable = false;
        buttons[2].GetComponent<Image>().color = Color.grey;
    }
    public void disableGreen()
    {
        delete = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < delete.Length; i++)
        {
            if (delete[i].gameObject.name.Contains("Green"))
            {
                delete[i].SetActive(false);
            }
        }
        //teams = 2;
        //addTanks();
    }

    public void enableGreen()
    {
        green.SetActive(true);
        for (int i = 0; i < delete.Length; i++)
        {
            if (delete[i].gameObject.name.Contains("Green"))
            {
                delete[i].SetActive(true);
            }
        }
        teams = 3;
        addTanks();
    }

    public void teamsOfOne()
    {
        teamSize = 1;
        addTanks();
    }

    public void teamsOfTwo()
    {
        teamSize = 2;
        addTanks();
    }
    public void addGreen(int m)
    {
        var rand = new Random();
        GameObject greenTank = Instantiate(green, new Vector3(3 + ((m+1)*2) , 1, 1 * rand.Next(2,6)) , Quaternion.identity) as GameObject;
    }

    public void addPurple(int m)
    {
        var rand = new Random();
        GameObject purpleTank = Instantiate(purple, new Vector3(10 + ((m+1)*2) ,1, 1 * rand.Next(4,10) ) , Quaternion.identity) as GameObject;
    }

    public void addBlue(int m)
    {
        var rand = new Random();
        GameObject blueTank = Instantiate(blue, new Vector3((28 - ((m+1)*2)), 1, 1 * rand.Next(16,28) ) , Quaternion.identity) as GameObject;
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            Text t = GameObject.Find("Winner").GetComponent<Text>();
            t.text = "Tie";
        }
    }
}