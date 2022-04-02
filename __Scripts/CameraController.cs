using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        //if the object instantiated is a bow man, then set the player GameObject component to bow man prefab, else if the object instantiated is a player(sword man), then set the player component to that
        if (GameObject.Find("Bow man(Clone)")) {
            player = GameObject.Find("Bow man(Clone)");
        } else if (GameObject.Find("Sword man(Clone)")){
            player = GameObject.Find("Sword man(Clone)");
        }
        
        offset = transform.position - player.transform.position;

    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

}
