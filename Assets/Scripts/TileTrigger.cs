using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TileTrigger : MonoBehaviour
{
    //tile manager
    public TileManager manager;
    //tells the trigger not to teleport until player has exited trigger
    public bool waitTillExit = false;

    [SerializeField] private bool teleportTrigger = false;
    [Tooltip("The trigger the player will get teleported into. (Prevents infinite teleport loop)")]
    [SerializeField] private TileTrigger otherTrigger;
    [Tooltip("The GameObject that this trigger will use as a reference to teleport the player to")]
    [SerializeField] private GameObject teleportRef;

    private void OnTriggerEnter(Collider other) {
        //caching the player pawn for ease of access
        Pawn player = GameManager.Instance.player.pawn;
        //dont want AI or projectiles messing with this
        if (other == player.gameObject.GetComponent<CharacterController>()) {
            //toggle tile state
            manager.ToggleTile();
            //for acting as a teleporter also
            if (teleportTrigger) {
                //if not prevented from teleporting
                if (!waitTillExit) {
                    //tell other trigger not to teleport player until they have exited
                    //(they still needa activate the trigger so the tile activates)
                    otherTrigger.waitTillExit = true;
                    //teleport player telative to their x and y axis
                    Vector3 basePos = player.gameObject.transform.position;
                    player.Teleport(new Vector3(basePos.x, basePos.y, teleportRef.transform.position.z));
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        //caching the player pawn for ease of access
        Pawn player = GameManager.Instance.player.pawn;
        //dont want AI or projectiles messing with this
        if (other == player.gameObject.GetComponent<CharacterController>()) {
            //enable teleporting on triggered
            waitTillExit = false;
        }
    }
}
