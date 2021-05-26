using UnityEngine;
using UnityEngine.UI;

public class AmmoController : MonoBehaviour
{
    public Text currentAmmo;
    public Text totalAmmo;

    private int currentMagazine;
    private int remainMagazine;
    private int oneMagazine;

    private void Start()
    {
        oneMagazine = 29;
        currentMagazine = oneMagazine;
        remainMagazine = 350;
        UpdateAmmo();
    }

    public bool CanFire()
    {
        return currentMagazine > 0;
    }

    public void Fire()
    {
        currentMagazine -= 1;
        UpdateAmmo();
    }
    
    public void reload()
    {
        if (remainMagazine <= 0)
        {
            return;
        }

        remainMagazine += currentMagazine;
        if (remainMagazine < oneMagazine)
        {
            currentMagazine = remainMagazine;
            remainMagazine = 0;
        }
        else
        {
            remainMagazine -= oneMagazine;
            currentMagazine = oneMagazine;
        }
        
        UpdateAmmo();
    }

    public void UpdateAmmo()
    {
        currentAmmo.text = currentMagazine.ToString();
        totalAmmo.text = remainMagazine.ToString();
    }
}