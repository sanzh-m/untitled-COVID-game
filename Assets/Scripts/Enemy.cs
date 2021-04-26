using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Animator anim;

    protected Rigidbody2D rb;

    protected AudioSource explosion;

    protected virtual void Start() 
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        explosion = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public virtual bool JumpedOn() 
    {
        anim.SetTrigger("Death");
        explosion.Play();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        return true;
        

    }

    public void KillEnemy()
    {
        anim.SetTrigger("Death");
        explosion.Play();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
