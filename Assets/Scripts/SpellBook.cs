using UnityEngine;

public class SpellBook : MonoBehaviour
{
	public static SpellBook Instance = null;	

	public GameObject Fireball;

	
	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public void UseFireballSpell(Vector2 position)
	{
		var obj = Instantiate(Fireball, position, Quaternion.identity);
		var fireball = obj.GetComponent<Fireball>();
		var target = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		fireball.setTarget((target - position).normalized);
	}
}
