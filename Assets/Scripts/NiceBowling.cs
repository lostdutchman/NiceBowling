using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class NiceBowling : MonoBehaviour {
	
	public GameObject Bumper, Bumper2, dumbPinSet, Cylinder, Sardine, Ramp, BallBeach, BallCannon, BallBucky, BallCactus, BallSpiked, BallBounce, BallPool, BallJelly, PinBig, PinToy;
    public float NBDelay;
	private float gravityX, gravityY, gravityZ;
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

    public void NiceManager()
    {
        //Get NB Effects
        //int[] WeightedRandom = new int[] { 1, 1, 1, 2, 2, 2, 2, 3, 3, 4 };
        //int NiceRandom = WeightedRandom[UnityEngine.Random.Range(0, WeightedRandom.Length)];
        //List<string> Effects = Effect(NiceRandom);
        List<string> Effects = Effect(1);
        StartCoroutine(NBUI.NiceBowlingEffects(Effects, FirstFrame));
        FirstFrame = false;
    }

    public List<string> Effect(int niceRandom) {
        //Determin primary effects
        List<int> UsedNum = new List<int>();
        List<string> Effects = new List<string>();

        for (int i = 0; i < niceRandom; i++)
        {
            int TempNum = UnityEngine.Random.Range(1, 2); //Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned

            if (!UsedNum.Contains(TempNum))
            {
                UsedNum.Add(TempNum);
                switch (TempNum)
                {
                    case 1: Effects.Add(Billiards()); break;
                    case 2: Effects.Add(PinMove()); break;
                    case 3: Effects.Add(DifferentPins()); break;
                    case 4: Effects.Add(BallSize()); break;
                    case 5: break;
                    case 6: Effects.Add(DifferentBall()); break;
                    case 7: Effects.Add(Bumpers()); break;
                    case 8: Effects.Add(SardineRain()); break;
                    case 9: Effects.Add(Obstical()); break;
                    case 10: Effects.Add(Obsticals()); break;
                    case 11: Effects.Add(RampAdd()); break;
                    case 12: Effects.Add(AddPins()); break;

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

#region Effect Switch Case's
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

    private string DifferentBall()
    {
        switch (UnityEngine.Random.Range(1, 8))//Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned
        {
            case 1: return BeachBall();
            case 2: return CannonBall();
            case 3: return BouncyBall();
            case 4: return BuckyBall();
            case 5: return SpikeBall();
            case 6: return CactusBall();
            case 7: return JellyBall();

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

    private string DifferentPins()
    {
        switch (UnityEngine.Random.Range(1, 3))//Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned
        {
            case 1: return IncreasePinSize();
            case 2: return DecreasePinSize();

            default: print("NiceBowling.DifferentPins switch case default triggered somehow"); break;
        }
        return "Pin Change Error!";
    }

    private string PinMove()
    {
        switch (UnityEngine.Random.Range(1, 8))//Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned
        {
            case 1: return BoardwalkPins();
            case 2: return NoGravityAllPins();
            case 3: return NoGravityAllPins();
            case 4: return NoGravityAllPins();
            case 5: return NoGravityAllPins();
            case 6: return IncreasePinDrag();
            case 7: return IncreasePinDrag();

            default: print("NiceBowling.PinMove switch case default triggered somehow"); break;
        }
        return "Pin movement Error!";
    }

    #endregion

#region Effects
    public string NoGravityAllPins(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.useGravity = false;
		}
        return("Zero G!");
	}

    public string BoardwalkPins()
    {
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            Rigidbody body = pin.GetComponent<Rigidbody>();
            body.isKinematic = true;
        }
        return ("Sleazy Pins!");
    }

	public string GravityYHeavy(){
		gravityY = UnityEngine.Random.Range (-600, -200);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
        return("4x Gravity!");
    }

	public string GravityYLight(){
		gravityY = UnityEngine.Random.Range (-150, 3);
		Physics.gravity= new Vector3(gravityX, gravityY, gravityZ);
        return("Low Gravity");
    }

	public string IncreasePinDrag(){
		foreach(Pins pin in GameObject.FindObjectsOfType<Pins>()){
			Rigidbody body = pin.GetComponent<Rigidbody>();
			body.angularDrag = 30f;
			body.drag = 15f;
		}
        return("Bullet-Time!");
    }

	public string IncreasePinSize(){
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            Vector3 Location = pin.transform.position;
            Destroy(pin.gameObject);
            Instantiate(PinBig, Location, Quaternion.Euler(-90, 0, 0));
        }
        return ("Big ol' Pins!");
    }

	public string DecreasePinSize(){
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            Vector3 Location = pin.transform.position;
            Destroy(pin.gameObject);
            Instantiate(PinToy, Location, Quaternion.Euler(-90, 0, 0));
        }
        return ("Tiny Pins!");
    }
	
	public string GiantBall(){
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 size = childBall.transform.localScale;
        childBall.transform.localScale = new Vector3 (size.x * 2, size.y * 2, size.z * 2);
        return("Big ol' Ball");
    }
	
	public string SmallBall(){
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 size = childBall.transform.localScale;
        childBall.transform.localScale = new Vector3(size.x / 1.5f, size.y / 1.5f, size.z / 1.5f);
        return ("Little Ball");
    }

	public string TinyBall(){
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 size = childBall.transform.localScale;
        childBall.transform.localScale = new Vector3(size.x / 2.5f, size.y / 2.5f, size.z / 2.5f);
        return ("Tiny Ball");
    }

	public string LargeBall(){
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 size = childBall.transform.localScale;
        childBall.transform.localScale = new Vector3(size.x * 1.5f, size.y * 1.5f, size.z * 1.5f);
        return ("Big Ball");
    }

	public string BeachBall(){
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 Location = childBall.transform.position;
        Destroy(childBall);
        Instantiate(BallBeach, Location, Quaternion.identity, GameObject.FindObjectOfType<Ball>().transform);
        return ("Beach Ball");
    }

	public string CannonBall(){
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 Location = childBall.transform.position;
        Destroy(childBall);
        Instantiate(BallCannon, Location, Quaternion.identity, GameObject.FindObjectOfType<Ball>().transform);
        return ("Cannon Ball!");
    }

    public string BouncyBall()
    {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 Location = childBall.transform.position;
        Destroy(childBall);
        Instantiate(BallBounce, Location, Quaternion.identity, GameObject.FindObjectOfType<Ball>().transform);
        return ("Bouncy Ball!");
    }

    public string BuckyBall()
    {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 Location = childBall.transform.position;
        Destroy(childBall);
        Instantiate(BallBucky, Location, Quaternion.identity, GameObject.FindObjectOfType<Ball>().transform);
        return ("Polyhedral Ball!");
    }

    public string CactusBall()
    {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 Location = childBall.transform.position;
        Destroy(childBall);
        Instantiate(BallCactus, Location, Quaternion.identity, GameObject.FindObjectOfType<Ball>().transform);
        return ("Jackfruit!");
    }

    public string SpikeBall()
    {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 Location = childBall.transform.position;
        Destroy(childBall);
        Instantiate(BallSpiked, Location, Quaternion.identity, GameObject.FindObjectOfType<Ball>().transform);
        return ("Spike Ball!");
    }

    public string JellyBall()
    {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 Location = childBall.transform.position;
        Destroy(childBall);
        Instantiate(BallJelly, Location, Quaternion.identity, GameObject.FindObjectOfType<Ball>().transform);
        return ("Wiggly Ball!");
    }

    public string Bumpers(){
		Bumper.SetActive(true);
		Bumper2.SetActive(true);
        return("Kiddie Bowling!");
    }

	public string SardineRain(){
		int rand = UnityEngine.Random.Range (10, 200);
		for(int i = 0; i < rand; i++){
            Instantiate(Sardine, new Vector3(UnityEngine.Random.Range(55f, -55f), UnityEngine.Random.Range(50f, 200f), UnityEngine.Random.Range(150f, 2000f)), Quaternion.Euler(UnityEngine.Random.Range(.1f, 360f), UnityEngine.Random.Range(.1f, 360f), UnityEngine.Random.Range(.1f, 360f)));
        }
        return("Fish?");
    }

	public string Obstical(){
		Instantiate(Cylinder, new Vector3(UnityEngine.Random.Range (55f, -55f), UnityEngine.Random.Range (40f, -30f), UnityEngine.Random.Range (300f, 1600f)), Quaternion.identity);
        return("Obstacle!");
    }

	public string Obsticals(){
		float z = UnityEngine.Random.Range (400f, 1600f);
		float y = UnityEngine.Random.Range (30f, -10f);
		Instantiate(Cylinder, new Vector3(30f, y, z), Quaternion.identity);
		Instantiate(Cylinder, new Vector3(-30f, y, z), Quaternion.identity);
        return("Obstacles!");
    }

    public string RampAdd(){
		Instantiate(Ramp, new Vector3(0, -3, UnityEngine.Random.Range (300f, 1600f)), Quaternion.Euler (-10, 0, 0));
        return("Ramp!");
    }

    private string Billiards()
    {
        if (UnityEngine.Random.Range(0, 20) > 17)
        {
            Instantiate(BallPool, new Vector3(UnityEngine.Random.Range(40f, -40f), 15, 1700f), Quaternion.identity);
            if (UnityEngine.Random.Range(0, 20) == 19)
            {
                Instantiate(BallPool, new Vector3(UnityEngine.Random.Range(40f, -40f), 15, 1700f), Quaternion.identity);
            }
        }
        Instantiate(BallPool, new Vector3(UnityEngine.Random.Range(40f, -40f), 15, 1700f), Quaternion.identity);
        return ("Trick Shot!");
    }
#region Pin Add
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
    #endregion

#endregion


}

