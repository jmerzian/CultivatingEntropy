using UnityEngine;
using System.Collections;

public partial class WinManager: MonoBehaviour
{
	public bool GetFood (int difficulty, ref string printOut)
	{
		int highest = 0;
		int check = 20 * difficulty;
		//Scale according to difficulty
		foreach(Village village in manager.villageControl.villages)
		{
			int temp = village.resources[(int)TileType.resource.FOOD];
			if(temp > highest) highest = temp;
		}
		printOut = "One villiage with " + highest + "/" + check + " food";
		if (highest >= check)return true;
		else return false;
	}
	public bool GetGold (int difficulty, ref string printOut)
	{
		int highest = 0;
		int check = 20 * difficulty;
		//Scale according to difficulty
		foreach(Village village in manager.villageControl.villages)
		{
			int temp = village.resources[(int)TileType.resource.GOLD];
			if(temp > highest) highest = temp;
		}
		printOut = "One villiage with " + highest + "/" + check + " gold";
		if (highest >= check)return true;
		else return false;
	}
	public bool GetScience (int difficulty, ref string printOut)
	{
		int highest = 0;
		int check = 20 * difficulty;
		//Scale according to difficulty
		foreach(Village village in manager.villageControl.villages)
		{
			int temp = village.resources[(int)TileType.resource.SCIENCE];
			if(temp > highest) highest = temp;
		}
		printOut = "One villiage with " + highest + "/" + check + " science";
		if (highest >= check)return true;
		else return false;
	}
	public bool WorldFood (int difficulty, ref string printOut)
	{
		int total = 0;
		int check = 40 * difficulty;
		//Scale according to difficulty
		foreach(Village village in manager.villageControl.villages)
		{
			total += village.resources[(int)TileType.resource.FOOD];
		}
		printOut = total + "/" + check + " food across the world";
		if (total >= check)return true;
		else return false;
	}
	public bool WorldGold (int difficulty, ref string printOut)
	{
		int total = 0;
		int check = 40 * difficulty;
		//Scale according to difficulty
		foreach(Village village in manager.villageControl.villages)
		{
			total += village.resources[(int)TileType.resource.GOLD];
		}
		printOut = total + "/" + check + " gold across the world";
		if (total >= check)return true;
		else return false;
	}
	public bool WorldScience (int difficulty, ref string printOut)
	{
		int total = 0;
		int check = 40 * difficulty;
		//Scale according to difficulty
		foreach(Village village in manager.villageControl.villages)
		{
			total += village.resources[(int)TileType.resource.SCIENCE];
		}
		printOut = total + "/" + check + " science across the world";
		if (total >= check)return true;
		else return false;
	}
}