using UnityEngine;
using System;
using System.Collections.Generic;

//Class name NEEDS to be the same as the disaster name
public class Earthquake : DisasterFunction
{
	public TileManager manager;
	public int x;
	public int y;
	
	public Earthquake(int X, int Y)
	{
		Debug.Log("okay, created at " + X + "," + Y);
		x = X;
		y = Y;
	}
	
	public void Init(TileManager Manager)
	{
		manager = Manager;
		//Creates mountains all around the starting location
		manager.ChangeTile("Mountain",manager.getTile[x,y]);
		manager.ChangeTile("Mountain",manager.getTile[x,y+1]);
		manager.ChangeTile("Mountain",manager.getTile[x,y-1]);
		manager.ChangeTile("Mountain",manager.getTile[x+1,y]);
		manager.ChangeTile("Mountain",manager.getTile[x-1,y]);
		manager.ChangeTile("Mountain",manager.getTile[x+1,y+1]);
		manager.ChangeTile("Mountain",manager.getTile[x-1,y+1]);
		manager.ChangeTile("Mountain",manager.getTile[x+1,y-1]);
		manager.ChangeTile("Mountain",manager.getTile[x-1,y-1]);
	}
	public void OnTurn()
	{
		Debug.Log("Put something to do every turn here");
	}
}