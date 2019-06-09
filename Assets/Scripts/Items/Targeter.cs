using UnityEngine;

namespace Assets.Scripts.Items
{
	public abstract class Targeter
	{
		public abstract Collider2D[] AquireTargets();
	}

	public class MouseTargeter : Targeter
	{
		private float _radius = 1f;
		private int _blockingLayer;

		public MouseTargeter()
		{
			_blockingLayer = 1 << 8;
		}
		
		public override Collider2D[] AquireTargets()
		{
			//Physics2D.CircleCast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), _radius, );
			var hitColliders = Physics2D.OverlapCircleAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), _radius, _blockingLayer);
			return hitColliders;
		}
	}
}
