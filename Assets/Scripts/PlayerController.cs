using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] LayerMask platformLayerMask;
    Rigidbody2D rigidbody2D;
    Animator animator;
    bool jumpInput;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(jumpInput && isGrounded())
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
        //rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rigidbody2D.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        jumpInput = Input.GetKey(KeyCode.Space);
        animator.SetBool("Grounded", isGrounded());
        animator.SetFloat("Direction Y", rigidbody2D.velocity.y);
    }

    bool isGrounded()
    {
        Collider2D groundCollider = Physics2D.OverlapBox(rigidbody2D.position + Vector2.down*0.7f, new Vector2(0.5f,0.5f), 0.0f, platformLayerMask);
        return groundCollider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(rigidbody2D.position + Vector2.down * 0.7f, new Vector2(0.5f, 0.5f));
    }
}
