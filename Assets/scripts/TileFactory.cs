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

	public GameObject CreateTile(int id, Dictionary<string, string> properties, int tilewidth) {
		if (sprites == null) {
			Load ();
		}
		GameObject tile = GameObject.Instantiate (spritePrefab);	
		tile.GetComponent<SpriteRenderer> ().sprite = sprites [id-1];
		if (properties.ContainsKey ("colliding")) {
			BoxCollider2D boxCollider2D = tile.GetComponent<BoxCollider2D> ();
			boxCollider2D.enabled = true;
			boxCollider2D.offset = new Vector2 (tilewidth / 2, -tilewidth / 2);
			boxCollider2D.size = new Vector2 (tilewidth, tilewidth);
		}
		return tile;
	}
}
