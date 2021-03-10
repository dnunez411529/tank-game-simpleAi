using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class AIChase : MonoBehaviour
{
    public string teamColor;
    public Text winner;
    //last detected object
    public GameObject lastHit;
    
    public Vector3 collision = Vector3.zero;
    // Adjust the speed for the application.
    public List<Transform> Enemies;
    private int speed = 2;
    private int mindist = 0;
    // brining the shoot function in with tank movement
    public TankMovement s;
    private Transform target;
    // time intervals to prevent rapid fire
    private tankManage tm;
    private GameObject[] tanks;
    private bool notRefreshed = true;
    private bool gameEnded = false;
    void Start()
    {
        Time.timeScale = 0;
        var setSpeedRand = new Random();
        this.speed = speed + setSpeedRand.Next(1, 2);
    }

    public void refreashEnemy()
    {
        tanks = GameObject.FindGameObjectsWithTag("Player");
        for (int j = 0; j < tanks.Length; j++)
        {
            if ((tanks[j].gameObject.name.Contains("Clone") && !tanks[j].name.Contains(teamColor)) || (tanks[j].gameObject.name.Contains("1") && !tanks[j].name.Contains(teamColor)))
            {
                Enemies.Add(tanks[j].transform);
                //print(Enemies[j]);
            }
        }
        switchToNearest();
        /*var rand = new Random();
        Enemies = Enemies.OrderBy(x => rand.Next()).ToList();*/
        
    }

    public void switchToNearest()
    {
        if (Enemies[0] == null)
        {
            for (int i = 1; i < Enemies.Count; i++)
            {
                if (Enemies[i] != null)
                {
                    Enemies[0] = Enemies[i];
                }
            }
        }

        float nearest = Vector3.Distance(Enemies[0].transform.position, gameObject.transform.position);
        int smallestIndex = 0;
        Enemies.RemoveAll(item => item == null);
        for (int j = 0; j < Enemies.Count; j++)
        {
            float x = Vector3.Distance(Enemies[j].transform.position, gameObject.transform.position);

            if(x < nearest)
            {
                nearest = x;
                smallestIndex = j;
            }
        }

        Transform tmp = Enemies[0];
        Enemies[0] = Enemies[smallestIndex];
        Enemies[smallestIndex] = tmp;
    }
    public void declareWinner()
    {
        winner.text = "Winner: " + teamColor;
        Time.timeScale = 0;
        gameEnded = true;
    }

    public void checkUnique()
    {
        GameObject[] currentTanks = GameObject.FindGameObjectsWithTag("Player");
        int purple = 0;
        int blue = 0;
        int green = 0;
        for (int i = 0; i < currentTanks.Length; i++)
        {
            if (currentTanks[i].name.Contains("Purple"))
            {
                purple++;
            }
            else if (currentTanks[i].name.Contains("Green"))
            {
                green++;
            }
            else if (currentTanks[i].name.Contains("Blue"))
            {
                blue++;
            }
        }

        if (purple == 0 && green == 0 && blue > 0)
        {
            declareWinner("Blue");
        }
        if (blue == 0 && green == 0 && purple > 0)
        {
            declareWinner("Purple");
        }
        if (purple == 0 && blue == 0 && green > 0)
        {
            declareWinner("Green");
        }
    }
    public void declareWinner(string color)
    {
        winner.text = "Winner: " + color;
        Time.timeScale = 0;
        gameEnded = true;
    }
    void Update()
    {
        try
        {
            if (Time.timeScale == 1 && notRefreshed)
            {
                notRefreshed = false;
                refreashEnemy();
            }

            if (!notRefreshed && !gameEnded)
            {
                if (Enemies[0] == null)
                {
                    Enemies.RemoveAll(item => item == null);
                    checkUnique();
                    switchToNearest();
                }
                else
                {
                    switchToNearest();
                    var ray = new Ray(this.transform.position, this.transform.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        lastHit = hit.transform.gameObject;
                        collision = hit.point;
                    }

                    if (Vector3.Distance(transform.position, Enemies[0].position) >= mindist)
                    {
                        Vector3 tarMove = default;


                        if (transform.position.x != Enemies[0].position.x)
                        {
                            tarMove = new Vector3(Enemies[0].position.x, 1, transform.position.z);

                        }

                        if (transform.position.z != Enemies[0].position.z)
                        {
                            tarMove = new Vector3(transform.position.x, 1, Enemies[0].position.z);
                        }

                        transform.LookAt(tarMove);
                        transform.position = Vector3.MoveTowards(transform.position,
                            tarMove, speed * Time.deltaTime);
                    }

                    if (Physics.Raycast(ray, out hit))
                    {
                        lastHit = hit.transform.gameObject;
                        if (lastHit.gameObject.name.Contains("1")) // need to check if lastHit is in Enemies
                        {
                            s.ShootSlow();
                            refreashEnemy();
                            switchToNearest();
                        }
                    }
                }
            }
        }
        catch { //there was a weird throw I couldn't get rid of here...
        }
    }
}
