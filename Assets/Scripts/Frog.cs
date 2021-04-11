using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{

    [SerializeField] private float leftCap;

    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength;

    [SerializeField] private float jumpHeight;

    [SerializeField] private LayerMask ground;

    private Collider2D coll;




    private bool facingLeft = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move();
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < .1f)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }

        else if (anim.GetBool("Falling") && coll.IsTouchingLayers(ground))
        {
            anim.SetBool("Falling", false);
        }
    }


    private void Move() 
    {

        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {

                // Make sure sprite is facing right location, and if not, face right direction
                // but only if enemy is on the ground
                if (transform.localScale.x != 1 && coll.IsTouchingLayers(ground))
                {
                    transform.localScale = new Vector3(1, 1);
                }
                // Jump if touching layer
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);

                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else 
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1 && coll.IsTouchingLayers(ground))
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

    private void Poppin()
    {

    }

}
