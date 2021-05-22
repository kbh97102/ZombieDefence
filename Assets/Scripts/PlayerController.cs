using System;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Fields

    public float m_moveSpeed = 2.0f;
    public Camera mainCamera;
    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    
    #endregion

    #region Private Fields

    private Animator _animator;

    #endregion
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMove();
        ChasingCursor();
        Fire();
    }

    void PlayerMove()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = Vector3.right * h;
        Vector3 moveVertical = Vector3.forward * v;
        Vector3 velocity = (moveHorizontal + moveVertical).normalized;

        transform.Translate(velocity * m_moveSpeed * Time.deltaTime, Space.World);
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetBool("Fire", true);
            Instantiate(bullet, bulletSpawnPoint.transform);
        }
    }
    
    private void ChasingCursor()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
 
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}