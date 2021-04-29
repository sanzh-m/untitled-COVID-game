using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{

    [SerializeField] private float leftCap;

    [SerializeField] private float rightCap;

    [SerializeField] private float moveLength;

    [SerializeField] private LayerMask ground;

    private Collider2D coll;




    private bool facingLeft = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (anim.GetBool("Walking"))
        {
            if (Mathf.Abs(rb.velocity.x) < .05f)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Idle", true);
                
            }
        }
    }


    private void Move() 
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {

                if (transform.localScale.x != -1 && coll.IsTouchingLayers(ground))
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-moveLength, 0);
                    anim.SetBool("Walking", true);
                    

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
                if (transform.localScale.x != 1 && coll.IsTouchingLayers(ground))
                {
                    transform.localScale = new Vector3(1, 1);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(moveLength, 0);
                    anim.SetBool("Walking", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

}
