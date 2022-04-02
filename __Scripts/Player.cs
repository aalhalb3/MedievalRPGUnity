using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Weapon thirdWeapon = null;
    private void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    //Update method returns the RespondToCombat and MoveInteract method each frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Switching weapons");
            GetComponent<Combat>().switchWeapon();
            return;
        }
        if (RespondToCombat()) return;
        if (MoveInteract()) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("jump");
            GetComponent<Animator>().SetTrigger("stopJumping");
        }
    }

    //This method of type boolean goes through a loop and checks if the raycast initiated by the mouse click hits the enemy. If it does, then the player goes to the enemy and initiates the fight method in the combat script
    private bool RespondToCombat()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetRay());
        foreach (RaycastHit touch in hits)
        {
            Target target = touch.transform.GetComponent<Target>();
            if (target == null)
            {
                continue;
            }
            //if the enemy gets left clicked on, then initiate the fight method from combat script
            if (Input.GetMouseButtonDown(0)) {
                GetComponent<Combat>().Fight(target);
            }
            return true;
        }
        return false;
    }

    private bool MoveInteract()
    {
    //this function allows the camera to shoot a ray to the point that the player clicks on. The character identifies the location of the ray and moves to the point.
        RaycastHit touch;
        bool isHit = Physics.Raycast(GetRay(), out touch);

        if (isHit)
        {
            if (Input.GetMouseButtonDown(0)) { //if the left mouse button is clicked then active the MoveToTarget function
                GetComponent<Mover>().StartMove(touch.point);
            }
            return true;
        }
        return false;
    }

    //This Ray method returns the mouse position of the raycast
    private static Ray GetRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    [Obsolete]
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("touched")) {
            ScoreScript.scoreValue += 200;
            GetComponent<Combat>().equipThird(thirdWeapon);
            this.gameObject.transform.position = new Vector3(-100, -0.14f, 140);
            Application.LoadLevel(1);
            SceneScript.currSceneValue = 2;
        }
    }
}
