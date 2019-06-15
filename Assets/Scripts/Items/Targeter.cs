using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Items
{
	public abstract class Targeter
	{
		public abstract Collider2D[] AquireTargets();
		public abstract Collider2D[] AquireTargets(float radius = 1f);
	}

	public class MouseTargeter : Targeter
	{
		private int _blockingLayer;

		public MouseTargeter()
		{
			_blockingLayer = 1 << 8;
		}
		
		public override Collider2D[] AquireTargets(float radius = 1f)
		{
			//Physics2D.CircleCast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), _radius, );
			var hitColliders = Physics2D.OverlapCircleAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), radius, _blockingLayer);
			return hitColliders;
		}

		public override Collider2D[] AquireTargets()
		{
			var hitColliders = Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0, _blockingLayer);
			return hitColliders.Select(h => h.collider).ToArray();
		}
	}
}
