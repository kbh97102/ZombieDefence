using System.Collections.Generic;
using UnityEngine;

public class PanelSwitch : MonoBehaviour
{
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject createPanel;
    [SerializeField] private GameObject roomPanel;

    private Dictionary<string, GameObject> panelMap;

    public const string LOBBY = "lobby";
    public const string CREATE = "create";
    public const string ROOM = "room";

    private void Awake()
    {
        panelMap = new Dictionary<string, GameObject>();
        panelMap.Add(LOBBY, lobbyPanel);
        panelMap.Add(CREATE, createPanel);
        panelMap.Add(ROOM, roomPanel);
    }

    public void UnActivePanels(string[] panels)
    {
        foreach (string panel in panels)
        {
            panelMap[panel].SetActive(false);
        }
    }

    public void ActivePanel(string panel)
    {
        panelMap[panel].SetActive(true);
    }
}