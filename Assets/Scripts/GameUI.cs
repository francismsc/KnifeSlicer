using TMPro;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Handle GameUI
/// </summary>
public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreUI;
    [SerializeField]
    private GameObject comboUI;
    [SerializeField]
    private GameObject winUI;
    [SerializeField]
    private GameObject loseUI;
    [SerializeField]
    private GameObject settingsUI;
    [SerializeField]
    private UnityEvent Win;
    [SerializeField]
    private UnityEvent Lose;
    public void OnWin()
    {
        winUI.SetActive(true);
        Lose.Invoke();
    }
    public void OnLose()
    {
        loseUI.SetActive(true);
        Win.Invoke();
    }
    public void OnScoreChange()
    {
        scoreUI.GetComponentInChildren<TextMeshProUGUI>().text = "Score:" + Score.ScoreSystem.GetScore().ToString();
    }
    public void OnComboChange()
    {
        comboUI.GetComponentInChildren<TextMeshProUGUI>().text = "Combo:" + Score.ScoreSystem.GetCombo();
    }
}
