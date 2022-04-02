using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class inherits from the ActionStarter class
public class Combat : ActionStarter
{
    
    public Weapon mainWeapon = null;
    public Weapon secondWeapon = null;
    public Weapon thirdWeapon = null;
    public Transform rightHand = null;
    public Transform leftHand = null;

    Health targetEnemy;
    float timeSinceHit = 0;
    Weapon currWeapon = null;
    float strength = 1f;
    //Start method only executed once in the code, and executes the SpawnWeapon method, declaring mainWeapon as its parameter
    void Start()
    {
        SpawnWeapon(mainWeapon);
    }

    void Update()
    {
        //after each frame, the timeSinceHit increases by Time.deltaTime
        timeSinceHit += Time.deltaTime;
        
        //If there is no target, then carry on with the rest of the code
        if (targetEnemy == null) {
            return;
        }

        //if the player is in range, then execute the Halt method from the Mover script and initiate the HitAnimation method, if the player isnt in range, then move to the enemy target's position 
        if (inRange())
        {
            GetComponent<Mover>().Halt();
            HitAnimation();

        }
        else
        {
            GetComponent<Mover>().MoveTo(targetEnemy.transform.position);
        }

    }

    //This method calls the spawn method from the weapon script and equips the weapon in the player's hand
    public void SpawnWeapon(Weapon weapon)
    {
        currWeapon = weapon;
        currWeapon.Spawn(rightHand, leftHand, GetComponent<Animator>());
    }

    //HitAnimation method when executed makes the player face the enemy's position and initiate the attack animation
    private void HitAnimation()
    {
        transform.LookAt(targetEnemy.transform.position);
        if (timeSinceHit > currWeapon.getHitTime())
        {
            GetComponent<Animator>().SetTrigger("attack");
            timeSinceHit = 0;
        }
    }
    //Hit() is a animation event which is provided in the animation timeframe, when the timeframe gets to the Hit() event, it executes the damage method
    void Hit()
    {
        //if enemy doesnt exist, then carry on with the code
        if (targetEnemy == null) return;
        //if the current weapon being used is a projectile weapon, then shoot the arrow
        if (currWeapon.IsProjectile())
        {
            currWeapon.LaunchArrow(rightHand, leftHand, targetEnemy);
        }
        else
        {
            targetEnemy.GetComponent<Health>().TakingDamage(currWeapon.getHit()*strength);
        }
    }
    //Shoot(), like Hit() is an animation event in the arrow shooting animation timeframe
    void Shoot()
    {
        Hit();
    }
    
    //This method returns a boolean value of true (if the player is in range) or false(if not in range)
    private bool inRange()
    {
        return Vector3.Distance(transform.position, targetEnemy.transform.position) < currWeapon.getRange();
    }

    //This Fight method executes the StartAction method from the ActionStarter script and stores Health script in target
    public void Fight(Target target) {
        StartAction(this);
        targetEnemy = target.GetComponent<Health>();
    }

    //This Halt method is overriden and when executed, executes the stopAttacking animation, which makes the person stop attacking. And it also sets the target to null, meaning the enemy is dead.
    public override void Halt() {
        GetComponent<Animator>().SetTrigger("stopAttacking");
        targetEnemy = null;
    }

    public void switchWeapon()
    {
        if (currWeapon == mainWeapon)
        {
            currWeapon.Despawn();
            SpawnWeapon(secondWeapon);

        }
        else if (currWeapon == secondWeapon)
        {
            if (thirdWeapon != null)
            {
                currWeapon.Despawn();
                SpawnWeapon(thirdWeapon);
            }
            else
            {
                currWeapon.Despawn();
                SpawnWeapon(mainWeapon);
            }
        }
        else if (currWeapon == thirdWeapon)
        {
            currWeapon.Despawn();
            SpawnWeapon(mainWeapon);

        }


    }

    public void equipThird(Weapon third)
    {
      thirdWeapon = third;
    }

    //this method increases stats when player levels up
    public void LevelUp()
    {
        strength += 0.5f;
    }
}
