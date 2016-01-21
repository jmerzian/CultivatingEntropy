using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIMain : MonoBehaviour 
{
	public TileManager tiles;
	public MenuController menu;
	public AudioManager sounds;
	int Buffer = Screen.height/10; //measurement for spacing and button dimensions -- to scale with screen
	int ButtonHeight;
	int ButtonWidth;
	public GUISkin style;
	
	bool firstRun = true;
	
	public string element;
	
	public LayerMask castMask = -257;
	
	void OnGUI ()
	{
		if (firstRun) {
			menu.StartWindowOpen = true;
			firstRun = false;
		}
		
		GUI.skin = style;
		
		if (!Global.pause) 
		{	
			ButtonWidth = ButtonHeight = Buffer;
			if (GUI.Button (new Rect (0, 0, ButtonWidth * 2, ButtonWidth), Resource.Menu_Btn)) 
			{
				sounds.Play (Resource.Click, 1f);
				menu.StartWindowOpen = true;
			}
			//Score and level
			//List<WinCondition> win = new List<WinCondition>(tiles.winControl.currentConditions.Keys);
			GUI.BeginGroup (new Rect(ButtonWidth * 2, ButtonWidth/3,Screen.width-(ButtonWidth * 4),ButtonWidth));
			GUI.DrawTexture(new Rect(0,0,Screen.width-(ButtonWidth * 4),ButtonWidth),Resource.CenterConsole);
			/*for(int i = 0; i < win.Count; i++)
			{
				GUI.Label(new Rect(0,i*20,Screen.width-(ButtonWidth * 4),ButtonWidth),win[i].description);
			}*/
			GUI.EndGroup();

			//Buttons
			GUI.BeginGroup (new Rect (Screen.width - ButtonWidth, Buffer * 3, ButtonWidth, Screen.height));

			if(Input.GetKeyDown(KeyCode.Q))
			{
				//element = (int)TileType.element.FIRE;
				Cursor.SetCursor (Resource.Fire_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if(Input.GetKeyDown(KeyCode.W))
			{
				//element = (int)TileType.element.WATER;
				Cursor.SetCursor (Resource.Water_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if(Input.GetKeyDown(KeyCode.E))
			{
				//element = (int)TileType.element.EARTH;
				Cursor.SetCursor (Resource.Earth_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if(Input.GetKeyDown(KeyCode.R))
			{
				//element = (int)TileType.element.AIR;
				Cursor.SetCursor (Resource.Air_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}

			if (GUI.Button (new Rect (0, 0, ButtonWidth, ButtonHeight), Resource.Fire_Btn)) {
				//element = (int)TileType.element.FIRE;
				Cursor.SetCursor (Resource.Fire_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if (GUI.Button (new Rect (0, ButtonHeight, ButtonWidth, ButtonHeight), Resource.Water_Btn)) {
				//element = (int)TileType.element.WATER;
				Cursor.SetCursor (Resource.Water_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if (GUI.Button (new Rect (0, ButtonHeight * 2, ButtonWidth, ButtonHeight), Resource.Earth_Btn)) {
				//element = (int)TileType.element.EARTH;
				Cursor.SetCursor (Resource.Earth_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if (GUI.Button (new Rect (0, ButtonHeight * 3, ButtonWidth, ButtonHeight), Resource.Air_Btn)) {
				//element = (int)TileType.element.AIR;
				Cursor.SetCursor (Resource.Air_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			
			GUI.EndGroup ();

			GUI.BeginGroup (new Rect (Buffer * 7, Screen.height - ButtonHeight, Screen.width, ButtonHeight*2));
			GUI.DrawTexture(new Rect(0,0,ButtonWidth*3,ButtonHeight),Resource.CenterConsole);
			if (GUI.Button (new Rect (0, 0, ButtonWidth, ButtonHeight), "PLANT"))
			{
				//element = -1;//(int)TileType.element.WATER;
				//upgrade = (int)TileType.enhancement.PLANT;
				Cursor.SetCursor(Resource.Menu_Cursor,new Vector2 (0.5f,0),CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if (GUI.Button (new Rect (ButtonWidth, 0, ButtonWidth, ButtonHeight), "ANIMAL")) 
			{
				//element = -1;//(int)TileType.element.EARTH;
				//upgrade = (int)TileType.enhancement.ANIMAL;
				Cursor.SetCursor(Resource.Menu_Cursor,new Vector2 (0.5f,0),CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if (GUI.Button (new Rect (ButtonWidth*2, 0, ButtonWidth, ButtonHeight), "MINERAL")) 
			{
				//element = -1;//(int)TileType.element.AIR;
				//upgrade = (int)TileType.enhancement.MINERAL;
				Cursor.SetCursor(Resource.Menu_Cursor,new Vector2 (0.5f,0),CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			GUI.EndGroup ();
		}
	}
	
	void Update()
	{
		if (!Global.pause) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit click;
			if (Physics.Raycast (ray, out click, Mathf.Infinity, castMask)) // Added layerMask to avoid hitting mouseDrag plane.
			{
				if (Input.GetMouseButtonDown (0)) 
				{
					tiles.ChangeTile(element,tiles.tileFromObject[click.transform.gameObject]);
				}
				tiles.OnHover (click.transform.gameObject);
			}
		}
	}
}