﻿using System;
using UnityEngine;

public class Enemy : MovingObject
{
	public int PlayerDamage;
	public int Health = 1;
	public event EventHandler OnEnemyDeath;
	public float speed = 5f;

	private Animator _animator;
	private Transform _target;
	private Player _player;
	private bool _skipMove;
	private TDCharacterController2D _controller;

	private float moveLimiter = 0.7f;
	private float attackRange = 1.25f;
	private float attackRate = 1.0f;
	private float nextAttack = 0f;
	private float detectionRange = 10f;

	private bool _detectedPlayer = false;

	protected override void Start ()
	{
		_controller = GetComponent<TDCharacterController2D>();
		GameManager.Instance.AddEnemyToList(this);
		_animator = GetComponent<Animator>();
		_target = GameObject.FindGameObjectWithTag("Player").transform;
		_player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Player>();
		base.Start();
	}

	
	public void ModifyHealth(int value)
	{
		Health = Health + value;
		if (Health <= 0)
		{
			OnEnemyDeath?.Invoke(this, null);
		}
	}

	void FixedUpdate()
	{
		float xDir = 0;
		float yDir = 0;
		
		//precompute our ray settings
		Vector3 start = transform.position;
		Vector3 direction = (_player.transform.position - transform.position).normalized;
		float distance = detectionRange;

		//draw the ray in the editor
		Debug.DrawRay(start, direction * distance, Color.red);

		var heading = _target.position - transform.position;

		RaycastHit2D[] sightTests = Physics2D.RaycastAll(start, direction, distance, 1 << 8);
		foreach(var sightTest in sightTests)
		{
			if (sightTest.collider.gameObject != gameObject)
			{
				if(sightTest.collider.gameObject.CompareTag("Player"))
				{
					_detectedPlayer = true;
				}
				break;
			}
		}

		if (!_detectedPlayer)
			return;

		if(heading.sqrMagnitude < attackRange * attackRange) { 
			if(Time.time > nextAttack)
			{
				nextAttack = Time.time + attackRate;
				_animator.SetTrigger("EnemyAttack");
				_player.ModifyHealth(-PlayerDamage);
			}
			return;
		}
		
		yDir = direction.y;
		xDir = direction.x;
		if (xDir != 0 && yDir != 0) // Check for diagonal movement
		{
			// limit movement speed diagonally, so you move at 70% speed
			xDir *= moveLimiter;
			yDir *= moveLimiter;
		}

		_controller.Move(new Vector2(xDir * speed * Time.deltaTime, yDir * speed * Time.deltaTime));
	}

}
