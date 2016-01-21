using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public interface DisasterFunction
{
	void Init(TileManager Manager);
	void OnTurn();
}

public class Disaster
{
	//current tile the disaster is on
	public Tile currentTile;
	public TileManager manager;
	
	public string name;
	public string description;

	//public Action turnFunction;
	public string function;
	public DisasterFunction plugin;
	
	public Dictionary<string,string> elementEffects = new Dictionary<string, string>();

	public Vector3 position;
	public int x;
	public int y;
	
	public Mesh mesh;
	public Texture2D material;

	public Disaster(TileManager Manager)
	{
		manager = Manager;
	}

	public Disaster Instantiate(Vector3 location, int X, int Y)
	{
		//clone it
		Disaster newDisaster = (Disaster)this.MemberwiseClone ();
		newDisaster.plugin = Compiler.GetDisasterFunction (function,name,X,Y);
		//set the X and Y coordinates
		newDisaster.position = location;
		newDisaster.x = X;
		newDisaster.y = Y;
		return newDisaster;
	}

	public void Init()
	{
		plugin.Init (manager);
	}

	public void Turn()
	{
		plugin.OnTurn ();
	}
}
