using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoom : MonoBehaviour
{
    [SerializeField] private GameObject createPanel;
    [SerializeField] private InputField nameInput;
    [SerializeField] private Room room;
    
    private void Start()
    {
        createPanel.gameObject.SetActive(false);
        nameInput.text = "";
    }

    public void CreateButtonClicked()
    {
        var roomName = "Default RoomName";

        byte maxPlayer;
        byte.TryParse("2", out maxPlayer);

        RoomOptions options = new RoomOptions {MaxPlayers = maxPlayer, PlayerTtl = 10000, IsVisible = true};
        PhotonNetwork.CreateRoom(roomName, options, null);
        
        UnActive();
        room.Active();
    }

    public void UnActive()
    {
        nameInput.text = "";
        createPanel.gameObject.SetActive(false);
    }

    public void Active()
    {
        createPanel.gameObject.SetActive(true);
    }
}