using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private UnityEvent lost;
    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.transform.tag == "Player")
        {
            LostAction();
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.tag == "Player")
        {
            LostAction();
        }
    }

    public void LostAction()
    {
        lost.Invoke();
    }

}
