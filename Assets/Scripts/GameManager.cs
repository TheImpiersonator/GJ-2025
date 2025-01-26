using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    UIManager masterUI;

    public PlayerController player;
    public TileManager[] levelTiles;
    public List<Pawn> ais;

    public int score;
    public bool is_GamePaused = true; //Maybe could use this in spawners so player doesn't get swarmed first thing?


    //UI OVERLAY PREFABS
    [SerializeField] MainMenu_Script mainMenuUIPrefab;
    [SerializeField] HUDScript HUDPrefab;
    [SerializeField] GameOver_Script gameOverUIPrefab;
    [SerializeField] Win_Overlay WinUIPrefab;

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

        masterUI = GetComponent<UIManager>();
    }

    private void Start()
    {
        Start_MainMenu();
    }

    public void Start_MainMenu()
    {
        //Restart Level
        //Switch UI to Main Menu
        masterUI.SwitchOverlay<MainMenu_Script>(mainMenuUIPrefab.gameObject);
        //disable 
        is_GamePaused = true;
    }
    public void StartGame()
    {
        masterUI.SwitchOverlay<HUDScript>(HUDPrefab.gameObject);

        is_GamePaused = false;
        //Initialize the Game
    }

    public void Start_GameOver()
    {
        //Open the Lose Level

        /*= OR =
            //Reload Level
            //Open the Lose Screen
            UIMaster.SwitchOverlay<GameOver_Script>(GameOverUIPrefab.gameObject);
        */


    }
    public void Start_WinScreen()
    {
        //Open the Win Level

        /*= OR =
            //Reload Level
            //Open the Win Screen
            UIMaster.SwitchOverlay<Win_Overlay>(winUIPrefab.gameObject);
        */
    }

    public UIManager get_UIMaster() { return masterUI; }
}