using UnityEngine;
using System.Collections.Generic;

public class TilePropertiesFactory : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frameS
	public void CreatePropertyComponents (GameObject tile, float tilewidth, Dictionary<string, string> properties) {    
        if (properties.ContainsKey("colliding")) {
            BoxCollider2D boxCollider2D = tile.GetComponent<BoxCollider2D>();
            boxCollider2D.enabled = true;
            boxCollider2D.offset = new Vector2(tilewidth / 2, -tilewidth / 2);
            boxCollider2D.size = new Vector2(tilewidth, tilewidth);
        }

	    if (properties.ContainsKey(("spawn"))){
	        tile.AddComponent<PlayerSpawner>();
	    }
	}
}
