using UnityEngine;
using System.Collections;

public class NiceBowling : MonoBehaviour {
	
	public bool timeForNice = false;
	public GameObject Bumper, Bumper2;
	public GameObject Cylinder, Sardine, Ramp;
	private float gravityX, gravityY, gravityZ;
	private GameObject light;
	public Material ball1, ball2, ball3, SkySpace, SkyUnity, SkyHorror; //Absolutly get gid of cosmetic changes that are not tied to gameplay changes and kill the horrer concept.
	public PhysicMaterial Bounce, none;
	public GameObject dumbPinSet;
	public Camera MainCamera;
	private float volume;
	

	// Use this for initialization
	void Start () {
		//This initializes the player pref incase its the first time playing and they didnt go to the options page
		if (!PlayerPrefs.HasKey ("nice_bowling"))
			PlayerPrefsManager.NiceBowlingSet(1);
		gravityX = 0;
		gravityY = -150;
		gravityZ = 0;
		light = GameObject.Find ("Directional Light");
		volume = PlayerPrefsManager.GetMasterVolume();
	}

	//use for initilization if level is loaded more then onece in a session
	void OnLevelWasLoaded(){
		gravityX = 0;
		gravityY = -150;
		gravityZ = 0;
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
		volume = PlayerPrefsManager.GetMasterVolume();
		MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
		musicManager.ChangeVolume (volume);
	}

	public void Effect() {
		if(PlayerPrefsManager.NiceBowlingGet() == 1){
		int niceRandom = Random.Range(1, 50);
		switch(niceRandom){
			case 1:NoGravityAllPins(); break;
			case 2:CarnyPins(); break; 
			case 3:GravityX(); break;
			case 4:GravityYHeavy(); break;
			case 5:GravityDiff(); break;
			case 6:GravityYLight(); break;
			case 7:IncreasePinDrag(); break; 
			case 8:IncreasePinSizeAllDiff(); break;
			case 9:IncreasePinSizeAllUnif(); break;
			case 10:DecreasePinSizeAllUnif(); break;
			case 11:DecreasePinSizeAllDiff(); break;
			case 12:BouncyBall(); break;
			case 13:OneSmallPin (); break;
			case 14:OneHugePin (); break;
			case 15:SomePinsMove(); break;
			case 16:GiantBall (); break;
			case 17:SmallBall (); break;
			case 18:Cheater(); break;
			case 19:AllPinsMove(); break;
			case 20:TinyBall(); break;
			case 21:LargeBall(); break;
			case 22:LightBall (); break;
			case 23:HeavyBall (); break;
			case 24:ResetBall (); break;
			case 25:ResetBall (); break;
			case 26:ResetBall (); break;
			case 27:CarnyPin(); break;
			case 28:BumpersOff(); break;
			case 29:Bumpers (); break;
			case 30:BumpersOff(); break;
			case 31:SardineRain (); break;
			case 32:Obstical (); break;
			case 33:Obsticals(); break;
			case 34:RampAdd(); break;
			case 35:SkyboxUnity(); break;
			case 36:Horror(); break;
			case 37:SkyboxSpace(); break;
			case 38:ResetBall(); GravityReset(); SkyboxSpace(); BumpersOff(); break;
			case 39:AddPinsx1(); AddPinsx2(); AddPinsx3(); AddPinsx4(); break;
			case 40:AddPinsx1(); break;
			case 41:AddPinsx2(); break;
			case 42:AddPinsx3(); break;
			case 43:AddPinsx4(); break;
			case 44:GravityReset (); break;
			case 45:GravityReset (); break;
			case 46:Effect(); Effect(); break;
			case 47:Effect(); Effect(); break;
			case 48:Effect(); Effect(); Effect(); break;
			case 49:Effect(); Effect(); Effect(); break;
			case 50:Effect(); Effect(); Effect(); Effect(); break;
			
			default:print ("NiceBowling.Effect switch case default triggered somehow"); break;
			}
		}
	}
	//Each switch case will call a function below
	
	//Case1
	public void NoGravityAllPins(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.useGravity = false;
		}
	}
	
	//Case2
	public void CarnyPins(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.isKinematic = true;
		}
	}
	
	//Case3 gravity flux (x) + or - whole lane
	public void GravityX(){
		gravityX = Random.Range (-10, 10);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
	}

	//Case4 gravity flux (y) Heavy side only
	public void GravityYHeavy(){
		gravityY = Random.Range (-600, -200);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
	}

	//Case5 gravity flux (z) + or - whole lane
	public void GravityDiff(){
		gravityY = Random.Range (-400, 3);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			int number = Random.Range (1, 4);
			if(number == 2){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.useGravity = false;
			}
		}
		
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
	}

	//Case6 gravity flux (y) light side only
	public void GravityYLight(){
		gravityY = Random.Range (-150, 3);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
	}

	//Case7 
	public void IncreasePinDrag(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.angularDrag = Random.Range (0.05f, 50f);
			body.drag = Random.Range (0, 20);
		}
	}
	
	//Case8 
	public void IncreasePinSizeAllDiff(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			float size = Random.Range (1.2f, 2.5f);
			pin.transform.localScale = new Vector3(size, size, size);
		}
	}

	//Case9
	public void IncreasePinSizeAllUnif(){
		float size = Random.Range (1.2f, 2.5f);
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			pin.transform.localScale = new Vector3(size, size, size);
		}
	}
	//Case10
	public void DecreasePinSizeAllUnif(){
		float size = Random.Range (0.6f, 0.95f);
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			pin.transform.localScale = new Vector3(size, size, size);
		}
	}
	//Case11
	public void DecreasePinSizeAllDiff(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			float size = Random.Range (0.5f, 0.95f);
			pin.transform.localScale = new Vector3(size, size, size);
		}
	}
	//Case12
	public void BouncyBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		SphereCollider col = ball.GetComponent<SphereCollider>();
		col.sharedMaterial = Bounce;
		
	}
	
	//Case13
	public void OneSmallPin(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			int number = Random.Range (1, 8);
			if(number == 4){pin.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);}
		}
	}
	//Case14
	public void OneHugePin(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			int number = Random.Range (1, 8);
			if(number == 4){pin.transform.localScale = new Vector3(4f, 4f, 4f);}
		}
	}
	//Case15
	public void SomePinsMove(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			int number = Random.Range (1, 6);
			if(number == 4){
				float x = Random.Range (-30, 30);
				float y = 1;
				float z = Random.Range (-50, 50);
				pin.transform.localPosition += new Vector3(x, y, z);
			}
		}
	}
	//Case16 
	public void GiantBall(){
	Ball ball = GameObject.FindObjectOfType<Ball>();
	ball.transform.localScale = new Vector3 (40f, 40f, 40f);
	}
	
	//Case17 
	public void SmallBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		ball.transform.localScale = new Vector3 (10f, 10f, 10f);
	}
	//Case18
	public void Cheater(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		ball.transform.localPosition += new Vector3(0, 0, 500f);
	}
	//Case19
	public void AllPinsMove(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			float x = Random.Range (-25, 25);
			float y = 1;
			float z = Random.Range (-40, 40);
			pin.transform.localPosition += new Vector3(x, y, z);
		}
	}
	//Case20 
	public void TinyBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		ball.transform.localScale = new Vector3 (5f, 5f, 5f);
	}
	//Case21 
	public void LargeBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		ball.transform.localScale = new Vector3 (30f, 30f, 30f);
	}
	//Case22 
	public void LightBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		Rigidbody body = ball.GetComponent<Rigidbody>();
		Renderer rend = ball.GetComponent<Renderer> ();
		rend.material = ball2;
		body.mass = .5f;
	}
	//Case23 
	public void HeavyBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		Rigidbody body = ball.GetComponent<Rigidbody>();
		Renderer rend = ball.GetComponent<Renderer> ();
		rend.material = ball3;
		body.mass = 70;
	}
	//Case24 - 26 Used multiple times since the pins already auto reset
	public void ResetBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		Rigidbody body = ball.GetComponent<Rigidbody>();
		Renderer rend = ball.GetComponent<Renderer> ();
		SphereCollider col = ball.GetComponent<SphereCollider>();
		rend.material = ball1;
		body.mass = 8;
		ball.transform.localScale = new Vector3 (21f, 21f, 21f);
		col.sharedMaterial = none;
		
	}
	//Case27
	public void CarnyPin(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			int number = Random.Range (1, 9);
			if(number == 8){
				Rigidbody body = pin.GetComponent<Rigidbody>();
				body.isKinematic = true;
				}
		}
	}
	
	//Case28 Bumpers off again
	
	//Case29 
	public void Bumpers(){
		Bumper.SetActive(true);
		Bumper2.SetActive(true);
	}
	//Case30 
	public void BumpersOff(){
		Bumper.SetActive(false);
		Bumper2.SetActive(false);
	}
	//Case31 
	public void SardineRain(){
		int rand = Random.Range (10, 200);
		for(int i = 0; i < rand; i++){
			Instantiate(Sardine, new Vector3(Random.Range (55f, -55f), Random.Range (50f, 200f), Random.Range (150f, 2000f)), Quaternion.Euler (Random.Range (0f, 360f), Random.Range (0f, 360f), Random.Range (0f, 360f)));
			}
	}
	//Case32 
	public void Obstical(){
		Instantiate(Cylinder, new Vector3(Random.Range (55f, -55f), Random.Range (40f, -30f), Random.Range (300f, 1600f)), Quaternion.identity);
	}
	//Case33 
	public void Obsticals(){
		float z = Random.Range (400f, 1600f);
		float y = Random.Range (30f, -10f);
		Instantiate(Cylinder, new Vector3(40f, y, z), Quaternion.identity);
		Instantiate(Cylinder, new Vector3(-40f, y, z), Quaternion.identity);
	}
	//Case34 
	public void RampAdd(){
		Instantiate(Ramp, new Vector3(0, -3, Random.Range (300f, 1600f)), Quaternion.Euler (-10, 0, 0));
	}
	//Case35
	public void SkyboxUnity(){
		Skybox sky = MainCamera.GetComponent<Skybox>();
		sky.material = SkyUnity;
		light.SetActive(true);
		MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
		musicManager.ChangeVolume (volume);
		
	}
	//Case36 
	public void Horror(){
		Skybox sky = MainCamera.GetComponent<Skybox>();
		MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
		musicManager.ChangeVolume (0f);
		sky.material = SkyHorror;
		light.SetActive(false);
		AudioSource JumpScare = this.GetComponent<AudioSource>();
		JumpScare.Play ();
		
	}
	//Case37 
	public void SkyboxSpace(){
		Skybox sky = MainCamera.GetComponent<Skybox>();
		MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
		musicManager.ChangeVolume (volume);
		sky.material = SkySpace;	
		light.SetActive(true);
	}
	
	//Case38 Global Reset
	
	//Case39 Add all the pins
	
	//Case40 add dummy pins X1
	public void AddPinsx1(){
		Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
	}
	//Case41 add dummy pins X2
	public void AddPinsx2(){
		Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
		Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
	}
	//Case42 add dummy pins x3
	public void AddPinsx3(){
		Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
		Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
		Instantiate(dumbPinSet, new Vector3(0, 1, 1460), Quaternion.identity);
	}
	//Case43 add dummy pins X4
	public void AddPinsx4(){
		Instantiate(dumbPinSet, new Vector3(0, 1, 1495), Quaternion.identity);
		Instantiate(dumbPinSet, new Vector3(0, 1, 1530), Quaternion.identity);
		Instantiate(dumbPinSet, new Vector3(0, 1, 1565), Quaternion.identity);
		Instantiate(dumbPinSet, new Vector3(0, 1, 1600), Quaternion.identity);
	}
	//Case44 - 45
	public void GravityReset(){
		gravityX = 0;
		gravityY = -150;
		gravityZ = 0;
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
	}
	//Cases 46-50 call effect multiple times
	
}

