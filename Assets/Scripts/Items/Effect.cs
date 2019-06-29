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
		GameManager.Instance.player.ModifyHealth(Value);
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
		GameManager.Instance.player.ModifyHealth(Value);
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
		colliders.DealDamageToAllTargets(Value);
	}
}