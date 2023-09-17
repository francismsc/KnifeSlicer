using UnityEngine;
/// <summary>
/// Cuts the Sliceable GameObjects in Half
/// </summary>
public class Cutting : MonoBehaviour
{
    [SerializeField]
    Vector3 leftsideAngle;
    [SerializeField]
    Vector3 rightsideAngle;
    /// <summary>
    /// If collides with a Sliceable GameObject
    /// Sens the left and right side in different directions
    /// by adding force to each of them
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Sliceable" )
        {
            col.gameObject.GetComponent<BoxCollider>().enabled = false;
            col.gameObject.GetComponent<Sliceable>().OnTriggerCut();
            foreach(Transform child in col.gameObject.transform)
            {
                if(child.tag == "Left")
                {
                    child.gameObject.GetComponent<BoxCollider>().enabled = false;
                    child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    child.GetComponent<Rigidbody>().AddForce(leftsideAngle);
                }
                else if(child.tag == "Right")
                {
                    child.gameObject.GetComponent<BoxCollider>().enabled = false;
                    child.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    child.GetComponent<Rigidbody>().AddForce(rightsideAngle);
                }
                else
                {
                    Debug.LogError("Child does not have an acceptable tag.",child);
                }
            }
        }
    }
}
