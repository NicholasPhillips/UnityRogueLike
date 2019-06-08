using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Items;
using UnityEngine;

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
	private TargetEffect _effect;

	private Targeter _targeter;

	private string _visualEffectKey;

	public TargetSpell(Guid? id = null) : base(id)
	{

		_effect = new DamageTarget();
		_targeter = new MouseTargeter();
		_visualEffectKey = "Explosion";
	}

	public override void UseSpell()
	{
		_effect.OnUse(_targeter.AquireTargets());
		GameManager.Instance.EffectScript.DisplaySpellEffect(_visualEffectKey);
	}
}

