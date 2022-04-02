using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customize : MonoBehaviour
{
    public GameObject swordMan;
    public GameObject bowMan;
    public Button swordManButton;
    public Button bowManButton;
    public GameObject cam;
    public GameObject ingameCan;

    void Start()
    {
        //The line below is a click button event where when one of the buttons is clicked, then SwordButtonClicked method/BowButtownClicked method will be executed
        swordManButton.GetComponent<Button>().onClick.AddListener(SwordButtonClicked);
        bowManButton.GetComponent<Button>().onClick.AddListener(BowButtonClicked);
    }

    //This method means when the Sword Man button is clicked, then instantiate the swordMan prefab player and destroy the canvas. Also, enable the CameraController script (initially disabled)
    private void SwordButtonClicked() {
        Instantiate(swordMan);
        Destroy(GameObject.Find("Canvas"));
        cam.GetComponent<CameraController>().enabled = true;
        ingameCan.GetComponent<Canvas>().enabled = true;
    }

    //This method means when the Bow Man button is clicked, then instantiate the bowMan prefab player and destroy the canvas. Also, enable the CameraController script (initially disabled)
    private void BowButtonClicked()
    {
        Instantiate(bowMan);
        Destroy(GameObject.Find("Canvas"));
        cam.GetComponent<CameraController>().enabled = true;
        ingameCan.GetComponent<Canvas>().enabled = true;
    }
}
