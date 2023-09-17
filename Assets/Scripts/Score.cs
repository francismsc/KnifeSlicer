using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Handles the Score Value
/// </summary>
public class Score : MonoBehaviour
{
    static Score scoreSystem;
    private int score;
    private int combo;
    [SerializeField]
    private UnityEvent changedScore;
    [SerializeField]
    private UnityEvent changedCombo;
    public static Score ScoreSystem
    {
        get
        {
            if(scoreSystem == null)
            {
                scoreSystem = GameObject.FindObjectOfType<Score>();
                if(scoreSystem == null)
                {
                    GameObject container = new GameObject("Score");
                    scoreSystem = container.AddComponent<Score>();
                }
            }
            return scoreSystem;
        }
    }
    public void Start()
    {
        score = 0;
        combo = 0;
    }
    public void IncreaseScore(int objectValue)
    {
        score += objectValue * (combo+1);
        changedScore.Invoke();
    }
    public void AddCombo()
    {
        combo++;
        changedCombo.Invoke();
    }
    public void ResetCombo()
    {
        combo = 0;
        changedCombo.Invoke();
    }
    public int GetScore()
    {
        return score;
    }
    public int GetCombo()
    {
        return combo;
    }
}
