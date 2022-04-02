using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{

    public float agroRange; //range at wich the enemy will start attacking the player
    public float attackDmg;
    public float range;
    public float timeBetweenHits = 2f;
    public float timeBetweenSpecial = 5f;
    public Transform rightHand = null;
    public Projectile projWeapon = null;
    public GameObject summon = null;

    Health targetEnemy;
    float timeSinceHit = 0;
    float timeSinceSpecial = 0;
    private GameObject TargetPlayer;
    bool waliking= false;


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
            Debug.Log("agro");            
            transform.LookAt(TargetPlayer.transform.position);
            attack();
        }
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
            GetComponent<FinalBossMover>().Halt();           
            transform.LookAt(targetEnemy.transform.position);
            waliking = false;
            if (timeSinceSpecial > timeBetweenSpecial)
            {

                SpecialAnimation();
                timeSinceSpecial = 0;
            }
            else if (timeSinceSpecial > 1.2f) { HitAnimation(); }


        }
        else if (!inRange())
        {
            Debug.Log("move");
            transform.LookAt(targetEnemy.transform.position);
            GetComponent<FinalBossMover>().StartMove(targetEnemy.transform.position);
        }
    }

    //determines if the player is in agro range
    private bool inAgroRange()
    {
        if (TargetPlayer != null)
        { return Vector3.Distance(transform.position, TargetPlayer.transform.position) < agroRange; }
        else
        { return false; }

    }
    private void SpecialAnimation()
    {
        Debug.Log("special");

        Instantiate(summon, transform.position, Quaternion.identity);
    }

    //HitAnimation method when executed makes the player face the enemy's position and initiate the attack animation
    private void HitAnimation()
    {
        transform.LookAt(targetEnemy.transform.position);
        if (timeSinceHit > timeBetweenHits)
        {
            Debug.Log("attack");
            GetComponent<Animator>().SetTrigger("attack");  
            Projectile projInst = Instantiate(projWeapon, rightHand.position, Quaternion.identity);
            projInst.SetTarget(targetEnemy, attackDmg);
            timeSinceHit = 0;
        }
    }

    //stops the combat animations and stars movement anaimation
    public void Haltboss()
    {
        if (!waliking)
        {
            GetComponent<Animator>().SetTrigger("move");
            waliking = true;
        }

    }

    //This method returns a boolean value of true (if the player is in range) or false(if not in range)
    private bool inRange()
    {
        return Vector3.Distance(transform.position, targetEnemy.transform.position) < range;
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
