using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningBall : MonoBehaviour {

	private Rigidbody rigidBody;
	public AudioSource canvas;
	public float launchSpeed;
	private UIFade menu;
	private float MenuTimeDelay;
    public GameObject Splash, logoE, logoJ;
    private bool SplashExists = true;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		menu = GameObject.FindObjectOfType<UIFade>();
        float yPos = rigidBody.transform.position.y;
		float zPos = rigidBody.transform.position.z;
		rigidBody.transform.position = new Vector3 (Random.Range(-10, 10), yPos, zPos);
		ThrowBall ();
        MenuTimeDelay = GameObject.FindObjectOfType<MusicPlayer>().MenuTimeDelay + .5f; //.5f for the splash screen fade time
        try {
            Splash = GameObject.FindObjectOfType<PanelFade>().gameObject;
        }
        catch
        {
            SplashExists = false; 
        }
        if (Random.Range(0, 6) == 5)
        {
            logoE.SetActive(false);
            logoJ.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update () {
        if (SplashExists)
        {
            if (Time.timeSinceLevelLoad >= MenuTimeDelay && Splash.activeSelf)
            {
                StartCoroutine(menu.FadeIn());
                Splash.SetActive(false);
            }
        }
	}
	
	public void ThrowBall ()
	{
		rigidBody.useGravity = true;
		rigidBody.velocity = new Vector3 (0, 0, launchSpeed);
		rigidBody.angularVelocity = new Vector3 (20, 0, 0);
	}
}
