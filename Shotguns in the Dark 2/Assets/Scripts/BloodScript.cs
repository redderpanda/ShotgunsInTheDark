using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScript : MonoBehaviour {
    public float uptime = 5f;


	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyAfter());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(uptime);
        Destroy(gameObject);
        Debug.Log("Blood Gone");
    }
}
