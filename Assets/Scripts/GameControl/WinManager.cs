using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class WinManager: MonoBehaviour
{
	public TileManager manager;
	public List<WinCondition> conditions;
	//The win condition and the difficulty of the win condition
	public Dictionary<WinCondition,int> currentConditions;

	public void Awake()
	{
		//Needs to be in a function as you cant call non-static variables
		//from outside of a function, which is lame...
		conditions  = new List<WinCondition> ()
		{
			new WinCondition("Get Food: ", (WinCondition.winCondition)GetFood),
			new WinCondition("Get Gold: ", (WinCondition.winCondition)GetGold),
			new WinCondition("Get Science: ", (WinCondition.winCondition)GetScience),
			new WinCondition("World Food: ", (WinCondition.winCondition)WorldFood),
			new WinCondition("World Gold: ", (WinCondition.winCondition)WorldFood),
			new WinCondition("World Science: ", (WinCondition.winCondition)WorldGold),
		};
	}

	public void NewWinConditions(int quantity,int difficulty)
	{
		currentConditions = new Dictionary<WinCondition, int> ();
		for(int i = 0; i < quantity; i++)
		{
			//Select a new random win condition
			WinCondition newCondition = conditions[Random.Range(0,conditions.Count)];
			//If it already exists in the dictionary, select a new one
			if(!currentConditions.ContainsKey(newCondition))
				currentConditions.Add(newCondition,difficulty);
			else i--;
		}
	}

	public void hasWon()
	{
		bool fullWin = true;
		bool[] win = new bool[currentConditions.Keys.Count];
		List<WinCondition> conditionList = new List<WinCondition>(currentConditions.Keys);
		//Check all the win conditions
		for(int i = 0; i < conditionList.Count; i++)
		{
			WinCondition condition = conditionList[i];
			if(condition.function(currentConditions[condition],ref condition.description)) 
				win[i] = true;
		}
		//if all the win conditions are true, you have won
		for(int i = 0; i < win.Length; i++)
		{
			if(!win[i]) fullWin = false;
		}
		//Set the global win to true
		Global.win = fullWin;
	}
}

public class WinCondition
{
	public delegate bool winCondition(int quantity, ref string printOut);

	public string description;
	public winCondition function;

	public WinCondition(string newDescription, winCondition newFunction)
	{
		description = newDescription;
		function = newFunction;
	}
}