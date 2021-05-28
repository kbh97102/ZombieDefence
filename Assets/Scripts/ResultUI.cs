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
        
    }

    public void OnClickBackToLobby()
    {
        SceneManager.LoadScene("StartScene");
    }    
    
    #endregion
   
    
}
