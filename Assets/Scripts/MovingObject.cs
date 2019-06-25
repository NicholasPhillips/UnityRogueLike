using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
	private BoxCollider2D _boxCollider;
	private Rigidbody2D _rb2D;

	// Use this for initialization
	protected virtual void Start ()
	{
		_boxCollider = GetComponent<BoxCollider2D>();
		_rb2D = GetComponent<Rigidbody2D>();
	}	
	
}
