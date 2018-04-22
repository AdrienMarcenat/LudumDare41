using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceManager : Singleton<RessourceManager>
{
	public GameObject LoadPrefab (string name)
	{
		GameObject prefab = (GameObject)Resources.Load ("Pattern/" + name, typeof(GameObject));
		if (!prefab) {
			Debug.LogWarning ("Prefab " + name + " could not be loaded");
		}                
		return prefab;
	}

	public Sprite LoadSprite (string name, int index)
	{
		Sprite[] sprite = Resources.LoadAll<Sprite> ("Sprites/Game/Item/" + name);
		if (index >= sprite.Length) {
			Debug.LogWarning ("Sprite " + name + " could not be loaded");
		}                
		return sprite [index];
	}
}
