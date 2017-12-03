using UnityEngine;

namespace Factory
{
	public class AttributeFactory
	{
		public Attribute GenerateAttribute()
		{
			return new Attribute(Random.Range(0,5), Random.Range(0, 50));
		}
	}
}
