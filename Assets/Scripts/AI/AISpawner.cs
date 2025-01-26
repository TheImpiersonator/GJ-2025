using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AISpawner : Spawner {
    [Tooltip("The controller to spawn with the AI character prefab")]
    [SerializeField] private GameObject controllerPrefab;
    [Tooltip("How much to offset the prefab when it is spawned")]
    [SerializeField] private Vector3 spawnOffset;

    private GameObject spawnedController;

    private void Start() {
        spawnedController = Object.Instantiate(controllerPrefab, transform);
    }

    public override void ResetSpawner() {
        Destroy(spawnedObject);
    }

    public override void ActivateSpawner() {
        //spawn object and set as variable
        spawnedObject = Object.Instantiate(spawnPrefab, transform.position + spawnOffset, transform.rotation);
        //link controller up with pawn
        spawnedController.GetComponent<Controller>().pawn = spawnedObject.GetComponent<Pawn>();
    }
}
