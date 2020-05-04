using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car_Colliision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("t");
            StartCoroutine(GameOver());
        }
    }
    IEnumerator GameOver()
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene().buildIndex==6)
        {
            SceneManager.LoadScene(curScene);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            SceneManager.LoadScene(curScene);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            SceneManager.LoadScene(curScene);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            SceneManager.LoadScene(curScene);
        }
        yield return new WaitForSeconds(0.01f);

    }
}
