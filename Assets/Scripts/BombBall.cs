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
    private ParticleSystem ExplosionEffect;

	// Use this for initialization
	void Start () {
        Mesh = GetComponent<MeshRenderer>();
        Body = GetComponent<Rigidbody>();
        Collider = GetComponent<SphereCollider>();
        Explosion = GetComponent<AudioSource>();
        ExplosionEffect = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision col)
    {
        Vector3 HitLocation = transform.position;
        if (col.gameObject.tag == "Pin")
        {
            Collider[] Colliders = Physics.OverlapSphere(HitLocation, ExplosionRadius);

            Explosion.Play();
            //make object invisible and stop its movement
            Mesh.enabled = false;
            Collider.enabled = false;
            Body.velocity = (new Vector3(0, 0, 0));
            Body.isKinematic = true;
            ExplosionEffect.Play();

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

    private IEnumerator RestoreBall()
    {
        yield return new WaitForSecondsRealtime(RestoreTime);
        Body.isKinematic = false;
        ExplosionEffect.Stop();
        Mesh.enabled = true;
        Collider.enabled = true;
        
    }
}
