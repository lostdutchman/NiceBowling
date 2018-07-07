using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour {

    private Rigidbody Body;
    private SphereCollider Sphere;
    [Tooltip("Amount the radius of the Shpere collider grows with eache item attatched.")]
    public float GrowthRate = .5f; //mostly for pins
    private float Growth = 0f;

    private void Start()
    {
        Body = GetComponent<Rigidbody>();
        Sphere = GetComponent<SphereCollider>();
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
                    case "Town Windmill 01(Clone)": Growth = .375f; break;
                    case "P1roomba(Clone)": Growth = .1125f; break;
                    case "LandMine(Clone)": Growth = 0; break;
                    default: break;
                }
                collision.gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            catch
            {
                try
                {
                    Growth = collision.gameObject.GetComponent<SphereCollider>().radius / 2;
                    collision.gameObject.GetComponent<SphereCollider>().enabled = false;
                }
                catch
                {
                    try
                    {
                        Destroy(collision.gameObject.GetComponent<FixedJoint>()); //Barrels
                        Growth = collision.gameObject.GetComponent<CapsuleCollider>().radius / 2;
                        collision.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    }
                    catch
                    {
                        try
                        {
                            Growth = ((collision.gameObject.GetComponent<BoxCollider>().size.x + collision.gameObject.GetComponent<BoxCollider>().size.y + collision.gameObject.GetComponent<BoxCollider>().size.z) / 3) /13;
                            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
                        }
                        catch
                        {

                            try
                            {
                                Growth = .02f;
                                collision.gameObject.GetComponent<iwasiRigid>().enabled = false; // This is to stop the sardines from pushing the ball around.
                                Destroy(collision.gameObject.transform.GetChild(0).gameObject); //This is to kill the Sardine pone structure
                            }
                            catch
                            {

                                try
                                {
                                    Growth = collision.gameObject.GetComponent<CapsuleCollider>().radius / 2;
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
        }
    }
}
