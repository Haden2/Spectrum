﻿using UnityEngine;
using System.Collections;

public class ToggleFlashlight : MonoBehaviour
{

  // Use this for initialization
  void Start ()
  {
//    gameObject.SetActive (true);
  }
	
  // Update is called once per frame
  void Update ()
  {
    if (Input.GetKeyDown ("q"))
    if (light.intensity == 0)
      light.intensity = 2;
    else
      light.intensity = 0;
  }
}
