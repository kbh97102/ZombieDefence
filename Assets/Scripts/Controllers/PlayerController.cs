using System;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Fields

    public float m_moveSpeed = 2.0f;
    public Camera mainCamera;

    public HPUI hpUI;
    
    #endregion

    #region Private Fields

    private Animator _animator;
    private int hp;
    private PhotonView photonView;

    #endregion

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        hp = 10;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        PlayerMove();
        // ChasingCursor();
        PlayerArrowKey();
    }

    private void PlayerArrowKey()
    {
        var selfRotation = transform.rotation;
        if (Input.GetKey (KeyCode.UpArrow)) {
            selfRotation = Quaternion.Euler (0, 0, 0);
        }
        if (Input.GetKey (KeyCode.DownArrow)) {
            selfRotation = Quaternion.Euler (0, 180, 0);
        }
        if (Input.GetKey (KeyCode.LeftArrow)) {
            selfRotation = Quaternion.Euler (0, 270, 0);
        }
        if (Input.GetKey (KeyCode.RightArrow)) {
            selfRotation = Quaternion.Euler (0, 90, 0);
        }
        if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.LeftArrow)) {
            selfRotation = Quaternion.Euler (0, 315, 0);
        }
        if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.RightArrow)) {
            selfRotation = Quaternion.Euler (0, 45, 0);
        }
        if (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.LeftArrow)) {
            selfRotation = Quaternion.Euler (0, 225, 0);
        }
        if (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.RightArrow)) {
            selfRotation = Quaternion.Euler (0, 135, 0);
        }
        
        transform.rotation = Quaternion.RotateTowards (transform.rotation, selfRotation, 1000 * Time.deltaTime);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Attacked();
        }
    }

    private void Attacked()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        hp -= 1;
        hpUI.UpdateHP(hp);
    }

    public int GetHP()
    {
        return hp;
    }

    public void ResetHP()
    {
        hp = 1;
    }
}