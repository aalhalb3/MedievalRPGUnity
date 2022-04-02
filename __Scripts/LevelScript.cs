using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public static int levelValue = 1;
    private int tempLevel = 1;
    private GameObject player;
    Text level; //declaring score of type Text

    // Start is called before the first frame update
    void Start()
    {
        
        level = GetComponent<Text>();//assigns the Text component of the object to score
        level.text = "Level: " + levelValue; //displays the level text plus the number of the level
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) { findPlayer(); } //finds the player object if it has not already been found
        levelValue = 1+ ScoreScript.scoreValue/200; //increases level for every 200 additional score
        if(levelValue> tempLevel)
        {
            tempLevel = levelValue;
            LevelUp();
        }
       
        
    }

    //level up method increases displayed level and increase stats
    void LevelUp()
    {
        level.text = "Level: " + levelValue; //displays the level text plus the number of the level
        player.transform.GetComponent<Health>().LevelUp();
        player.transform.GetComponent<Combat>().LevelUp();
    }

    // method to search for the player
    void findPlayer()
    {
        if (GameObject.Find("Bow man(Clone)"))
        {
           player = GameObject.Find("Bow man(Clone)");
        }
        else if (GameObject.Find("Sword man(Clone)"))
        {
            player = GameObject.Find("Sword man(Clone)");
        }
    }
}
