using System.Linq;
using UnityEngine;

public class SpriteStorage : MonoBehaviour
{
	public static SpriteStorage Instance;
	public GameObject[] PotionTiles;
	public GameObject[] HelmetTiles;

	public GameObject[] GetLoot()
	{
		return PotionTiles.Concat(HelmetTiles).ToArray();
	}

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
	}
}

