using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NiceBowling : MonoBehaviour {
	
	public GameObject Bumper, Bumper2, dumbPinSet, Cylinder, Sardine, Ramp;
    public float NBDelay;
	private float gravityX, gravityY, gravityZ;
	public Material ball1, ball2, ball3; 
	public PhysicMaterial Bounce, none;
	private Camera MainCamera;
	private float volume;
    public UIAnimation NBUI;
    private bool FirstFrame;

    // Use this for initialization
    void Start () {
        //This initializes the player pref incase its the first time playing and they didnt go to the options page
        volume = PlayerPrefsManager.GetMasterVolume();
        MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
        musicManager.ChangeVolume(volume);
        gravityX = Physics.gravity.x;
        gravityY = Physics.gravity.y;
        gravityZ = Physics.gravity.z;
        FirstFrame = true;
    }

    //use for initilization if level is loaded more then onece in a session
    void OnLevelWasLoaded()
    {
        volume = PlayerPrefsManager.GetMasterVolume();
        MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
        musicManager.ChangeVolume(volume);
        gravityX = Physics.gravity.x;
        gravityY = Physics.gravity.y;
        gravityZ = Physics.gravity.z;
    }

    public void NiceManager()
    {
        //Get NB Effects
        int[] WeightedRandom = new int[] { 1, 1, 1, 2, 2, 2, 2, 3, 3, 4 };
        int NiceRandom = WeightedRandom[UnityEngine.Random.Range(0, WeightedRandom.Length)];
        List<string> Effects = Effect(NiceRandom);
        StartCoroutine(NBUI.NiceBowlingEffects(Effects, FirstFrame));
        FirstFrame = false;
    }

    public List<string> Effect(int niceRandom) {
        //Determin primary effects
        List<int> UsedNum = new List<int>();
        List<string> Effects = new List<string>();

        for (int i = 0; i < niceRandom; i++)
        {
            int TempNum = UnityEngine.Random.Range(1, 14); //Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned

            if (!UsedNum.Contains(TempNum))
            {
                UsedNum.Add(TempNum);
                switch (TempNum)
                {
                    case 1: Effects.Add(Gravity()); break;
                    case 2: Effects.Add(PinMove()); break;
                    case 3: Effects.Add(PinSize()); break;
                    case 4: Effects.Add(BouncyBall()); break;
                    case 5: Effects.Add(BallSize()); break;
                    case 6: Effects.Add(Cheater()); break;
                    case 7: Effects.Add(BallMass()); break;
                    case 8: Effects.Add(Bumpers()); break;
                    case 9: Effects.Add(SardineRain()); break;
                    case 10: Effects.Add(Obstical()); break;
                    case 11: Effects.Add(Obsticals()); break;
                    case 12: Effects.Add(RampAdd()); break;
                    case 13: Effects.Add(AddPins()); break;

                    default: print("NiceBowling.Effect switch case default triggered somehow"); break;
                }
            }
            else
            {
                i--; //To not count that loop.
            }
        }
        return Effects;
    }

    //Choice Effects
    private string AddPins()
    {
        switch (UnityEngine.Random.Range(1, 14))//Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned
        {
            case 1: return AddPinsx1(); 
            case 2: return AddPinsx2(); 
            case 3: return AddPinsx3(); 
            case 4: return AddPinsx4(); 
            case 5: return AddPinsx5();
            case 6: return AddPinsx6(); 
            case 7: return AddPinsx7(); 
            case 8: return AddPinsx8(); 
            case 9: return AddPinsx9(); 
            case 10: return AddPinsx10();
            case 11: return AddPinsx11();
            case 12: return AddPinsExplode();
            case 13: return AddPinsExplodeBig();

            default: print("NiceBowling.AddPins switch case default triggered somehow"); break;
        }
        return "Extra Pins Error!";
    }

    private string BallMass()
    {
        switch (UnityEngine.Random.Range(1, 3))//Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned
        {
            case 1: return LightBall();
            case 2: return HeavyBall();

            default: print("NiceBowling.BallMass switch case default triggered somehow"); break;
        }
        return "Ball Mass Error!";
    }

    private string BallSize()
    {
        switch (UnityEngine.Random.Range(1, 5))//Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned
        {
            case 1: return GiantBall();
            case 2: return SmallBall();
            case 3: return TinyBall();
            case 4: return LargeBall();

            default: print("NiceBowling.BallSize switch case default triggered somehow"); break;
        }
        return "Ball Size Error!";
    }

    private string PinSize()
    {
        switch (UnityEngine.Random.Range(1, 3))//Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned
        {
            case 1: return IncreasePinSize();
            case 2: return DecreasePinSize();

            default: print("NiceBowling.PinSize switch case default triggered somehow"); break;
        }
        return "Pin Size Error!";
    }

    private string PinMove()
    {
        switch (UnityEngine.Random.Range(1, 10))//Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned
        {
            case 1: return BoardwalkPins();
            case 2: return IncreasePinDrag();
            case 3: return IncreasePinDrag();
            case 4: return IncreasePinDrag();
            case 5: return IncreasePinDrag();
            case 6: return IncreasePinDrag();
            case 7: return IncreasePinDrag();
            case 8: return IncreasePinDrag();
            case 9: return IncreasePinDrag();

            default: print("NiceBowling.PinMove switch case default triggered somehow"); break;
        }
        return "Pin movement Error!";
    }

    private string Gravity()
    {
        switch (UnityEngine.Random.Range(1, 6))//Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned
        {
            case 1: return NoGravityAllPins();
            case 2: return GravityX();
            case 3: return GravityYHeavy();
            case 4: return GravityDiff(); 
            case 5: return GravityYLight();

            default: print("NiceBowling.Gravity switch case default triggered somehow"); break;
        }
        return "Gravity Error!";
    }


	//Primary Effects
	public string NoGravityAllPins(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.useGravity = false;
		}
        return("Zero G!");
	}
	
	public string BoardwalkPins(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.isKinematic = true;
		}
        return("Boardwalk Pins!");
    }

    public string GravityX(){
		gravityX = UnityEngine.Random.Range (-10, 10);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
        return("Gravity Flux");
    }

	public string GravityYHeavy(){
		gravityY = UnityEngine.Random.Range (-600, -200);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
        return("J-J-J-Jupiter!");
    }

	public string GravityDiff(){
		gravityY = UnityEngine.Random.Range (-400, 3);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			int number = UnityEngine.Random.Range (1, 4);
			if(number == 2){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.useGravity = false;
			}
        }
		
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
        return("Wind");
    }

	public string GravityYLight(){
		gravityY = UnityEngine.Random.Range (-150, 3);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
        return("Wind");
    }

	public string IncreasePinDrag(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.angularDrag = 30f;
			body.drag = 15f;
		}
        return("Molassus!!!");
    }

	public string IncreasePinSize(){
		float size = UnityEngine.Random.Range (1.2f, 2.5f);
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			pin.transform.localScale = new Vector3(size, size, size);
		}
        return("Big ol Pins!");
    }

	public string DecreasePinSize(){
		float size = UnityEngine.Random.Range (0.6f, 0.95f);
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			pin.transform.localScale = new Vector3(size, size, size);
		}
        return("Baby Pins!");
    }

	public string BouncyBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		SphereCollider col = ball.GetComponent<SphereCollider>();
		col.sharedMaterial = Bounce;
        return("Springy");
    }
	
	public string GiantBall(){
	    Ball ball = GameObject.FindObjectOfType<Ball>();
        Vector3 size = ball.transform.localScale;
	    ball.transform.localScale = new Vector3 (size.x * 2, size.y * 2, size.z * 2);
        return("Giant Ball");
    }
	
	public string SmallBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
        Vector3 size = ball.transform.localScale;
        ball.transform.localScale = new Vector3(size.x / 1.5f, size.y / 1.5f, size.z / 1.5f);
        return ("Small Ball");
    }

	public string Cheater(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		ball.transform.localPosition += new Vector3(0, 0, 1000f);
        return("Cheater!");
    }

	public string TinyBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
        Vector3 size = ball.transform.localScale;
        ball.transform.localScale = new Vector3(size.x / 2.5f, size.y / 2.5f, size.z / 2.5f);
        return ("Tiny Ball");
    }

	public string LargeBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
        Vector3 size = ball.transform.localScale;
        ball.transform.localScale = new Vector3(size.x * 1.5f, size.y * 1.5f, size.z * 1.5f);
        return ("Big Ball");
    }

	public string LightBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		Rigidbody body = ball.GetComponent<Rigidbody>();
		Renderer rend = ball.GetComponent<Renderer> ();
		rend.material = ball2;
		body.mass = .5f;
        return("Bouncy Ball");
    }

	public string HeavyBall(){
		Ball ball = GameObject.FindObjectOfType<Ball>();
		Rigidbody body = ball.GetComponent<Rigidbody>();
		Renderer rend = ball.GetComponent<Renderer> ();
		rend.material = ball3;
		body.mass = 70;
        return("Concrete!");
    }
	
	public string Bumpers(){
		Bumper.SetActive(true);
		Bumper2.SetActive(true);
        return("Kid Bowling");
    }

	public string SardineRain(){
		int rand = UnityEngine.Random.Range (10, 200);
		for(int i = 0; i < rand; i++){
			Instantiate(Sardine, new Vector3(UnityEngine.Random.Range (55f, -55f), UnityEngine.Random.Range (50f, 200f), UnityEngine.Random.Range (150f, 2000f)), Quaternion.Euler (UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f)));
			}
        return("Fish?");
    }

	public string Obstical(){
		Instantiate(Cylinder, new Vector3(UnityEngine.Random.Range (55f, -55f), UnityEngine.Random.Range (40f, -30f), UnityEngine.Random.Range (300f, 1600f)), Quaternion.identity);
        return("Obstical");
    }

	public string Obsticals(){
		float z = UnityEngine.Random.Range (400f, 1600f);
		float y = UnityEngine.Random.Range (30f, -10f);
		Instantiate(Cylinder, new Vector3(40f, y, z), Quaternion.identity);
		Instantiate(Cylinder, new Vector3(-40f, y, z), Quaternion.identity);
        return("Obsticals");
    }

    public string RampAdd(){
		Instantiate(Ramp, new Vector3(0, -3, UnityEngine.Random.Range (300f, 1600f)), Quaternion.Euler (-10, 0, 0));
        return("Ramp");
    }
	
	public string AddPinsx1(){
		Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        return("Dumb Pins");
    }

    public string AddPinsx2()
    {
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        return ("Dumb Pins");
    }

    public string AddPinsx3()
    {
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        return ("Many Pins!");
    }

    public string AddPinsx4()
    {
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        return ("Many Pins!");
    }

    public string AddPinsx5()
    {
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
        return ("To Many Pins!");
    }

    public string AddPinsx6()
    {
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1460), Quaternion.identity);
        return ("To Many Pins!");
    }

    public string AddPinsx7()
    {
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1460), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1495), Quaternion.identity);

        return ("Way to many pins!");
    }

    public string AddPinsx8()
    {
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1460), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1495), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1530), Quaternion.identity);

        return ("Way to many pins!");
    }

    public string AddPinsx9()
    {
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1460), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1495), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1530), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1565), Quaternion.identity);

        return ("PINS! PINS! PINS!");
    }

    public string AddPinsx10()
    {
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1460), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1495), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1530), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1565), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1600), Quaternion.identity);
        return ("PINS! PINS! PINS!");
    }

    public string AddPinsx11()
    {
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1460), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1495), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1530), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1565), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1600), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1635), Quaternion.identity);
        return ("Unfair number of pins!");
    }

    public string AddPinsExplode()
    {
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1460), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1495), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1530), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1565), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1600), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1635), Quaternion.identity);
        return ("Pin Explosion!");
    }

    public string AddPinsExplodeBig(){
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1460), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1495), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1530), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1565), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1600), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1635), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1460), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1495), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1530), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1565), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1600), Quaternion.identity);
        Instantiate(dumbPinSet, new Vector3(0, 1, 1635), Quaternion.identity);
        return ("Game Breaking number of pins!");
    }

}

