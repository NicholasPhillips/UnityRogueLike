using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
	float velX = 10f;
	float velY = 10f;
	float speed = 20f;
	Rigidbody2D rb;
	TDCharacterController2D _controller;
	GameObject _particleSystemObject;
	ParticleSystem _particleSystem;
	SpriteRenderer _spriteRenderer;
	CircleCollider2D _collider;
	float moveLimiter = 0.7f;
	bool isMoving = true;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		_controller = GetComponent<TDCharacterController2D>();
		_particleSystemObject = gameObject.transform.GetChild(1).gameObject;
		_particleSystem = _particleSystemObject.GetComponent<ParticleSystem>();
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		_collider = gameObject.GetComponent<CircleCollider2D>();
	}

	public void setTarget(Vector2 vector)
	{
		if (vector.x != 0 && vector.y != 0) // Check for diagonal movement
		{
			// limit movement speed diagonally, so you move at 70% speed
			vector.x *= moveLimiter;
			vector.y *= moveLimiter;
		}
		velX = vector.x;
		velY = vector.y;
	}
    
    void FixedUpdate()
    {
		if (isMoving)
		{
			_controller.Move(new Vector2(velX * speed * Time.deltaTime, velY * speed * Time.deltaTime));
		}
		else if(!_particleSystem.isPlaying)
		{
			Destroy(gameObject);
		}		
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		var targets = PositionTargeter.AquireTargets(gameObject.transform.position, 1f);
		foreach(var target in targets)
		{
			target.DealDamageToTarget(1);
		}
		_particleSystemObject.SetActive(true);
		_particleSystem.Play(true);
		isMoving = false;
		_spriteRenderer.enabled = false;
		_collider.enabled = false;
	}
}
