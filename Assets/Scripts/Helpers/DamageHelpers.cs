using UnityEngine;

public static class DamageHelpers
{
	public static void DealDamageToAllTargets(this Collider2D[] colliders, int damage)
	{
		foreach (var collider in colliders)
		{
			if (collider != null && collider.gameObject.CompareTag("Enemy"))
			{
				var enemy = collider.gameObject.GetComponent<Enemy>();
				enemy.ModifyHealth(-damage);
			}
			if (collider != null && collider.gameObject.CompareTag("Untagged"))
			{
				var wall = collider.gameObject.GetComponent<Wall>();
				if (wall != null)
				{
					wall.ModifiyHealth(-damage);
				}
			}
		}
	}

	public static void DealDamageToTarget(this Collider2D collider, int damage)
	{
		if (collider != null && collider.gameObject.CompareTag("Enemy"))
		{
			var enemy = collider.gameObject.GetComponent<Enemy>();
			enemy.ModifyHealth(-damage);
		}
		if (collider != null && collider.gameObject.CompareTag("Untagged"))
		{
			var wall = collider.gameObject.GetComponent<Wall>();
			if (wall != null)
			{
				wall.ModifiyHealth(-damage);
			}
		}		
	}

	public static bool IsInRange(this Transform transform, Transform target, float attackRange)
	{
		var heading = target.position - transform.position;
		var direction = heading / heading.magnitude;

		if (heading.sqrMagnitude < attackRange * attackRange)
			return true;

		return false;
	}
}