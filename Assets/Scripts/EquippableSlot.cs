using UnityEngine;
using Assets.Scripts.Enums;
using UnityEngine.UI;

public class EquippableSlot : MonoBehaviour
{

	public Image Icon;
	public Button ActivateButton;

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
			case ItemTypes.Helmet:
				Inventory.Instance.AddItem(_item, true);
				Inventory.Instance.UnequipItem(_item);
				break;
		}
		
	}
}
