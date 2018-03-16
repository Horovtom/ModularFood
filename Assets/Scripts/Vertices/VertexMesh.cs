using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexMesh {
	private FunctionsRepository repository = new FunctionsRepository();
	private FunctionsRepository.Shape shape;
	private float scale;
	private int resolution;
	private GraphController parent;
	private Vector3[] vertices;

	public VertexMesh(GraphController parent, FunctionsRepository.Shape shape, float scale, int resolution) {
		this.parent = parent;
		this.shape = shape;
		this.scale = scale;
		this.resolution = resolution;
		repository.SetShape(shape);
		repository.SetTastes(11, 11, 11, 11, 11);
		ReDoMesh();
	}


	public void UpdateMesh(FunctionsRepository.Shape shape, float scale, float sweet, float sour, float umami, float bitter, float salty) {

		this.shape = shape;
		this.scale = scale;
		repository.SetShape(shape);
		repository.SetTastes(sweet, sour, umami, bitter, salty);
		ReDoMesh();
	}

	private void ReDoMesh() {
		Mesh mesh;
		MeshFilter filter = parent.GetComponent<MeshFilter>();
		filter.mesh = null;
		filter.mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";

		Vector2 uBounds = repository.GetURange(), vBounds = repository.GetVRange();
		float uSpan = uBounds.y - uBounds.x, vSpan = vBounds.y - vBounds.x;
		float uRes = uSpan / (this.resolution - 1), vRes = vSpan / (this.resolution - 1);

		vertices = new Vector3[resolution * resolution];
		Vector2[] uv = new Vector2[vertices.Length];

		float u, v;
		for (int y = 0, a = 0; y < this.resolution; y++) {
			v = vBounds.x + y * vRes;
			for (int x = 0; x < this.resolution; x++, a++) {
				u = uBounds.x + x * uRes;
				vertices[a] = repository.GetVect(u, v);
				uv[y] = new Vector2(x / this.resolution, y / this.resolution);
			}
		}

		mesh.vertices = vertices;
		mesh.uv = uv;




		int[] triangles = new int[this.resolution * this.resolution * 6];
		int verts = this.resolution * this.resolution;
		for (int i = 0; i < verts; i++) {
			triangles[i * 6] = i;
			triangles[(i * 6) + 1] = (i + 1) % verts;
			triangles[(i * 6) + 2] = (i + this.resolution + 1) % verts;
			triangles[(i * 6) + 3] = i;
			triangles[(i * 6 + 4)] = (i + 1 + this.resolution) % verts;
			triangles[(i * 6) + 5] = (i + this.resolution) % verts;
		}

		mesh.triangles = triangles;
		mesh.RecalculateTangents();
		mesh.RecalculateNormals();
	}

	public void DrawGizmos() {
		if (vertices == null)
			return;
		Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Length; i++) {
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}

}
