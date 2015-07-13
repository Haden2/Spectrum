using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EchoSpherez : MonoBehaviour {
	public EchoSphere2.ShaderPackingMode CurrentPackingMode = EchoSphere2.ShaderPackingMode.Texture;
	public Texture2D EchoTexture;
	public Material EchoMaterial = null;

	public GameObject rock;

	public bool isGrounded;

	public int SphereCount = 5;
	public int CurrentSphere = 0;
	
	public float SphereMaxRadius = 40.0f;		//Final size of the echo sphere.
	public float FadeDelay = 0.0f;			//Time to delay before triggering fade.
	public float FadeRate = 1.0f;			//Speed of the fade away
	public float echoSpeed = 9.0f;			//Speed of the sphere growth.


	public Vector3 pingLocation;
	public Vector3 rockLocation;

	public Inventory inventory;
	public SpectrumController spectrum;
	public RockNoise rockNoise;

	
	public List<EchoSphere2> Spheres = new List<EchoSphere2>();
	
	// Use this for initialization
	void Start () 
	{
		//floor = GameObject.Find ("Floor").GetComponent<Renderer> ();
		inventory = GetComponent<Inventory> ();
		spectrum = GetComponent<SpectrumController> ();
		CreateEchoTexture();
		InitializeSpheres();
	}
	
	void InitializeSpheres(){
		for(int i = 0; i < SphereCount; i++){
			EchoSphere2 es = new  EchoSphere2{
				EchoMaterial = EchoMaterial,
				EchoTexture = EchoTexture,
				echoSpeed = echoSpeed,
				SphereMaxRadius = SphereMaxRadius,
				FadeDelay = FadeDelay,
				FadeRate = FadeRate,
				SphereIndex = i,
				CurrentPackingMode = CurrentPackingMode
			};
			Spheres.Add(es);
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
		if(isGrounded)
		{
			rockNoise = GameObject.Find("ThrownRock(Clone)").GetComponent<RockNoise>();
			rock = GameObject.Find("ThrownRock(Clone)");
		}
		if(EchoMaterial == null)return;	
		foreach (EchoSphere2 es in Spheres)
		{
			es.Update();
		}
		UpdateRayCast();
	}
	
	// Called to manually place echo pulse
	void UpdateRayCast() 
	{
		if (Input.GetButtonDown("Fire2") && spectrum.isSonar && inventory.showInventory == false)
		{
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit, Mathf.Infinity)) 
			{
				pingLocation = gameObject.transform.position;
				Spheres[CurrentSphere].TriggerPulse();
				Spheres[CurrentSphere].Position = pingLocation;
				CurrentSphere += 1;
				if(CurrentSphere >= Spheres.Count)CurrentSphere = 0;
			}
		}  
		if(rockNoise.isgrounded)
		{
			print ("Is Grounded");
			isGrounded = true;
			rockLocation = rock.transform.position;
			Spheres[CurrentSphere].TriggerPulse();
			Spheres[CurrentSphere].Position = rockLocation;
			CurrentSphere += 1;
			if(CurrentSphere >= Spheres.Count)CurrentSphere = 0;
			rockNoise.isgrounded = false;
		}
	}
}
[Serializable]
public class EchoSphere2 {
	public enum ShaderPackingMode { Texture, Property };
	public ShaderPackingMode CurrentPackingMode = ShaderPackingMode.Texture;
	
	public Texture2D EchoTexture;
	public Material EchoMaterial = null;
	public Vector3 Position;
	public int SphereIndex = 0;

	public float SphereMaxRadius = 10.0f;		//Final size of the echo sphere.
	public float sphereCurrentRadius = 0.0f;	//Current size of the echo sphere
	
	public float FadeDelay = 0.0f;			//Time to delay before triggering fade.
	public float FadeRate = 1.0f;			//Speed of the fade away
	public float echoSpeed = 1.0f;			//Speed of the sphere growth.
	//public bool is_manual = false;			//Is pulse manual.  if true, pulse triggered by left-mouse click
	
	public bool is_animated = false;		//If true, pulse is currently running.
	
	private float deltaTime = 0.0f;
	private float fade = 0.0f;


	public EchoSphere2(){}

	public void Start()
	{
		sphereCurrentRadius = 0;
		fade = 0;
		EchoMaterial.SetFloat("_Radius"+SphereIndex.ToString(),sphereCurrentRadius);

	}

	public void Update () 
	{
		EchoMaterial.SetFloat("_Radius"+SphereIndex.ToString(),sphereCurrentRadius);

		if(EchoMaterial == null)return;
		
		// If manual selection is disabled, automatically trigger a pulse at the given freq.
		deltaTime += Time.deltaTime;
		UpdateEcho();
		
	//	if(CurrentPackingMode == ShaderPackingMode.Texture)UpdateTexture();
		if(CurrentPackingMode == ShaderPackingMode.Property)UpdateProperties();
	}
	
	// Called to trigger an echo pulse
	public void TriggerPulse()
	{
		deltaTime = 0.0f;
		sphereCurrentRadius = 0.0f;
		fade = 0.0f;
		is_animated = true;
	}
	
	// Called to halt an echo pulse.
	void HaltPulse()
	{
		is_animated = false;	
	}
	
	void ClearPulse()
	{
		fade = 0.0f;
		sphereCurrentRadius = 0.0f;
		is_animated = false;
	}
	
	void UpdateProperties()
	{
		if(!is_animated)return;
		float maxRadius = SphereMaxRadius;
		float maxFade = SphereMaxRadius / echoSpeed;
		
		//Debug.Log("Updating _Position"+SphereIndex.ToString());
		EchoMaterial.SetVector("_Position"+SphereIndex.ToString(),Position);
		EchoMaterial.SetFloat("_Radius"+SphereIndex.ToString(),sphereCurrentRadius);
		EchoMaterial.SetFloat("_Fade"+SphereIndex.ToString(),fade);	
		EchoMaterial.SetFloat("_MaxRadius",maxRadius);
		EchoMaterial.SetFloat("_MaxFade",maxFade);
	}
	
/*	void UpdateTexture()
	{	
		if(!is_animated)return;
		float maxRadius = SphereMaxRadius;
		float maxFade = SphereMaxRadius / echoSpeed;
		
		EchoTexture.SetPixel(SphereIndex,0,FloatPacking.ToColor(Position.x));
		EchoTexture.SetPixel(SphereIndex,1,FloatPacking.ToColor(Position.y));
		EchoTexture.SetPixel(SphereIndex,2,FloatPacking.ToColor(Position.z));
		EchoTexture.SetPixel(SphereIndex,3,FloatPacking.ToColor(sphereCurrentRadius));
		EchoTexture.SetPixel(SphereIndex,4,FloatPacking.ToColor(fade));
		EchoTexture.Apply();	
		
		EchoMaterial.SetFloat("_MaxRadius",maxRadius);
		EchoMaterial.SetFloat("_MaxFade",maxFade);
	}*/
	// Called to update the echo front edge
	void UpdateEcho()
	{
		if(!is_animated)return;
		if(sphereCurrentRadius >= SphereMaxRadius)
		{
			HaltPulse();
		} else {
			sphereCurrentRadius += Time.deltaTime * echoSpeed;  
		}
		
		float radius = sphereCurrentRadius;
		float maxRadius = SphereMaxRadius;
		float maxFade = SphereMaxRadius / echoSpeed;
		if(fade > maxFade){
			return;
		}
		
		if(deltaTime > FadeDelay)
			fade += Time.deltaTime * FadeRate;
	}
}
