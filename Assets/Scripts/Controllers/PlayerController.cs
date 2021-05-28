using System;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public Fields

    public float m_moveSpeed = 2.0f;
    public Camera mainCamera;

    #endregion

    #region Private Fields

    private Animator _animator;
    private int hp;

    #endregion


    private void Start()
    {
        hp = 10;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMove();
        ChasingCursor();
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Attacked();
        }
    }

    private void Attacked()
    {
        hp -= 1;
    }

    public int GetHP()
    {
        return hp;
    }
}