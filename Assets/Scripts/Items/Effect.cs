using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class SelfEffect
{
	public abstract void OnUse();
	public int Value;
	public abstract string Name { get; }
}

public class HealSelf : SelfEffect
{
	public HealSelf()
	{
		Value = Random.Range(1,100);
	}
	
	public override string Name { get {return "Healing";} }

	public override void OnUse()
	{
		GameObject.Find("Player").GetComponent<Player>().ModifyHealth(Value);
	}
}

public class DamageSelf : SelfEffect
{
	public DamageSelf()
	{
		Value = Random.Range(-100, -1);
	}

	public override string Name { get { return "Hurting"; } }

	public override void OnUse()
	{
		GameObject.Find("Player").GetComponent<Player>().ModifyHealth(Value);
	}
}

public abstract class TargetEffect
{
	public abstract void OnUse(Collider2D[] hits);
}


public class DamageTarget : TargetEffect
{
	public int Value;

	public DamageTarget()
	{
		Value = Random.Range(1, 100);
	}

	public override void OnUse(Collider2D[] colliders)
	{
		foreach (var collider in colliders)
		{
			if (collider != null && collider.gameObject.tag == "Enemy")
			{
				var enemy = collider.gameObject.GetComponent<Enemy>();
				enemy.ModifyHealth(-Value);
			}
			if (collider != null && collider.gameObject.tag == "Untagged")
			{
				var wall = collider.gameObject.GetComponent<Wall>();
				if (wall != null)
				{
					wall.DamageWall(Value);
				}
			}
		}
	}
}