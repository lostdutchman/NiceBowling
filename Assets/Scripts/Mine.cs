using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    private AudioSource Explosion;
    public float ExplosionRadius, ExplosionForce;
    private MeshRenderer[] Meshs;
    private BoxCollider Collider;
    private GameObject ExplosionEffect;
    public GameObject Laser, LaserUmbra; 

    // Use this for initialization
    void Start()
    {
        Meshs = GetComponentsInChildren<MeshRenderer>();
        Collider = GetComponent<BoxCollider>();
        Explosion = GetComponent<AudioSource>();
        ExplosionEffect = GameObject.Find("Explosion");
    }

    private void OnTriggerEnter(Collider col)
    {
        Vector3 HitLocation = transform.position;
        if (col.gameObject.name != "Ramp")
        {
            Collider[] Colliders = Physics.OverlapSphere(HitLocation, ExplosionRadius);
            Explosion.Play();

            //make object invisible and stop its movement
            foreach(MeshRenderer mesh in Meshs)
            {
                mesh.enabled = false;

            }
            Collider.enabled = false;
            Laser.SetActive(false);
            LaserUmbra.SetActive(false);

            //Play Explosion Effects
            ExplosionEffect.transform.position = HitLocation;
            ExplosionEffect.GetComponent<ParticleSystem>().Play();


            // For all balls within the sphere, apply a force.
            foreach (var HitObject in Colliders)
            {
                if (!HitObject)
                    continue;

                // This automatically scales the force depending on distnance from hit pt!
                try
                {
                    if (HitObject.gameObject.name == "sardineskinWithScript(Clone)")
                    {
                        HitObject.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce * 2, HitLocation, ExplosionRadius * 0.8f, 0.0f, ForceMode.VelocityChange);
                    }
                    else
                    {
                        HitObject.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, HitLocation, ExplosionRadius * 0.8f, 0.0f, ForceMode.VelocityChange);
                    }
                }
                catch
                {
                    //Do Nothing
                }
            }
        }
    }

}
