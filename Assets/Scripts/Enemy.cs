using System;
using UnityEngine;
using System.Collections;

public class Enemy : MovingObject
{
	public int PlayerDamage;
	public int Health = 1;
	public event EventHandler OnEnemyDeath;

	private Animator _animator;
	private Transform _target;
	private bool _skipMove;
	
	protected override void Start ()
	{
		GameManager.Instance.AddEnemyToList(this);
		_animator = GetComponent<Animator>();
		_target = GameObject.FindGameObjectWithTag("Player").transform;
		base.Start();
	}

	
	public void ModifyHealth(int value)
	{
		Health = Health + value;
		if (Health <= 0)
		{
			if(OnEnemyDeath != null)
				OnEnemyDeath(this, null);
		}
	}

	public void MoveEnemy()
	{
		int xDir = 0;
		int yDir = 0;

		if (Mathf.Abs(_target.position.x - transform.position.x) < float.Epsilon)
			yDir = _target.position.y > transform.position.y ? 1 : -1;
		else
			xDir = _target.position.x > transform.position.x ? 1 : -1;

		Debug.Log($"{xDir} {yDir}");
	}

}
