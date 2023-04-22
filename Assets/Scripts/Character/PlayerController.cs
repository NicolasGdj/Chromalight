using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float jumpForce = 8.0f;
    public float speed = 4.0f;
    public bool editing = false;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public LayerMask collisionLayers;
    
    private Checkpoint checkpoint;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;
    
    public void SetCheckpoint(Checkpoint checkpoint) {
        this.checkpoint = checkpoint;
    }

    public Checkpoint GetCheckpoint() {
        return checkpoint;
    }

    public void SetEditing(bool editing) {
        this.editing = editing;
    }
    
    public bool IsEditing() {
        return editing;
    }

    public bool CanMove() {
        return !editing;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private float GetAxis(string axisName) {
        float axis = 0.0f;

        if (axisName == "HorizontalArrow") {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                axis = -1.0f;
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                axis = 1.0f;
            }
        }

        return axis;
    }

    void Update() {
        if(CanMove()) {
            bool isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position, collisionLayers);
            if ((Input.GetButtonDown("Jump") ||  Input.GetKeyDown(KeyCode.UpArrow) ) && isGrounded) {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            if (Input.GetKey(KeyCode.DownArrow) && !isGrounded) {
                        rb.gravityScale = 3.0f; // Change this value to adjust the fall speed
            } else {
                rb.gravityScale = 1.0f; // Reset the gravity scale
            }
        }
    }

    void FixedUpdate() {
        if(CanMove()) {
            Move(Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        } else {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Move(float horizontalMvt) {
        if (horizontalMvt > 0 && !facingRight || horizontalMvt < 0 && facingRight) {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        Vector3 target = new Vector2(horizontalMvt, rb.velocity.y);
        rb.velocity =  Vector3.SmoothDamp(rb.velocity, target, ref velocity, 0.05f);
    }
}
