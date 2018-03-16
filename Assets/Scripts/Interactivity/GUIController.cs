using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {
	public UnityEngine.UI.Text sweet, sour, bitter, umami, salty;
	public UnityEngine.UI.Slider sweetSlider, sourSlider, bitterSlider, umamiSlider, saltySlider;
	// Use this for initialization
	void Start() {
		sweetSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 100, 20);
		sourSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 100, 20);
		bitterSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 100, 20);
		saltySlider.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 100, 20);
		umamiSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 100, 20);

		sweet.text = sweetSlider.value.ToString();
		sour.text = sourSlider.value.ToString();
		bitter.text = bitterSlider.value.ToString();
		salty.text = saltySlider.value.ToString();
		umami.text = umamiSlider.value.ToString();

	}
	
	// Update is called once per frame
	void Update() {
		//sweetSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 100, 20);

	}

	public void SetSweet(float value) {
		sweet.text = value.ToString();
	}

	public void SetSour(float value) {
		sour.text = value.ToString();
	}

	public void SetUmami(float value) {
		umami.text = value.ToString();
	}

	public void SetSalty(float value) {
		salty.text = value.ToString();
	}

	public void SetBitter(float value) {
		bitter.text = value.ToString();
	}
}
