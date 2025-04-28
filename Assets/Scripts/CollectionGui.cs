using System;
using UnityEngine;

public class CollectionGui : MonoBehaviour
{
    private PickUpItem keyVariable; // Declare the variable

    // Initialize in Awake or Start
    void Start()
    {
        keyVariable = GameObject.Find("Player").GetComponent<PickUpItem>();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 100, 40), keyVariable.collectedKeys.ToString());
    }
}