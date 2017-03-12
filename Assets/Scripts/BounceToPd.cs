using UnityEngine;
using System.Collections;

using LibPDBinding;

public class BounceToPd : MonoBehaviour {
	Rigidbody rb;
	private float lastVelocity;
	private float thisVelocity;
	private bool bounceMoment;


    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody>();
		lastVelocity = 0;
		bounceMoment = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 v3Velocity = rb.velocity; 
		thisVelocity = v3Velocity.y;

		if (thisVelocity - lastVelocity > 0) {
			bounceMoment = true;
		} else {
			bounceMoment = false;
		}
			


		if (bounceMoment) {
            int pitch = GetPitch();
			LibPD.SendFloat ("bounce", pitch);
			bounceMoment = false;
			lastVelocity = -1;

		}
		lastVelocity = thisVelocity;

		float sphereY = GameObject.Find("Sphere").transform.position.y;
		LibPD.SendFloat("pitchOffset", sphereY);


	}

    private int[] scale = { 0, 2, 4, 5, 7, 9, 11 };
    private int root = 60;

    int GetPitch () {
        int i = Random.Range(0, scale.Length);
        return scale[i] + root;
    }
}



