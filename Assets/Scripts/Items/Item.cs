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
	public string Name { get; set; }
	public SelfEffect Effect;
	public Attribute Attribute;
}

public class Potion : Item
{
}

public class Helmet : Item
{
}

