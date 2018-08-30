using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;

    public AudioClip[] myMusic;
    private AudioSource audioSource;
    public float MenuTimeDelay;

    // Use this for initialization
    void Start () {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad >= MenuTimeDelay)
        {
            if (!audioSource.isPlaying)
                playRandomMusic();
        }
    }

    public void playRandomMusic()
    {
        audioSource.clip = myMusic[Random.Range(0, myMusic.Length)];
        audioSource.Play();
    }
}
