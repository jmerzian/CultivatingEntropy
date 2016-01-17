using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class UpgradeManager : MonoBehaviour
{
	public TileManager manager;

	public List<TileType.resourceFunction> plantFunctions; 
	public List<TileType.resourceFunction> animalFunctions;
	public List<TileType.resourceFunction> mineralFunctions;

	public void Awake()
	{
		plantFunctions = new List<TileType.resourceFunction> (){
			Cactus
		};
		animalFunctions = new List<TileType.resourceFunction> (){
			Snake
		};
		mineralFunctions = new List<TileType.resourceFunction> (){
			Glass
		};
	}

	public void AddUpgrade(GameObject tile, int enhancement)
	{
		int type;
		//Perform a lookup
		switch(enhancement)
		{
		case (int)TileType.enhancement.PLANT:
			type = UpgradeHelper.PlantLookup (manager.tileFromObject [tile].type);
			if(type != -1)
			{
				Upgrade newUpgrade = new Upgrade(manager.tileFromObject [tile],plantFunctions[type],enhancement,type);
				SetFlair(manager.getFlair[tile],Resource.plantMeshes[type],Resource.plantMaterials[type]);
				manager.tileUpgrades.Add(newUpgrade);
				manager.tileFromObject [tile].upgrade = newUpgrade;
			}
			break;
		case (int)TileType.enhancement.ANIMAL:
			type = UpgradeHelper.PlantLookup (manager.tileFromObject [tile].type);
			if(type != -1)
			{
				Upgrade newUpgrade = new Upgrade(manager.tileFromObject [tile],animalFunctions[type],enhancement,type);
				SetFlair(manager.getFlair[tile],Resource.animalMeshes[type],Resource.animalMaterials[type]);
				manager.tileUpgrades.Add(newUpgrade);
				manager.tileFromObject [tile].upgrade = newUpgrade;
			}
			break;
		case (int)TileType.enhancement.MINERAL:
			type = UpgradeHelper.PlantLookup (manager.tileFromObject [tile].type);
			if(type != -1)
			{
				Upgrade newUpgrade = new Upgrade(manager.tileFromObject [tile],mineralFunctions[type],enhancement,type);
				SetFlair(manager.getFlair[tile],Resource.mineralMeshes[type],Resource.mineralMaterials[type]);
				manager.tileUpgrades.Add(newUpgrade);
				manager.tileFromObject [tile].upgrade = newUpgrade;
			}
			break;
		case -1:
			Debug.Log("Nothing goes here");
			break;
		}
	}

	public void UpdateUpgrades()
	{
		for(int i = 0; i < manager.tileUpgrades.Count; i++)
		{
			Tile newTile = manager.tileUpgrades[i].tile;
			newTile.resources = manager.tileUpgrades[i].function(newTile);
		}
	}

	public void RemoveUpgrade(Upgrade tile)
	{
		tile.tile.resources = new int[3];
		manager.tileUpgrades.Remove (tile);
	}

	private void SetFlair(GameObject flair, Mesh mesh, Material material)
	{
		//Set the flair material and mesh
		flair.GetComponent<MeshRenderer>().material = material;
		flair.GetComponent<MeshFilter>().mesh = mesh;	
	}
}
