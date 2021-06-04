using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoom : MonoBehaviour
{
    [SerializeField] private PanelSwitch panelSwitch;
    [SerializeField] private InputField nameInput;
    [SerializeField] private Room room;
    
    private void Start()
    {
        panelSwitch.UnActivePanels(new string[]{PanelSwitch.CREATE});
        nameInput.text = "";
    }

    public void CreateButtonClicked()
    {
        var roomName = "Default RoomName";

        byte maxPlayer;
        byte.TryParse("2", out maxPlayer);

        RoomOptions options = new RoomOptions {MaxPlayers = maxPlayer, PlayerTtl = 10000, IsVisible = true};
        PhotonNetwork.CreateRoom(roomName, options, null);

        nameInput.text = "";
        panelSwitch.UnActivePanels(new []{PanelSwitch.CREATE});
        panelSwitch.ActivePanel(PanelSwitch.ROOM);
    }

    public void CancelButtonClicked()
    {
        nameInput.text = "";
        panelSwitch.UnActivePanels(new []{PanelSwitch.CREATE});
        panelSwitch.ActivePanel(PanelSwitch.LOBBY);
    }

    public void Active()
    {
        panelSwitch.UnActivePanels(new []{PanelSwitch.LOBBY});
        panelSwitch.ActivePanel(PanelSwitch.CREATE);
    }
}