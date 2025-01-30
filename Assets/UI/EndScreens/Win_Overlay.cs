using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Win_Overlay : UI_Overlay
{
    [SerializeField] TextMeshProUGUI scoreTxt;

    private void Awake()
    {
        UpdateScore();
    }

    void UpdateScore() {
        scoreTxt.text = GameManager.Instance.Get_Score().ToString();
    }

    public void GoBack() {
        SceneManager.LoadSceneAsync(0);
    }


}
