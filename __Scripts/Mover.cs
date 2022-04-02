using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//class inherits from ActionStarter
public class Mover : ActionStarter
{
    public Transform target;

    private void Start()
    {
        
    }

    void Update()
    {
        AnimatorUpdate(); // call the AnimatorUpdate function
    }

    //This function syncs the animation that was imported to the character.
    private void AnimatorUpdate()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 vLocal = transform.InverseTransformDirection(velocity);
        float speed = vLocal.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

    //This function enables to player to move to from location to location using the MoveTo method as well as the the StartAction method from ActionStarter class
    public void StartMove(Vector3 location)
    {
        StartAction(this);
        GetComponent<Combat>().Halt();
        MoveTo(location);
    }

    //This method allows the player to move to the location the mouse clicked on (goes to the raycast)
    public void MoveTo(Vector3 location)
    {
        GetComponent<NavMeshAgent>().destination = location;
        GetComponent<NavMeshAgent>().isStopped = false;
    }

    //Overrides the halt method from ActionStarter and changes isStopped to true
    public override void Halt() {
        GetComponent<NavMeshAgent>().isStopped = true;
    }

}
