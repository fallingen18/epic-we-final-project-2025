using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class DualGrabSpawn : MonoBehaviour
{
    private GrabInteractable grabInteractable;
    private List<IInteractorView> currentGrabbers = new List<IInteractorView>();

    public GameObject objectToSpawn;
    public Transform spawnPoint;

    private bool hasSpawned = false;

    void Awake()
    {
        grabInteractable = GetComponent<GrabInteractable>();
    }

    void Update()
    {
        if (grabInteractable == null) return;

        // Get all current interactors (hands/controllers grabbing this object)
        currentGrabbers.Clear();
        foreach (var interactorView in grabInteractable.Interactors)
        {
            if (interactorView != null)
            {
                currentGrabbers.Add(interactorView);
            }
        }

        // Check if two hands are grabbing
        if (currentGrabbers.Count == 2 && !hasSpawned)
        {
            Debug.Log("Second hand grabbed — spawning object!");
            SpawnObject();
            hasSpawned = true;
        }
        else if (currentGrabbers.Count < 2)
        {
            hasSpawned = false;
        }
    }

    private void SpawnObject()
    {
        if (objectToSpawn != null && spawnPoint != null)
        {
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Missing objectToSpawn or spawnPoint reference.");
        }
    }
}
