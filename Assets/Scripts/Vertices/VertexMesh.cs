using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexMesh : MonoBehaviour {
	private FunctionsRepository repository = new FunctionsRepository();
	private FunctionsRepository.Shape shape;
	private float scale;
	private int resolution;

	private Vector3[] vertices;
	private Mesh mesh;

	public VertexMesh(FunctionsRepository.Shape shape, float scale, int resolution, MeshFilter meshFilter) {
		this.shape = shape;
		this.scale = scale;
		this.resolution = resolution;
		meshFilter.mesh = this.mesh = new Mesh();
		repository.SetShape(shape);
		ReDoMesh();
	}

	public void UpdateMesh(FunctionsRepository.Shape shape, float scale, int resolution) {
		this.shape = shape;
		this.scale = scale;
		this.resolution = resolution;
		this.mesh = mesh = new Mesh();
		repository.SetShape(shape);
		ReDoMesh();
	}

	private void ReDoMesh() {
		mesh.name = "Procedural Grid";

		Vector2 uRange = repository.GetURange(), vRange = repository.GetVRange();
		float uSpan = uRange.y - uRange.x, vSpan = vRange.y - vRange.x;
		float uRes = uSpan / (this.resolution - 1), vRes = vSpan / (this.resolution - 1);

		vertices = new Vector3[resolution * resolution];
		Vector2[] uv = new Vector2[vertices.Length];

		float u, v;
		for (int y = 0, a = 0; y < this.resolution; y++) {
			v = vRange.x + y * vRes;
			for (int x = 0; x < this.resolution; x++, a++) {
				u = uRange.x + x * uRes;
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
