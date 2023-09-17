using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// GameObject to finish the Level
/// </summary>
public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private UnityEvent finish;
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.tag == "Player")
        {
            finish.Invoke();
        }
    }
}
