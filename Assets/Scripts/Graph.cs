using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
	private const int MIN_RESOLUTION = 10;
	private const int MAX_RESOLUTION = 10000;
	private FunctionsRepository repository = new FunctionsRepository();

	private FunctionsRepository.Shape _shape;
	public FunctionsRepository.Shape shape;

	public GameObject pointPrefab;

	private int _resolution;
	public int resolution;

	private bool changed = true;

    List<GameObject> points = new List<GameObject>();

	void Awake() {
		Transform point;
		for (int i = 0; i < 10; i++) {
            //point = instantiate(pointprefab);
            //point.setparent(this.transform);
            //point.localposition = vector3.right * (i / 5f - 1f);
            //point.localScale = Vector3.one / 5f;
        }
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
        //TODO: UPDATE
	}

	private void CreateCubes() {
		

	}
}
