using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float duration;    //the max time of a walking session (set to ten)
    public float agroRange; //range at wich the enemy will start attacking the player
    float elapsedTime = 0f; //time since started walk
    float wait = 0f; //wait this much time
    float waitTime = 0f; //waited this much time

    float randomX;  //randomly go this X direction
    float randomZ;  //randomly go this Z direction
    // float randomRotate; //randomly chooses a direction to walk
    bool move = true; //start moving
    private GameObject TargetPlayer;

    void Start()
    {
        findPlayer();

        randomX = Random.Range(-3, 3);
        randomZ = Random.Range(-3, 3);

        //work in progress code for more persise direction the enemy faces
        //randomRotate =  180*((Mathf.Atan(randomZ/ randomX))/Mathf.PI);
        // randomRotate = -1f*(randomRotate-90f);
    }

    void Update()
    {
        if (TargetPlayer == null) { findPlayer(); }
        if (inAgroRange())
        {
            //Debug.Log("attacking");
            Target targetP = TargetPlayer.transform.GetComponent<Target>();
            GetComponent<Combat>().Fight(targetP);
        }
        else
        {

            randomMove();
        }

    }
    // method to search for the player
    void findPlayer()
    {
        if (GameObject.Find("Bow man(Clone)"))
        {
            TargetPlayer = GameObject.Find("Bow man(Clone)");
        }
        else if (GameObject.Find("Sword man(Clone)"))
        {
            TargetPlayer = GameObject.Find("Sword man(Clone)");
        }
    }

    private bool inAgroRange()
    {
        if (TargetPlayer != null)
        { return Vector3.Distance(transform.position, TargetPlayer.transform.position) < agroRange; }
        else
        { return false; }

    }


    void randomMove()
    {

        if (elapsedTime < duration && move)
        {

            //the opject meoves a bit each update cycle
            Vector3 target = new Vector3(transform.position.x + randomX, 0, transform.position.z + randomZ);
            // transform.LookAt(target);
            GetComponent<Mover>().StartMove(target);
            //transform.Translate(new Vector3(randomX, 0, randomZ) * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, randomRotate, 0);
            elapsedTime += Time.deltaTime;

        }
        else if (move)
        {
            //generates a random time to wait for
            move = false;
            wait = Random.Range(5, 8);
            waitTime = 0f;
            //Debug.Log("wait");

        }

        if (waitTime < wait && !move)
        {
            // waiting
            waitTime += Time.deltaTime;


        }

        else if (!move)
        {
            //done waiting. generates a new random time
            move = true;
            elapsedTime = 0f;
            randomX = Random.Range(-3, 3);
            randomZ = Random.Range(-3, 3);




        }

    }

}
