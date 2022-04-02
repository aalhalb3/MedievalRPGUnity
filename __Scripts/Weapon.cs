using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This CreateAssetMenu function allows for the code to be more user-friendly and reusable. Can be created in the prefab for any weapon and change as you like depending on the weapon
[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
//This class is of type ScriptableObject, which allows for reusability
public class Weapon : ScriptableObject
{
    public AnimatorOverrideController weaponAnimationOverride = null;
    public GameObject equippedWeaponPrefab = null;
    public float hitDamage = 5f;
    public float rangeDamage = 2f;
    public float timeBetweenHits = 1f;
    public bool isRightHand = true;
    public GameObject equippedPrefab = null;
    public Projectile projWeapon = null;
    

    private GameObject spawnedWeapon;

    //This Spawn method Instantiates the weapon on either the rightHand, or the leftHand depending on the weapon. It also overrides the animation depending on the weapon being used.
    public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
    {
        if (equippedWeaponPrefab != null)
        {
            Transform hand = WeaponTransform(rightHand, leftHand);
            Instantiate(equippedWeaponPrefab, hand);
        }
        if (weaponAnimationOverride != null)
        {
            animator.runtimeAnimatorController = weaponAnimationOverride;
        }

    }

    //this method destroys the current object this si used when switching weapon
    public void Despawn()
    {
        if (equippedWeaponPrefab != null)
        { 
            if (GameObject.Find(equippedWeaponPrefab.transform.name + "(Clone)"))
            {
             spawnedWeapon = GameObject.Find(equippedWeaponPrefab.transform.name + "(Clone)");
             Destroy(spawnedWeapon);
            }
        }
    }

    //This method returns whether the weapon being used is a right handed or left handed weapon, then stored in the hand variable
    private Transform WeaponTransform(Transform rightHand, Transform leftHand)
    {
        Transform hand;
        if (isRightHand)
        {
            hand = rightHand;
        }
        else
        {
            hand = leftHand;
        }

        return hand;
    }

    //IsProjectile() is a boolean method that stores the projectile weapon's value
    public bool IsProjectile()
    {
        return projWeapon != null;
    }

    //This LaunchArrow method instantiates the arrow being shot and it calls the SetTarget method and basically sets the target to the object being shot at
    public void LaunchArrow(Transform rightHand, Transform leftHand, Health target)
    {
        Projectile projInst = Instantiate(projWeapon, WeaponTransform(rightHand, leftHand).position, Quaternion.identity);
        projInst.SetTarget(target, hitDamage);
    }

    //stores hitDamage
    public float getHit()
    {
        return hitDamage;
    }

    //stores rangeDamage
    public float getRange()
    {
        return rangeDamage;
    }

    public float getHitTime()
    {
        return timeBetweenHits;
    }
} 
