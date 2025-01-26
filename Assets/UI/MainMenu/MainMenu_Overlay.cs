using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Script : UI_Overlay
{
    [SerializeField] Credits_Overlay creditsUIPrefab;
    [SerializeField] Controls_Overlay controlsUIPrefab;

    public void pressed_Start()
    {
        GameManager.Instance.StartGame();
    }

    public void pressed_Credits()
    {
        GameManager.Instance.get_UIMaster().OpenOverlay<Credits_Overlay>(creditsUIPrefab.gameObject);
    }

    public void pressed_Controls()
    {
        GameManager.Instance.get_UIMaster().OpenOverlay<Controls_Overlay>(controlsUIPrefab.gameObject);
    }

    public void pressed_Quit()
    {
        Application.Quit();
    }
}