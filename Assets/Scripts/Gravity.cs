using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Inverts the gravity of an object that enters 
/// the trigger collider of this object
/// </summary>
public class Gravity : MonoBehaviour
{
    [SerializeField]
    private UnityEvent invertGravity;
    [SerializeField]
    private Rigidbody rb; 
    ///Ongravity - GameObjects that are already being
    ///            affected by the gravity inversion
    private HashSet<GameObject> Ongravity = new HashSet<GameObject>();
    /// <summary>
    /// Inverts the gravity
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !Ongravity.Contains(rb.gameObject))
        {
            InvokeGravity();
            Ongravity.Add(rb.gameObject);
        }
    }
    /// <summary>
    /// Adds force to the GameObjects affected by the InvertedGravity
    /// </summary>
    /// <param name="col"></param>
    public void FixedUpdate()
    {
        if(Ongravity.Contains(rb.gameObject))
        {
            rb.AddForce(0, 9.81f, 0, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 9.81f);
        }
    }
    /// <summary>
    /// Makes the gravity normal again
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerExit(Collider col)
    {
        if (Ongravity.Contains(rb.gameObject))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.x);
            Ongravity.Remove(rb.gameObject);
            InvokeGravity();
        }
    }
    /// <summary>
    /// Invokes UnityEvents
    /// </summary>
    private void InvokeGravity()
    {
        invertGravity.Invoke();
    }
}
