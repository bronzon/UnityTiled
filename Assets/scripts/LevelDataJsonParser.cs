using UnityEngine;
using System.Collections.Generic;

public class LevelDataJsonParser : MonoBehaviour {

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
		    if (tilepropertiesJson != null){
                for (int i = 0; i < tilepropertiesJson.list.Count; i++)
                {
                    JSONObject customPropertiesForTileJson = (JSONObject)tilepropertiesJson.list[i];
                    if (customPropertiesForTileJson != null)
                    {
                        int key = int.Parse((string)tilepropertiesJson.keys[i]);

                        if (!tileset.tileproperties.ContainsKey(key))
                        {
                            tileset.tileproperties[key] = new Dictionary<string, string>();
                        }

                        Dictionary<string, string> properties = tileset.tileproperties[key];

                        for (int n = 0; n < customPropertiesForTileJson.list.Count; n++)
                        {
                            string propertyKey = (string)customPropertiesForTileJson.keys[n];
                            string value = customPropertiesForTileJson.list[n].str;
                            properties.Add(propertyKey, value);
                        }
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
			List<TileData> tileData = null;
			if(layer.type == "objectgroup") {
				tileData = ParseObjects (jsonLayer ["objects"]);
			} else if(layer.type == "tilelayer") {
				tileData = ParseTiles (jsonLayer ["data"]);
			}
				
			layer.data = tileData;
			layerList.Add (layer);
		}
		return layerList;
	}

	List<TileData> ParseTiles(JSONObject tileDataJson) {
		List<TileData> tileDataList = new List<TileData> ();

		foreach (JSONObject dataJson in tileDataJson.list) {
			tileDataList.Add (new TileData((int)dataJson.n));
		}
		return tileDataList;
	}

	List<TileData> ParseObjects(JSONObject objectDataJson) {
		List<TileData> tileDataList = new List<TileData> ();

		foreach (JSONObject dataJson in objectDataJson.list) {
			var tileData = new TileData ((int)dataJson.GetField("gid").n);
			tileData.x = dataJson.GetField ("x").n;
			tileData.y = dataJson.GetField ("y").n;
		    tileData.visible = dataJson.GetField("visible").b;
			JSONObject objectPropertiesJson = dataJson.GetField ("properties");

			if (objectPropertiesJson != null) {
				for (int n = 0; n < objectPropertiesJson.list.Count; n++) {
					string propertyKey = (string)objectPropertiesJson.keys[n];
					string value = objectPropertiesJson.list [n].str;
					tileData.tileProperties.Add (propertyKey, value);
				}
			}

			tileDataList.Add (tileData);
		}
		return tileDataList;
	}

}

