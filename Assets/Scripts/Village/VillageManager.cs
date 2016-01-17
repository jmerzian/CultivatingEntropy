using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour
{
	public List<Village> villages = new List<Village>();
	public TileManager tileControl;

	public void AddVillage(Tile newTile)
	{
		GameObject tile = tileControl.objectFromTile [newTile];
		GameObject newVillage = new GameObject ("Village");
		Village newVillageObject = new Village (newTile,tileControl);

		villages.Add (newVillageObject);

		MeshFilter filter = newVillage.AddComponent<MeshFilter> ();
		MeshRenderer renderer = newVillage.AddComponent<MeshRenderer>();

		//temporary placeholder for art
		filter.mesh = Resource.villageMesh;
		renderer.material = Resource.tileMaterial[6];
		
		newVillage.transform.parent = tile.transform;
		newVillage.transform.position = new Vector3 (tile.transform.position.x, tile.transform.position.y+tileControl.worldScale.y, tile.transform.position.z);
	}

	public void UpdateVillages()
	{
		for(int i = 0; i < villages.Count; i++)
		{
			villages[i].GatherResources();
		}
	}

	public void Clear()
	{
		villages.Clear ();
	}

	public void OnGUI()
	{
		for(int i = 0; i < villages.Count; i++)
		{
			GameObject tileObject = tileControl.getFlair[tileControl.objectFromTile[villages[i].tile]];
			Vector3 objectPosition = tileObject.transform.position;
			objectPosition += new Vector3(0,1,0);
			Vector3 position = Camera.main.WorldToScreenPoint(objectPosition);
			for(int j = 0; j < 3; j++)
			{
				GUI.Label(new Rect(position.x,(Screen.height-position.y)+(20*j),60,20),villages[i].resources[j].ToString());
			}
		}
	}
}

