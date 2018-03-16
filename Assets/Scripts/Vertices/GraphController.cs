using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GraphController : MonoBehaviour {
	private FunctionsRepository.Shape _shape;
	public FunctionsRepository.Shape shape;

	private float _sweet, _sour, _umami, _salty, _bitter;
	[Range(-30, 30)]
	public float sweet, sour, umami, salty, bitter;
	 
	private VertexMesh vertexMesh;

	private float _scale;
	public float scale = 0.01f;

	private int resolution = 100;


	// Use this for initialization
	void Start() {
		shape = FunctionsRepository.Shape.Custom;
		scale = 0.1f;
		resolution = 100;
		this.vertexMesh = new VertexMesh(this, shape, scale, resolution);
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

		changed = changed || TastesChanged();

		if (changed && vertexMesh != null) {
			vertexMesh.UpdateMesh(shape, scale, sweet, sour, umami, bitter, salty);
		}

	}

	//FIXME: Implement boundaries for tastes!
	private bool TastesChanged() {
		bool changed = false;
		if (_sweet != sweet) {
			Debug.Log("Changed sweet to: " + sweet);
			_sweet = sweet;
			changed = true;
		}
		if (_bitter != bitter) {
			Debug.Log("Changed bitter to: " + bitter);

			_bitter = bitter;
			changed = true;
		}
		if (_sour != sour) {
			Debug.Log("Changed sour to: " + sour);

			_sour = sour;
			changed = true;
		}
		if (_salty != salty) {
			Debug.Log("Changed salty to: " + salty);

			_salty = salty;
			changed = true;
		}
		if (_umami != umami) {
			Debug.Log("Changed umami to: " + umami);

			_umami = umami;
			changed = true;
		}
		return changed;
	}

	private void OnDrawGizmos() {
		if (vertexMesh == null)
			return;
		//vertexMesh.DrawGizmos();
	}

	public void SetSweet(float value) {
		this.sweet = value;
	}

	public void SetSour(float value) {
		this.sour = value;
	}

	public void SetUmami(float value) {
		this.umami = value;
	}

	public void SetSalty(float value) {
		this.salty = value;
	}

	public void SetBitter(float value) {
		this.bitter = value;
	}
}
