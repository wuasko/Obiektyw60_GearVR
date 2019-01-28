using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>WheelchairMovement</c> handles steering of the wheelchair.
/// </summary>
public class WheelchairMovement : MonoBehaviour {

    /// <summary>
    /// Parameter <c>m_MovementAcceleration</c> controls the rate at which the wheelchair gains forward speed while accelerating.
    /// </summary>
    public float m_MovementAcceleration = 1f;
    /// <summary>
    /// Parameter <c>m_MaxMovementSpeed</c> controls the value of the terminal velocity of the wheelchair. Wheelchair cannot accelerate above that.
    /// </summary>
    public float m_MaxMovementSpeed = 2f;
    /// <summary>
    /// Parameter <c>m_MovementDecceleration</c> controls the rate at which the wheelchair loses speed while not accelerating.
    /// </summary>
    public float m_MovementDecceleration = 2.5f;
    /// <summary>
    /// Parameter <c>m_RotationAcceleration</c> controls the rate at which the wheelchair gains speed of rotation while turning.
    /// </summary>
    public float m_RotationAcceleration = 10f;
    /// <summary>
    /// Parameter <c>m_MaxRotationSpeed</c> controls the value of the terminal velocity of rotation of the wheelchair. Wheelchair cannot rotate faster than that.
    /// </summary>
    public float m_MaxRotationSpeed = 20f;
    /// <summary>
    /// Parameter <c>m_RotationDeccelration</c> controls the rate at which the wheelchair loses speed of rotation while not turning (that is, input is 0).
    /// </summary>
    public float m_RotationDeccelration = 25f; //!< Rate of wheelchair losing rotating speed while not rotating.

    /* unimplemented
    /// <summary>
    /// Variable <c>m_Remote</c> holds reference to the game object of remote controller needed to steer the wheelchair.
    /// </summary>
    public GameObject m_Remote;
    */

    /// <summary>
    /// Instance variable <c>m_MovementInput</c> holds the value of the input controlling movement forward and backward.
    /// </summary>
    /// <see cref="Update"/>
    private float m_MovementInput;
    /// <summary>
    /// Instance variable <c>m_RotationInput</c> holds the value of the input controlling rotation left and right.
    /// </summary>
    /// <see cref="Update"/>
    private float m_RotationInput;
    /* unimplemented
    /// <summary>
    /// Instance variable <c>m_BreakInput</c> holds the value of the input controlling breaking movement and rotation immediatelly.
    /// </summary>
    /// <see cref="Update"/>
    private float m_BreakInput;
    */

    /// <summary>
    /// Instance variable <c>m_MovementSpeed</c> holds the value of the movement speed of the wheelchair.
    /// </summary>
    /// <see cref="Move"/>
    private float m_MovementSpeed = 0f;
    /// <summary>
    /// Instance variable <c>m_RotationSpeed</c> holds the value of the turning speed of the wheelchair.
    /// </summary>
    /// <see cref="Turn"/>
    private float m_RotationSpeed = 0f;

    /// <summary>
    /// Variable <c>rb</c> holds reference to Unity's physics component governing the wheelchair's physics.
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// Function <c>Awake</c> is run once, when game starts. It finds the wheelchair's physics component and stores a reference to it in <c>rb</c>.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
 
    /// <summary>
    /// Function <c>Update</c> is called once per frame. Takes user input from both joysticks, sums it by axes and stores those values to corresponding input variables <c>m_MovementInput</c> and <c>m_RotationInput</c>.
    /// </summary>
    void Update()
    {
        // Getting user input
        ///m_MovementInput = Input.GetAxis("Vertical"); // W, S; moving joystick up or down /// obsolete implemention, intented for cross-platform
        ///m_RotationInput = Input.GetAxis("Horizontal"); // A, D; moving joystick left or right /// obsolete
        m_MovementInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y + OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y; // sum of vertical axes of both joysticks
        m_RotationInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x + OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x; // sum of horizontal axes of both  joysticks
        
        ///m_BreakInput = Input.GetAxis("Break"); // space /// unimplemented
    }

    /// <summary>
    /// Function <c>FixedUpdate</c> is called once per frame to calculate the physics. Calls functions <c>Move</c> and <c>Turn</c> to execute movement.
    /// </summary>
    void FixedUpdate()
    {
        Move();
        Turn();
    }

    /// <summary>
    /// Function <c>Move</c> handles movement forward and backward.
    /// </summary>
    private void Move()
    {
        m_MovementSpeed = m_MovementSpeed + m_MovementInput * m_MovementAcceleration * Time.fixedDeltaTime; // when forward/backward motion button is pressed speed is increased at a rate controlled by acceleration variable
        if (m_MovementSpeed > m_MaxMovementSpeed) m_MovementSpeed = m_MaxMovementSpeed; // caps the forward speed at max speed
        if (m_MovementSpeed < -m_MaxMovementSpeed) m_MovementSpeed = -m_MaxMovementSpeed; // caps the backward speed at max speed

        // when forward/backward motion button is not pressed speed is decreased at rate controlled by decceleration variable
        if (m_MovementInput == 0)
        {
            // speed can be forward or backward
            if (m_MovementSpeed > 0) // forward
            {
                m_MovementSpeed = m_MovementSpeed - m_MovementDecceleration * Time.fixedDeltaTime;
                if (m_MovementSpeed < 0) m_MovementSpeed = 0; // caps decreasing speed on 0
            }
            if (m_MovementSpeed < 0) // backward
            {
                m_MovementSpeed = m_MovementSpeed + m_MovementDecceleration * Time.fixedDeltaTime;
                if (m_MovementSpeed > 0) m_MovementSpeed = 0; // caps decreasing speed on 0
            }
        }

        /* unimplemented
        // break button stops motion faster
        if (m_MovementInput == 0)
        {
            // speed can be forward or backward
            if (m_MovementSpeed > 0)
            {
                m_MovementSpeed = m_MovementSpeed - 5 * m_MovementDecceleration * Time.fixedDeltaTime;
                if (m_MovementSpeed < 0) m_MovementSpeed = 0; // caps decreasing speed on 0
            }
            if (m_MovementSpeed < 0)
            {
                m_MovementSpeed = m_MovementSpeed + 5 * m_MovementDecceleration * Time.fixedDeltaTime;
                if (m_MovementSpeed > 0) m_MovementSpeed = 0; // caps decreasing speed on 0
            }
        }
        */
        
        Vector3 movement; // vector of forward/backwards motion
        movement = transform.forward * m_MovementSpeed * Time.fixedDeltaTime; // Time.deltaTime allows for parameter to be speed in meters per second instead of meters per frame

        rb.MovePosition(rb.position + movement); // transforms object's position
    }
    
    /// <summary>
    /// Function <c>Turn</c> handles rotation left and right.
    /// </summary>
    private void Turn()
    {
        m_RotationSpeed = m_RotationSpeed + m_RotationInput * m_RotationAcceleration * Time.fixedDeltaTime; // when rotation button is pressed rotation speed is increased at a rate controlled by acceleration variable
        if (m_RotationSpeed > m_MaxRotationSpeed) m_RotationSpeed = m_MaxRotationSpeed; // caps the speed at max speed

        // when rotation button is not pressed speed is decreased at rate controlled by decceleration variable
        if (m_RotationInput == 0)
        {
            // speed can be left or right
            if (m_RotationSpeed > 0)
            {
                m_RotationSpeed = m_RotationSpeed - m_RotationDeccelration * Time.fixedDeltaTime;
                if (m_RotationSpeed < 0) m_RotationSpeed = 0; // caps decreasing speed on 0
            }
            if (m_RotationSpeed < 0)
            {
                m_RotationSpeed = m_RotationSpeed + m_RotationDeccelration * Time.fixedDeltaTime;
                if (m_RotationSpeed > 0) m_RotationSpeed = 0; // caps decreasing speed on 0
            }
        }

        /* unimplemented
        // break button stops motion faster
        if (m_RotationInput == 0)
        {
            // speed can be left or right
            if (m_RotationSpeed > 0)
            {
                m_RotationSpeed = m_RotationSpeed - 5 * m_RotationDeccelration * Time.fixedDeltaTime;
                if (m_RotationSpeed < 0) m_RotationSpeed = 0; // caps decreasing speed on 0
            }
            if (m_RotationSpeed < 0)
            {
                m_RotationSpeed = m_RotationSpeed + 5 * m_RotationDeccelration * Time.fixedDeltaTime;
                if (m_RotationSpeed > 0) m_RotationSpeed = 0; // caps decreasing speed on 0
            }
        }
        */

        float turn = m_RotationSpeed * Time.fixedDeltaTime; // Time.deltaTime allows for parameter to be speed in meters per second instead of meters per frame

        Quaternion rotation; // quaternion of left/right rotation
        rotation = Quaternion.Euler(0f, turn, 0f); // we can only turn left/right, rotating around the Y axis, so axes X and Z remain unmodified

        rb.MoveRotation(rb.rotation * rotation); // transforms objects' rotation
    }

    /* unimplemented
    /// <summary>
    /// Function <c>OnCollisionEnter</c> is executed when the wheelchair's collider detects a collision with another collider. It stops all wheelchair movement when collision occurs with any game object possessing a collider component except <c>Floor</c>.
    /// <summary>
    /// <param><c>collision</c> is Unity's class containing information about the collision: contact points, impact velocity etc. See Unity documentation for more details.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Floor") // stop wheelchair if it collides with anything except floor
        {
            ///m_MovementSpeed = 0;
            ///m_RotationSpeed = 0;
        }
    }
    */
}
