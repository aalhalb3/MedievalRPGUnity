using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject); //game object does not get destroyed when loaded to a new scene
    }
}
