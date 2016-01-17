using UnityEngine;
using System.Collections;

public static class TileType
{
	//Enumerators for tile and element types
	public enum tile: int{DESERT,MARSH,FOREST,LAKE,MOUNTAIN,PLAIN,CRAGS,VILLAGE};
	public enum element: int{EARTH,AIR,WATER,FIRE};
	public enum disaster: int{STORM,VOLCANO,FLOOD,EARTHQUAKE};

	//Stuff for enhancing tiles
	public enum resource: int{FOOD,GOLD,SCIENCE};
	public enum enhancement: int{PLANT, ANIMAL, MINERAL};

	//Delegate template for calculating points at any given tile
	public delegate int[] resourceFunction (Tile tile);
	//types of plants
	public enum plantTypes: int{
		CACTUS,CATTAILS,APPLE,SEAWEED,PINE,GRASS,SAGE,//Level one plants
		//Level 2 plants
	};
	//types of animals
	public enum animalTypes: int{
		SNAKE,ALLIGATOR,DEER,FISH,BEAR,PRARIEDOG,ANTELLOPE,//level one animals
		//Level 2 animals
	};
	//types of mineral
	public enum mineralTypes: int{
		GLASS,PEAT,COAL,CORAL,COPPER,CLAY,STONE,//level one minerals
		//Level 2 minerals
	};
}