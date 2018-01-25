using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NiceBowling : MonoBehaviour {
	
	public GameObject Bumper, Bumper2, dumbPinSet, Cylinder, Sardine, Ramp, NBEffect, NBUI;
    public float NBDelay;
	private float gravityX, gravityY, gravityZ;
	public Material ball1, ball2, ball3; //Absolutly get gid of cosmetic changes that are not tied to gameplay changes and kill the horrer concept.
	public PhysicMaterial Bounce, none;
	private Camera MainCamera;
	private float volume;
    private Text NBtext;

    // Use this for initialization
    void Start () {
        //This initializes the player pref incase its the first time playing and they didnt go to the options page
        volume = PlayerPrefsManager.GetMasterVolume();
        MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
        musicManager.ChangeVolume(volume);
        NBtext = NBUI.GetComponent<Text>();
    }

    //use for initilization if level is loaded more then onece in a session
    void OnLevelWasLoaded()
    {
        volume = PlayerPrefsManager.GetMasterVolume();
        MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
        musicManager.ChangeVolume(volume);
    }

    public void NiceManager()
    {
        NBEffect.SetActive(true);
        StartCoroutine(Effect());
        NBEffect.SetActive(false);
    }

    public void NiceUI(string Text)
    {
        NBtext.text = Text;
    }

    public IEnumerator Effect() {
        int niceRandom = Random.Range(1, 50);
		switch(niceRandom){
		    case 1:NoGravityAllPins(); yield return new WaitForSeconds(NBDelay); break;
		    case 2:CarnyPins(); yield return new WaitForSeconds(NBDelay); break;
            case 3:GravityX(); yield return new WaitForSeconds(NBDelay); break;
            case 4:GravityYHeavy(); yield return new WaitForSeconds(NBDelay); break;
            case 5:GravityDiff(); yield return new WaitForSeconds(NBDelay); break;
            case 6:GravityYLight(); yield return new WaitForSeconds(NBDelay); break;
            case 7:IncreasePinDrag(); yield return new WaitForSeconds(NBDelay); break;
            case 8:IncreasePinSize(); yield return new WaitForSeconds(NBDelay); break;
            case 9:DecreasePinSize(); yield return new WaitForSeconds(NBDelay); break;
            case 10:BouncyBall(); yield return new WaitForSeconds(NBDelay); break;
            case 11:GiantBall(); yield return new WaitForSeconds(NBDelay); break;
            case 12:SmallBall(); yield return new WaitForSeconds(NBDelay); break;
            case 13:Cheater(); yield return new WaitForSeconds(NBDelay); break;
            case 14:TinyBall(); yield return new WaitForSeconds(NBDelay); break;
            case 15:LargeBall(); yield return new WaitForSeconds(NBDelay); break;
            case 16:LightBall(); yield return new WaitForSeconds(NBDelay); break;
            case 17:HeavyBall(); yield return new WaitForSeconds(NBDelay); break;
            case 18:CarnyPin(); yield return new WaitForSeconds(NBDelay); break;
            case 19:Bumpers(); yield return new WaitForSeconds(NBDelay); break;
            case 20:SardineRain(); yield return new WaitForSeconds(NBDelay); break;
            case 21:Obstical(); yield return new WaitForSeconds(NBDelay); break;
            case 22:Obsticals(); yield return new WaitForSeconds(NBDelay); break;
            case 23:RampAdd(); yield return new WaitForSeconds(NBDelay); break;
            case 24:AddPinsx1(); yield return new WaitForSeconds(NBDelay); break;
            case 25:AddPinsx4(); yield return new WaitForSeconds(NBDelay); break;
            case 26:Effect(); break;
            case 27:Effect(); break;
            case 28:Effect(); break;
            case 29:Effect(); break;
            case 30:Effect(); break;
            case 31:Effect(); break;
            case 32:Effect(); break;
            case 33:Effect(); break;
            case 34:Effect(); break;
            case 35:Effect(); break;
            case 36:Effect(); break;
            case 37:Effect(); break;
            case 38:Effect(); break;
            case 39:Effect(); break;
            case 40:Effect(); break;
            case 41:Effect(); break;
            case 42:Effect(); break;
            case 43:Effect(); break;
            case 44:Effect(); break;
            case 45:EffectX3(); yield return new WaitForSeconds(NBDelay); break; 
            case 46:EffectX3(); yield return new WaitForSeconds(NBDelay); break;
            case 47:EffectX3(); yield return new WaitForSeconds(NBDelay); break;
            case 48:EffectX4(); yield return new WaitForSeconds(NBDelay); break;
            case 49:EffectX4(); yield return new WaitForSeconds(NBDelay); break;
            case 50:EffectX5(); yield return new WaitForSeconds(NBDelay); break;

            default:print ("NiceBowling.Effect switch case default triggered somehow"); yield return new WaitForSeconds(NBDelay); break;
        }
	}
	//Each switch case will call a function below
	
	//Case1
	public void NoGravityAllPins(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.useGravity = false;
		}
        NiceUI("Zero G!");
	}
	
	//Case2
	public void CarnyPins(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.isKinematic = true;
		}
        NiceUI("Carny Pins!");
    }

    //Case3 gravity flux (x) + or - whole lane (Wind on x axis)
    public void GravityX(){
		gravityX = Random.Range (-10, 10);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
        NiceUI("Gravity Flux");
    }

	//Case4 gravity flux (y) Heavy side only 
	public void GravityYHeavy(){
		gravityY = Random.Range (-600, -200);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
        NiceUI("J-J-J-Jupiter!");
    }

	//Case5 gravity flux (z) + or - whole lane (Wind on Z axis)
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
        NiceUI("Wind");
    }

	//Case6 gravity flux (y) light side only
	public void GravityYLight(){
		gravityY = Random.Range (-150, 3);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
        NiceUI("Wind");
    }

	//Case7 
	public void IncreasePinDrag(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.angularDrag = Random.Range (0.05f, 50f);
			body.drag = Random.Range (0, 20);
		}
        NiceUI("Molassus!!!");
    }
	
	//Case8 

	public void IncreasePinSize(){
		float size = Random.Range (1.2f, 2.5f);
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			pin.transform.localScale = new Vector3(size, size, size);
		}
        NiceUI("Big ol Pins!");
    }
	//Case9
	public void DecreasePinSize(){
		float size = Random.Range (0.6f, 0.95f);
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			pin.transform.localScale = new Vector3(size, size, size);
		}
        NiceUI("Baby Pins!");
    }

	//Case10
	public void BouncyBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		SphereCollider col = ball.GetComponent<SphereCollider>();
		col.sharedMaterial = Bounce;
        NiceUI("Springy");
    }
	
	//Case11
	public void GiantBall(){
	    Ball ball = GameObject.FindObjectOfType<Ball>();
	    ball.transform.localScale = new Vector3 (40f, 40f, 40f);
        NiceUI("Giant Ball");
    }
	
	//Case12
	public void SmallBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		ball.transform.localScale = new Vector3 (10f, 10f, 10f);
        NiceUI("Small Ball");
    }
	//Case13
	public void Cheater(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		ball.transform.localPosition += new Vector3(0, 0, 1000f);
        NiceUI("Cheater!");
    }

	//Case14
	public void TinyBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		ball.transform.localScale = new Vector3 (5f, 5f, 5f);
        NiceUI("Tiny Ball");
    }
	//Case15
	public void LargeBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		ball.transform.localScale = new Vector3 (30f, 30f, 30f);
        NiceUI("Big Ball");
    }
	//Case16
	public void LightBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		Rigidbody body = ball.GetComponent<Rigidbody>();
		Renderer rend = ball.GetComponent<Renderer> ();
		rend.material = ball2;
		body.mass = .5f;
        NiceUI("Bouncy Ball");
    }
	//Case17
	public void HeavyBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		Rigidbody body = ball.GetComponent<Rigidbody>();
		Renderer rend = ball.GetComponent<Renderer> ();
		rend.material = ball3;
		body.mass = 70;
        NiceUI("Concrete!");
    }

	//Case18
	public void CarnyPin(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			int number = Random.Range (1, 9);
			if(number == 8){
				Rigidbody body = pin.GetComponent<Rigidbody>();
				body.isKinematic = true;
				}
		}
        NiceUI("Carny Pin");
    }
	
	//Case19
	public void Bumpers(){
		Bumper.SetActive(true);
		Bumper2.SetActive(true);
        NiceUI("Kid Bowling");
    }
	//Case20
	public void SardineRain(){
		int rand = Random.Range (10, 200);
		for(int i = 0; i < rand; i++){
			Instantiate(Sardine, new Vector3(Random.Range (55f, -55f), Random.Range (50f, 200f), Random.Range (150f, 2000f)), Quaternion.Euler (Random.Range (0f, 360f), Random.Range (0f, 360f), Random.Range (0f, 360f)));
			}
        NiceUI("Fish?");
    }
	//Case21
	public void Obstical(){
		Instantiate(Cylinder, new Vector3(Random.Range (55f, -55f), Random.Range (40f, -30f), Random.Range (300f, 1600f)), Quaternion.identity);
        NiceUI("Obstical");
    }
	//Case22
	public void Obsticals(){
		float z = Random.Range (400f, 1600f);
		float y = Random.Range (30f, -10f);
		Instantiate(Cylinder, new Vector3(40f, y, z), Quaternion.identity);
		Instantiate(Cylinder, new Vector3(-40f, y, z), Quaternion.identity);
        NiceUI("Obsticals");
    }
	//Case23
	public void RampAdd(){
		Instantiate(Ramp, new Vector3(0, -3, Random.Range (300f, 1600f)), Quaternion.Euler (-10, 0, 0));
        NiceUI("Ramp");
    }
	
	//Case24 add dummy pins X1
	public void AddPinsx1(){
		Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        NiceUI("Dumb Pins");
    }

	//Case25
	public void AddPinsx4(){
		Instantiate(dumbPinSet, new Vector3(0, 1, 1495), Quaternion.identity);
		Instantiate(dumbPinSet, new Vector3(0, 1, 1530), Quaternion.identity);
		Instantiate(dumbPinSet, new Vector3(0, 1, 1565), Quaternion.identity);
		Instantiate(dumbPinSet, new Vector3(0, 1, 1600), Quaternion.identity);
        NiceUI("To Many Pins!");
    }

    //Case26
    //Case27
    //Case29
    //Case30
    //Case31
    //Case32
    //Case33
    //Case34
    //Case35
    //Case36
    //Case37
    //Case38
    //Case39
    //Case40
    //Case41
    //Case42
    //Case43
    //Case44
    //Cases 45 - 47
    public void EffectX3()
    {
        NiceUI("X3!");
        StartCoroutine(Effect());
        StartCoroutine(Effect());
        StartCoroutine(Effect());
    }
    //Cases 46 - 49
    public void EffectX4()
    {
        NiceUI("X4");
        StartCoroutine(Effect());
        StartCoroutine(Effect());
        StartCoroutine(Effect());
        StartCoroutine(Effect());
    }
    //Cases 50
    public void EffectX5()
    {
        NiceUI("X5");
        StartCoroutine(Effect());
        StartCoroutine(Effect());
        StartCoroutine(Effect());
        StartCoroutine(Effect());
        StartCoroutine(Effect());
    }

}

