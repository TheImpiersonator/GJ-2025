using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Overlay : MonoBehaviour
{
    // Add a method to initialize or show the overlay
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    // Add a method to hide the overlay
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
