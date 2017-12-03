using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Items;
using UnityEngine;

[CreateAssetMenu]
public abstract class Spell : ScriptableObject
{
	protected Spell(Guid? id)
	{
		Id = id ?? Guid.NewGuid();
	}

	public abstract void UseSpell();
	public Guid Id { get; set; }
	public SpellTypes Type;
}

public class TargetSpell : Spell
{
	public TargetSpell(Guid? id) : base(id)
	{
	}

	public override void UseSpell()
	{
		Effect.OnUse(Targeter.AquireTargets());
	}

	public TargetEffect Effect;

	public Targeter Targeter;
}

