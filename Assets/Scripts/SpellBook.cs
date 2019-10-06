using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpellBook : MonoBehaviour
{
	public static SpellBook Instance = null;

	public List<GameObject> Fireballs;
	public int AmountToPool = 30;
	public GameObject Fireball;

	
	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Fireballs = new List<GameObject>();
		for (int i = 0; i < AmountToPool; i++)
		{
			var obj = Instantiate(Fireball);
			obj.SetActive(false);
			Fireballs.Add(obj);
		}
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < Fireballs.Count; i++)
		{
			if (!Fireballs[i].activeInHierarchy)
			{
				return Fireballs[i];
			}
		}
		return null;
	}

	public void UseFireballSpell(Vector2 position)
	{
		var obj = GetPooledObject();
		obj.transform.position = position;		
		var fireball = obj.GetComponent<Fireball>();
		var source = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var target = (source - position).normalized;
		fireball.setTarget(target);
		obj.SetActive(true);
	}
}
