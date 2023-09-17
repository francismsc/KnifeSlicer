using System.Collections;
using UnityEngine;
/// <summary>
/// GameObjects that can be Sliced 
/// </summary>
public class Sliceable : MonoBehaviour
{
    [SerializeField]
    private int value = 100;
    public void OnTriggerCut()
    {
        Score.ScoreSystem.IncreaseScore(value);
        Score.ScoreSystem.AddCombo();
        Destroy(gameObject,2);
    }

}
