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
		var source = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var target = (source - position).normalized;
		fireball.setTarget(target);
	}
}
