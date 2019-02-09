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
        Body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision col)
    {
        Vector3 HitLocation = transform.position;
        if (col.gameObject.tag == "Pin" || col.gameObject.tag == "NBTemp")
            if(col.gameObject.name != "Ramp(Clone)" && col.gameObject.name != "sardineskinWithScript(Clone)")
            {
                {
                    Collider[] Colliders = Physics.OverlapSphere(HitLocation, ExplosionRadius);
                    Explosion.Play();

                    //make object invisible and stop its movement
                    Fuse.Stop();
                    Mesh.enabled = false;
                    Collider.enabled = false;
                    Body.isKinematic = true;

                    //Play Explosion Effects
                    ExplosionEffect.transform.position = HitLocation;
                    ExplosionEffect.GetComponent<ParticleSystem>().Play();

                    // For all objects within the sphere, apply a force.
                    foreach (var HitObject in Colliders)
                    {
                        if (!HitObject)
                            continue;

                        if(HitObject.gameObject.name == "sardineskinWithScript(Clone)")
                        {
                            HitObject.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce * 2, HitLocation, ExplosionRadius * 0.8f, 0.0f, ForceMode.VelocityChange);
                        }
                        else if (HitObject.transform.tag == "Pin" || HitObject.transform.tag == "NBTemp")
                        {    // This automatically scales the force depending on distnance from hit pt!
                            try
                            {
                                HitObject.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, HitLocation, ExplosionRadius * 0.8f, 0.0f, ForceMode.VelocityChange);
                            }
                            catch
                            {
                                //Cand apply force to this object.
                            }
                        }
                    }
                    StartCoroutine(RestoreBall());
                }
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
        Collider.enabled = true;
    }

    private IEnumerator RestoreBallAfterMiss()
    {
        yield return new WaitForSecondsRealtime(8f);
        Fuse.Play();
    }

}
