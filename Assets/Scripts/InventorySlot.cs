using System;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Enums;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

	public Image Icon;
	public Button ActivateButton;
	public Text Tooltip;
	public Image TooltipBackground;

	private Item _item;

	public void AddItem(Item newItem)
	{
		_item = newItem;

		Icon.sprite = _item.Sprite;
		Icon.enabled = true;
	}

	public void ClearSlot()
	{
		_item = null;

		Icon.sprite = null;
		Icon.enabled = false;
	}

	public void OnClick()
	{
		if (_item == null) return;

		switch (_item.Type)
		{
			case ItemTypes.Potion:
				UseItem(_item as Potion);
				Inventory.Instance.RemoveItem(_item);
				break;
			case ItemTypes.Helmet:
				Inventory.Instance.EquipItem(_item);
				Inventory.Instance.RemoveItem(_item);
				break;
		}
		
	}

	public void ShowTooltip()
	{
		if (_item != null)
		{
			Tooltip.text = "I am " + _item.Type;
			TooltipBackground.gameObject.SetActive(true);
		}
	}

	public void HideTooltip()
	{
		Tooltip.text = "";
		TooltipBackground.gameObject.SetActive(false);
	}

	private void UseItem(Potion potion)
	{
		potion.Effect.OnUse();
	}
}
