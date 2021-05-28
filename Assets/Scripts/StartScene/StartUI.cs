using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public void OnClickStartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void OnClickHelp()
    {
        var text = "Press K -> stop bgm\n" +
                   "Pree L -> play bgm";
    }
}