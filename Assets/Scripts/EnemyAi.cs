using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAi : MonoBehaviour
{

	public Transform target;
	public float speed = 3f;
	public float nextWaypointDistance = 3f;
	private Path path;
	private int currentWaypoint = 0;
	private bool reachedEndOfPath = false;
	private Seeker seeker;
	private Rigidbody2D rb;
	private Vector2 direction;
	private Vector2 force;
	private float distance;
	private Enemy enemy;

	void Start()
	{
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();
		enemy = GetComponent<Enemy>();
		InvokeRepeating("UpdatePath", 0f, 0.5f);		
	}

	void UpdatePath()
	{
		if(seeker.IsDone())
		{
			if(enemy.Health < 5)
			{
				seeker.StartPath(rb.position, GameManager.Instance.exit.transform.position, OnPathComplete);
			} else
			{
				seeker.StartPath(rb.position, target.position, OnPathComplete);
			}
			
		}		
	}

	private void OnPathComplete(Path p)
	{
		if (!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
	}

	// Update is called once per frame
	void FixedUpdate()
    {
		if (path == null || !enemy.detectedPlayer)
			return;

		if(currentWaypoint >= path.vectorPath.Count)
		{
			reachedEndOfPath = true;
			return;
		}
		else
		{
			reachedEndOfPath = false;
		}

		direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

		force = direction * speed * Time.deltaTime;
		rb.AddForce(force);

		distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

		if(distance < nextWaypointDistance)
		{
			currentWaypoint++;
		}
	}
}
