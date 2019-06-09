using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EffectManager : MonoBehaviour
{
	[Serializable]
	public struct SpellEffect
	{
		public string Key;
		public GameObject Effect;
	}

	public SpellEffect[] SpellEffects;
	private Dictionary<string, GameObject> _spellEffects = new Dictionary<string, GameObject>();

	void Awake()
	{
		foreach (var spellEffect in SpellEffects)
		{
			_spellEffects.Add(spellEffect.Key, spellEffect.Effect);
		}
	}

	public void DisplaySpellEffect(string effectKey)
	{
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		pos.x = pos.x - 0.5f;
		pos.y = pos.y + 0.5f;
		var clone = (GameObject)Instantiate(_spellEffects[effectKey], pos, Quaternion.identity);
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
