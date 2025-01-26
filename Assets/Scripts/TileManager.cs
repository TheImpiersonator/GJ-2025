using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Spawner[] AISpawns;
    public Spawner[] ItemSpawns;

    private bool active = false;

    [Tooltip("This will activate the tile on Start() for debug reasons")]
    [SerializeField] private bool debugStartActive = false;

    private void Start() {
        if(debugStartActive) {
            active = true;
        }
    }
    public void InitiateTile() {
        foreach (Spawner spawner in AISpawns) {
            spawner.ActivateSpawner();
        }
        foreach (Spawner spawner in ItemSpawns) {
            spawner.ActivateSpawner();
        }
        active = true;
    }

    public void ResetTile() {
        foreach (Spawner spawner in AISpawns) {
            spawner.ResetSpawner();
        }
        foreach (Spawner spawner in ItemSpawns) {
            spawner.ResetSpawner();
        }
        active = false;
    }

    public void ToggleTile() {
        if (active) {
            ResetTile();
        }
        else {
            InitiateTile();
        }
    }
}
