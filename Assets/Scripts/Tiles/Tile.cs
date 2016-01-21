using UnityEngine;
using System.Collections.Generic;

public class Tile
{
	public TileManager manager;

	public string name;
	public string description;

	public Dictionary<string,string> reactionEffects = new Dictionary<string, string>();

	public Vector3 position;
	public int x;
	public int y;

	public Mesh mesh;
	public Texture2D material;

	public float growthFactor;
	public float elevation;

	public Tile(TileManager Manager)
	{
		manager = Manager;
	}

	public Tile Instantiate(Vector3 location, int X, int Y)
	{
		//clone it
		Tile newTile = (Tile)this.MemberwiseClone ();
		//set the X and Y coordinates
		newTile.position = location;
		newTile.x = X;
		newTile.y = Y;
		return newTile;
	}
}
