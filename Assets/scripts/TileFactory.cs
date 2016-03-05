using UnityEngine;
using System.Collections.Generic;

public class TileFactory : MonoBehaviour {
	public GameObject spritePrefab;
	private Sprite[] sprites;

	void Start () {
		Load ();
	}
	

	void Load() {
		sprites = Resources.LoadAll<Sprite> ("tiles");
	}

	public GameObject CreateTile(int id, int tilewidth) {
		if (sprites == null) {
			Load ();
		}
		GameObject tile = GameObject.Instantiate (spritePrefab);
        tile.GetComponent<SpriteRenderer>().sprite = sprites[id - 1];
		return tile;
	}
}
