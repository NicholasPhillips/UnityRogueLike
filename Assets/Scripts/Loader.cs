using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{

	public GameObject gameManager;
	public GameObject inventory;
	public GameObject spriteStorage;
	public GameObject spellBook;

	void Awake ()
	{
		if (GameManager.Instance == null)
			Instantiate(gameManager);

		if (Inventory.Instance == null)
			Instantiate(inventory);

		if (SpriteStorage.Instance == null)
			Instantiate(spriteStorage);

		if (SpellBook.Instance == null)
			Instantiate(spellBook);
	}
}
