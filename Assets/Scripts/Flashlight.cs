using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
	public GameObject fLight;
	public GameObject lightIntensity1;
	public GameObject lightIntensity2;
	public GameObject lightIntensity3;
	public GameObject lightIntensity4;
	public GameObject lightIntensity5;

	public float FlashlightIntensity;
	public float lightIntensityOne;
	public float lightIntensityTwo;
	public float lightIntensityThree;
	public float lightIntensityFour;
	public float lightIntensityFive;

	public float lensBright1;
	public float lensBright2;
	public float lensBright3;
	public float lensBright4;
	public float lensBright5;

	//public Texture2D dust;
	//public float fAlpha = 0.35F;
	
	public SpectrumController spectrum;

  // Update is called once per frame
  	void Start()
	{
		fLight = GameObject.Find ("BlueFlashlight");
		lightIntensity1 = GameObject.Find ("Flare1");
		lightIntensity2 = GameObject.Find ("Flare2");
		lightIntensity3 = GameObject.Find ("Flare3");
		lightIntensity4 = GameObject.Find ("Flare4");
		lightIntensity5 = GameObject.Find ("Flare5");	

		FlashlightIntensity = 2f;
		lightIntensityOne = .5F;
		lightIntensityTwo = .25F;
		lightIntensityThree = 1F;
		lightIntensityFour = .35F;
		lightIntensityFive = .5F;

		lensBright1 = .5F;
		lensBright2 = .5F;
		lensBright3 = .25F;
		lensBright4 = .34F;
		lensBright5 = .5F;

		spectrum = GameObject.Find ("First Person Controller").GetComponent<SpectrumController> ();
	}

	void Update ()
  {
	if (Input.GetKeyDown ("q")) 
	{
			if (fLight.GetComponent<Light>().intensity == 0) //If the light was off, turn it on.
       { 
			fLight.GetComponent<Light>().intensity = FlashlightIntensity;
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
				spectrum.isFlashLight = true;
			}
      else
      	{	
			fLight.GetComponent<Light>().intensity = 0; //If the light was on, turn it off.
			lightIntensity1.GetComponent<Light>().intensity = 0;
			lightIntensity2.GetComponent<Light>().intensity = 0;
			lightIntensity3.GetComponent<Light>().intensity = 0;
			lightIntensity4.GetComponent<Light>().intensity = 0;
			lightIntensity5.GetComponent<Light>().intensity = 0;
			lightIntensity1.GetComponent<LensFlare>().brightness = 0;
			lightIntensity2.GetComponent<LensFlare>().brightness = 0;		
			lightIntensity3.GetComponent<LensFlare>().brightness = 0;
			lightIntensity4.GetComponent<LensFlare>().brightness = 0;
			lightIntensity5.GetComponent<LensFlare>().brightness = 0;
				spectrum.isFlashLight = false;
			}
		}
	}

	/*void OnGUI()
	{
		var colPreviousGUIColor = GUI.color;
		GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);
		GUI.DrawTexture(new Rect(0.0F, 0.0F, Screen.width, Screen.height), dust);
	}*/
}