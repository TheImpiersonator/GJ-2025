
using System.Threading;
using UnityEngine;

public class AISpawner : Spawner {
    [Tooltip("The controller to spawn with the AI character prefab")]
    [SerializeField] private GameObject controllerPrefab;

    private GameObject spawnedController;

    [SerializeField] private Color debugColor = Color.white;
    [SerializeField] private Vector3 cubeSize = new Vector3(1, 1, 1);

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

    public void OnDrawGizmos() {
        Gizmos.color = debugColor;
        Gizmos.DrawWireCube(transform.position + spawnOffset, cubeSize);
    }
}
