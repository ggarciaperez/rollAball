using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // This will reload the scene
    public void RestartGame(){
        SceneManager.LoadScene("MiniGame");
    }

}
