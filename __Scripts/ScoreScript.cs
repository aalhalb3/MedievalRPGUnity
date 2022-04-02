using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int scoreValue = 0;
    Text score; //declaring score of type Text

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();//assigns the Text component of the object to score
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "XP: " + scoreValue; //displays the score text plus the number of the score and keeps updating

    }
}
