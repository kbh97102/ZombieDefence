using System;
using System.Collections;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    public Camera mainCamera;
    public AmmoController ammoController;
    
    
    private Animator _animator;

    private bool isReloading;

    private void Start()
    {
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

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            return cameraRay.GetPoint(rayLength);
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
        if (Input.GetMouseButtonDown(0))
        {
            ammoController.Fire();
            _animator.SetTrigger("Fire");
            var bulletObject = Instantiate(bullet, bulletSpawnPoint.transform);
            bulletObject.GetComponent<BulletController>().shooting(GetCursorPosition());
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("test");
            isReloading = true;
            _animator.SetTrigger("Reload");
            ammoController.reload();
        }
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(2.5f);
        isReloading = false;
    }
}