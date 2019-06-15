using UnityEngine;
using Assets.Scripts.Items;
using UnityEngine.UI;
using System.Linq;

public class Player : MovingObject
{
	public float RestartLevelDelay = 1f;
	public Text HealthText;
	public float runSpeed = 10.0f;

	private int _damage;
	private Animator _animator;
	private int _maxHealthPoints;
	private int _currentHealthPoints;

	private float horizontal = 0;
	private float vertical = 0;
	private float moveLimiter = 0.7f;

	private float attackRange = 1.25f;
	private float attackRate = 1.0f;
	private float nextAttack = 0f;

	private TDCharacterController2D _controller;

	private MouseTargeter mouseTargeter = new MouseTargeter();

	// Use this for initialization
	protected override void Start()
	{
		_controller = GetComponent<TDCharacterController2D>();
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
		horizontal = (int)Input.GetAxisRaw("Horizontal");
		vertical = (int)Input.GetAxisRaw("Vertical");

		if (Input.GetKeyUp(KeyCode.I))
		{
			GameObject.Find("Canvas").transform.Find("InventoryPanel").gameObject.SetActive(!GameObject.Find("Canvas").transform.Find("InventoryPanel").gameObject.activeSelf);
		}

		if (Input.GetMouseButtonUp(1))
		{
			Inventory.Instance.Spell.UseSpell();
		}

		if (Input.GetMouseButton(0))
		{
			if (Time.time > nextAttack)
			{
				var collisions = mouseTargeter.AquireTargets();
				var target = collisions.FirstOrDefault();
				if(target != null)
				{
					if(transform.IsInRange(target.transform, attackRange))
					{
						nextAttack = Time.time + attackRate;
						_animator.SetTrigger("PlayerChop");
						collisions.FirstOrDefault().DealDamageToTarget(_damage);
					}
				}				
			}
		}

		if (Input.GetKeyUp(KeyCode.PageUp))
		{
			gameObject.GetComponentInChildren<Camera>().orthographicSize++;
		}
		if (Input.GetKeyUp(KeyCode.PageDown))
		{
			gameObject.GetComponentInChildren<Camera>().orthographicSize--;
		}
	}

	void FixedUpdate()
	{
		if (horizontal != 0 && vertical != 0) // Check for diagonal movement
		{
			// limit movement speed diagonally, so you move at 70% speed
			horizontal *= moveLimiter;
			vertical *= moveLimiter;
		}

		_controller.Move(new Vector2(horizontal * runSpeed * Time.deltaTime, vertical * runSpeed * Time.deltaTime));
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
