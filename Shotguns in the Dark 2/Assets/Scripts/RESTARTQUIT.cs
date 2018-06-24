using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RESTARTQUIT : MonoBehaviour {
    public string ctr_num;
    public void Update()
    {
        Restart("FIRST");
        Quit();
    }

    public void Restart(string scene)
    {
        if (Input.GetButtonDown(ctr_num + " A BUTTON"))
            SceneManager.LoadScene(scene);

    }
    public void Quit()
    {
        if (Input.GetButtonDown(ctr_num + " B BUTTON"))
            Application.Quit();
    }
}
