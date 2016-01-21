using UnityEngine;
using System.Collections;

public static class MeshHelper
{
	public static void ScaleMesh(ref Mesh mesh, float scale)
	{
		Vector3[] baseVertices = mesh.vertices;
		
		var vertices = new Vector3[baseVertices.Length];
		
		for (var i=0;i<vertices.Length;i++)
		{
			Vector3 vertex = baseVertices[i];
			vertex.x = vertex.x * scale;
			vertex.y = vertex.y * scale;
			vertex.z = vertex.z * scale;
			
			vertices[i] = vertex;
		}
		
		mesh.vertices = vertices;

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}
}

