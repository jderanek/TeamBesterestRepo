using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePortrait : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("BobFlag");
        animator.SetTrigger("MoveRightFlag");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
