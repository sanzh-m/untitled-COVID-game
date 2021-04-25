using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SmartEnemy : IndestructibleEnemy
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool discoveredTarget = false;

    Seeker seeker;
    private Collider2D coll;

    private bool facingLeft = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        seeker = GetComponent<Seeker>();

        InvokeRepeating("CalcPath", 0f, .5f);
    }

    void CalcPath()
    {
        if (!discoveredTarget && Vector2.Angle((Vector2)target.position - rb.position, facingLeft ? Vector2.left : Vector2.right) < 45 && ((Vector2)target.position - rb.position).magnitude < 5)
        {
            discoveredTarget = true;
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
