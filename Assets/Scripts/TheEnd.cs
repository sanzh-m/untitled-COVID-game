using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPlayer : MonoBehaviour
{
    private bool facingLeft = false;

    private Rigidbody2D rb;
    private Collider2D coll;
    [SerializeField] private Animator anim;

    private enum State { idle, running, jumping, falling, hurt, climb };
    private State state = State.idle;

    [SerializeField] private AudioSource footstep;

    private float runTime = 3.0f;
    private float idleTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        state = State.running;
        anim.SetInteger("state", (int)state);
    }

    // Update is called once per frame
    void Update()
    {
        if(runTime > 0)
        {
            transform.Translate(Vector2.right * 3.0f * Time.deltaTime);
            runTime = runTime - Time.deltaTime;
            if(runTime <= 0)
            {
                idleTime = 1.0f;
                state = State.idle;
                anim.SetInteger("state", (int)state);
            }
        }
        else 
        {
            idleTime = idleTime - Time.deltaTime;
            if(idleTime <= 0)
            {
                state = State.running;
                anim.SetInteger("state", (int)state);
                runTime = 3.0f;
            }

        }

    }

    private void Footstep()
    {
        footstep.Play();
    }


}
