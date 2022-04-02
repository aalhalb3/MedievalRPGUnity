using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStarter : MonoBehaviour
{
    MonoBehaviour actionCurrent;

    //This function when called upon, stores the current action provided by player (move action, animation, etc)
    public void StartAction(MonoBehaviour action) {

        if (actionCurrent == action) return;
        if (actionCurrent != null)
        {
            
        }
        actionCurrent = action;
    }

    //a method which allows the player to stop moving. This method is being overrided in both Combat and Mover class
    public virtual void Halt() { 
    
    }
}
