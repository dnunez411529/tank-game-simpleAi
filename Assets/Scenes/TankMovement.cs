using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    private bool firstshot = true;
    public Rigidbody shell;

    public Transform FireStart;

    public int Direction = 0;

    public float FireRate = 0.5f;

    public float LastShot = 0f;

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode fire;

    public Transform enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void Shoot()
    {
        Rigidbody mshell = Instantiate(shell, FireStart.position, FireStart.rotation);
        mshell.AddForce(transform.forward * 12, ForceMode.VelocityChange);
        /*if(Direction == 0)
            mshell.AddForce(new Vector3(0, 0, 20f), ForceMode.VelocityChange);
        else if(Direction == 1)
            mshell.AddForce(new Vector3(0, 0, -20f), ForceMode.VelocityChange);
        else if (Direction == 2)
            mshell.AddForce(new Vector3(-20f, 0, 0), ForceMode.VelocityChange);
        else if (Direction == 3)
            mshell.AddForce(new Vector3(20f, 0, 0), ForceMode.VelocityChange);*/

    }

    public void ShootSlow()
    {
        if (firstshot)
        {
            firstshot = false;
            Shoot();
        }
        if(Time.time > FireRate + LastShot) {
            Shoot();
            LastShot = Time.time;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody)
        {
            if (Input.GetKey(up))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(new Vector3(0, 0, 0.3f));
                Direction = 0;
            }
            else if (Input.GetKey(down))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(new Vector3(0, 0, 0.3f));
                Direction = 1;
            }
            else if (Input.GetKey(left))
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                transform.Translate(new Vector3(0, 0, 0.3f));
                Direction = 2;
            }
            else if (Input.GetKey(right))
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                transform.Translate(new Vector3(0, 0, 0.3f));
                Direction = 3;
            }

            if (Input.GetKey(fire))
            {
                if(Time.time > FireRate + LastShot) {
                    Shoot();
                    LastShot = Time.time;
                }
            }
        }

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnCollisonExit(Collision other)
    {
        
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        
    }

    void disableMovement()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    
}
