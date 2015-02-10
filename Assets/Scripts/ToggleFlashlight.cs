using UnityEngine;
using System.Collections;

public class ToggleFlashlight : MonoBehaviour
{
  public float lightIntensity;

  // Update is called once per frame
  void Update ()
  {
    if (Input.GetKeyDown ("q")) 
	{
      if (light.intensity == 0) //If the light was off, turn it on.
        light.intensity = lightIntensity;
      else
        light.intensity = 0; //If the light was on, turn it off.
    }
  }

}
