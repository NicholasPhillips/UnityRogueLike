using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Items
{
	public abstract class Targeter
	{
		public abstract Collider2D[] AquireTargets();
		public abstract Collider2D[] AquireTargets(float radius = 1f);
	}

	public static class MouseTargeter
	{
		private readonly static int _blockingLayer = 1 << 8 | 1 << 9 | 1 << 10;

		public static Collider2D[] AquireTargets(float radius = 1f)
		{
			var hitColliders = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), radius, _blockingLayer);
			return hitColliders;
		}

		public static Collider2D[] AquireTargets()
		{
			var hitColliders = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, _blockingLayer);
			return hitColliders.Select(h => h.collider).ToArray();
		}
	}

	public static class PositionTargeter
	{
		private readonly static int _blockingLayer = 1 << 8 | 1 << 9 | 1 << 10;

		public static Collider2D[] AquireTargets(Vector2 position, float radius = 1f)
		{
			var hitColliders = Physics2D.OverlapCircleAll(position, radius, _blockingLayer);
			return hitColliders;
		}

		public static Collider2D[] AquireTargets(Vector2 position)
		{
			var hitColliders = Physics2D.RaycastAll(position, Vector2.zero, 0, _blockingLayer);
			return hitColliders.Select(h => h.collider).ToArray();
		}
	}
}
