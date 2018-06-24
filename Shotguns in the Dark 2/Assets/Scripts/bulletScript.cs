using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public float lifetime = 5f;
    public Rigidbody2D Rb;
    public float speed;
    public Vector2 velocity;
    public int damage = 10;
    public GameObject bloodSplatter;
    public GameObject alienBloodSplatter;

	// Use this for initialization
	private void Start () {
        Destroy(gameObject,lifetime);
        Rb = GetComponent<Rigidbody2D>();
        // find the rotation of the object by its rotation.z value
        velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z));
        
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        Rb.MovePosition(Rb.position + velocity * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Monster")
        {
            collision.gameObject.GetComponent<S_P_Cont>().currentHealth -= damage;
            if (collision.gameObject.tag == "Player")
            {
                Instantiate(bloodSplatter, transform.position, transform.rotation);
            }else {
                Instantiate(alienBloodSplatter, transform.position, transform.rotation);
            }
            
           
        }
        if(collision.gameObject.tag == "DestructableWall")
        {
            collision.gameObject.GetComponent<DestructableWall>().currenthealth -= damage;
        }
        Destroy(gameObject);

    }
}