using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    public void Press(string name)
    {
        Pause._paused = false;
        SceneManager.LoadScene(name);
    }
}
