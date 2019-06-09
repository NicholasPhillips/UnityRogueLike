using System.Collections.Generic;
using UnityEngine;
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
		pos.x = pos.x - 1f;
		pos.y = pos.y + 1f;
		var spellEffect = _spellEffects[effectKey];
		var clone = (GameObject)Instantiate(spellEffect, pos, Quaternion.identity);
		Destroy(clone, 1);
	}
}
