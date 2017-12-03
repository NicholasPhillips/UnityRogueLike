using System;
using Assets.Scripts.Enums;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
	protected Item()
	{
		Id = Guid.NewGuid();
	}

	public Guid Id { get; set; }
	public Sprite Sprite;
	public ItemTypes Type;
}

public class Potion : Item
{
	public SelfEffect Effect;
}

public class Helmet : Item
{
	public Attribute Attribute;
}

