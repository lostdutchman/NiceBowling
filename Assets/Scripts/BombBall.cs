using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBall : MonoBehaviour {

    private AudioSource Explosion;
    public float ExplosionRadius, ExplosionForce;
    private MeshRenderer Mesh;
    private Rigidbody Body;
    private SphereCollider Collider;
    public float RestoreTime = 1f;
    private ParticleSystem Fuse;
    private GameObject ExplosionEffect;

	// Use this for initialization
	void Start () {
        Mesh = GetComponent<MeshRenderer>();
        Collider = GetComponent<SphereCollider>();
        Explosion = GetComponent<AudioSource>();
        Fuse = GetComponentInChildren<ParticleSystem>();
        ExplosionEffect = GameObject.Find("Explosion");
        transform.rotation = Quaternion.Euler(new Vector3(265f, 45f, 0f));
    }

    private void OnCollisionEnter(Collision col)
    {
        Vector3 HitLocation = transform.position;
        if (col.gameObject.tag == "Pin")
        {
            Collider[] Colliders = Physics.OverlapSphere(HitLocation, ExplosionRadius);
            Explosion.Play();

            //make object invisible and stop its movement
            Fuse.Stop();
            Mesh.enabled = false;
            Collider.enabled = false;

            //Play Explosion Effects
            ExplosionEffect.transform.position = HitLocation;
            ExplosionEffect.GetComponent<ParticleSystem>().Play();
            

            // For all balls within the sphere, apply a force.
            foreach (var HitObject in Colliders)
            {
                if (!HitObject)
                    continue;

                if (HitObject.transform.tag == "Pin" || HitObject.transform.tag == "NBTemp")
                {    // This automatically scales the force depending on distnance from hit pt!
                    HitObject.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, HitLocation, ExplosionRadius * 0.8f, 0.0f, ForceMode.VelocityChange);
                }
            }
            StartCoroutine(RestoreBall());
        }
    }

    private void Update()
    {
        if((transform.position.z > 1900) || (transform.position.y < -10))
        {
            Fuse.Stop();
            StartCoroutine(RestoreBallAfterMiss());
        }
    }

    private IEnumerator RestoreBall()
    {
        yield return new WaitForSecondsRealtime(RestoreTime);
        Fuse.Play();
        Mesh.enabled = true;
        Collider.enabled = true;
    }

    private IEnumerator RestoreBallAfterMiss()
    {
        yield return new WaitForSecondsRealtime(8f);
        Fuse.Play();
    }

}
