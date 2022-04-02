using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject portal;
    bool isCreated = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.childCount == 0) //if childcount of object is 0 then spawn the portal
        {
            if (!isCreated)
            {
                Instantiate(portal, portal.transform.position, portal.transform.rotation);
                isCreated = true;
            }

        }
    }
}
