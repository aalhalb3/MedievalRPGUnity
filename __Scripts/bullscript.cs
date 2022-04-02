using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullscript : MonoBehaviour


{
    public float agroRange; //range at wich the enemy will start attacking the player
    public float timeBetweenHits = 1f;
    public float timeBetweenSpecial = 5f;

    Health targetEnemy; 
    float timeSinceHit = 0;
    float timeSinceSpecial = 0;
    private GameObject TargetPlayer;
    bool waliking;


    // Start is called before the first frame update
    void Start()
    {
        findPlayer();
    }

    // Update is called once per frame
    void Update()
    {
      
        if (TargetPlayer == null) { findPlayer(); }
        
        
        if (inAgroRange())
        {
            transform.LookAt(TargetPlayer.transform.position);            
            attack();
        }
        

    }


    private bool inAgroRange()
    {
        if (TargetPlayer != null)
        { return Vector3.Distance(transform.position, TargetPlayer.transform.position) < agroRange; }
        else
        { return false; }

    }

    void attack()
    {  //after each frame, the timeSinceHit increases by Time.deltaTime
        timeSinceHit += Time.deltaTime;
        timeSinceSpecial += Time.deltaTime;

        //If there is no target, then carry on with the rest of the code
        if (targetEnemy == null)
        {
            return;
        }

        //if the player is in range, then execute the Halt method from the Mover script and initiate the HitAnimation method, if the player isnt in range, then move to the enemy target's position 
        if (inRange())
        {
            GetComponent<BossMover>().Halt();
            waliking = false;
            transform.LookAt(targetEnemy.transform.position);
            if (timeSinceSpecial > timeBetweenSpecial)
            {
                //Debug.Log("spin");
                GetComponent<Animator>().SetTrigger("spin");
                timeSinceSpecial = 0;
            }
            else if (timeSinceSpecial > 1.2f) { HitAnimation(); }


        }
        else if(!inRange())
        {
            transform.LookAt(targetEnemy.transform.position);
            GetComponent<BossMover>().StartMove(targetEnemy.transform.position);
        }
    }


    //HitAnimation method when executed makes the player face the enemy's position and initiate the attack animation
    private void HitAnimation()
    {
        transform.LookAt(targetEnemy.transform.position);
        if (timeSinceHit > timeBetweenHits)
        {
            //Debug.Log("attack");
            GetComponent<Animator>().SetTrigger("attack");
            timeSinceHit = 0;
        }
    }

    //stops the combat animations and stars movement anaimation
    public void Haltboss()
    {
        if (!waliking) 
        {
            GetComponent<Animator>().SetTrigger("walk");
            waliking = true;
        }
        
    }


    //animation event for the hit aniamtion
    void BullHit()
    {
        if (targetEnemy == null) return;
        else
        {
            targetEnemy.GetComponent<Health>().TakingDamage(8);
        }
    }

    //animation event for the spin animation
    void BullSpinHit()
    {
        if (targetEnemy == null) return;
        if(Vector3.Distance(transform.position, TargetPlayer.transform.position) <6)
        {
            targetEnemy.GetComponent<Health>().TakingDamage(10);
        }
    }


    //This method returns a boolean value of true (if the player is in range) or false(if not in range)
    private bool inRange()
    {
        return Vector3.Distance(transform.position, targetEnemy.transform.position) < 6;
    }


    // method to search for the player
    void findPlayer()
    {
        if (GameObject.Find("Bow man(Clone)"))
        {
            TargetPlayer = GameObject.Find("Bow man(Clone)");
            targetEnemy = TargetPlayer.transform.GetComponent<Health>();
        }
        else if (GameObject.Find("Sword man(Clone)"))
        {
            TargetPlayer = GameObject.Find("Sword man(Clone)");
            targetEnemy = TargetPlayer.transform.GetComponent<Health>();
        }
    }
}
