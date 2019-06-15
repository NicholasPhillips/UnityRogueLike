using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float LevelStartDelay = 0f;
	public static GameManager Instance;
	public BoardManager BoardScript;
	public EffectManager EffectScript;

	private Text _levelText;
	private GameObject _levelImage;
	private int _level;
	private List<Enemy> _enemies;
	private bool _doingSetup;

	// Use this for initialization
	void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else if(Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		_enemies = new List<Enemy>();
		BoardScript = GetComponent<BoardManager>();
		EffectScript = GetComponent<EffectManager>();
	}

	void InitGame()
	{
		_doingSetup = true;
		_levelImage = GameObject.Find("LevelImage");
		_levelText = GameObject.Find("LevelText").GetComponent<Text>();
		_levelText.text = "Day " + _level;
		_levelImage.SetActive(true);
		Invoke("HideLevelImage", LevelStartDelay);

		_enemies.Clear();
		BoardScript.SetupScene(_level);
	}

	private void HideLevelImage()
	{
		_levelImage.SetActive(false);
		_doingSetup = false;
	}

	public void GameOver()
	{
		_levelText.text = "After " + _level + " days, you starved.";
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
		_enemies.Add(enemy);
	}

	private void Enemy_OnEnemyDeath(object sender, EventArgs e)
	{
		var enemy = sender as Enemy;
		if (enemy != null)
		{
			_enemies.Remove(enemy);
			Destroy(enemy.gameObject);
		}
	}

	public void ChangeScene()
	{
		SceneManager.LoadScene(0); 
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
