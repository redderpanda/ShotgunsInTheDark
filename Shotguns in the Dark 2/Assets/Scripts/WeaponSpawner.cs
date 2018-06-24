using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour {
    public GameObject[] weaponOptions;
    public bool objectSpawned;

	// Use this for initialization
	void Start () {
        objectSpawned = false;
        InvokeRepeating("PlaceWeapon", 0, 30f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaceWeapon()
    {
        if(objectSpawned == false)
        {
            int random_weapon = Random.Range(0, weaponOptions.Length);
            Instantiate(weaponOptions[random_weapon], transform.position, Quaternion.identity);
            objectSpawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            objectSpawned = false;
        }
    }
}
