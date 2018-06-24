using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpot : MonoBehaviour {

    public CircleCollider2D cir;
    public bool containsMonster;

    //public PlayerController selectedPlayer;

    public bool attacking;

    //public int damage;
    

	// Use this for initialization
	void Start () {
        cir = GetComponent<CircleCollider2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            containsMonster = true;
        }

    }

    public Vector3 getLocation()
    {
        return transform.position;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            containsMonster = false;
        }
    }

    public void Warning()
    {
        transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y * 2); //for now the warning will be just doubling in size. The warning is undone in the Attack function
    }

    public void Attack(int damage)
    {
        //Debug.Log("About to deal some damage : " + damage);
        if (containsMonster)
        {
            //selectedPlayer.HurtPlayer(damage);
            //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            containsMonster = true;
            //selectedPlayer = collision.gameObject.GetComponent<PlayerController>();
            //if(attacking)
            //{
            //    collision.gameObject.GetComponent<PlayerController>().HurtPlayer(damage);
            //    attacking = false;
            //}
        }

    }

    

    // Update is called once per frame
    void Update () {
        //Debug.Log("Does this contain a player : " + containsPlayer);
	}
}
