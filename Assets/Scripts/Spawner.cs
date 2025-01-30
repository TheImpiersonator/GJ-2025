
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [Tooltip("The prefab that this object will manage spawning and destroying")]
    public GameObject spawnPrefab;
    [Tooltip("How much to offset the prefab when it is spawned")]
    public Vector3 spawnOffset;
    //object that will be set when spawned
    [HideInInspector] public GameObject spawnedObject;

    public abstract void ActivateSpawner();
    public abstract void ResetSpawner();
}