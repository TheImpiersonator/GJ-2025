using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Spawner[] AISpawns;
    public Spawner[] ItemSpawns;

    public void InitiateTile() {
        foreach (Spawner spawner in AISpawns) {
            spawner.ActivateSpawner();
        }
        foreach (Spawner spawner in ItemSpawns) {
            spawner.ActivateSpawner();
        }
    }

    public void ResetTile() {
        foreach (Spawner spawner in AISpawns) {
            spawner.ResetSpawner();
        }
        foreach (Spawner spawner in ItemSpawns) {
            spawner.ResetSpawner();
        }
    }
}
