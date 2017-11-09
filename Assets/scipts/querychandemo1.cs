using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class querychandemo1 : MonoBehaviour {
    private Animator animator;
    private bool Active;//updateの有効化on/off
                        // Use this for initialization
    void Start () {
        Active = true;
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Active == true && Input.GetKey("up"))
        {
            transform.position += transform.forward * 0.08f;
            animator.SetBool("isrunning", true);
        }
        else
        {
            animator.SetBool("isrunning", false);
        }
        if (Active == true && Input.GetKey("right"))
        {
            transform.Rotate(0, 2, 0);
        }
        if (Active == true && Input.GetKey("left"))
        {
            transform.Rotate(0, -2, 0);
        }
    }
}
