using UnityEngine;
using System.Collections;

public class ToggleFlashlight : MonoBehaviour
{
 	public float lightIntensityOne;
	public Light lightIntensity1;

	public float lightIntensityTwo;
	public Light lightIntensity2;

	public float lightIntensityThree;
	public Light lightIntensity3;

	public float lightIntensityFour;
	public Light lightIntensity4;

	public float lightIntensityFive;
	public Light lightIntensity5;

	public float lightIntensitySix;
	public Light lightIntensity6;

	public  LensFlare lf;
	public float lensBright;
	public LensFlare lf1;
	public float lensBright1;
	public LensFlare lf2;
	public float lensBright2;
	public LensFlare lf3;
	public float lensBright3;
	public LensFlare lf4;
	public float lensBright4;
	public Texture2D texture;
	public float fAlpha = 0.35F;

  // Update is called once per frame
  void Update ()
  {
	if (Input.GetKeyDown ("q")) 
	{
      if (lightIntensity1.intensity == 0 && lf.brightness == 0) //If the light was off, turn it on.
       { 
			lightIntensity1.light.intensity = lightIntensityOne; //.75
			lightIntensity2.light.intensity = lightIntensityTwo;
			lightIntensity3.light.intensity = lightIntensityThree;
			lightIntensity4.light.intensity = lightIntensityFour;
			lightIntensity5.light.intensity = lightIntensityFive;
			lightIntensity6.light.intensity = lightIntensitySix;
			lf.brightness = lensBright; //2
			lf1.brightness = lensBright1;
			lf2.brightness = lensBright2;
			lf3.brightness = lensBright3;
			lf4.brightness = lensBright4;

			}

      else
      	{	
			lightIntensity1.light.intensity = 0; //If the light was on, turn it off.
			lightIntensity2.light.intensity = 0;
			lightIntensity3.light.intensity = 0;
			lightIntensity4.light.intensity = 0;
			lightIntensity5.light.intensity = 0;
			lightIntensity6.light.intensity = 0;
			lf.brightness = 0;
			lf1.brightness = 0;
			lf2.brightness = 0;
			lf3.brightness = 0;
			lf4.brightness = 0;
			}
		}
    
	}

	void OnGUI()
	{
		var colPreviousGUIColor = GUI.color;
		GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);
		GUI.DrawTexture(new Rect(0.0F, 0.0F, Screen.width, Screen.height), texture);
	}
}