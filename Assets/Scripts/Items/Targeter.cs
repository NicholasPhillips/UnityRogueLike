using UnityEngine;

namespace Assets.Scripts.Items
{
	public abstract class Targeter
	{
		public abstract RaycastHit2D[] AquireTargets();
	}

	public class MouseTargeter : Targeter
	{
		public override RaycastHit2D[] AquireTargets()
		{
			return Physics2D.RaycastAll(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);
		}
	}
}
