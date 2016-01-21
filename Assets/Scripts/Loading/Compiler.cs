using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;
using UnityEngine;

public static class Compiler
{
	/*public static Action GetAction(string file, string name, string function)
	{
		Assembly assembly = Compile(file);

		Type[] availableTypes = assembly.GetTypes ();
		MethodInfo method = assembly.GetType("Earthquake").GetMethod(function);
		Action del = (Action)Delegate.CreateDelegate(typeof(Action), method);
		return del;
	}*/

	public static DisasterFunction GetDisasterFunction(string source, string name, int X, int Y)
	{
		try
		{
			Assembly pluginAssembly = Compile(source);
			Type classType = pluginAssembly.GetType(name);
			
			DisasterFunction plugin = (DisasterFunction) Activator.CreateInstance(classType, X, Y);
			
			if (plugin == null)
				throw new Exception("Plugin not correctly configured");

			return plugin;
		}
		catch (Exception e)
		{
			Debug.Log(e);
			return null;
		}
	}
	
	public static Assembly Compile(string source)
	{
		var provider = new CSharpCodeProvider();
		var param = new CompilerParameters();
		
		// Add ALL of the assembly references
		foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
		{
			param.ReferencedAssemblies.Add(assembly.Location);
		}
		
		// Add specific assembly references
		//param.ReferencedAssemblies.Add("System.dll");
		//param.ReferencedAssemblies.Add("CSharp.dll");
		//param.ReferencedAssemblies.Add("UnityEngines.dll");
		
		// Generate a dll in memory
		param.GenerateExecutable = false;
		param.GenerateInMemory = true;
		
		// Compile the source
		var result = provider.CompileAssemblyFromSource(param, source);
		
		if (result.Errors.Count > 0) 
		{
			var msg = new StringBuilder();
			foreach (CompilerError error in result.Errors) 
			{
				msg.AppendFormat("Error ({0}): {1}\n",
				                 error.ErrorNumber, error.ErrorText);
			}
			throw new Exception(msg.ToString());
		}
		
		// Return the assembly
		return result.CompiledAssembly;
	}
}

/* test script
 * @"
        using UnityEngine;
 
        public class Test : Disaster
        {
            public static void Foo()
            {
                Debug.Log(""Did that actually work?!?!"");
            }
        }"

*/
