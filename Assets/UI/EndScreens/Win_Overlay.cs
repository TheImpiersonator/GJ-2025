using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine;

public class Win_Overlay : UI_Overlay
{
    [SerializeField] Text scoreTxt;


    void UpdateScore() {
        scoreTxt.text = GameManager.Instance.Get_Score().ToString();
    }
}
