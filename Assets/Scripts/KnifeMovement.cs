using System.Collections;
using UnityEngine;
/// <summary>
/// Handles the knife movements
/// </summary>
public class KnifeMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private Vector3 angularVelocity;
    private const float MAX_HORIZONTAL_VELOCITY = 2f;
    private const float MAX_VERTICAL_VELOCITY = 6f;
    private const float MAX_ROTATION_VELOCITY = 10f;
    public int gravity = 1;
    bool isGrounded = true;
    bool Stuck = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    /// <summary>
    /// Wait for input
    /// If the Object is in the air rotate it
    /// </summary>
    void Update()
    {
        UpdateAcceleration();
        if (isGrounded == false)
        {
            WhileInTheAir();
        }
    }
    /// <summary>
    /// If there is a touch from the player
    /// AddForce to move the GameObject
    /// </summary>
    public void UpdateAcceleration()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (Stuck)
            {
                StartCoroutine(TimerRoutine());
            }
            isGrounded = false;;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(MaxSpeedX(),MaxSpeedY()*gravity,0), ForceMode.Impulse);
            rb.AddTorque(new Vector3(angularVelocity.x, angularVelocity.y, 
                MaxRotationZ(angularVelocity.z)*gravity), ForceMode.Impulse);
        }

        if(Input.touchCount == 1)
        {
            foreach(Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (Stuck)
                    {
                        StartCoroutine(TimerRoutine());
                    }

                    isGrounded = false; ;
                    rb.isKinematic = false;
                    rb.AddForce(new Vector3(MaxSpeedX(), MaxSpeedY() * gravity, 0), ForceMode.Impulse);
                    rb.AddTorque(new Vector3(angularVelocity.x, angularVelocity.y, MaxRotationZ(angularVelocity.z) * gravity), ForceMode.Impulse);
                }
            }
        }
    }
    /// <summary>
    /// Method to stop the object from colliding
    /// multiple times in a row
    /// </summary>
    private IEnumerator TimerRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        Stuck = false;
    }
    /// <summary>
    /// When Trigger Coliding with the floor while not stuck:
    /// Stop the GameObject velocity and angularVelocity
    /// Reset Combo 
    /// GameObject is now stuck and grounded
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Floor" && !Stuck)
        {
            isGrounded = true;
            rb.isKinematic = true;
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            Score.ScoreSystem.ResetCombo();
            Stuck = true;
        }
    }
    /// <summary>
    /// If the GameObject collides with the floor
    /// Resets Combo value and GameObject is now on the ground
    /// </summary>
    /// <param name="col"></param>
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            isGrounded = true;
            Score.ScoreSystem.ResetCombo();
        }
    }
    /// <summary>
    /// Rotates the knife while in the Air
    /// </summary>
    public void WhileInTheAir()
    {
        switch (gravity)
        {
            case 1:
                if (transform.eulerAngles.z < 185 && transform.eulerAngles.z >= 95)
                {
                    rb.AddTorque(0, 0, MaxRotationZ(-2));
                }
                else if (transform.eulerAngles.z < 95)
                {
                    rb.AddTorque(0, 0, MaxRotationZ(-0.5f));
                }
                else if (transform.eulerAngles.z <= 360 && transform.eulerAngles.z >= 270)
                {
                    rb.AddTorque(0, 0, MaxRotationZ(-0.1f));
                }
                else if (transform.eulerAngles.z < 270 && transform.eulerAngles.z > 185)
                {
                    rb.AddTorque(0, 0, MaxRotationZ(-0.3f));
                }
                break;
            case 2:
                if (transform.eulerAngles.z < 275 && transform.eulerAngles.z >= 185)
                {
                    rb.AddTorque(0, 0, MaxRotationZ(2));
                }
                else if (transform.eulerAngles.z < 185 && transform.eulerAngles.z > 90)
                {
                    rb.AddTorque(0, 0, MaxRotationZ(0.5f));
                }
                else if (transform.eulerAngles.z <= 90)
                {
                    rb.AddTorque(0, 0, MaxRotationZ(0.1f));
                }
                else if (transform.eulerAngles.z < 360 && transform.eulerAngles.z > 185)
                {
                    rb.AddTorque(0, 0, MaxRotationZ(0.3f));
                }
                break;
        }
    }
    /// <summary>
    /// Checks if the force to be added goes over the MAX_ROTATION_VELOCITY
    /// then removes the extra force
    /// </summary>
    /// <param name="angularVelocity"> Torque to be added</param>
    /// <returns>Torque to be added in Z</returns>
    public float MaxRotationZ(float angularVelocity)
    {
        if (rb.angularVelocity.z + angularVelocity > MAX_ROTATION_VELOCITY)
        {
            return angularVelocity - (rb.angularVelocity.z + angularVelocity - MAX_ROTATION_VELOCITY);
        }
        return angularVelocity;
    }
    /// <summary>
    /// Diminishes the velocity value if it goes over  MAX_VERTICAL_VELOCITY
    /// </summary>
    /// <returns></returns>
    public float MaxSpeedY()
    {
        if (rb.velocity.y + velocity.y > MAX_VERTICAL_VELOCITY)
        {
            return velocity.y - (rb.velocity.y + velocity.y - MAX_VERTICAL_VELOCITY);
        }
        return velocity.y;
    }
    /// <summary>
    /// Diminishes the velocity value if it goes over  MAX_HORIZONTAL_VELOCITY
    /// </summary>
    /// <returns></returns>
    public float MaxSpeedX()
    {
        if (rb.velocity.x + velocity.x > MAX_HORIZONTAL_VELOCITY)
        {
            return velocity.x - (rb.velocity.x + velocity.x - MAX_HORIZONTAL_VELOCITY);
        }
        return velocity.x;
    }
    /// <summary>
    /// Changes the gravity variable if the object
    /// is affected by gravity inversion
    /// </summary>
    public void SetGravity()
    {
        switch(rb.useGravity)
        {
            case true:
                rb.useGravity = false;
                break;
            case false:
                rb.useGravity  = true;
                break;
        }
        gravity *= -1;
    }
}
