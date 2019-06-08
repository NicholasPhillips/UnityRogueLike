using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectManager : MonoBehaviour
{
	public Dictionary<string, GameObject> SpellEffects;


	public void DisplaySpellEffect(string effectKey)
	{
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		pos.x = Mathf.Round(pos.x) - 0.5f;
		pos.y = Mathf.Round(pos.y) + 0.5f;
		var clone = (GameObject)Instantiate(SpellEffects[effectKey], pos, Quaternion.identity);
		Destroy(clone, 1);
	}

	public void DisplaySpellEffect(List<Vector3> asdasd)
	{
		//foreach (var asd in asdasd)
		//{
		//	var clone = (GameObject)Instantiate(SpellEffects.First(), asd, Quaternion.identity);
		//	Destroy(clone, 1);
		//}
	}
}
