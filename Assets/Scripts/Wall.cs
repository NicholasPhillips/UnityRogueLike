using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
	public Sprite DmgSprite;
	public int hp = 4;

	private SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Awake ()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void DamageWall(int loss)
	{
		_spriteRenderer.sprite = DmgSprite;
		hp -= loss;

		if (hp <= 0)
			gameObject.SetActive(false);
	}
}
