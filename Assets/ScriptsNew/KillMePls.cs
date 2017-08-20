using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMePls : MonoBehaviour {

	void Start ()
    {
        Invoke("Die", 3);	
	}
	
	void Die () {
        Destroy(this.gameObject);
	}
}
