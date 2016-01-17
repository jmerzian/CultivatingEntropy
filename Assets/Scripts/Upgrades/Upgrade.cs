using UnityEngine;
using System.Collections;

public class Upgrade
{
	public Tile tile;
	public TileType.resourceFunction function;
	public int type;
	public int species;

	public Upgrade(Tile newTile, TileType.resourceFunction newFunction, int newType, int newSpecies)
	{
		function = newFunction;
		tile = newTile;
		type = newType;
		species = newSpecies;
	}
}

