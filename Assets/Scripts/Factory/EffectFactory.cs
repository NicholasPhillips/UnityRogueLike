using UnityEngine;

namespace Factory
{
	public class EffectFactory
	{
		public SelfEffect GeneratePotionEffect()
		{
			if (Random.Range(0, 2) == 1)
			{
				return new HealSelf();
			}
			return new DamageSelf();
		}
	}
}
