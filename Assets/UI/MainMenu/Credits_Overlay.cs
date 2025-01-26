using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits_Overlay : UI_Overlay
{
    //copied from MainMenu_Overlay with terms changed from Credits to MainMenu
    [SerializeField] MainMenu_Overlay mainmenuUIPrefab;

    public void pressed_Back()
    {
        GameManager.Instance.get_UIMaster().OpenOverlay<MainMenu_Overlay>(mainmenuUIPrefab.gameObject);
    }
}
