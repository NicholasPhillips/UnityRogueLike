using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float LevelStartDelay = 0f;
	public static GameManager Instance;
	public EffectManager EffectScript;
	public Player player;
	public Transform playerTransform;
	public GameObject exit;
	public GameObject inventoryPanel;

	private Text _levelText;
	private GameObject _levelImage;
	private int _level;
	public List<Enemy> enemies;
	private bool _doingSetup;

	// Use this for initialization
	void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else if(Instance != this)
			Destroy(gameObject);

		enemies = new List<Enemy>();
		EffectScript = GetComponent<EffectManager>();
		playerTransform = GameObject.Find("Player").transform;
		player = GameObject.Find("Player").GetComponent<Player>();
		inventoryPanel = GameObject.Find("Canvas").transform.Find("InventoryPanel").gameObject;
		exit = GameObject.Find("Exit");
	}

	void InitGame()
	{
		_doingSetup = true;
		_levelImage = GameObject.Find("LevelImage");
		_levelText = GameObject.Find("LevelText").GetComponent<Text>();
		_levelText.text = "Day " + _level;
		_levelImage.SetActive(true);
		Invoke("HideLevelImage", LevelStartDelay);

		enemies.Clear();
	}

	private void HideLevelImage()
	{
		_levelImage.SetActive(false);
		_doingSetup = false;
	}

	public void GameOver()
	{
		_levelText.text = "After " + _level + " days, you died.";
		_levelImage.SetActive(true);
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_doingSetup)
			return;
	}

	public void AddEnemyToList(Enemy enemy)
	{
		enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
		enemies.Add(enemy);
	}

	private void Enemy_OnEnemyDeath(object sender, EventArgs e)
	{
		var enemy = sender as Enemy;
		if (enemy != null)
		{
			enemies.Remove(enemy);
			Destroy(enemy.gameObject);
		}
	}

	public void ChangeScene()
	{
		SceneManager.LoadScene(1); 
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		_level++;
		InitGame();
	}
	
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}
}
