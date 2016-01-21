using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Xml;

public static class CFGParser
{
	public static List<Dictionary<string,string>> Parse(string file)
	{
		List<Dictionary<string,string>> objectList = new List<Dictionary<string,string>> ();
		Dictionary<string,string> objects = new Dictionary<string,string> ();
		StreamReader reader = File.OpenText(file);
		string line;
		while((line = reader.ReadLine()) != null)
		{
			//Create a new parameter object
			if(line.Contains('{'))
			{
				objects = new Dictionary<string,string> ();
			}
			//close the current object and add it to the object list
			else if(line.Contains('}'))
			{
				objectList.Add(objects);
			}
			else
			{
				string[] item = Regex.Split(line," = ");
				if(item.Count() >= 2) 
				{
					for(int i = 0; i < item.Count(); i++) 
					{
						item[i] = item[i].Trim();
					}
					objects.Add(item[0],item[1]);
				}
			}
		}
		return objectList;
	}
}

