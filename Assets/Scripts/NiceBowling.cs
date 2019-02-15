using UnityEngine;
using System.Collections.Generic;

public class NiceBowling : MonoBehaviour {
	
	public GameObject Bumper, Bumper2, dumbPinSet, Cylinder, Sardine, Ramp, BallBeach, BallCannon, BallBucky, BallSpiked, BallBounce, BallJelly, BallBomb, PinBig, PinMetal, Pendulum, Windmill, Roomba, Wall, SpeedUp, Mine;
    public GameObject[] BilliardBalls, Marbles;
    public float NBDelay;
	private Camera MainCamera;
	private float volume;
    public UIAnimation NBUI;
    private bool FirstFrame;
    List<int> LastRoundNum = new List<int>();
    int i = 1; //for promotional and testing

    // Use this for initialization
    void Start () {
        FirstFrame = true;
    }

    public void NiceManager()
    {
        ////Get NB Effects
        //int[] WeightedRandom = new int[] { 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 4 };
        //int NiceRandom = WeightedRandom[UnityEngine.Random.Range(0, WeightedRandom.Length)];
        //List<string> Effects = Effect(NiceRandom);
        //For video and promotional work and testing effects
        List<string> Effects = new List<string>();
        switch (i)
        {
            case 1: Effects.Add(SpikeBall()); Effects.Add(Obsticals()); break;
            case 2: Effects.Add(SpikeBall()); Effects.Add(Obsticals()); break;
            case 3: Effects.Add(SpikeBall()); Effects.Add(Obsticals()); break;
            case 4: Effects.Add(SpikeBall()); Effects.Add(Obsticals()); break;
            case 5: Effects.Add(SpikeBall()); Effects.Add(MinigolfWindmill()); break;
            case 6: Effects.Add(SpikeBall()); Effects.Add(MinigolfWindmill()); break;
            case 7: Effects.Add(SpikeBall()); Effects.Add(MinigolfWindmill()); break;
            case 8: Effects.Add(SpikeBall()); Effects.Add(Pendulums()); break;
            case 9: Effects.Add(SpikeBall()); Effects.Add(Pendulums()); break;
            case 10: Effects.Add(SpikeBall()); Effects.Add(Pendulums()); break;
            case 11: Effects.Add(SpikeBall()); Effects.Add(Pendulums()); break;

            default: print("NiceBowling.Effect switch case default triggered somehow"); break;
        }
        i++;
        StartCoroutine(NBUI.NiceBowlingEffects(Effects, FirstFrame));
        FirstFrame = false;
    }

    private string PrintListInt(List<int> list)
    {
        string result = "";
        foreach (var number in list)
        {
            result += number + ", ";
        }
        return result;
    }

    public List<string> Effect(int niceRandom) {
        //Determin primary effects
        List<int> UsedNum = LastRoundNum;
        List<int> ThisRoundNum = new List<int>();
        List<string> Effects = new List<string>();
        for (int i = 0; i < niceRandom; i++)
        {
            int TempNum = UnityEngine.Random.Range(1, 20); //Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned

            if (!UsedNum.Contains(TempNum))
            {
                //Make sure that next round does not have the same stuff that was in previous round
                ThisRoundNum.Add(TempNum);
                //Make sure you dont call these effects 2 times in the same round
                if (TempNum == 1 || TempNum == 2 || TempNum == 3) { UsedNum.Add(1); UsedNum.Add(2); UsedNum.Add(3); }
                else if (TempNum == 4 || TempNum == 5) { UsedNum.Add(4); UsedNum.Add(5); }
                else { UsedNum.Add(TempNum); }
            }
            else
            {
                i--; //To not count that loop.
            }
        }
        //Put the effects in order before activating them to ensure eccefts that effect eachother happen in the correct order.
        ThisRoundNum.Sort();
        foreach (int effect in ThisRoundNum) 
        {
            switch (effect)
            {
                case 1: Effects.Add(DifferentBall()); break;
                case 2: Effects.Add(DifferentBall()); break;
                case 3: Effects.Add(DifferentBall()); break;
                case 4: Effects.Add(DifferentPins()); break;
                case 5: Effects.Add(DifferentPins()); break;
                case 6: Effects.Add(AddPins()); break;
                case 7: Effects.Add(CubeWall()); break;
                case 8: Effects.Add(SpeedBoost()); break;
                case 9: Effects.Add(Bumpers()); break;
                case 10: Effects.Add(Billiards()); break;
                case 11: Effects.Add(Obstical()); break;
                case 12: Effects.Add(Obsticals()); break;
                case 13: Effects.Add(LandMine()); break;
                case 14: Effects.Add(SardineRain()); break;
                case 15: Effects.Add(RampAdd()); break;
                case 16: Effects.Add(Pendulums()); break;
                case 17: Effects.Add(MinigolfWindmill()); break;
                case 18: Effects.Add(RoombaSummon()); break;
                case 19: Effects.Add(PinMove()); break; //has to be called last

                default: print("NiceBowling.Effect switch case default triggered somehow"); break;
            }
        }
        LastRoundNum = ThisRoundNum;
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
        switch (UnityEngine.Random.Range(1, 10))//Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned
        {
            case 1: return BeachBall();
            case 2: return CannonBall();
            case 3: return BouncyBall();
            case 4: return BuckyBall();
            case 5: return SpikeBall();
            case 6: return TinyBall();
            case 7: return JellyBall();
            case 8: return BombBall();
            case 9: return GiantBall();

            default: print("NiceBowling.BallMass switch case default triggered somehow"); break;
        }
        return "Ball Mass Error!";
    }

    private string DifferentPins()
    {
        switch (UnityEngine.Random.Range(1, 3))//Note that max is exclusive, so using Random.Range( 0, 10 ) will return values between 0 and 9. If max equals min, min will be returned
        {
            case 1: return IncreasePinSize();
            case 2: return MetalPins();

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
        foreach (DumbPins pin in GameObject.FindObjectsOfType<DumbPins>())
        {
            Rigidbody body = pin.GetComponent<Rigidbody>();
            body.useGravity = false;
        }
        return ("Zero G!");
	}

    public string BoardwalkPins()
    {
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            if(Random.Range(1, 5) == 2)
            {
                Rigidbody body = pin.GetComponent<Rigidbody>();
                body.isKinematic = true;
                return ("Cheater Pin!");
            }
        }
        return ("");
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

    public string MetalPins()
    {
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            Vector3 Location = pin.transform.position;
            Destroy(pin.gameObject);
            Instantiate(PinMetal, Location, Quaternion.Euler(-90, 0, 0));
        }
        return ("Heavy Pins!");
    }

    public string GiantBall(){
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 size = childBall.transform.localScale;
        childBall.transform.localScale = new Vector3 (size.x * 2, size.y * 2, size.z * 2);
        return("Giant Ball");
    }

	public string TinyBall(){
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 size = childBall.transform.localScale;
        childBall.transform.localScale = new Vector3(size.x / 4, size.y / 4f, size.z / 4f);
        return ("Marble");
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
        return ("Cannonball!");
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
        return ("Jiggly Ball!");
    }

    public string BombBall()
    {
        GameObject childBall = GameObject.FindGameObjectWithTag("ChildBall");
        Vector3 Location = childBall.transform.position;
        Destroy(childBall);
        Instantiate(BallBomb, Location, Quaternion.identity, GameObject.FindObjectOfType<Ball>().transform);
        return ("Bomb Ball!");
    }

    public string Bumpers(){
		Bumper.SetActive(true);
		Bumper2.SetActive(true);
        return("Kiddie Bowling!");
    }

    public string MinigolfWindmill()
    {
        Instantiate(Windmill);
        return ("Mini Golf!");
    }

    public string RoombaSummon()
    {
        Instantiate(Roomba, new Vector3(0, 2.3f, UnityEngine.Random.Range(200f, 1500f)), Quaternion.Euler(0, UnityEngine.Random.Range(0f, 360f), 0));
        return ("Robot Vaccuum is Helping!");
    }

    public string SardineRain(){
		int rand = UnityEngine.Random.Range (10, 100);
		for(int i = 0; i < rand; i++){
            Instantiate(Sardine, new Vector3(UnityEngine.Random.Range(55f, -55f), UnityEngine.Random.Range(50f, 200f), UnityEngine.Random.Range(150f, 1700f)), Quaternion.Euler(UnityEngine.Random.Range(.1f, 360f), UnityEngine.Random.Range(.1f, 360f), UnityEngine.Random.Range(.1f, 360f)));
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

    public string CubeWall()
    {
        Instantiate(Wall, new Vector3(0, 1, UnityEngine.Random.Range(500f, 1500f)), Quaternion.identity);
        return ("B-B-B-Break Out!");
    }

    public string RampAdd(){
		Instantiate(Ramp, new Vector3(0, -3, UnityEngine.Random.Range (300f, 1300f)), Quaternion.Euler (-8, 0, 0));
        return("Ramp!");
    }

    private string Billiards()
    {
        if (UnityEngine.Random.Range(0, 20) > 12)
        {
            Instantiate(BilliardBalls[UnityEngine.Random.Range(0, BilliardBalls.Length)], new Vector3(UnityEngine.Random.Range(40f, -40f), 15, UnityEngine.Random.Range(1500f, 1700f)), Quaternion.identity);
            if (UnityEngine.Random.Range(0, 20) > 15)
            {
                Instantiate(BilliardBalls[UnityEngine.Random.Range(0, BilliardBalls.Length)], new Vector3(UnityEngine.Random.Range(40f, -40f), 15, UnityEngine.Random.Range(600f, 1700f)), Quaternion.identity);
            }
        }
        Instantiate(BilliardBalls[UnityEngine.Random.Range(0, BilliardBalls.Length)], new Vector3(UnityEngine.Random.Range(40f, -40f), 15, UnityEngine.Random.Range(1000f, 1700f)), Quaternion.identity);
        return ("Trick Shot!");
    }

    private string Pendulums()
    {
        int rand = UnityEngine.Random.Range(2, 9);
        for (int i = 0; i < rand; i++)
        {
            if(UnityEngine.Random.Range(0,2) == 0) //Coin todss to determine if right of left side pendulum
            {
                Instantiate(Pendulum, new Vector3(0f, 28f, UnityEngine.Random.Range(70f, 1600f)), Quaternion.Euler(0, 0, 90));
            }
            else 
            {
                Instantiate(Pendulum, new Vector3(0f, 28f, UnityEngine.Random.Range(90f, 1600f)), Quaternion.Euler(0, 180, 90));
            }
        }
        return ("Pendulums");
    }

    private string SpeedBoost()
    {
        int rand = UnityEngine.Random.Range(1, 3);
        for (int i = 0; i < rand; i++)
        {
            Instantiate(SpeedUp, new Vector3(UnityEngine.Random.Range(-30f, 30f), 1, UnityEngine.Random.Range(100f, 1000f)), Quaternion.Euler(90, 180, 0));
        }
        return ("Faster!");
    }

    private string LandMine()
    {
        int rand = UnityEngine.Random.Range(1, 4);
        for (int i = 0; i < rand; i++)
        {
            Instantiate(Mine, new Vector3(UnityEngine.Random.Range(-45f, 45f), .52f, UnityEngine.Random.Range(100f, 1000f)), Quaternion.Euler(0, 0, 0));
        }
        return ("MineSweeper");
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

