using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
    public string ctr_num;
    public void Update()
    {
        ChangetoScene("MainMenu");
    }

    public void ChangetoScene(string scene)
    {
        if (Input.GetButtonDown(ctr_num + " B BUTTON"))
            SceneManager.LoadScene(scene);
    }
}

