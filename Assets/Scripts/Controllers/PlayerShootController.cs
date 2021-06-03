using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerShootController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    public Camera mainCamera;
    public AmmoController ammoController;

    private Animator _animator;

    private bool isReloading;

    private FireSoundController soundController;

    private float fireDelta = 0f;
    private float fireTime = 0.4f;
    
    private void Start()
    {
        soundController = new FireSoundController(GetComponent<AudioSource>());
        isReloading = false;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Fire();
        Reload();
    }

    private Vector3 GetCursorPosition()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        Debug.DrawLine(transform.position, cameraRay.direction);

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            var test = cameraRay.GetPoint(rayLength);
            test.x = bulletSpawnPoint.transform.position.x;
            test.y = bulletSpawnPoint.transform.position.y;
            return test;
        }

        return Vector3.zero;
    }

    private void Fire()
    {
        if (isReloading)
        {
            return;
        }

        if (!ammoController.CanFire())
        {
            return;
        }

        if (fireDelta < fireTime)
        {
            fireDelta += Time.deltaTime;
            return;
        }
        else
        {
            fireDelta = 0;
        }
        
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            soundController.PlaySound("fire");
            ammoController.Fire();
            _animator.SetTrigger("Fire");
            var bulletObject = PhotonNetwork.Instantiate("bullet", bulletSpawnPoint.transform.position,
                bulletSpawnPoint.transform.rotation);


            bulletObject.GetComponent<Rigidbody>().AddForce(bulletObject.transform.forward * 3000);
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            soundController.PlaySound("reload");
            StartCoroutine("ReloadFinish");
            isReloading = true;
            _animator.SetTrigger("Reload");
            ammoController.reload();
        }
    }

    IEnumerator ReloadFinish()
    {
        yield return new WaitForSeconds(2.5f);
        isReloading = false;
    }

    public void ResetAmmo()
    {
        ammoController.ResetAmmo();
    }

    public int GetAmmo()
    {
        return ammoController.GetAllAmmo();
    }
}