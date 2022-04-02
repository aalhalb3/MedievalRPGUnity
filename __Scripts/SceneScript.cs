using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public static int currSceneValue = 1;
    Text currScene;
    // Start is called before the first frame update
    void Start()
    {
        currScene = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currScene.text = "Scene: " + currSceneValue;

    }
}
