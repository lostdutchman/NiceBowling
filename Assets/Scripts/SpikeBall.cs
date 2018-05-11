using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour {

    private Rigidbody Body;
    private SphereCollider Sphere;
    [Tooltip("Amount the radius of the Shpere collider grows with eache item attatched.")]
    public float GrowthRate = .5f;

    private void Start()
    {
        Body = GetComponent<Rigidbody>();
        Sphere = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((collision.gameObject.tag == "Pin") || (collision.gameObject.tag == "NBTemp")) && (collision.gameObject.transform.parent != this.transform))
        {
            try
            {
                Body.mass += collision.rigidbody.mass;
                Destroy(collision.rigidbody);
            }
            catch
            {
                Body.mass += 5; //add one to mass if object has no rigid body.
            }
            try
            {
                collision.gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            catch
            {
                try
                {
                    collision.gameObject.GetComponent<SphereCollider>().enabled = false;
                }
                catch
                {
                    try
                    {
                        collision.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    }
                    catch
                    {
                        try
                        {
                            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
                        }
                        catch
                        {

                            try
                            {
                                collision.gameObject.GetComponent<iwasiRigid>().enabled = false; // This is to stop the sardines from pushing the ball around.
                                Destroy(collision.gameObject.transform.GetChild(0).gameObject); //This is to kill the Sardine pone structure
                            }
                            catch
                            {

                                //If something still braks check for more colliders
                            }
                        }
                    }
                }
            }

            collision.gameObject.transform.parent = transform;
            collision.gameObject.tag = "WasStuck";
            Sphere.radius += GrowthRate;
        }
    }
}
