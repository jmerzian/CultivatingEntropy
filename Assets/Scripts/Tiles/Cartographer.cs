using UnityEngine;
using System.Collections.Generic;

public class Cartographer
{
	public TileManager manager;

	private string[,] mapData;

	public float noise = 0.2f;
	public float baseNoise = 0.2f;

	public Cartographer(TileManager Manager)
	{
		manager = Manager;
	}

	public string[,] GenerateBiomes (int width, int height) 
	{
		
		mapData = new string[width, height];
		
		int[,] elevData = new int[width, height];
		int[,] precipData = new int[width, height];
		
		// The double perlin?  Probably.  At least double.  Maybe for each element?  I need combinations, so math time.
		float elevEcks = Random.Range (0f, 500f);
		float elevWhy = Random.Range (0f, 500f);
		
		float precipEcks = Random.Range (0f, 500f);
		float precipWhy = Random.Range (0f, 500f);
		
		for (int why = 0; why < height; why++) 
		{
			// Create list and sort by elevation data
			List<Tile> tile = new List<Tile>(Resource.tileTemplate.Values);
			tile.Sort((x,y) => x.elevation.CompareTo(y.elevation));

			for (int ecks = 0; ecks < width; ecks++) 
			{
				// Need to generate more extreme data here.
				float noiseEcks = ((float)ecks)*noise;
				float noiseWhy = ((float)why)*noise;
				
				
				elevData[ecks, why] = Mathf.RoundToInt(Resource.tileTemplate.Count*Mathf.PerlinNoise(elevEcks+noiseEcks, elevWhy+noiseWhy));
				if(elevData[ecks,why] >= Resource.tileTemplate.Count) elevData[ecks, why] = Resource.tileTemplate.Count-1;
				else if(elevData[ecks,why] < 0) elevData[ecks,why] = 0;

				mapData[ecks,why] = tile[elevData[ecks,why]].name;
			}
		}
		
		return mapData;
	}

}

