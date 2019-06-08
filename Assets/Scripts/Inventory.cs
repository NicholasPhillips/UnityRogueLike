using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Items;
using Factory;

public class Inventory : MonoBehaviour
{
	public static Inventory Instance = null;
	public const int NumItemSlots = 32;
	public List<Item> InventoryItems = new List<Item>();

	public Helmet Helmet;


	public TargetSpell Spell;

	public List<Item> EquippableItems
	{
		get
		{
			var items = new List<Item>();
			if (Helmet != null)
				items.Add(Helmet);
			return items;
		}
	}

	public event EventHandler OnInventoryItemAdded;
	public event EventHandler OnEquippableItemAdded;
	private readonly EffectFactory _effectFactory = new EffectFactory();
	private readonly AttributeFactory _attributeFactory = new AttributeFactory();

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		if (Spell == null)
		{
			Spell = new TargetSpell();
		}

		DontDestroyOnLoad(gameObject);
	}

	public void AddItem(Item itemToAdd, bool isExisting = false)
	{
		if (itemToAdd.Type == ItemTypes.Potion)
		{
			var potion = ScriptableObject.CreateInstance<Potion>();
			potion.Sprite = itemToAdd.Sprite;
			potion.Id = itemToAdd.Id;
			potion.Type = itemToAdd.Type;
			potion.Effect = _effectFactory.GeneratePotionEffect();
			InventoryItems.Add(potion);

			TriggerInventoryItemAdded();
		}
		if (itemToAdd.Type == ItemTypes.Helmet)
		{
			var helmet = ScriptableObject.CreateInstance<Helmet>();
			helmet.Sprite = itemToAdd.Sprite;
			helmet.Id = itemToAdd.Id;
			helmet.Type = itemToAdd.Type;
			if (isExisting)
			{
				var existingItem = itemToAdd as Helmet;
				if (existingItem != null) helmet.Attribute = existingItem.Attribute;
			}
			else
			{
				helmet.Attribute = _attributeFactory.GenerateAttribute();
			}
			
			InventoryItems.Add(helmet);

			TriggerInventoryItemAdded();
		}
	}

	public void EquipItem(Item itemToAdd)
	{
		if (itemToAdd.Type == ItemTypes.Helmet)
		{
			
			if (Helmet != null)
			{
				InventoryItems.Add(Helmet);
				TriggerInventoryItemAdded();
				Helmet = null;
			}
			var helmet = ScriptableObject.CreateInstance<Helmet>();
			helmet.Sprite = itemToAdd.Sprite;
			helmet.Id = itemToAdd.Id;
			helmet.Type = itemToAdd.Type;

			helmet.Attribute = (itemToAdd as Helmet).Attribute;
			Helmet = helmet;
			TriggerEquippableItemAdded();
		}
	}

	public void UnequipItem(Item itemToRemove)
	{
		switch (itemToRemove.Type)
		{
			case ItemTypes.Helmet:
				Helmet = null;
				TriggerEquippableItemAdded();
				break;
		}
	}

	public void RemoveItem(Item itemToRemove)
	{
		var item = InventoryItems.Single(i => i.Id == itemToRemove.Id);
		InventoryItems.Remove(item);
		TriggerInventoryItemAdded();
	}

	private void TriggerInventoryItemAdded()
	{
		if (OnInventoryItemAdded != null)
		{
			OnInventoryItemAdded(this, null);
		}
	}

	private void TriggerEquippableItemAdded()
	{
		if (OnEquippableItemAdded != null)
		{
			OnEquippableItemAdded(this, null);
		}
	}
}
