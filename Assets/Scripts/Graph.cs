using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    public Transform pointPrefab;

    void Awake() {
        Transform point;
        for (int i = 0; i < 10; i++ ) {
            point = Instantiate(pointPrefab);
            point.SetParent(this.transform);
            point.localPosition = Vector3.right * (i / 5f - 1f);
            point.localScale = Vector3.one / 5f;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
