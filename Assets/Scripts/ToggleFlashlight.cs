using UnityEngine;
using System.Collections;

public class ToggleFlashlight : MonoBehaviour
{
	public float FlashlightIntensity;
	public GameObject Flashlight;

	public float lightIntensityOne;
	public GameObject lightIntensity1;

	public float lightIntensityTwo;
	public GameObject lightIntensity2;

	public float lightIntensityThree;
	public GameObject lightIntensity3;

	public float lightIntensityFour;
	public GameObject lightIntensity4;

	public float lightIntensityFive;
	public GameObject lightIntensity5;

	public float lensBright1;
	public float lensBright2;
	public float lensBright3;
	public float lensBright4;
	public float lensBright5;

	public Texture2D dust;
	public float fAlpha = 0.35F;

  // Update is called once per frame
  	void Start()
	{
		Flashlight = GameObject.FindGameObjectWithTag ("BlueLight");
		FlashlightIntensity = 2f;
		lightIntensity1 = GameObject.FindGameObjectWithTag ("Flare1");
		lightIntensityOne = .5F;
		lightIntensity2 = GameObject.FindGameObjectWithTag ("Flare2");
		lightIntensityTwo = .25F;
		lightIntensity3 = GameObject.FindGameObjectWithTag ("Flare3");
		lightIntensityThree = 1F;
		lightIntensity4 = GameObject.FindGameObjectWithTag ("Flare4");
		lightIntensityFour = .35F;
		lightIntensity5 = GameObject.FindGameObjectWithTag ("Flare5");
		lightIntensityFive = .5F;
		lensBright1 = .5F;
		lensBright2 = .5F;
		lensBright3 = .25F;
		lensBright4 = .34F;
		lensBright5 = .5F;
	}

	void Update ()
  {
	if (Input.GetKeyDown ("q")) 
	{
			if (Flashlight.GetComponent<Light>().intensity == 0) //If the light was off, turn it on.
       { 
			Flashlight.GetComponent<Light>().intensity = FlashlightIntensity;
			lightIntensity1.GetComponent<Light>().intensity = lightIntensityOne;
			lightIntensity2.GetComponent<Light>().intensity = lightIntensityTwo;
			lightIntensity3.GetComponent<Light>().intensity = lightIntensityThree;
			lightIntensity4.GetComponent<Light>().intensity = lightIntensityFour;
			lightIntensity5.GetComponent<Light>().intensity = lightIntensityFive;
			lightIntensity1.GetComponent<LensFlare>().brightness = lensBright1; 
			lightIntensity2.GetComponent<LensFlare>().brightness = lensBright2; 
			lightIntensity3.GetComponent<LensFlare>().brightness = lensBright3; 
			lightIntensity4.GetComponent<LensFlare>().brightness = lensBright4; 
			lightIntensity5.GetComponent<LensFlare>().brightness = lensBright5;
			}
      else
      	{	
			Flashlight.GetComponent<Light>().intensity = 0; //If the light was on, turn it off.
			lightIntensity1.GetComponent<Light>().intensity = 0;
			lightIntensity2.GetComponent<Light>().intensity = 0;
			lightIntensity3.GetComponent<Light>().intensity = 0;
			lightIntensity4.GetComponent<Light>().intensity = 0;
			lightIntensity5.GetComponent<Light>().intensity = 0;
			lightIntensity1.GetComponent<LensFlare>().brightness = 0;
			lightIntensity2.GetComponent<LensFlare>().brightness = 0;		
			lightIntensity3.GetComponent<LensFlare>().brightness = 0;
			lightIntensity4.GetComponent<LensFlare>().brightness = 0;
			lightIntensity5.GetComponent<LensFlare>().brightness = 0;			}
		}
	}

	void OnGUI()
	{
		var colPreviousGUIColor = GUI.color;
		GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);
		GUI.DrawTexture(new Rect(0.0F, 0.0F, Screen.width, Screen.height), dust);
	}
}