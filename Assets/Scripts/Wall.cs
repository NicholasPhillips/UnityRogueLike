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

	public void ModifiyHealth(int value)
	{
		if(value < 0)
			_spriteRenderer.sprite = DmgSprite;

		hp = hp + value;

		if (hp <= 0)
			gameObject.SetActive(false);
	}
}
