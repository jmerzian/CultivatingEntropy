using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Xml;

public class LoadManager : MonoBehaviour
{
	private bool[] loaded = new bool[10];

	public TileManager tile;

	void Start ()
	{
		loaded [0] = true;
		StartCoroutine(LoadReactionTypes(1));
		StartCoroutine(LoadDisasterTypes (3));
		StartCoroutine(LoadTileTypes (2));
	}

	//Load the different tile types
	private IEnumerator LoadTileTypes(int order) 
	{		
		//wait for previous step to get completed before loading next thing
		while(!loaded[order-1])
		{
			yield return null;
		}
		//Get all the folders in the Tile directory
		string topLevel = Application.streamingAssetsPath + "/Tiles/";
		string[] topLevelFiles = Directory.GetFiles (topLevel);
		foreach (string file in topLevelFiles)
		{
			//Get the extension as that tells us what to do with it
			string extention = Path.GetExtension(file);
			//cfg file tells us what this folder is actually used for
			if(extention == ".cfg")
			{
				//Parse it out
				List<Dictionary<string,string>> parsedFile = CFGParser.Parse(file);
				//Loop through the output
				foreach(Dictionary<string,string> newObject in parsedFile)
				{
					//Create new reactions and fill them with the appropriate data
					Tile newTile = new Tile(tile);

					//Set name and description
					newTile.name = newObject["Name"];
					newTile.description = newObject["Description"];

					newTile.elevation =  float.Parse(newObject["Elevation"]);
					newTile.growthFactor =  float.Parse(newObject["GrowthFactor"]);

					foreach(string reaction in Resource.reactionTemplate.Keys)
					{
						if(newObject.ContainsKey(reaction))newTile.reactionEffects.Add(reaction,newObject[reaction]);
					}

					//get the folder location
					string folder = topLevel + newObject["File"];
					string[] files = Directory.GetFiles (folder);

					foreach(string newFile in files)
					{
						//Get the extension as that tells us what to do with it
						string newExtention = Path.GetExtension(newFile);
						//mesh
						if(newExtention == ".obj") 
						{
							newTile.mesh = ObjImporter.ImportFile(newFile);
							MeshHelper.ScaleMesh(ref newTile.mesh,0.05f);
						}
						//material
						else if(newExtention == ".png")
						{
							byte[] fileData = File.ReadAllBytes(newFile);
							newTile.material = new Texture2D(1,1);
							newTile.material.LoadImage(fileData);
						}
						//Add other things here
						else
						{
							//Do nothing right now...
						}
					}					
					Resource.tileTemplate.Add(newTile.name,newTile);
					yield return null;
				}
			}
		}
		//components loaded
		loaded [order] = true;
		tile.CreateMap ((int)tile.boardSize.x, (int)tile.boardSize.y);
	}
	public IEnumerator LoadReactionTypes(int order)
	{
		//wait for previous step to get completed before loading next thing
		while(!loaded[order-1])
		{
			yield return null;
		}
		//Reactions found in Parsed file
		List<Reaction> newReactions = new List<Reaction> ();

		//Get all the folders in the Reaction directory
		string[] topLevelFiles = Directory.GetFiles (Application.streamingAssetsPath + "/Reactions/");
		foreach (string file in topLevelFiles)
		{
			//Get the extension as that tells us what to do with it
			string extention = Path.GetExtension(file);
			//cfg file tells us what this folder is actually used for
			if(extention == ".cfg")
			{
				//Parse it out
				List<Dictionary<string,string>> parsedFile = CFGParser.Parse(file);
				//Loop through the output
				foreach(Dictionary<string,string> newObject in parsedFile)
				{
					//Create new reactions and fill them with the appropriate data
					Reaction newReaction = new Reaction();

					newReaction.name = newObject["Name"];
					newReaction.description = newObject["Description"];

					Resource.reactionTemplate.Add(newReaction.name,newReaction);
				}
			}
			yield return null;
		}
		//components loaded
		loaded [order] = true;
	}

	//Load disaster types
	public IEnumerator LoadDisasterTypes(int order) 
	{
		//wait for previous step to get completed before loading next thing
		while(!loaded[order-1])
		{
			yield return null;
		}
		//Get all the folders in the Disaster directory
		string topLevel = Application.streamingAssetsPath + "/Disasters/";
		string[] topLevelFiles = Directory.GetFiles (topLevel);
		foreach (string file in topLevelFiles)
		{
			//Get the extension as that tells us what to do with it
			string extention = Path.GetExtension(file);
			//cfg file tells us what this folder is actually used for
			if(extention == ".cfg")
			{
				//Parse it out
				List<Dictionary<string,string>> parsedFile = CFGParser.Parse(file);
				//Loop through the output
				foreach(Dictionary<string,string> newObject in parsedFile)
				{
					//Create new reactions and fill them with the appropriate data
					Disaster newDisaster = new Disaster(tile);
					
					//Set name and description
					newDisaster.name = newObject["Name"];
					newDisaster.description = newObject["Description"];
					
					//get the folder location
					string folder = topLevel + newObject["File"];
					string[] files = Directory.GetFiles (folder);
					
					foreach(string newFile in files)
					{
						//Get the extension as that tells us what to do with it
						string newExtention = Path.GetExtension(newFile);
						//mesh
						if(newExtention == ".obj") 
						{
							newDisaster.mesh = ObjImporter.ImportFile(newFile);
							MeshHelper.ScaleMesh(ref newDisaster.mesh,0.05f);
						}
						//material
						else if(newExtention == ".png")
						{
							byte[] fileData = File.ReadAllBytes(newFile);
							newDisaster.material = new Texture2D(1,1);
							newDisaster.material.LoadImage(fileData);
						}
						else if(newExtention == ".cs")
						{
							string program = File.ReadAllText(newFile);
							//newDisaster.plugin = Compiler.GetDisasterFunction(program,newDisaster.name,tile);
							newDisaster.function = program;
						}
						//Add other things here
						else
						{
							//Do nothing right now...
						}
					}					
					Resource.disasterTemplate.Add(newDisaster.name,newDisaster);
					yield return null;
				}
			}
		}
		//components loaded
		loaded [order] = true;
		tile.AddDisaster ("Earthquake", 5, 5);
	}
}

