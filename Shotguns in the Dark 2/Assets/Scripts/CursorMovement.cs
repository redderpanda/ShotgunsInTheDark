using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    public string ctr_num;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(-Input.GetAxis(ctr_num + " RIGHT JOYSTICK VERTICAL"), Input.GetAxis(ctr_num + " RIGHT JOYSTICK HORIZONTAL")));
    }
}
