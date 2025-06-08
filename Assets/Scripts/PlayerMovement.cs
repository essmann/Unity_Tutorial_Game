using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement :MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform transform;

    public int facingDirection = 1;
    
  
    //called once per frame
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        animator.SetFloat("horizontal", Math.Abs(horizontal));
        animator.SetFloat("vertical", Math.Abs(vertical));
        //reads input for A or left arrow
        if(horizontal < 0 && transform.localScale.x > 0
            || horizontal > 0 && transform.localScale.x < 0
            )
        {
            Flip();
        }
        
        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;


    }
    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

    }
}
