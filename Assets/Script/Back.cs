using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    public void BackMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
