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
	float moveLimiter = 0.7f;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		_controller = GetComponent<TDCharacterController2D>();
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
		_controller.Move(new Vector2(velX * speed * Time.deltaTime, velY * speed * Time.deltaTime));
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		other.DealDamageToTarget(5);
		Destroy(gameObject);
	}
}
