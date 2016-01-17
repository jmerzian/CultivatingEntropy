using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Village 
{
	public Tile tile;
	public int radius = 2;
	public int[] resources = new int[3];
	public TileManager manager;

	public Village(Tile pos,TileManager tileControl)
	{
		tile = pos;
		manager = tileControl;
	}
	public void GatherResources()
	{
		resources = new int[3];
		for(int x = -radius; x <= radius; x++)
		{
			for(int y = -radius; y <= radius; y++)
			{
				for(int i = 0; i < resources.Length; i++)
					resources[i] += manager.getTile[tile.x+x,tile.y+y].resources[i];
			}
		}
	}
}
