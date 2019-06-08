using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
	public float MoveTime = 0.001f;
	public LayerMask BlockingLayer;

	private BoxCollider2D _boxCollider;
	private Rigidbody2D _rb2D;
	private float _inverseMoveTime;

	// Use this for initialization
	protected virtual void Start ()
	{
		_boxCollider = GetComponent<BoxCollider2D>();
		_rb2D = GetComponent<Rigidbody2D>();
		_inverseMoveTime = 1f/MoveTime;
	}

	protected IEnumerator SmoothMovement(Vector3 end)
	{
		float sqrReminaingDistance = (transform.position - end).sqrMagnitude;
		while (sqrReminaingDistance > float.Epsilon)
		{
			Vector3 newPosition = Vector3.MoveTowards(_rb2D.position, end, _inverseMoveTime*Time.deltaTime);
			_rb2D.MovePosition(newPosition);
			sqrReminaingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	protected bool Move(int xDir, int yDIr, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2(xDir, yDIr);

		//Disable own collider so we don't hit ourself
		_boxCollider.enabled = false;
		hit = Physics2D.Linecast(start, end, BlockingLayer);
		_boxCollider.enabled = true;

		if (hit.transform == null)
		{
			StartCoroutine(SmoothMovement(end));
			return true;
		}

		return false;
	}

	protected virtual void AttemptMove(int xDir, int yDir, bool isEnemy = false)
	{
		RaycastHit2D hit;
		bool canMove = Move(xDir, yDir, out hit);

		if (hit.transform == null)
			return;

		if (isEnemy)
		{
			var hitEnemmy = hit.transform.GetComponent<Player>();
			
			if (!canMove && hitEnemmy != null)
			{
				OnCantMove(hitEnemmy);
			}
		}
		else
		{
			var hitWall = hit.transform.GetComponent<Wall>();
			var hitEnemy = hit.transform.GetComponent<Enemy>();

			if (!canMove && hitWall != null)
			{
				OnCantMove(hitWall);
			}
			else if (!canMove && hitEnemy != null)
			{
				OnCantMove(hitEnemy);
			}
		}

	}

	protected abstract void OnCantMove<T>(T component) where T:Component;
	
		
	
}
