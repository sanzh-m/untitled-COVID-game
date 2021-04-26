using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SmartEnemy : IndestructibleEnemy
{

    public Transform target;
    public float speed = 300f;
    public float nextWaypointDistance = .5f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool discoveredTarget = false;

    Seeker seeker;
    private Collider2D coll;
    private bool facingLeft;
    private Transform transform;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        seeker = GetComponent<Seeker>();
        transform = GetComponent<Transform>();
        if (transform.localScale.x >= 0f) facingLeft = false;
        else facingLeft = true;

        InvokeRepeating("CalcPath", 0f, .05f);
    }

    void CalcPath()
    {
        if (!discoveredTarget && Vector2.Angle((Vector2)target.position - rb.position, facingLeft ? Vector2.left : Vector2.right) < 45 && ((Vector2)target.position - rb.position).magnitude < 5)
        {
            discoveredTarget = true;
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", true);
        }
        else if (discoveredTarget && seeker.IsDone()) {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        if ((force.x >= 0.01f && transform.localScale.x <= -0.01f) || (force.x <= -0.01f && transform.localScale.x >= 0.01f))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    private void Move()
    {
        
    }

}
