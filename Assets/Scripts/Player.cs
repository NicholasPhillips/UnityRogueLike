using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Items;
using UnityEngine.UI;

public class Player : MovingObject
{
	public float RestartLevelDelay = 1f;
	public Text HealthText;

	private int _damage;
	private Animator _animator;
	private int _maxHealthPoints;
	private int _currentHealthPoints;

	// Use this for initialization
	protected override void Start()
	{
		_animator = GetComponent<Animator>();
		Inventory.Instance.OnEquippableItemAdded += SetAttributes;
		SetAttributes(this, null);
		_currentHealthPoints = 100;
		UpdateHealthText();
		base.Start();
	}

	private void SetAttributes(object sender, System.EventArgs e)
	{
		var attributes = EquipmentStatsHelper.CalculateStats();
		if (attributes != null)
		{
			_damage = attributes.Damage;
			_maxHealthPoints = attributes.Health;
			if (_currentHealthPoints > _maxHealthPoints)
			{
				_currentHealthPoints = _maxHealthPoints;
			}
		}
		UpdateHealthText();
	}

	private void OnDisable()
	{
		Inventory.Instance.OnEquippableItemAdded -= SetAttributes;
	}

	// Update is called once per frame
	void Update()
	{
		if (!GameManager.Instance.PlayersTurn) return;

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw("Horizontal");
		vertical = (int)Input.GetAxisRaw("Vertical");

		if (Input.GetKeyUp(KeyCode.I))
		{
			GameObject.Find("Canvas").transform.Find("InventoryPanel").gameObject.SetActive(!GameObject.Find("Canvas").transform.Find("InventoryPanel").gameObject.activeSelf);
		}

		if (Input.GetMouseButtonUp(1))
		{
			Inventory.Instance.Spell.UseSpell();

			GameManager.Instance.PlayersTurn = false;
		}

		if (Input.GetKeyUp(KeyCode.PageUp))
		{
			gameObject.GetComponentInChildren<Camera>().orthographicSize++;
		}
		if (Input.GetKeyUp(KeyCode.PageDown))
		{
			gameObject.GetComponentInChildren<Camera>().orthographicSize--;
		}

		if (horizontal != 0)
			vertical = 0;

		if (horizontal != 0 || vertical != 0)
		{
			AttemptMove(horizontal, vertical);
		}
	}

	protected override void AttemptMove(int xDir, int yDir, bool isEnemy = false)
	{
		base.AttemptMove(xDir, yDir);

		CheckIfGameOver();

		GameManager.Instance.PlayersTurn = false;
	}

	protected override void OnCantMove<T>(T component)
	{
		Wall hitWall = component as Wall;
		if (hitWall != null)
		{
			hitWall.DamageWall(_damage);
			_animator.SetTrigger("PlayerChop");
		}
		Enemy hitEnemy = component as Enemy;
		if (hitEnemy != null)
		{
			hitEnemy.ModifyHealth(-_damage);
			_animator.SetTrigger("PlayerChop");
		}
	}

	private void Restart()
	{
		GameManager.Instance.ChangeScene();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Exit")
		{
			Invoke("Restart", RestartLevelDelay);
			enabled = false;
		}
		else if (other.tag == "Loot")
		{
			var loot = other.gameObject.GetComponent<GameWorldItem>();
			var item = ScriptableObject.CreateInstance<Item>();
			item.Sprite = (other.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite;
			item.Type = loot.Type;
			Inventory.Instance.AddItem(item);
			other.gameObject.SetActive(false);
		}
	}

	public void ModifyHealth(int value)
	{
		if (value < 0)
		{
			_animator.SetTrigger("PlayerHit");
		}
		_currentHealthPoints = _currentHealthPoints + value;
		if (_currentHealthPoints > _maxHealthPoints)
		{
			_currentHealthPoints = _maxHealthPoints;
		}
		else if (_currentHealthPoints < 0)
		{
			_currentHealthPoints = 0;
		}

		UpdateHealthText();

		CheckIfGameOver();
	}

	private void UpdateHealthText()
	{
		HealthText.text = "Health: " + _currentHealthPoints + " / " + _maxHealthPoints + " Damage: " + _damage;
	}

	private void CheckIfGameOver()
	{
		if (_currentHealthPoints <= 0)
		{
			GameManager.Instance.GameOver();
		}
	}
}
