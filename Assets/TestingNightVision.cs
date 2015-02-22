using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]

public class TestingNightVision : MonoBehaviour
{
	public Texture2D texture;
	public bool isNightVision = false;
	public float fAlpha = 0.35F;
	public GameObject NightVisionLight;
	public GameObject blueLight;
	public GameObject top;
	public GameObject secondTop;
	public GameObject middle;
	public GameObject secondLowest;
	public GameObject lowest;
	public bool isFlashLight;

	void Start()
	{
		NightVisionLight.SetActive(false);
		GetComponent <ToggleFlashlight> ();
		isFlashLight = true;
	}
	
	void Update()
	{
		if (Input.GetKeyDown("g") && (isNightVision == false))
		{
			blueLight.SetActive(false);
			isNightVision = true;
			isFlashLight = false;
			NightVisionLight.SetActive(true);
			//blueLight.SetActive(false);
		}
		else if (Input.GetKeyDown("g") && (isNightVision == true))
		{
			blueLight.SetActive(true);
			isNightVision = false;
			isFlashLight = true;
			top.SetActive(true);
			secondTop.SetActive(true);
			middle.SetActive(true);
			secondLowest.SetActive(true);
			lowest.SetActive(true);
			NightVisionLight.SetActive(false);
			//blueLight.SetActive(true);
		}
	}
	
	void OnGUI()
	{
		if (isNightVision == true)
		{
			blueLight.SetActive(false);
			top.SetActive(false);
			secondTop.SetActive(false);
			middle.SetActive(false);
			secondLowest.SetActive(false);
			lowest.SetActive(false);

			var colPreviousGUIColor = GUI.color;
			GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);
			GUI.DrawTexture(new Rect(0.0F, 0.0F, Screen.width, Screen.height), texture);
		}
	}
}
