using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    #region Button CallBacks

    public void OnClickRetry()
    {
        Debug.Log("?");
        this.gameObject.SetActive(false);
        gameManager.StartGame();
    }

    public void OnClickBackToLobby()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }    
    
    #endregion
   
    
}
