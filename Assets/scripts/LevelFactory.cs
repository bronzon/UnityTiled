using UnityEngine;
using System.Collections.Generic;

public class LevelFactory : MonoBehaviour {
	public TextAsset levelJsonFile;
	public int tileWidth;
	public LevelDataJsonParser jsonParser;
    public TilePropertiesFactory tilePropertiesFactory;

	public TileFactory tileFactory;
	private GameObject levelParent;

	void Awake () {
		LevelData levelData = jsonParser.Parse (levelJsonFile.text);
		levelParent = new GameObject ("Level");
		foreach (Layer layer in levelData.layers) {
			if (!layer.visible) {
				continue;
			}
			GameObject layerGameObject = new GameObject(layer.name);
			layerGameObject.transform.SetParent (levelParent.transform);
			int tileIndex = 0;
			if (layer.type == "tilelayer") { 
				for (int rowIndex = 0; rowIndex < layer.height; rowIndex++) {
					for (int columnIndex = 0; columnIndex < layer.width; columnIndex++) {
					    TileData tileData = layer.data [tileIndex++];
					    int spriteIndex = tileData.brushIndex;
						if (spriteIndex == 0) {
							continue;
						}
						Dictionary<string, string> tileProperties = levelData.tilesets [0].tileproperties.ContainsKey (spriteIndex - 1) ? levelData.tilesets [0].tileproperties [spriteIndex - 1] : new Dictionary<string, string> ();
						CreateTileObject (new Vector3 (columnIndex * tileWidth, -rowIndex * tileWidth), levelData, spriteIndex, layerGameObject, tileData, tileProperties); 
                        
					}
				}	
			} else if (layer.type == "objectgroup") {
				foreach (TileData tileData in layer.data) {
					CreateTileObject (new Vector3 (tileData.x, -tileData.y+levelData.tilesets [0].tileheight), levelData, tileData.brushIndex, layerGameObject, tileData, tileData.tileProperties);
				}
			}
		}
	}
	
	void CreateTileObject(Vector3 position, LevelData levelData, int spriteIndex, GameObject parent, TileData tileData, Dictionary<string, string> tileProperties) {
		GameObject tile = tileFactory.CreateTile (spriteIndex, levelData.tilesets [0].tilewidth);
        tilePropertiesFactory.CreatePropertyComponents(tile, tileWidth, tileProperties);
		tile.transform.SetParent (parent.transform);
		tile.transform.Translate (position);
	    if (!tileData.visible){
	        tile.GetComponent<SpriteRenderer>().enabled = false;
	    }
	}
}
