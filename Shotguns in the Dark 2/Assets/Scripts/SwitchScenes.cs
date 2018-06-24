using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public string ctr_num;

    public void Update()
    {
        ChangetoScene("FIRST");
        HowtoPlay("HOWTOPLAY");
    }

    public void ChangetoScene(string scene)
    {
        if (Input.GetButtonDown(ctr_num + " A BUTTON"))
        {
          //  Debug.Log("Pressed");
            SceneManager.LoadScene(scene);
        }
    }
    public void HowtoPlay(string scene)
    {
        if (Input.GetButtonDown(ctr_num + " X BUTTON"))
        {
         //   Debug.Log("Pressedx");
            SceneManager.LoadScene(scene);
        }
    }
}


