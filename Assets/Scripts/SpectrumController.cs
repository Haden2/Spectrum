using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;


[RequireComponent(typeof(Light))]

public class SpectrumController : MonoBehaviour
{
	public Shader echo;
	public Shader multiEcho;
	public Renderer[] rend;
	public Renderer Floor;
	public Material EchoMaterial = null;
	public Material multiMaterial;
	public Material Default;
	public Material FloorMat;
	//public Texture2D greenStatic;
	public GameObject blueLight;
	public GameObject top;
	public GameObject secondTop;
	public GameObject middle;
	public GameObject secondLowest;
	public GameObject lowest;
	public GameObject NightVisionLight;
	public GameObject sonarLight;
	public GameObject hospitalGirl;
	public GameObject[] lights;
	public GameObject[] environ;
	public GameObject[] enemies;
	public GameObject[] misc;
	public GameObject[] items;

	public bool isFlashLight = true;
	public bool isNightVision = false;
	public bool isSonar;
	//public bool pulse;
	//public bool ready;
	//public float fAlpha = 0.35F;
	public Flashlight flashLight;
	public NoiseAndGrain grain;
	public EchoSpherez echoSpherez;
	public HospitalGirl enemyDamage;
	//public DepthOfField DoF;

	void Start()
	{
		Floor = GameObject.Find ("Floor").GetComponent<Renderer>();

		blueLight = GameObject.Find ("BlueFlashlight");
		blueLight.GetComponent<Light>().intensity = 2;
		top = GameObject.Find("Flare1");
		secondTop = GameObject.Find ("Flare2");
		middle = GameObject.Find ("Flare3");
		secondLowest = GameObject.Find ("Flare4");
		lowest = GameObject.Find ("Flare5");
		NightVisionLight = GameObject.Find ("NightVision");
		NightVisionLight.SetActive(false);
		sonarLight = GameObject.Find ("SonarLight");
		sonarLight.SetActive (false);
		hospitalGirl = GameObject.Find ("HospitalGirl");
		lights = GameObject.FindGameObjectsWithTag ("Light");
		environ = GameObject.FindGameObjectsWithTag ("Environment");
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		misc = GameObject.FindGameObjectsWithTag ("Other");
		items = GameObject.FindGameObjectsWithTag ("Item");

		flashLight = GetComponent<Flashlight> ();
		grain = GameObject.Find ("Main Camera").GetComponent<NoiseAndGrain> ();
		grain.enabled = false;
		echoSpherez = GetComponent<EchoSpherez> ();
		enemyDamage = GameObject.Find ("HospitalGirl").GetComponent <HospitalGirl> ();
		//DoF = Camera.main.GetComponent<DepthOfField>();
		rend = new Renderer[environ.Length];
	}
	
	void Update()
	{
		//print (Floor.material);
		if (Input.GetKeyDown("1"))
		{
			isNightVision = false;
			isFlashLight = true;
			isSonar = false;
			grain.enabled = false;
		}
		if (Input.GetKeyDown("2"))
		{
			isNightVision = true;
			isFlashLight = false;
			isSonar = false;
			grain.enabled = true;
		}
		if(Input.GetKeyDown("3") && enemyDamage.flash)
		{
			isNightVision = false;
			isFlashLight = false;
			isSonar = true;
			grain.enabled = false;
			//DoF.enabled = true;
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
			flashLight.enabled = true;
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
			for(int i = 0 ; i < environ.Length ; i++)
			{
				rend[i] = environ[i].GetComponent<Renderer>();
				rend[i].material.shader = multiEcho;
				rend[i].material = multiMaterial;
			}
			for(int l = 0; l < lights.Length; l++)
			{
				lights[l].SetActive(false);
			}
			for(int i = 0 ; i < enemies.Length ; i++)
			{
				rend[i] = enemies[i].GetComponent<Renderer>();
				rend[i].material.shader = multiEcho;
				rend[i].material = multiMaterial;
			}
			for(int i = 0 ; i < misc.Length ; i++)
			{
				rend[i] = misc[i].GetComponent<Renderer>();
				rend[i].material.shader = multiEcho;
				rend[i].material = multiMaterial;
			}
			for(int i = 0 ; i < items.Length ; i++)
			{
				rend[i] = items[i].GetComponent<Renderer>();
				rend[i].material.shader = multiEcho;
				rend[i].material = multiMaterial;
			}

			sonarLight.SetActive(true);
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
				rend[t].material = Default;
			}
			Floor.material = FloorMat;
			for(int l = 0; l < lights.Length; l++)
			{
				lights[l].SetActive(true);
			}
			sonarLight.SetActive(false);
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
			flashLight.enabled = false;

			//var colPreviousGUIColor = GUI.color;
			//GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);
			//GUI.DrawTexture(new Rect(0.0F, 0.0F, Screen.width, Screen.height), greenStatic); 
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
