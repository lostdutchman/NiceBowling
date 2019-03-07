using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour {

    private Rigidbody Body;
    private SphereCollider Sphere;
    [Tooltip("Amount the radius of the Shpere collider grows with eache item attatched.")]
    public float GrowthRate = .075f; //mostly for pins
    private float Growth = 0f;
    private AudioSource SFX;

    private void Start()
    {
        Body = GetComponent<Rigidbody>();
        Sphere = GetComponent<SphereCollider>();
        SFX = GetComponent<AudioSource>();
        KillBadCollitions();
    }

    private void KillBadCollitions()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("NBTemp");
        foreach(GameObject obj in objects)
        {
            if(obj.name == "Cylinder(Clone)")
            {
                obj.GetComponent<CapsuleCollider>().isTrigger = true;
            }else if (obj.name == "pCube75")
            {
                obj.GetComponent<MeshCollider>().isTrigger = true;
            }else if (obj.name == "barrel")
            {
                obj.GetComponent<CapsuleCollider>().isTrigger = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Growth = GrowthRate;
        if (((collision.gameObject.tag == "Pin") || (collision.gameObject.tag == "NBTemp")) && (collision.gameObject.transform.parent != this.transform && collision.gameObject.name != "Ramp(Clone)"))
        {
            try
            {
                switch (collision.gameObject.GetComponent<MeshCollider>().name)
                {
                    case "P1roomba(Clone)": Growth = .15f; break;
                    case "LandMine(Clone)": Growth = 0; break;
                    default: break;
                }
                collision.gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            catch
            {
                try
                {
                    Growth = collision.gameObject.GetComponent<SphereCollider>().radius / 12;
                    collision.gameObject.GetComponent<SphereCollider>().enabled = false;
                }
                catch
                {
                    try
                    {
                        Destroy(collision.gameObject.GetComponent<FixedJoint>()); //Barrels
                        Growth = collision.gameObject.GetComponent<CapsuleCollider>().radius / 12;
                        collision.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    }
                    catch
                    {
                        try
                        {
                            Growth = ((collision.gameObject.GetComponent<BoxCollider>().size.x + collision.gameObject.GetComponent<BoxCollider>().size.y + collision.gameObject.GetComponent<BoxCollider>().size.z) / 3) /12;
                            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
                        }
                        catch
                        {

                            try
                            {
                                Growth = .03f;
                                collision.gameObject.GetComponent<iwasiRigid>().enabled = false; // This is to stop the sardines from pushing the ball around.
                                Destroy(collision.gameObject.transform.GetChild(0).gameObject); //This is to kill the Sardine pone structure
                            }
                            catch
                            {

                                try
                                {
                                    Growth = collision.gameObject.GetComponent<CapsuleCollider>().radius / 12;
                                    collision.gameObject.GetComponent<CapsuleCollider>().enabled = false; //in case any non-barrel capsle colliders get added
                                }
                                catch
                                {

                                    //If something still braks check for more colliders
                                }
                            }
                        }
                    }
                }
            }
            try
            {
                Body.mass += collision.rigidbody.mass;
                Destroy(collision.rigidbody);
            }
            catch
            {
                Body.mass += 5; //add one to mass if object has no rigid body.
            }

            collision.gameObject.transform.parent = transform;
            collision.gameObject.tag = "WasStuck";
            Sphere.radius += Growth;
            SFX.Play();
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        Growth = GrowthRate;
        if (((collider.gameObject.tag == "Pin") || (collider.gameObject.tag == "NBTemp")) && (collider.gameObject.transform.parent != this.transform && collider.gameObject.name != "Ramp(Clone)"))
        {
            try
            {
                if (collider.gameObject.GetComponent<MeshCollider>().name == "pCube75")
                    Growth = .6f;
                collider.gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            catch
            {
                try
                {
                    Growth = collider.gameObject.GetComponent<SphereCollider>().radius / 12;
                    collider.gameObject.GetComponent<SphereCollider>().enabled = false;
                }
                catch
                {
                    try
                    {
                        Destroy(collider.gameObject.GetComponent<FixedJoint>()); //Barrels
                        Growth = collider.gameObject.GetComponent<CapsuleCollider>().radius / 12;
                        collider.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    }
                    catch
                    {
                        try
                        {
                            Growth = ((collider.gameObject.GetComponent<BoxCollider>().size.x + collider.gameObject.GetComponent<BoxCollider>().size.y + collider.gameObject.GetComponent<BoxCollider>().size.z) / 3) / 12;
                            collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                        }
                        catch
                        {

                            try
                            {
                                Growth = .03f;
                                collider.gameObject.GetComponent<iwasiRigid>().enabled = false; // This is to stop the sardines from pushing the ball around.
                                Destroy(collider.gameObject.transform.GetChild(0).gameObject); //This is to kill the Sardine pone structure
                            }
                            catch
                            {

                                try
                                {
                                    Growth = collider.gameObject.GetComponent<CapsuleCollider>().radius / 12;
                                    collider.gameObject.GetComponent<CapsuleCollider>().enabled = false; //in case any non-barrel capsle colliders get added
                                }
                                catch
                                {

                                    //If something still braks check for more colliders
                                }
                            }
                        }
                    }
                }
            }
            try
            {
                Rigidbody colBody = collider.GetComponent<Rigidbody>();
                Body.mass += colBody.mass;
                Destroy(colBody);
            }
            catch
            {
                Body.mass += 5; //add one to mass if object has no rigid body.
            }

            collider.gameObject.transform.parent = transform;
            collider.gameObject.tag = "WasStuck";
            Sphere.radius += Growth;
            SFX.Play();
        }

    }
}
