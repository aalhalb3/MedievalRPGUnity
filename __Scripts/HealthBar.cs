using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health healthComponent = null;
    [SerializeField] RectTransform foreground = null;

    // Update is called once per frame
    void Update()
    {
        foreground.localScale = new Vector3(healthComponent.FractionOfHealth(), 1, 1);
    }
}
