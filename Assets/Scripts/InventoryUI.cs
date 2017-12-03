using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	public Transform InventorySlotsParent;

	public Transform EquippableSlotsParent;

	Inventory _inventory;

	private InventorySlot[] _inventorySlots;
	private EquippableSlot[] _equippableSlots;

	public const int NumItemSlots = 4;

	void Start()
	{
		_inventory = Inventory.Instance;
		_inventory.OnInventoryItemAdded += UpdateInventorySlotsUI;
		_inventory.OnEquippableItemAdded += UpdateEquippableSlotsUI;
		_inventorySlots = InventorySlotsParent.GetComponentsInChildren<InventorySlot>();
		_equippableSlots = EquippableSlotsParent.GetComponentsInChildren<EquippableSlot>();
		UpdateInventorySlotsUI(this, null);
		UpdateEquippableSlotsUI(this, null);
	}

	void UpdateEquippableSlotsUI(object sender, System.EventArgs e)
	{
		_equippableSlots = EquippableSlotsParent.GetComponentsInChildren<EquippableSlot>();
		foreach (var equippableSlot in _equippableSlots)
		{
			if (equippableSlot.name == "HelmetSlot")
			{
				var helmet = _inventory.Helmet;
				if (helmet != null)
				{
					equippableSlot.AddItem(helmet);
				}
				else
				{
					equippableSlot.ClearSlot();
				}
			}
		}
	}

	void UpdateInventorySlotsUI(object sender, System.EventArgs e)
	{
		_inventorySlots = InventorySlotsParent.GetComponentsInChildren<InventorySlot>();
		for (int i = 0; i < _inventorySlots.Length; i++)
		{
			if (i < _inventory.InventoryItems.Count)
			{
				_inventorySlots[i].AddItem(_inventory.InventoryItems[i]);
			}
			else
			{
				_inventorySlots[i].ClearSlot();
			}
		}
	}

	void OnDisable()
	{
		_inventory.OnInventoryItemAdded -= UpdateInventorySlotsUI;
		_inventory.OnEquippableItemAdded -= UpdateEquippableSlotsUI;
	}
}
