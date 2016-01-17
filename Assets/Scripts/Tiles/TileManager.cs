using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*****************************************
 * This file is dedicated to initial setup
 * This contains all the constructors, map setup, and new game stuffs
 * ****************************************/
public partial class TileManager : MonoBehaviour
{
	//Game map
	public Tile[,] getTile;

	//Camera control
	public CameraControl cameraControl;

	//Other game managers
	public CascadeManager cascade;
	public Cartographer mapControl;
	public AudioManager audioControl;
	public VillageManager villageControl;
	public UpgradeManager upgradeControl;
	public WinManager winControl;

	//Tile to game object
	public Dictionary<GameObject,Tile> tileFromObject = new Dictionary<GameObject,Tile>();
	public Dictionary<Tile,GameObject> objectFromTile = new Dictionary<Tile,GameObject>();
	public Dictionary<GameObject,GameObject> getFlair = new Dictionary<GameObject,GameObject> ();
	//Scale of the world corresponding to the X,Y coordinates
	public Vector3 worldScale;
	public Vector2 boardSize;
	//Plant manager
	//public PlantManager plant;
	
	public List<Upgrade> tileUpgrades = new List<Upgrade>();
	public List<Fire> fires = new List<Fire>();
	public List<Storm> storms = new List<Storm>();
	public List<Disaster> disasters = new List<Disaster>();

	private float lastTime = 0;
	private float gameTurn = 0.5f;

	//Hover functions
	private GameObject lastHover;

	//When the game starts, do this
	public void Start()
	{
		NewLevel (10);
		cascade = new CascadeManager(this);
	}

	//Adds a new tile
	public void Add(int tileType, int X, int Y)
	{
		//scale to world coordinates
		Vector3 newLocation = new Vector3(worldScale.x*X,worldScale.y,worldScale.z*Y);
		Tile newTile = new Tile (tileType,newLocation,X,Y);

		//add to array
		getTile [X, Y] = newTile;

		//create the gameobject
		Place (newTile,X,Y);
	}

	//Place a new tile in the scene
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

		renderer.material = tile.material;
		flairRenderer.material = tile.material;
		//TODO add a texture and shader

		//position the gameobject
		newTile.transform.position = tile.position;
		tileFlair.transform.position = new Vector3 (tile.position.x, tile.position.y + 0.5f, tile.position.z);
		newTile.transform.parent = transform;
		tileFlair.transform.parent = newTile.transform;
	}

	public void NewLevel(int difficulty)
	{
		ClearLevel ();
		winControl.NewWinConditions (3,difficulty);

		//build the map
		mapControl.BuildDifficulty ((difficulty-1)+(difficulty*Global.levelNumber));

		//Change the map position so it is centered
		Global.center = objectFromTile [getTile[getTile.GetLength (0) - 1, getTile.GetLength (1) - 1]].transform.position;
		Global.center.Scale(new Vector3(-0.25f,-0.25f,-0.25f));
		transform.position = Global.center;
	}

	public void ClearLevel()
	{
		//Set the position to zero
		transform.position = Vector3.zero;
		//reset the score
		Global.turns = 0;
		//Clear all the dictionaries
		tileFromObject.Clear();
		objectFromTile.Clear ();
		getFlair.Clear ();
		while (fires.Count > 0)
			fires [fires.Count - 1].Kill ();
		fires.Clear ();
		while (storms.Count > 0)
			storms [storms.Count - 1].Kill ();
		storms.Clear ();
		while (disasters.Count > 0)
			disasters [disasters.Count - 1].Kill ();
		disasters.Clear ();
		villageControl.Clear ();
		
		//Set the lastHover to null so it is ignored
		lastHover = null;
		
		//Set the game states
		Global.win = false;
		Global.lose = false;
		Global.playingTheme = false;

		//Clear the counters for number of tiles and types of tiles
		Global.tileTypes = new int[7];
		Global.numOfTiles = 0;
	}

	//Create map using a multidimensional array of ints corresponding to the TileType.type ENUM
	public void CreateMap(int[,] map)
	{
		//Create a new array with the size of the fed int array
		getTile = new Tile[map.GetLength(0),map.GetLength(1)];

		//Clear the old game map
		for (int i = transform.childCount-1; i >= 0; i--)
		{
			GameObject child = transform.GetChild(i).gameObject;
			Destroy( child );
		}

		//Go through the fed int array and set the map
		for(int x = 0; x < map.GetLength(0); x++)
		{
			for(int y = 0; y < map.GetLength(1); y++)
			{
				//Add a new tile
				Add(map[x,y],x,y);
				//Update the types of & number of tiles on the map
				Global.numOfTiles++;
			}
		}
		//Set the board size
		boardSize = new Vector2 (map.GetLength(0)*worldScale.x,map.GetLength(1)*worldScale.z);
		//Position the camera accordingly
		cameraControl.Init();
	}
	
	public void OnHover(GameObject tile)
	{
		if (lastHover != null)
		{
			lastHover.GetComponent<MeshRenderer>().material = tileFromObject[lastHover].material;
			getFlair[lastHover].GetComponent<MeshRenderer> ().material = tileFromObject[lastHover].material;
		}
		tile.GetComponent<MeshRenderer> ().material.color += new Color(0.5f,0.5f,0.5f);
		if(getFlair.ContainsKey(tile))
			if(tileFromObject[tile].type != (int)TileType.tile.VILLAGE)
				getFlair[tile].GetComponent<MeshRenderer> ().material.color += new Color(0.5f,0.5f,0.5f);
		lastHover = tile;
	}

	/*****************************************
	 * This file is for doing things during gameplay
	 * this includes checking for updates, user interface etc.
	 * ****************************************/
	//Updating the map during game cycles
	public void Update()
	{
		if(Global.pause == false)
		{
			if(Time.time > lastTime+gameTurn)
			{
				lastTime = Time.time;
				Turn ();
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) Global.pause = !Global.pause;
	}
	
	private void UpdateAll()
	{
		villageControl.UpdateVillages ();
		upgradeControl.UpdateUpgrades ();
	}
	
	//Do a turn
	public void Turn()
	{
		//Grow the plants/fires/storms/disasters
		//plant.Grow ();
		if(fires.Count > 0)
		{
			for(int i = 0; i < fires.Count; i++)
			{
				fires[i].Grow();
			}
		}
		if(storms.Count > 0)
		{
			for(int i = 0; i < storms.Count; i++)
			{
				storms[i].Turn();
			}
		}
		if(disasters.Count > 0)
		{
			for(int i = 0; i < disasters.Count; i++)
			{
				disasters[i].Turn();
			}
		}
		//Check to see if the player has won yet...
		winControl.hasWon ();
		UpdateAll ();
		//Increase the turn count
		//Global.turns++;
	}
	
	
	//Change the tile object
	public void ChangeType(int x, int y, int element)
	{
		//get the game object
		GameObject tile = objectFromTile[getTile[x,y]];
		//Change it's type accoring to the TileHelper chart
		ChangeType (tile, element);
	}
	
	//Change the tile based on the gameObject
	public void ChangeType(GameObject tile, int element)
	{
		//Play the sound corresponding to element type
		audioControl.Play (Resource.elementSound [element],0.5f);
		
		//Apply an elemental effect
		Tile changedTile = tileFromObject [tile];
		cascade.OnElement (changedTile,element);
		Change (tile, changedTile);
		//Turn ();
	}
	
	//Actually change a tile
	public void Change(GameObject tile, Tile changedTile)
	{
		//Set the material
		tile.GetComponent<MeshRenderer>().material = changedTile.material;
		//Add the flair
		Transform flair = getFlair [tile].transform;
		//Set the flair material and mesh
		flair.GetComponent<MeshRenderer>().material = changedTile.material;
		flair.GetComponent<MeshFilter>().mesh = changedTile.mesh;		
		//If there was a resource here, kill it
		if(changedTile.upgrade != null)upgradeControl.RemoveUpgrade (changedTile.upgrade);
		UpdateAll ();
	}
	
	public void EnhanceTile(GameObject tile, int enhancement)
	{
		upgradeControl.AddUpgrade (tile,enhancement);
		UpdateAll ();
	}

	/*****************************************
	 * This file is for adding things to a gamemap
	 * this includes weather events, villages, upgrades etc.
	 * ****************************************/
	
	//Creates a village at the specified point
	public void AddVillage(int x, int y)
	{
		if (villageControl != null)
		{
			villageControl.AddVillage (getTile [x, y]);
			getTile [x, y].type = (int)TileType.tile.VILLAGE;
			Change(objectFromTile[getTile[x,y]],getTile[x,y]);
		}
		UpdateAll ();
	}
	
	//Creates fire at the specified point
	public void AddFire(int x, int y)
	{
		GameObject newFireObject = new GameObject ("Fire" + x + "," + y);
		Fire newFire = newFireObject.AddComponent<Fire>();
		newFire.manager = this;
		newFire.StartFire(getTile[x,y]);
		fires.Add (newFire);
	}
	
	//Creates a storm at the specified tile
	public void AddStorm(int x, int y)
	{
		GameObject newStormObject = new GameObject ("Storm" + x + "," + y);
		Storm newStorm = newStormObject.AddComponent<Storm>();
		newStorm.manager = this;
		newStorm.StartStorm(getTile[x,y]);
		storms.Add (newStorm);
	}
	
	//Adds a new disaster at the specified tile
	public void AddDisaster(int x, int y) 
	{
		GameObject newDisasterObject = new GameObject ("Disaster" + x + "," + y);
		Disaster newDisaster = newDisasterObject.AddComponent<Disaster>();
		newDisaster.manager = this;
		newDisaster.StartDisaster(getTile[x,y]);
		disasters.Add (newDisaster);
		//Remove existing flair on that tile
		getFlair [objectFromTile [getTile [x, y]]].transform.GetComponent<MeshFilter> ().mesh = null;
	}
	

}
