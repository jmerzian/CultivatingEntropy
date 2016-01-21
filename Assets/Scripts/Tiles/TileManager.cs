using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour 
{
	/************************************************
	 * Setting up the game and objects which will be used
	 * **********************************************/
	//Game map
	public Tile[,] getTile;
	public Cartographer mapControl;

	//Hover functions
	private GameObject lastHover;

	public bool tilesLoaded;

	//Tile to game object
	public Dictionary<GameObject,Tile> tileFromObject = new Dictionary<GameObject,Tile>();
	public Dictionary<Tile,GameObject> objectFromTile = new Dictionary<Tile,GameObject>();
	public Dictionary<GameObject,GameObject> getFlair = new Dictionary<GameObject,GameObject> ();

	//Scale of the world corresponding to the X,Y coordinates
	public Vector3 worldScale;
	public Vector2 boardSize;

	//Since this just replaces the flair tile on the tile, it doesn't need to be linked to a gameobject
	public List<Disaster> disasters = new List<Disaster>();

	public void ClearLastMap()
	{
		for (int i = transform.childCount-1; i >= 0; i--)
		{
			GameObject child = transform.GetChild(i).gameObject;
			Destroy( child );
		}
	}

	public void Turn()
	{
		for(int i = 0; i < disasters.Count; i++)
		{
			disasters[i].Turn();
		}
	}

	public void Update()
	{
		Turn ();
	}

	public void AddDisaster(string type, int x, int y) 
	{
		Vector3 newPosition = getTile [x, y].position + new Vector3 (0,0.5f,0);
		Disaster newDisaster = Resource.disasterTemplate[type].Instantiate(newPosition,x,y);
		
		getFlair[objectFromTile[getTile[x,y]]].GetComponent<MeshFilter>().mesh = newDisaster.mesh;
		getFlair[objectFromTile[getTile[x,y]]].GetComponent<MeshRenderer>().material = Resource.baseMaterial;
		getFlair[objectFromTile[getTile[x,y]]].GetComponent<MeshRenderer>().material.mainTexture = newDisaster.material;

		disasters.Add (newDisaster);
		newDisaster.Init ();
	}

	public void ChangeTile(string type, Tile tile)
	{
		if(Resource.tileTemplate.ContainsKey(type))
		{
			Change(objectFromTile[tile],Resource.tileTemplate[type]);
		}
	}

	public void Change(GameObject tile, Tile changedTile)
	{
		//Set the material
		tile.GetComponent<MeshRenderer>().material.mainTexture = changedTile.material;
		//Add the flair
		Transform flair = getFlair [tile].transform;
		//Set the flair material and mesh
		flair.GetComponent<MeshRenderer>().material.mainTexture = changedTile.material;
		flair.GetComponent<MeshFilter>().mesh = changedTile.mesh;	
		
		//change the tile elevation, no idea what's going on with the y... but that does seem to fix it
		tile.transform.position = new Vector3 (tile.transform.position.x, 
		                                       worldScale.y, 
		                                       tile.transform.position.z);
	}

	public void AddTile(string tile, int X, int Y)
	{
		if(Resource.tileTemplate.ContainsKey(tile))
		{
			Vector3 newLocation = new Vector3 (
				worldScale.x * X,
				worldScale.y,
				worldScale.z * Y);
			Tile newTile = Resource.tileTemplate[tile].Instantiate(newLocation,X,Y);

			getTile [X,Y] = newTile;

			Place(newTile, X, Y);
		}
	}

	public void Place(Tile tile, int x, int y)
	{
		//Creates a new tile
		//A purely cosmetic object to be placed above the base tile
		GameObject newTile = new GameObject ("Tile" + x + "," + y);
		GameObject tileFlair = new GameObject ("Flair");
		//adds it to the dictionary
		tileFromObject.Add (newTile, tile);
		objectFromTile.Add (tile, newTile);
		getFlair.Add (newTile,tileFlair);
		
		//Creates a collider for Jade to hit with a raycast (can't remember if a rigidbody is needed....
		BoxCollider collider = newTile.AddComponent<BoxCollider> ();
		collider.size = worldScale;
		//TODO get the size correct
		collider.center = new Vector3 (0, worldScale.y / 2, 0);
		
		//Creates the mesh holder and adds a mesh
		MeshFilter filter = newTile.AddComponent<MeshFilter> ();
		MeshRenderer renderer = newTile.AddComponent<MeshRenderer>();
		MeshFilter flairFilter = tileFlair.AddComponent<MeshFilter> ();
		MeshRenderer flairRenderer = tileFlair.AddComponent<MeshRenderer>();
		
		filter.mesh = Resource.baseMesh;
		flairFilter.mesh = tile.mesh;
		
		renderer.material = new Material(Resource.baseMaterial);
		renderer.material.mainTexture = tile.material;
		flairRenderer.material = new Material(Resource.baseMaterial);
		flairRenderer.material.mainTexture = tile.material;
		
		//position the gameobject
		newTile.transform.position = tile.position;
		tileFlair.transform.position = new Vector3 (tile.position.x, tile.position.y + 0.5f, tile.position.z);
		newTile.transform.parent = transform;
		tileFlair.transform.parent = newTile.transform;
	}

	public void CreateMap(int X, int Y)
	{

		string[,] map;
		mapControl = new Cartographer (this);
		map = mapControl.GenerateBiomes (X, Y);
		//Create a new array with the size of the fed int array
		getTile = new Tile[map.GetLength(0),map.GetLength(1)];
		
		//Clear the old game map
		ClearLastMap ();
		
		//Go through the fed int array and set the map
		for(int x = 0; x < map.GetLength(0); x++)
		{
			for(int y = 0; y < map.GetLength(1); y++)
			{
				//Add a new tile
				AddTile(map[x,y],x,y);
			}
		}
		//Set the board size
		boardSize = new Vector2 (map.GetLength(0)*worldScale.x,map.GetLength(1)*worldScale.z);
		//Position the camera accordingly
		//cameraControl.Init();
	}

	//When the mouse is hovering over a tile
	public void OnHover(GameObject tile)
	{
		if (lastHover != null)
		{
			lastHover.GetComponent<MeshRenderer>().material.mainTexture = tileFromObject[lastHover].material;
			getFlair[lastHover].GetComponent<MeshRenderer> ().material.mainTexture = tileFromObject[lastHover].material;
		}
		//check that the material being used has a Color component which can be changed
		if(tile.GetComponent<MeshRenderer> ().material.HasProperty("_Color"))
		{
			tile.GetComponent<MeshRenderer> ().material.color += new Color(0.5f,0.5f,0.5f);
			if(getFlair.ContainsKey(tile))getFlair[tile].GetComponent<MeshRenderer> ().material.color += new Color(0.5f,0.5f,0.5f);
			lastHover = tile;
		}
	}
}