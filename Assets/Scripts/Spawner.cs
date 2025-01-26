using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [Tooltip("The prefab that this object will manage spawning and destroying")]
    public GameObject spawnPrefab;
    [HideInInspector] public GameObject spawnedObject;

    public abstract void ActivateSpawner();
    public abstract void ResetSpawner();
}
