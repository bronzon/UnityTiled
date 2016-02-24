using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Tileset {
	public int tilecount;
	public int tilewidth;
	public int tileheight;
	public Dictionary<int, Dictionary<string,string>> tileproperties = new Dictionary<int, Dictionary<string, string>>();
}

[Serializable]
public class TileData {
	public TileData(int brushIndex) {
		this.brushIndex = brushIndex;
	}
	public Dictionary<string, string> tileProperties = new Dictionary<string, string> ();
	public int brushIndex;
	public float x;
	public float y;
}

[Serializable]
public class Layer {
	public bool visible;
	public string type;
	public int width;
	public int height;
	public string name;
	public List<TileData> data;
}

[Serializable]
public class LevelData  {
	public List<Layer> layers;
	public List<Tileset> tilesets;
}
