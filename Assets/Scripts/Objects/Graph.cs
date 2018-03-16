using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
	private const int MIN_RESOLUTION = 10;
	private const int MAX_RESOLUTION = 500;
	private FunctionsRepository repository = new FunctionsRepository();

	private FunctionsRepository.Shape _shape = FunctionsRepository.Shape.Oval;
	public FunctionsRepository.Shape shape;

	private float _scale = 0f;
	public float scale = 0.01f;

	public GameObject pointPrefab;

	private int _resolution;
	[Range(MIN_RESOLUTION, MAX_RESOLUTION)]
	public int resolution;

	private bool changed = true;

	List<GameObject> points = new List<GameObject>();

	void Awake() {
	}

	// Use this for initialization
	void Start() {
		SimplePool.Preload(pointPrefab, 100);
		shape = FunctionsRepository.Shape.Cube;
		resolution = MIN_RESOLUTION;
		CreateCubes();
	}
	
	// Update is called once per frame
	void Update() {
		CreateCubes();
		if (_scale != scale) {
			changed = true;
			_scale = scale;
		}

		if (shape != _shape) {
			Debug.Log("Shape changed to: " + shape);
			_shape = shape;
			changed = true;
			repository.SetShape(shape);
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
		if (changed) {
			changed = false;
			UpdateParticles();
		}
	}

	private void UpdateParticles() {
		Vector2 uRange = repository.GetURange(), vRange = repository.GetVRange();
		float uSpan = uRange.y - uRange.x, vSpan = vRange.y - vRange.x;
		float uRes = uSpan / this.resolution, vRes = vSpan / this.resolution;
		//float scale = Mathf.Max(uRes, vRes);
		Debug.Log("Scale " + scale);
		float u, v;
		for (int i = 0; i < this.resolution; i++) {
			u = uRange.x + i * uRes;
			for (int j = 0; j < this.resolution; j++) {
				v = vRange.x + j * vRes;
				GameObject part = points[GetIndex(i, j)];

				Vector3 p = repository.GetVect(u, v);
				part.transform.SetPositionAndRotation(p, Quaternion.identity);
				part.transform.localScale = Vector3.one * scale;
			}
		}
	}

	/// <summary>
	/// Returns 1d index from 2d indices
	/// </summary>
	private int GetIndex(int x, int y) {
		return y * resolution + x;
	}

	/// <summary>
	/// Despawns all unused points or spawns those that are needed
	/// </summary>
	private void CreateCubes() {
		int size = this.resolution * this.resolution;
		if (size < points.Count) {
			for (int i = points.Count - 1; i >= size; i--) {
				SimplePool.Despawn(points[i]);
				points.RemoveAt(i);
			}
		} else if (size > points.Count) {
			for (int i = points.Count; i < size; i++) {
				GameObject go = SimplePool.Spawn(pointPrefab, Vector3.zero, Quaternion.identity);
				go.transform.SetParent(this.transform, false); // Might be true
				points.Add(go);
			}
		}
	}
}
