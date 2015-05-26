using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]

public class TestingNightVision : MonoBehaviour
{
	public Texture2D greenStatic;
	public bool isNightVision = false;
	public float fAlpha = 0.35F;
	public GameObject NightVisionLight;
	public GameObject blueLight;
	public GameObject top;
	public GameObject secondTop;
	public GameObject middle;
	public GameObject secondLowest;
	public GameObject lowest;
	public GameObject hospitalGirl;
	public bool isFlashLight = true;

	void Start()
	{
		NightVisionLight = GameObject.FindGameObjectWithTag ("NightVisionLight");
		blueLight = GameObject.FindGameObjectWithTag ("BlueLight");
		top = GameObject.FindGameObjectWithTag ("Flare1");
		secondTop = GameObject.FindGameObjectWithTag ("Flare2");
		middle = GameObject.FindGameObjectWithTag ("Flare3");
		secondLowest = GameObject.FindGameObjectWithTag ("Flare4");
		lowest = GameObject.FindGameObjectWithTag ("Flare5");
		hospitalGirl = GameObject.FindGameObjectWithTag ("Enemy");
		NightVisionLight.SetActive(false);
		blueLight.GetComponent<Light>().intensity = 2;
	}
	
	void Update()
	{
		if (Input.GetKeyDown("g") && (isNightVision == false))
		{
			isNightVision = true;
			isFlashLight = false;
			NightVisionLight.SetActive(true);
			blueLight.GetComponent<Light>().intensity = 0;
		}
		else if (Input.GetKeyDown("g") && (isNightVision == true))
		{
			isNightVision = false;
			isFlashLight = true;
			NightVisionLight.SetActive(false);
			blueLight.GetComponent<Light>().intensity = 2;
		}
	}
	
	void OnGUI()
	{
		if (isNightVision == true)
		{
			hospitalGirl.GetComponent<Renderer>().enabled = false;
			top.SetActive(false);
			secondTop.SetActive(false);
			middle.SetActive(false);
			secondLowest.SetActive(false);
			lowest.SetActive(false);

			var colPreviousGUIColor = GUI.color;
			GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);
			GUI.DrawTexture(new Rect(0.0F, 0.0F, Screen.width, Screen.height), greenStatic); 
		}
		if (isNightVision == false)
		{
			hospitalGirl.GetComponent<Renderer>().enabled = true;
			top.SetActive(true);
			secondTop.SetActive(true);
			middle.SetActive(true);
			secondLowest.SetActive(true);
			lowest.SetActive(true);
		}

	}
}
