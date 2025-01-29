using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*====| DELEGATE EVENTS |====*/
    //___Game Start Event
    public delegate void GameStartCall();
    public GameStartCall OnGameStart;
    //___Round Timer Update Event
    public delegate void TimerUpdateCall(float timeLeft);
    public TimerUpdateCall OnTimerUpdate;
    //___Round Timer End Event
    public delegate void TimerDoneCall();
    public TimerDoneCall OnTimerDone;
    //___Score Update Event
    public delegate void ScoreUpdateCall();
    public ScoreUpdateCall OnScoreUpdate;


    /*====| GAME REFERENCES |====*/
    public static GameManager Instance; //persistent static game instance
    UIManager masterUI; //UI Manager reference

    public PlayerController player; //The Player Reference
    public TileManager[] levelTiles;//The Tiles for the levels
    public List<Pawn> ais;          //List of AIs

    /*====| GAME VARIABLES |====*/
    //___Score
    [SerializeField] int score; // The Score to the 
    //___Round Timer
    [SerializeField] float roundDuration = 10f;  //The Amount of seconds for the round
    [SerializeField] float timeLeft; //Tracks the time that's left within the round

    /*UI OVERLAY PREFABS*/
    [SerializeField] MainMenu_Script mainMenuUIPrefab;  //Main Menu
    [SerializeField] HUDScript HUDPrefab;               //HUD
    [SerializeField] GameOver_Script gameOverUIPrefab;  //Game Over
    [SerializeField] Win_Overlay WinUIPrefab;           //Win

    private void Awake()
    {
        //if we dont have an instance, make one
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //if another instance exists, delete this one
        else
        {
            Destroy(gameObject);
        }

        masterUI = GetComponent<UIManager>(); // Set up the UI Manager


        /*=====| EVENT SUBSCRIPTIONS |=====*/
        OnGameStart += StartRoundTimer;
        OnTimerDone += StopRoundTimer;
        OnTimerDone += ProcessEndRound();

    }

    /*When the program begins*/
    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            Start_MainMenu(); //Start on the Main Menu
        }
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            StartGame();
        }
    }

    /*Starts the Main Menu Level*/
    public void Start_MainMenu()
    {

        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            //Restart Level
            //Switch UI to Main Menu
            masterUI.SwitchOverlay<MainMenu_Script>(mainMenuUIPrefab.gameObject);
        }
    }
    /*Starts the Game*/
    public void StartGame()
    {
        masterUI.SwitchOverlay<HUDScript>(HUDPrefab.gameObject);

        InitRound();    //Initialize the Round

        OnGameStart.Invoke();   //invokes the Game start event

        StartRoundTimer(); // start the Round
    }
    /*Initializes the Round*/
    void InitRound()
    {
        score = 0;
        timeLeft = roundDuration;
    }
    /*Processes the end of the round call*/
    void ProcessEndRound()
    {
        /*LOSING CASES*/
        //CHECK: Player Health is depleted
        if (player.pawn.GetComponent<HealthComponent>().currentHealth <= 0)
        { Start_GameOver(); } //Game Over 

        /*WINNING CASES*/
        //CHECK: Round Time ended
        else if (timeLeft <= 0)
        { Start_WinScreen(); } //Win
    }

    //Starts the Game Over Level*/
    public void Start_GameOver()
    {
        //Open the Lose Level

        /*= OR =
            //Reload Level
            //Open the Lose Screen
            UIMaster.SwitchOverlay<GameOver_Script>(GameOverUIPrefab.gameObject);
        */


    }
    //Starts the Win Level*/
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

    /*Getter for the game's score*/
    public int Get_Score() { return score; }
    /*Adjusts the score by a set amount*/
    public void AdjustScore(int amount)
    {
        score += amount; //Adjust the score
        OnScoreUpdate?.Invoke();   //Invoke the score update event
    }

    /*====| ROUND TIMER LOGIC |====*/
    Coroutine roundTimer = null; // Refernce for the round timer coroutine
    //Starts the Round Timer
    void StartRoundTimer()
    {
        roundTimer = StartCoroutine(RoundTimer());
    }
    //Stops the Round Timer
    void StopRoundTimer()
    {
        StopCoroutine(roundTimer);
    }
    //Coroutine that handle Round timer loop logic every frame
    IEnumerator RoundTimer()
    {
        //Loop until time remaining is zero
        while (timeLeft > 0)
        {
            yield return null; //Waits for the next Frame
            timeLeft -= Time.deltaTime; //Remove the time it took from in between frames
            OnTimerUpdate?.Invoke(timeLeft); //Invoke Time update Event
        }
        Debug.Log("TIMER DONE!");
        OnTimerDone?.Invoke(); //Invoke the Timer is done
    }
}