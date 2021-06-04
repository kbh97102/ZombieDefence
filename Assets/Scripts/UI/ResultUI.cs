using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Text remainBullet;
    [SerializeField] private Text coreHP;
    [SerializeField] private Text playerHP;
    [SerializeField] private Text lastWave;
    [SerializeField] private Text remainZombies;

    private PhotonView photonView;


    public void SetPhotonView(PhotonView photonView)
    {
        this.photonView = photonView;
    }
    
    #region Button CallBacks

    public void OnClickRetry()
    {
        this.gameObject.SetActive(false);
        gameManager.StartGame();
    }

    public void OnClickBackToLobby()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        gameObject.SetActive(false);
        PhotonNetwork.LoadLevel("Lobby");
        PhotonNetwork.LeaveRoom();
    }    
    
    #endregion

    public void SetResult(string remainBullet, string coreHP, string playerHP, string lastWave, string remainZombies)
    {
        this.remainBullet.text = remainBullet;
        this.coreHP.text = coreHP;
        this.playerHP.text = playerHP;
        this.lastWave.text = lastWave;
        this.remainZombies.text = remainZombies;
    }
}
