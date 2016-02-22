﻿using UnityEngine;
using System.Collections.Generic;

public class LevelFactory : MonoBehaviour {
	public TextAsset levelJsonFile;
	public int tileWidth;
	public JsonParser jsonParser;
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
						int spriteIndex = layer.data [tileIndex++];
						if (spriteIndex == 0) {
							continue;
						}
						Dictionary<string, string> tileProperties = null;
						tileProperties = levelData.tilesets [0].tileproperties.ContainsKey (spriteIndex-1) ? levelData.tilesets [0].tileproperties [spriteIndex-1] : new Dictionary<string, string> ();
						GameObject tile = tileFactory.CreateTile (spriteIndex, tileProperties, levelData.tilesets[0].tilewidth);
						tile.transform.Translate(new Vector3(columnIndex*tileWidth, -rowIndex*tileWidth));
						tile.transform.SetParent (layerGameObject.transform);
					}
				}	
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
