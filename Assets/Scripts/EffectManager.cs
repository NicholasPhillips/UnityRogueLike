using UnityEngine;
using System.Linq;

public class EffectManager : MonoBehaviour
{
	public GameObject[] SpellEffects;
	public void DisplaySpellEffect()
	{
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		pos.x = Mathf.Round(pos.x) - 0.5f;
		pos.y = Mathf.Round(pos.y) + 0.5f;
		var clone = (GameObject)Instantiate(SpellEffects.First(), pos, Quaternion.identity);
		Destroy(clone, 1);
	}
}
