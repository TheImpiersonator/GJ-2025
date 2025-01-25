using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public PlayerController player;

    private void Awake() {
        //if we dont have an instance, make one
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //if another instance exists, delete this one
        else {
            Destroy(gameObject);
        }
    }
}
