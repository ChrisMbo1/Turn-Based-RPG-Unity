using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;

    void FixedUpdate()
    {
        float horizontal =  0f;
        float vertical = 0f ;


        if(Keyboard.current != null)
        {
            if(Keyboard.current.wKey.isPressed) vertical += 1;
            if(Keyboard.current.sKey.isPressed) vertical -= 1;
            if(Keyboard.current.aKey.isPressed) horizontal -= 1;
            if(Keyboard.current.dKey.isPressed) horizontal += 1;
        }

        if(horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
        {
            FlipDirection();
        }

        anim.SetFloat("horizontal", Mathf.Abs (horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;
    }

    void FlipDirection()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
