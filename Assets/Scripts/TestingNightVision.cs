using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;


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
	public GameObject sonarLight;
	public EnemyDamage enemyDamage;
	public bool isFlashLight = true;
	public bool isSonar;
	//public DepthOfField DoF;
	//public bool pulse;
	//public bool ready;
	public GameObject[] environ;
	public Shader echo;
	public Renderer[] rend;
	public Material EchoMaterial = null;
	public Material Default = null;
	public GameObject[] lights;
	

	void Start()
	{
		NightVisionLight = GameObject.FindGameObjectWithTag ("NightVisionLight");
		enemyDamage = GameObject.FindGameObjectWithTag ("Enemy").GetComponent <EnemyDamage> ();
		blueLight = GameObject.FindGameObjectWithTag ("BlueLight");
		top = GameObject.FindGameObjectWithTag ("Flare1");
		secondTop = GameObject.FindGameObjectWithTag ("Flare2");
		middle = GameObject.FindGameObjectWithTag ("Flare3");
		secondLowest = GameObject.FindGameObjectWithTag ("Flare4");
		lowest = GameObject.FindGameObjectWithTag ("Flare5");
		hospitalGirl = GameObject.FindGameObjectWithTag ("Enemy");
		NightVisionLight.SetActive(false);
		blueLight.GetComponent<Light>().intensity = 2;
		//DoF = Camera.main.GetComponent<DepthOfField>();
		sonarLight = GameObject.Find ("SonarLight");
		sonarLight.SetActive (false);
		environ = GameObject.FindGameObjectsWithTag ("Environment");
		rend = new Renderer[environ.Length];
		lights = GameObject.FindGameObjectsWithTag ("Light");
	}
	
	void Update()
	{
		if (Input.GetKeyDown("2"))
		{
			isNightVision = true;
			isFlashLight = false;
			isSonar = false;
		}
		else if (Input.GetKeyDown("1"))
		{
			isNightVision = false;
			isFlashLight = true;
			isSonar = false;
		}
		if(Input.GetKeyDown("3"))
		{
			isNightVision = false;
			isFlashLight = false;
			isSonar = true;
			//DoF.enabled = true;
			for(int i = 0 ; i < environ.Length ; i++)
			{
				rend[i] = environ[i].GetComponent<Renderer>();
				rend[i].material.shader = echo;
				rend[i].material = EchoMaterial;
			}
			for(int l = 0; l < lights.Length; l++)
			{
				lights[l].SetActive(false);
			}
			//rend.material.shader = echo;
		}
		/*if(DoF.isActiveAndEnabled)
		{
			if(Input.GetKeyDown ("y"))
			{
				pulse = true;
				ready = false;
			}
		}
		if(pulse)
		{
			DoF.focalLength += 20*Time.deltaTime;
			if(DoF.focalLength > 40)
			{
				DoF.focalLength = 0;
				pulse = false;
				ready = true;
			}
		}*/
		if(isFlashLight)
		{
			NightVisionLight.SetActive(false);
			blueLight.GetComponent<Light>().intensity = 2;
		}
		if(isFlashLight == false)
		{
			top.SetActive(false);
			secondTop.SetActive(false);
			middle.SetActive(false);
			secondLowest.SetActive(false);
			lowest.SetActive(false);
			blueLight.GetComponent<Light>().intensity = 0;
		}
		if(isSonar)
		{
			if(Input.GetKeyDown("2") || Input.GetKeyDown("1"))
			{
				isSonar = false;
			}
		}
		if(isSonar == false)
		{
			for(int t = 0 ; t < environ.Length ; t++)
			{
				///Need floor to be different tag.
				rend[t] = environ[t].GetComponent<Renderer>();
				//rend[i].material.shader = echo;
				rend[t].material = Default;
				print (environ.Length);
			}
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
			NightVisionLight.SetActive(true);

			var colPreviousGUIColor = GUI.color;
			GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);
			GUI.DrawTexture(new Rect(0.0F, 0.0F, Screen.width, Screen.height), greenStatic); 
		}
		if (isNightVision == false)
		{
			NightVisionLight.SetActive(false);
			hospitalGirl.GetComponent<Renderer>().enabled = true;
			top.SetActive(true);
			secondTop.SetActive(true);
			middle.SetActive(true);
			secondLowest.SetActive(true);
			lowest.SetActive(true);
		}
		/*if(DoF.isActiveAndEnabled && pulse == false)
		{
			blackAlpha = 100f;
			var colPreviousGUIColor = GUI.color;
			GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, blackAlpha);
			GUI.DrawTexture(new Rect(0.0F, 0.0F, Screen.width, Screen.height), Black); 
		}
		if(pulse)
		{
			blackAlpha = 0;
			var colPreviousGUIColor = GUI.color;
			GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, blackAlpha);
			GUI.DrawTexture(new Rect(0.0F, 0.0F, Screen.width, Screen.height), Black); 
		}
		if(ready)
		{
			blackAlpha = 100f;
			var colPreviousGUIColor = GUI.color;
			GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, blackAlpha);
			GUI.DrawTexture(new Rect(0.0F, 0.0F, Screen.width, Screen.height), Black); 
		}*/
	}
}
