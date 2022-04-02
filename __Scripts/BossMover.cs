using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveTo(Vector3 location)
    {
        GetComponent<NavMeshAgent>().destination = location;
        GetComponent<NavMeshAgent>().isStopped = false;
    }
    //This function enables to player to move to from location to location using the MoveTo method as well as the the StartAction method from ActionStarter class
    public void StartMove(Vector3 location)
    {
        GetComponent<bullscript>().Haltboss();
        MoveTo(location);
    }

    //Overrides the halt method from ActionStarter and changes isStopped to true
    public void Halt()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
        
    }
}
