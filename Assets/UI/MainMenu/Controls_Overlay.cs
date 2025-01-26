using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls_Overlay : UI_Overlay
{
    [SerializeField] MainMenu_Overlay mainmenuUIPrefab;

    public void pressed_Back()
    {
        GameManager.Instance.get_UIMaster().OpenOverlay<MainMenu_Overlay>(mainmenuUIPrefab.gameObject);
    }
}
