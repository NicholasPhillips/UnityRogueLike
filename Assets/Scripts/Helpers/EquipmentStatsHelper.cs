using Assets.Scripts.Enums;

public static class EquipmentStatsHelper
{
	public static Attribute CalculateStats()
	{
		var attribute = new Attribute(100, 1);
		foreach (var item in Inventory.Instance.EquippableItems)
		{
			switch (item.Type)
			{
					case ItemTypes.Helmet:
					var helmet = item as Helmet;
					if (helmet != null)
					{
						attribute.Health += helmet.Attribute.Health;
						attribute.Damage += helmet.Attribute.Damage;
					}
					break;
			}
		}
		return attribute;
	}
}

