using UnityEngine;  
using System;  
using System.Collections;
using System.Collections.Generic;


public class EchoSpheres : MonoBehaviour {  
	// Echo sphere Properties
	public List<int> Spheres = new List<int>();
	
	public Material EchoMaterial = null;
	public Texture EchoTexture = null;
	
	public float SphereMaxRadius = 40.0f;     //Final size of the echo sphere.
	public float SphereCurrentRadius = 0.0f;  //Current size of the echo sphere
	
	public float FadeDelay = 0.0f;          //Time to delay before triggering fade.
	public float FadeRate = 1.0f;           //Speed of the fade away
	public float EchoSpeed = 9.0f;          //Speed of the sphere growth.
	
	public int SphereCount = 1;
	public int CurrentSphere = 0;
	public int SphereIndex = 0;
	
	private bool isAnimated = false;    
	private float deltaTime = 0.0f;
	
	public float fade = 0.0f;
	public bool isTexturedScene = true;
	
	public Vector3 Position;
	
	public Vector3 pingLocation;
	public Vector3 rockLocation;
	public TestingNightVision appControl;
	public bool isGrounded;
	public RockNoise rockNoise;
	public GameObject rock;
	//public EchoSphere2 es;
	
	// Use this for initialization
	void Start () 
	{	
		//SetupSimpleScene1();
		InitializeSpheres();
		//es = GameObject.FindGameObjectWithTag ("Player").GetComponent<EchoSphere2> ();
		appControl = GetComponent<TestingNightVision> ();
	}
	
	public EchoSpheres(){}
	/// 
	/// Scenario1: Monocolor echo. 
	///
	void InitializeSpheres()
	{
		for (int i = 0; i < SphereCount; i++) 
		{
			//EchoSphere es = new  EchoSphere()
			{
				EchoMaterial = EchoMaterial;
				EchoTexture = EchoTexture;
				EchoSpeed = EchoSpeed;
				SphereMaxRadius = SphereMaxRadius;
				FadeDelay = FadeDelay;
				FadeRate = FadeRate;
				SphereIndex = i;
				isTexturedScene = false;
			};
			//Spheres.Add(es);
		}
	}
	
	void SetupSimpleScene1()
	{
		SphereMaxRadius = 40.0f;
		SphereCurrentRadius = 0.0f;
		FadeDelay = 0.0f;
		FadeRate = 1f;
		EchoSpeed = 9.0f;
		EchoMaterial.mainTexture = null;
		//EchoShader = Shader.Find ("GlowOutline");
		//rend = GetComponent<Renderer> ();
		
		//EchoMaterial.SetFloat("_DistanceFade",1.0f);
		isTexturedScene = false;
	}
	
	/// 
	/// Scenario2: Diffuse texture echo
	/// 
	void SetupSimpleScene2()
	{
		SphereMaxRadius = 40.0f;
		SphereCurrentRadius = 0.0f;
		FadeDelay = 0.0f;
		FadeRate = 1.0f;
		EchoSpeed = 9.0f;
		EchoMaterial.mainTexture = EchoTexture;
		
		//EchoMaterial.SetFloat("_DistanceFade",0.0f);
		isTexturedScene = true;
	}
	
	/*	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Scenarios");
		
		GUI.enabled = isTexturedScene;
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "No Texture")) {
			SetupSimpleScene1();
		}
		GUI.enabled = true;
		
		GUI.enabled = !isTexturedScene;
		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Textured")) {
			SetupSimpleScene2();
		}
		GUI.enabled = true;
	}*/
	// Update is called once per frame
	void Update () 
	{
		print (SphereCount);
		print (CurrentSphere);
		print (SphereIndex);
		if(isGrounded)
		{
			rockNoise = GameObject.Find("ThrownRock(Clone)").GetComponent<RockNoise>();
			rock = GameObject.Find("ThrownRock(Clone)");
		}
		if(isGrounded == false)
		{
			//rockNoise = null;
			//rock = null;
		}
		deltaTime += Time.deltaTime;
		
		if(EchoMaterial == null)return;	
		foreach (int es in Spheres)
		{
		//	es.Update();
		}
		UpdateRayCast();
		UpdateEcho();
		UpdateProperties ();
		//UpdateShader();
	}
	
	// Called to trigger an echo pulse
	void TriggerPulse()
	{
		deltaTime = 0.0f;
		SphereCurrentRadius = 0.0f;
		fade = 0.0f;
		isAnimated = true;
		if(rockNoise.isgrounded)
		{
			rockNoise.isgrounded = false;
		}
	}
	
	// Called to halt an echo pulse.
	void HaltPulse()
	{
		isAnimated = false; 
	}
	
	void ClearPulse()
	{
		fade = 0.0f;
		SphereCurrentRadius = 0.0f;
		isAnimated = false;
	}
	
	// Called to manually place echo pulse
	void UpdateRayCast() 
	{
		//rockNoise = GameObject.Find ("ThrownRock").GetComponent<RockNoise> ();
		if (Input.GetButtonDown("Fire1") && appControl.isSonar){
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit, Mathf.Infinity)) 
			{
				Debug.Log("Triggering pulse["+CurrentSphere.ToString()+"]");
				pingLocation = gameObject.transform.position;
				//Spheres[CurrentSphere] = hit.point;
				if(CurrentSphere == 0)
				{
					EchoMaterial.SetVector("_Position0", hit.point);
					TriggerPulse();
					CurrentSphere += 1;
				}
				if(CurrentSphere == 1 && Input.GetButtonDown("Fire2"))
				{
					EchoMaterial.SetVector("_Position1", hit.point);
					TriggerPulse();
					CurrentSphere += 1;
				}
				//Spheres[CurrentSphere].TriggerPulse();
				//if(CurrentSphere >= Spheres.Count)CurrentSphere = 0;
			}
		}  
		/*	if (Input.GetButtonDown("Fire2") && appControl.isSonar){
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit, Mathf.Infinity)) 
			{
				pingLocation = gameObject.transform.position;
				EchoMaterial.SetVector("_Position0", pingLocation);
			//	Spheres[CurrentSphere].Position = hit.point;
				TriggerPulse();
				//Spheres[CurrentSphere].TriggerPulse();
			}
		}  */
		if(rockNoise.isgrounded)
		{
			print ("Is Grounded");
			rockLocation = rock.transform.position;
			EchoMaterial.SetVector("_Position0", rockLocation);
			TriggerPulse();
		}
	}
	
	// Called to update the echo front edge
	void UpdateEcho()
	{
		if(!isAnimated)return;
		if(SphereCurrentRadius >= SphereMaxRadius)
		{
			HaltPulse();
		} else 
		{
			SphereCurrentRadius += Time.deltaTime * EchoSpeed;  
		}
		
		float radius = SphereCurrentRadius;
		float maxRadius = SphereMaxRadius;
		float maxFade = SphereMaxRadius / EchoSpeed;
		if(fade > maxFade)
		{
			return;
		}
		
		if(deltaTime > FadeDelay)
			fade += Time.deltaTime * FadeRate;
		/*
		if(CurrentRadius >= SphereMaxRadius){
			HaltPulse();
		} else {
			CurrentRadius += Time.deltaTime * EchoSpeed;  
		}*/
	}
	
	void UpdateProperties()
	{
		if(!isAnimated)return;
		float maxRadius = SphereMaxRadius;
		float maxFade = SphereMaxRadius / EchoSpeed;
		
		Debug.Log("Updating _Position"+SphereIndex.ToString());
		EchoMaterial.SetVector("_Position0"+SphereIndex.ToString(),Position);
		EchoMaterial.SetFloat("_Radius"+SphereIndex.ToString(),SphereCurrentRadius);
		EchoMaterial.SetFloat("_Fade"+SphereIndex.ToString(),fade);
		
		EchoMaterial.SetFloat("_MaxRadius",maxRadius);
		EchoMaterial.SetFloat("_MaxFade",maxFade);
	}
	
	// Called to update the actual shader values (some of which only change once but are included here
	// for illustrative purposes)
	/*void UpdateShader()
	{
		float radius = CurrentRadius;
		float maxRadius = SphereMaxRadius;
		float maxFade = SphereMaxRadius / EchoSpeed;
		
		if(deltaTime > FadeDelay)
			fade += Time.deltaTime * FadeRate;
		
		// Update our shader properties (requires Echo.shader)
		//EchoMaterial.SetVector("_Position",pingLocation);
		EchoMaterial.SetFloat("_Radius",radius);
		EchoMaterial.SetFloat("_MaxRadius",maxRadius);
		EchoMaterial.SetFloat("_Fade",fade);
		EchoMaterial.SetFloat("_MaxFade",maxFade);
	}*/
}