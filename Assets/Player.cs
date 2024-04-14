using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool canMove = true;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool up, down, left, right, anyinput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetInput();
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
    }

    void GetInput()
    {
        up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        anyinput = up || down || left || right;
    }

    void Movement()
    {
        if (canMove)
        {
            velocity = Vector2.zero;

            // Check diagonal directions first
            if (up && right && !left && !down) // North-East
                velocity = new Vector2(speed, speed).normalized * speed;
            else if (up && left && !right && !down) // North-West
                velocity = new Vector2(-speed, speed).normalized * speed;
            else if (down && right && !left && !up) // South-East
                velocity = new Vector2(speed, -speed).normalized * speed;
            else if (down && left && !right && !up) // South-West
                velocity = new Vector2(-speed, -speed).normalized * speed;
            else if (right && !left && !up && !down) // East
                velocity = new Vector2(speed, 0);
            else if (left && !right && !up && !down) // West
                velocity = new Vector2(-speed, 0);
            else if (up && !down && !left && !right) // North
                velocity = new Vector2(0, speed);
            else if (down && !up && !left && !right) // South
                velocity = new Vector2(0, -speed);
        }

        // Apply the velocity to the Rigidbody
        if (anyinput)
            rb.velocity = Vector2.Lerp(rb.velocity, velocity, 0.2f); 
        else
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.2f);
    }

    public void SetMovement(bool canMove)
    {
        this.canMove = canMove;
    }
}
