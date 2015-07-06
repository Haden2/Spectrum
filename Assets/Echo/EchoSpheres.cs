using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EchoSpheres : MonoBehaviour 
{
	public List<EchoSphere2> Spheres = new List<EchoSphere2>();
	public EchoSphere2.ShaderPackingMode CurrentPackingMode = EchoSphere2.ShaderPackingMode.Texture;
	public Texture2D EchoTexture;
	public Material EchoMaterial = null;
	
	public int SphereCount = 1;
	public int CurrentSphere = 0;
	
	// Echo sphere Properties
	public float SphereMaxRadius = 10.0f;		//Final size of the echo sphere.
	public EchoSphere2 es;
	public float FadeDelay = 0.0f;			//Time to delay before triggering fade.
	public float FadeRate = 1.0f;			//Speed of the fade away
	public float echoSpeed = 1.0f;			//Speed of the sphere growth.
	//public EchoSphere2 echoSphere;

	void Start () {		
		CreateEchoTexture();
		InitializeSpheres();
		//echoSphere = GameObject.FindGameObjectWithTag ("Player").GetComponent<EchoSphere2> ();
	}
	
	void InitializeSpheres(){
		for(int i = 0; i < SphereCount; i++)
		{
			{
				EchoMaterial = EchoMaterial;
				EchoTexture = EchoTexture;
				echoSpeed = echoSpeed;
				SphereMaxRadius = SphereMaxRadius;
				FadeDelay = FadeDelay;
				FadeRate = FadeRate;
				SphereCount = i;
				CurrentPackingMode = CurrentPackingMode;
			};
			Spheres.Add(es);
			/*EchoSphere2 es = new EchoSphere2
			{
				EchoMaterial = EchoMaterial,
				EchoTexture = EchoTexture,
				echoSpeed = echoSpeed,
				SphereMaxRadius = SphereMaxRadius,
				FadeDelay = FadeDelay,
				FadeRate = FadeRate,
				SphereIndex = i,
				CurrentPackingMode = CurrentPackingMode
			};
			Spheres.Add(es);*/
		}
	}
	/// <summary>
	/// Create an echo texture used to hold multiple echo sources and fades.
	/// </summary>
	void CreateEchoTexture(){
		EchoTexture = new Texture2D(128,128,TextureFormat.RGBA32,false);
		EchoTexture.filterMode = FilterMode.Point;
		EchoTexture.Apply();
		
		EchoMaterial.SetTexture("_EchoTex",EchoTexture);
	}
	// Update is called once per frame
	void Update () {
		if(EchoMaterial == null)return;	
		foreach (EchoSphere2 es in Spheres)
		{
			es.Update();
		}
		UpdateRayCast();
	}
	
	// Called to manually place echo pulse
	void UpdateRayCast() {
		if (Input.GetButtonDown("Fire1")){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit, 10000)) {
				Debug.Log("Triggering pulse["+CurrentSphere.ToString()+"]");
				Spheres[CurrentSphere].TriggerPulse();
				Spheres[CurrentSphere].Position = hit.point;
				print ("Script2");
				CurrentSphere += 1;
				if(CurrentSphere >= Spheres.Count)CurrentSphere = 0;
			}
		}
	}
}

