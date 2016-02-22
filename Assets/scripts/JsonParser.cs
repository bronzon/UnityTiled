using UnityEngine;
using System.Collections.Generic;

public class JsonParser : MonoBehaviour {

	// Use this for initialization
	public LevelData Parse (string levelJson) {
		JSONObject jsonObject = new JSONObject (levelJson);
		LevelData levelData = new LevelData ();
		List<Layer> layers = ParseLayers(jsonObject.GetField ("layers"));
		levelData.layers = layers;

		List<Tileset> tilesets = ParseTilesets(jsonObject.GetField("tilesets"));
		levelData.tilesets = tilesets;

		return levelData;
	}

	List<Tileset> ParseTilesets(JSONObject tilesetsJson) {
		List<Tileset> tilesetList = new List<Tileset> ();	
		foreach (JSONObject tilesetJson in tilesetsJson.list) {
			Tileset tileset = new Tileset ();
			tileset.tilewidth = (int)tilesetJson.GetField ("tilewidth").n;
			tileset.tileheight = (int)tilesetJson.GetField ("tileheight").n;
			JSONObject tilepropertiesJson = tilesetJson.GetField ("tileproperties");
			for (int i = 0; i < tilepropertiesJson.list.Count; i++) {
				JSONObject customPropertiesForTileJson = (JSONObject)tilepropertiesJson.list[i];
				if (customPropertiesForTileJson != null) {
					int key = int.Parse((string)tilepropertiesJson.keys[i]);

					if (!tileset.tileproperties.ContainsKey(key)) {
						tileset.tileproperties [key] = new Dictionary<string, string> ();				
					}

					Dictionary<string, string> properties = tileset.tileproperties [key];

					for (int n = 0; n < customPropertiesForTileJson.list.Count; n++) {
						string propertyKey = (string)customPropertiesForTileJson.keys[n];
						string value = customPropertiesForTileJson.list [n].str;
						properties.Add (propertyKey, value);
					}
				}

			}
			tilesetList.Add (tileset);
		}
		return tilesetList;
	}

	List<Layer> ParseLayers(JSONObject layersJson) {
		List<Layer> layerList = new List<Layer> ();
		for (int i = layersJson.list.Count-1; i >= 0; i--) {
			JSONObject jsonLayer = layersJson.list [i];
			Layer layer = new Layer();

			layer.width = (int)jsonLayer ["width"].n;
			layer.height = (int)jsonLayer ["height"].n;
			layer.type = jsonLayer ["type"].str;
			layer.name = jsonLayer ["name"].str;
			layer.visible = jsonLayer ["visible"].b;

			JSONObject tileDataJson = jsonLayer ["data"];
			List<int> tileData = new List<int> ();

			foreach (JSONObject dataJson in tileDataJson.list) {
				tileData.Add ((int)dataJson.n);
			}
			layer.data = tileData;

			layerList.Add (layer);
		}
		return layerList;
	}
}

