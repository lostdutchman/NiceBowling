using UnityEngine;
using System.Collections;

public class NiceBowlingReset : MonoBehaviour
{
    public GameObject Bumper, Bumper2;
    public PhysicMaterial BowlingBall;


    // Use this for initialization
    void Start()
    {
        //Find values?
    }

    //use for initilization if level is loaded more then onece in a session
    void Reset()
    {
        //Reset Lane
        Physics.gravity = new Vector3(0, -150, 0);

        //Reset Pins
        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            Rigidbody body = pin.GetComponent<Rigidbody>();
            body.angularDrag = 0.05f;
            body.drag = 0;
            pin.transform.localScale = new Vector3(1, 1, 1);
        }

        //Reset Ball
        Ball ball = GameObject.FindObjectOfType<Ball>();
        SphereCollider col = ball.GetComponent<SphereCollider>();
        col.sharedMaterial = BowlingBall;
    }

    //Case13
    //public void OneSmallPin()
    //{
    //    foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
    //    {
    //        int number = Random.Range(1, 8);
    //        if (number == 4) { pin.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); }
    //    }
    //}
    ////Case14
    //public void OneHugePin()
    //{
    //    foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
    //    {
    //        int number = Random.Range(1, 8);
    //        if (number == 4) { pin.transform.localScale = new Vector3(4f, 4f, 4f); }
    //    }
    //}
    ////Case15
    //public void SomePinsMove()
    //{
    //    foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
    //    {
    //        int number = Random.Range(1, 6);
    //        if (number == 4)
    //        {
    //            float x = Random.Range(-30, 30);
    //            float y = 1;
    //            float z = Random.Range(-50, 50);
    //            pin.transform.localPosition += new Vector3(x, y, z);
    //        }
    //    }
    //}
    ////Case16 
    //public void GiantBall()
    //{
    //    Ball ball = GameObject.FindObjectOfType<Ball>();
    //    ball.transform.localScale = new Vector3(40f, 40f, 40f);
    //}

    ////Case17 
    //public void SmallBall()
    //{
    //    Ball ball = GameObject.FindObjectOfType<Ball>();
    //    ball.transform.localScale = new Vector3(10f, 10f, 10f);
    //}
    ////Case18
    //public void Cheater()
    //{
    //    Ball ball = GameObject.FindObjectOfType<Ball>();
    //    ball.transform.localPosition += new Vector3(0, 0, 500f);
    //}
    ////Case19
    //public void AllPinsMove()
    //{
    //    foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
    //    {
    //        float x = Random.Range(-25, 25);
    //        float y = 1;
    //        float z = Random.Range(-40, 40);
    //        pin.transform.localPosition += new Vector3(x, y, z);
    //    }
    //}
    ////Case20 
    //public void TinyBall()
    //{
    //    Ball ball = GameObject.FindObjectOfType<Ball>();
    //    ball.transform.localScale = new Vector3(5f, 5f, 5f);
    //}
    ////Case21 
    //public void LargeBall()
    //{
    //    Ball ball = GameObject.FindObjectOfType<Ball>();
    //    ball.transform.localScale = new Vector3(30f, 30f, 30f);
    //}
    ////Case22 
    //public void LightBall()
    //{
    //    Ball ball = GameObject.FindObjectOfType<Ball>();
    //    Rigidbody body = ball.GetComponent<Rigidbody>();
    //    Renderer rend = ball.GetComponent<Renderer>();
    //    rend.material = ball2;
    //    body.mass = .5f;
    //}
    ////Case23 
    //public void HeavyBall()
    //{
    //    Ball ball = GameObject.FindObjectOfType<Ball>();
    //    Rigidbody body = ball.GetComponent<Rigidbody>();
    //    Renderer rend = ball.GetComponent<Renderer>();
    //    rend.material = ball3;
    //    body.mass = 70;
    //}
    ////Case24 - 26 Used multiple times since the pins already auto reset
    //public void ResetBall()
    //{
    //    Ball ball = GameObject.FindObjectOfType<Ball>();
    //    Rigidbody body = ball.GetComponent<Rigidbody>();
    //    Renderer rend = ball.GetComponent<Renderer>();
    //    SphereCollider col = ball.GetComponent<SphereCollider>();
    //    rend.material = ball1;
    //    body.mass = 8;
    //    ball.transform.localScale = new Vector3(21f, 21f, 21f);
    //    col.sharedMaterial = none;

    //}
    ////Case27
    //public void CarnyPin()
    //{
    //    foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
    //    {
    //        int number = Random.Range(1, 9);
    //        if (number == 8)
    //        {
    //            Rigidbody body = pin.GetComponent<Rigidbody>();
    //            body.isKinematic = true;
    //        }
    //    }
    //}

    ////Case28 Bumpers off again

    ////Case29 
    //public void Bumpers()
    //{
    //    Bumper.SetActive(true);
    //    Bumper2.SetActive(true);
    //}
    ////Case30 
    //public void BumpersOff()
    //{
    //    Bumper.SetActive(false);
    //    Bumper2.SetActive(false);
    //}
    ////Case31 
    //public void SardineRain()
    //{
    //    int rand = Random.Range(10, 200);
    //    for (int i = 0; i < rand; i++)
    //    {
    //        Instantiate(Sardine, new Vector3(Random.Range(55f, -55f), Random.Range(50f, 200f), Random.Range(150f, 2000f)), Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
    //    }
    //}
    ////Case32 
    //public void Obstical()
    //{
    //    Instantiate(Cylinder, new Vector3(Random.Range(55f, -55f), Random.Range(40f, -30f), Random.Range(300f, 1600f)), Quaternion.identity);
    //}
    ////Case33 
    //public void Obsticals()
    //{
    //    float z = Random.Range(400f, 1600f);
    //    float y = Random.Range(30f, -10f);
    //    Instantiate(Cylinder, new Vector3(40f, y, z), Quaternion.identity);
    //    Instantiate(Cylinder, new Vector3(-40f, y, z), Quaternion.identity);
    //}
    ////Case34 
    //public void RampAdd()
    //{
    //    Instantiate(Ramp, new Vector3(0, -3, Random.Range(300f, 1600f)), Quaternion.Euler(-10, 0, 0));
    //}
    ////Case35
    //public void SkyboxUnity()
    //{
    //    Skybox sky = mc.GetComponent<Skybox>();
    //    sky.material = SkyUnity;
    //    //light.SetActive(true);
    //    MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
    //    musicManager.ChangeVolume(volume);

    //}
    ////Case36 
    //public void Horror()
    //{
    //    Skybox sky = mc.GetComponent<Skybox>();
    //    MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
    //    musicManager.ChangeVolume(0f);
    //    sky.material = SkyHorror;
    //    //light.SetActive(false);
    //    AudioSource JumpScare = this.GetComponent<AudioSource>();
    //    JumpScare.Play();

    //}
    ////Case37 
    //public void SkyboxSpace()
    //{
    //    Skybox sky = mc.GetComponent<Skybox>();
    //    MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
    //    musicManager.ChangeVolume(volume);
    //    sky.material = SkySpace;
    //    //light.SetActive(true);
    //}

    ////Case40 add dummy pins X1
    //public void AddPinsx1()
    //{
    //    Instantiate(dumbPinSet, new Vector3(0, 1, 1285), Quaternion.identity);
    //}
    ////Case41 add dummy pins X2
    //public void AddPinsx2()
    //{
    //    Instantiate(dumbPinSet, new Vector3(0, 1, 1320), Quaternion.identity);
    //    Instantiate(dumbPinSet, new Vector3(0, 1, 1355), Quaternion.identity);
    //}
    ////Case42 add dummy pins x3
    //public void AddPinsx3()
    //{
    //    Instantiate(dumbPinSet, new Vector3(0, 1, 1390), Quaternion.identity);
    //    Instantiate(dumbPinSet, new Vector3(0, 1, 1425), Quaternion.identity);
    //    Instantiate(dumbPinSet, new Vector3(0, 1, 1460), Quaternion.identity);
    //}
    ////Case43 add dummy pins X4
    //public void AddPinsx4()
    //{
    //    Instantiate(dumbPinSet, new Vector3(0, 1, 1495), Quaternion.identity);
    //    Instantiate(dumbPinSet, new Vector3(0, 1, 1530), Quaternion.identity);
    //    Instantiate(dumbPinSet, new Vector3(0, 1, 1565), Quaternion.identity);
    //    Instantiate(dumbPinSet, new Vector3(0, 1, 1600), Quaternion.identity);
    //}
    ////Case44 - 45
    //public void GravityReset()
    //{
    //    gravityX = 0;
    //    gravityY = -150;
    //    gravityZ = 0;
    //    Physics.gravity = new Vector3(gravityX, gravityY, gravityZ);
    //}
    ////Cases 46-50 call effect multiple times

}

