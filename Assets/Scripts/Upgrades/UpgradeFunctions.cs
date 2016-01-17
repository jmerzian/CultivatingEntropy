using UnityEngine;
using System.Collections;

public partial class UpgradeManager : MonoBehaviour
{
	/************************************************
	 * Plants
	 * *******************************************/
	public int[] Cactus(Tile tile)
	{
		//Adds food depending on the total amount of desert tiles nearby
		int[] returnResources = new int[3];
		
		for(int i = 0; i <Global.directions.Length; i++)
		{
			if(manager.getTile[tile.x+Global.directions[i].x,tile.y+Global.directions[i].y].type == (int)TileType.tile.DESERT)
			{
				returnResources[(int)TileType.resource.FOOD]++;
			}
		}
		return returnResources;
	}
	public int[] Dandelion(Tile tile)
	{
		//Adds food depending on the total amount of desert tiles nearby
		int[] returnResources = new int[3];
		
		for(int i = 0; i <Global.directions.Length; i++)
		{
			if(manager.getTile[tile.x+Global.directions[i].x,tile.y+Global.directions[i].y].type == (int)TileType.tile.DESERT)
			{
				returnResources[(int)TileType.resource.FOOD]++;
			}
		}
		return returnResources;
	}
	/************************************************
	 * Animals
	 * *******************************************/
	public int[] Snake(Tile tile)
	{
		//Adds gold depending on the total amount of food in the adjacent tiles
		int[] returnResources = new int[3];
		
		for(int i = 0; i <Global.directions.Length; i++)
		{
			Tile adjacentTile = manager.getTile[tile.x+Global.directions[i].x,tile.y+Global.directions[i].y];
			returnResources[(int)TileType.resource.GOLD] += adjacentTile.resources[(int)TileType.resource.FOOD]/2;
		}
		return returnResources;
	}
	/************************************************
	 * Minerals
	 * *******************************************/
	public int[] Glass(Tile tile)
	{
		//Adds science when lit on fire
		int[] returnResources = new int[3];
		returnResources[(int)TileType.resource.SCIENCE] = Global.tileTypes[(int)TileType.tile.DESERT];

		return returnResources;
	}
}

