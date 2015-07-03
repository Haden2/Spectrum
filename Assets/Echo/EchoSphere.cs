using UnityEngine;  
using System;  
using System.Collections;

public class EchoSphere : MonoBehaviour {  
	// Echo sphere Properties
	public Material EchoMaterial = null;
	public Texture EchoTexture = null;
	//public Shader EchoShader = null;
	
	public float MaxRadius = 10.0f;     //Final size of the echo sphere.
	public float CurrentRadius = 0.0f;  //Current size of the echo sphere
	
	public float FadeDelay = 0.0f;          //Time to delay before triggering fade.
	public float FadeRate = 1.0f;           //Speed of the fade away
	public float EchoSpeed = 1.0f;          //Speed of the sphere growth.
	
	private bool isAnimated = false;    
	private float deltaTime = 0.0f;
	
	public float fade = 0.0f;
	public bool isTexturedScene = true;
	public Vector3 pingLocation;
	public TestingNightVision appControl;
	
	// Use this for initialization
	void Start () {
		SetupSimpleScene1();
		appControl = GetComponent<TestingNightVision> ();
	}
	
	/// 
	/// Scenario1: Monocolor echo. 
	/// 
	void SetupSimpleScene1(){
		MaxRadius = 40.0f;
		CurrentRadius = 0.0f;
		FadeDelay = 0.0f;
		FadeRate = 1f;
		EchoSpeed = 9.0f;
		EchoMaterial.mainTexture = null;
		//EchoShader = Shader.Find ("GlowOutline");
		//rend = GetComponent<Renderer> ();

	//	EchoMaterial.SetColor ("_MainColor", Color.red);
		EchoMaterial.SetFloat("_DistanceFade",1.0f);
		isTexturedScene = false;
	}
	
	/// 
	/// Scenario2: Diffuse texture echo
	/// 
	void SetupSimpleScene2(){
		MaxRadius = 20.0f;
		CurrentRadius = 0.0f;
		FadeDelay = 0.0f;
		FadeRate = 1.0f;
		EchoSpeed = 9.0f;
		EchoMaterial.mainTexture = EchoTexture;
		
		EchoMaterial.SetFloat("_DistanceFade",0.0f);
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
	void Update () {
		deltaTime += Time.deltaTime;
		
		UpdateRayCast();
		UpdateEcho();
		UpdateShader();
	}
	
	// Called to trigger an echo pulse
	void TriggerPulse(){
		deltaTime = 0.0f;
		CurrentRadius = 0.0f;
		fade = 0.0f;
		isAnimated = true;
	}
	
	// Called to halt an echo pulse.
	void HaltPulse(){
		isAnimated = false; 
	}
	
	void ClearPulse(){
		fade = 0.0f;
		CurrentRadius = 0.0f;
		isAnimated = false;
	}
	
	// Called to manually place echo pulse
	void UpdateRayCast() 
	{

		if (Input.GetButtonDown("Fire1") && appControl.isSonar){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray,out hit, Mathf.Infinity)) 
			{
				pingLocation = gameObject.transform.position;
				//transform.position = hit.point;
				//rend.material.shader = EchoShader;
				TriggerPulse();
			}
		}  
	}
	
	// Called to update the echo front edge
	void UpdateEcho(){
		if(!isAnimated)return;
		
		if(CurrentRadius >= MaxRadius){
			HaltPulse();
		} else {
			CurrentRadius += Time.deltaTime * EchoSpeed;  
		}
	}
	
	// Called to update the actual shader values (some of which only change once but are included here
	// for illustrative purposes)
	void UpdateShader(){
		float radius = CurrentRadius;
		float maxRadius = MaxRadius;
		float maxFade = MaxRadius / EchoSpeed;
		
		if(deltaTime > FadeDelay)
			fade += Time.deltaTime * FadeRate;
		
		// Update our shader properties (requires Echo.shader)
		EchoMaterial.SetVector("_Position",pingLocation);
		EchoMaterial.SetFloat("_Radius",radius);
		EchoMaterial.SetFloat("_MaxRadius",maxRadius);
		EchoMaterial.SetFloat("_Fade",fade);
		EchoMaterial.SetFloat("_MaxFade",maxFade);
	}
}