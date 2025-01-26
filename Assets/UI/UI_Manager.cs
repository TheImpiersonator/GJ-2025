using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    List<UI_Overlay> UI_Order = new List<UI_Overlay>();

    public T OpenOverlay<T>(GameObject prefab) where T : UI_Overlay {
        T overlay = Instantiate(prefab).GetComponent<T>(); // Either will Instantiate or Find the Type of Overlay
        UI_Order.Add(overlay); //Add it to the List Order
        
        get_TopOverlay().Show(); //Display the top Overlay

        return overlay;
    }

    public void SwitchOverlay<T>(GameObject prefab) where T : UI_Overlay {
        ClearOverlays();
        OpenOverlay<T>(prefab);
    }

    public void BringToFront(int index)
    {
        //GUARD: Ensure the index is within Bounds
        if (index < 0 || index >= UI_Order.Count) return;

        UI_Overlay overlay = UI_Order[index];
        UI_Order.RemoveAt(index);
        UI_Order.Add(overlay); // Move it to the end

        get_TopOverlay().Show(); // Ensure the Top Overlay is Shown
    }

    public void CloseTopOverlay() {
        UI_Overlay overlay = get_TopOverlay();
        UI_Order.Remove(get_TopOverlay());

        Destroy(overlay.gameObject);
    }

    public void ClearOverlays() {
        UI_Order.Clear();
    }

    UI_Overlay get_TopOverlay() {
        return UI_Order.Count > 0 ? UI_Order[UI_Order.Count - 1] : null;
    }
}
