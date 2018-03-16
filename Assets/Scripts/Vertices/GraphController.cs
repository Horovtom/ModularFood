using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GraphController : MonoBehaviour {
	private const int MIN_RESOLUTION = 10;
	private const int MAX_RESOLUTION = 500;

	private FunctionsRepository.Shape _shape;
	public FunctionsRepository.Shape shape;
	 
	private VertexMesh mesh;

	private float _scale;
	public float scale = 0.01f;

	[Range(MIN_RESOLUTION, MAX_RESOLUTION)]
	public int resolution;
	private int _resolution;

	// Use this for initialization
	void Start() {
		shape = FunctionsRepository.Shape.Custom;
		scale = 0.1f;
		resolution = 100;
		mesh = new VertexMesh(shape, scale, resolution, GetComponent<MeshFilter>());
	}
	
	// Update is called once per frame
	void Update() {
		bool changed = false;
		if (_scale != scale) {
			changed = true;
			_scale = scale;
		}

		if (shape != _shape) {
			Debug.Log("Shape changed to: " + shape);
			_shape = shape;
			changed = true;
		}
		if (_resolution != resolution) {
			if (resolution < MIN_RESOLUTION || resolution > MAX_RESOLUTION) {
				Debug.Log("Trying to set Resolution to wrong value! Resetting to minimum");
				resolution = MIN_RESOLUTION;
			}
			Debug.Log("Resolution changed to: " + resolution);
			_resolution = resolution;
			changed = true;
		}
		if (changed && mesh != null) {
			mesh.UpdateMesh(shape, scale, resolution);
		}

	}

	private void OnDrawGizmos() {
		if (mesh == null)
			return;
		mesh.DrawGizmos();
	}
}
