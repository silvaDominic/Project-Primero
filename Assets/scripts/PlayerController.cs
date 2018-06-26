using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementForce = 15f;
    public float maxMovementSpeed = 2f;
    public float jumpForce = 100f;
    public float doubleJumpForce = 75f;
    [Tooltip("The amount the movement force will be divded by when moving from left to right in the air")]
    public float airBornMovementDetraction = 0;
    public bool grounded = false;

    private Rigidbody2D rb2d;
    private int lockAxis = 0;
    private Animator anim;


    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    private void Start() {
        // Check for controller
        foreach (string controller in Input.GetJoystickNames()) {
            Debug.Log(controller);
        }
    }

    // Update is called once per frame
    void Update() {

        // Prevent rotation of player due to physics
        transform.rotation = Quaternion.Euler(new Vector3(lockAxis, lockAxis, lockAxis));

        anim.SetBool("isGrounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("LeftJoystickHorizontal")));
    }

    private void FixedUpdate() {
        float LeftJoyH = Input.GetAxis("LeftJoystickHorizontal");
        float LeftJoyV = Input.GetAxis("LeftJoystickVertical");

        rb2d.AddForce((Vector2.right * movementForce) * LeftJoyH);

        //if (rb2d.velocity.x > maxMovementSpeed) {
        //    rb2d.velocity = new Vector2(maxMovementSpeed, rb2d.velocity.y);
        //    Debug.Log("Engaged");
        //}

        //if (rb2d.velocity.x < -maxMovementSpeed) {
        //    rb2d.velocity = new Vector2(-maxMovementSpeed, rb2d.velocity.y);
        //    Debug.Log("Engaged");
        //}

        //rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

    }
}

/*TODO 
 * 
 * Create action states for characters
 * Create switch statement for movement (maybe)
 * Get scale right
 * Create camera that follows center of action (not character)
 * 
 */
