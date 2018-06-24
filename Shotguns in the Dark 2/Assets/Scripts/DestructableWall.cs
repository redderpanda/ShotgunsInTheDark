using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour {
    public int currenthealth;
    public int maxhealth = 30;
    public Sprite[] wallsprites;
	// Use this for initialization
	void Start () {
        currenthealth = maxhealth;
	}

    private void Update()
    {
        Changing_sprite();
    }

    private void Changing_sprite()
    {
        if(currenthealth == maxhealth)
            GetComponent<SpriteRenderer>().sprite = wallsprites[0];
        if(currenthealth <= 20)
            GetComponent<SpriteRenderer>().sprite = wallsprites[1];
        if(currenthealth <= 10)
            GetComponent<SpriteRenderer>().sprite = wallsprites[2];
        if (currenthealth <= 0)
            Destroy(this.gameObject);
    }
}
