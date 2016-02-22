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
public class TiledObject {
	public int gid;
}

[Serializable]
public class Layer {
	public bool visible;
	public string type;
	public int width;
	public int height;
	public string name;
	public TiledObject[] objects;
	public List<int> data;
}

[Serializable]
public class LevelData  {
	public List<Layer> layers;
	public List<Tileset> tilesets;
}
