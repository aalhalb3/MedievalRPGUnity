using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    Health targetEnemy = null;
    float damage = 0;

    //In this update method, the projectile (arrow) faces the object being shot at and goes at the speed provided by the user
    void Update()
    {
        if (targetEnemy == null) return;
        transform.LookAt(TargetLocation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    //This method returns the location at which the projectile (arrow) will go to. In this case, the projectile goes to the CapsuleCollider height divided by 2 (around the enemy's stomach)
    private Vector3 TargetLocation()
    {
        CapsuleCollider targetCollider = targetEnemy.GetComponent<CapsuleCollider>();
        if (targetCollider == null)
        {
            return targetEnemy.transform.position;
        }
        return targetEnemy.transform.position + Vector3.up * targetCollider.height / 2;
    }

    //This method stores the target's location as well as the amount of damage
    public void SetTarget(Health targetEnemy, float damage)
    {
        this.targetEnemy = targetEnemy;
        this.damage = damage;
    }

    //OnTriggerEnter is a collider event where when the projectile hits the collider component of the enemy, the projectile(arrow) itself gets destroyed and disappears
    private void OnTriggerEnter(Collider collide)
    {
        if (collide.GetComponent<Health>() != targetEnemy) return;
        targetEnemy.TakingDamage(damage);
        Destroy(gameObject);
    }
}
