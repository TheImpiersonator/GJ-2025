using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HUDScript : UI_Overlay
{
    [SerializeField] PlayerController player;       //Reference for hte player that this HUD is attached to 
    [SerializeField] TextMeshProUGUI timeDisplay;   //Time Display Text Reference
    [SerializeField] TextMeshProUGUI pointsDisplay; //point Display Text Reference
    [SerializeField] UnityEngine.UI.Image HPFill;

    private void Start()
    {
        /*====| SUBSCRIBE TO GAME EVENTS |====*/
        //___Timer Display Update
        GameManager.Instance.OnTimerUpdate += UpdateTimer;
        //___Score Update
        GameManager.Instance.OnScoreUpdate +=  UpdatePointDisplay;
        //___Health Display for player health
        HealthComponent playerHealth = player.pawn.GetComponent<HealthComponent>();
        playerHealth.OnDamaged += UpdateHealthDisplay; //Damaged
        playerHealth.OnHeal += UpdateHealthDisplay;    //Healed
    }

    //Updates the Health Bar to the current values found within the health system given
    void UpdateHealthDisplay() {
        HealthComponent playerHealth = player.pawn.GetComponent<HealthComponent>();
        HPFill.fillAmount = Mathf.Clamp01(playerHealth.get_HealthPercent());
    }

    //Updates the score display to the current saved score
    void UpdatePointDisplay() { 
        pointsDisplay.text = GameManager.Instance.Get_Score().ToString();
    }
    //Updates the timer to the current time of the round
    void UpdateTimer(float newTime) {

        int ceiltime = Mathf.CeilToInt(newTime);
        timeDisplay.text = ceiltime.ToString();
    }


    void FullUpdate() { 
        UpdateHealthDisplay();
        UpdatePointDisplay();
    }
}