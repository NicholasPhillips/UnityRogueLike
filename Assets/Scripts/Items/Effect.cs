using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class SelfEffect
{
	public abstract void OnUse();
}

public class HealSelf : SelfEffect
{
	public int Value;

	public HealSelf()
	{
		Value = Random.Range(1,100);
	}

	public override void OnUse()
	{
		GameObject.Find("Player").GetComponent<Player>().ModifyHealth(Value);
	}
}

public class DamageSelf : SelfEffect
{
	public int Value;

	public DamageSelf()
	{
		Value = Random.Range(-100, -1);
	}

	public override void OnUse()
	{
		GameObject.Find("Player").GetComponent<Player>().ModifyHealth(Value);
	}
}

public abstract class TargetEffect
{
	public abstract void OnUse(RaycastHit2D[] hits);
}


public class DamageTarget : TargetEffect
{
	public int Value;

	public DamageTarget()
	{
		Value = Random.Range(1, 100);
	}

	public override void OnUse(RaycastHit2D[] hits)
	{
		foreach (var hit in hits)
		{
			if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
			{
				var enemy = hit.collider.gameObject.GetComponent<Enemy>();
				enemy.ModifyHealth(-Value);
			}
			if (hit.collider != null && hit.collider.gameObject.tag == "Untagged")
			{
				var wall = hit.collider.gameObject.GetComponent<Wall>();
				if (wall != null)
				{
					wall.DamageWall(Value);
				}
			}
		}
	}
}