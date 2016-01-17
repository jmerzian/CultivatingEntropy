using UnityEngine;
using System.Collections;

public static class UpgradeHelper
{
	public static int PlantLookup(int terrain)
	{
		switch(terrain)
		{
		case (int)TileType.tile.DESERT:
			return (int)TileType.plantTypes.CACTUS;
		}
		return -1;
	}
	public static int AnimalLookup(int terrain)
	{
		switch(terrain)
		{
		case (int)TileType.tile.DESERT:
			return (int)TileType.animalTypes.SNAKE;
		}
		return -1;
	}
	public static int MineralLookup(int terrain)
	{
		switch(terrain)
		{
		case (int)TileType.tile.DESERT:
			return (int)TileType.mineralTypes.GLASS;
		}
		return -1;
	}
}

