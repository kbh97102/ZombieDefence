using System;
using System.Collections;
using UnityEngine;

public class ZombieCharacterControl : MonoBehaviour
{
    private enum ControlMode
    {
        Tank,
        Direct,
        Auto
    }

    #region Public Field

    [SerializeField] private float m_moveSpeed = 5;
    [SerializeField] private float m_turnSpeed = 200;

    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Auto;
    [SerializeField] private ParticleSystem particleSystem;
    
    #endregion


    private GameObject target;
    private float m_currentV = 0;
    private float m_currentH = 0;
    private readonly float m_interpolation = 10;
    private Vector3 m_currentDirection = Vector3.zero;
    private int hp;
    private bool isAlive;
    private GameManager gameManager;
    private ZombieSoundController zombieSoundController;

    private void Awake()
    {
        zombieSoundController = new ZombieSoundController(GetComponent<AudioSource>());
        isAlive = true;
        hp = 3;
        if (!m_animator)
        {
            gameObject.GetComponent<Animator>();
        }

        if (!m_rigidBody)
        {
            gameObject.GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            AutoUpdate();
        }
    }

    private void AutoUpdate()
    {
        var targetPosition = target.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f * Time.deltaTime);
        Transform camera = Camera.main.transform;
        if (camera == null)
        {
            return;
        }

        m_currentV = Mathf.Lerp(m_currentV, targetPosition.z, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, targetPosition.x, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;


        if (direction != Vector3.zero)
        {
            // m_currentDirection = Vector3.Slerp(m_currentDirection, direction,  m_interpolation);
            //
            // transform.rotation = Quaternion.LookRotation(m_currentDirection);
            //
            // Debug.Log("rotation "+transform.rotation);

            Vector3 lookAt = target.transform.position;
            lookAt.y = transform.position.y;
            transform.LookAt(lookAt);

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
            zombieSoundController.PlaySound(ZombieSoundController.ZombieSounds.Idle);
        }
        // else
        // {
        //     m_animator.SetFloat("MoveSpeed", Time.deltaTime * m_moveSpeed);
        // }
    }

    private void TankUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Attacked();
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Core") && target.name == "ForestCastle_Red")
        {
            m_animator.SetBool("Attack", true);
            Destroy(gameObject);
            zombieSoundController.PlaySound(ZombieSoundController.ZombieSounds.Attack);
        }

        if (other.gameObject.CompareTag("Player") && target.name == "unitychan")
        {
            m_animator.SetBool("Attack", true);
            zombieSoundController.PlaySound(ZombieSoundController.ZombieSounds.Attack);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Core") && target.name == "ForestCastle_Red")
        {
            m_animator.SetBool("Attack", false);
        }

        if (other.gameObject.CompareTag("Player") && target.name == "unitychan")
        {
            m_animator.SetBool("Attack", false);
        }
    }

    private void Attacked()
    {
        particleSystem.Play();
        hp -= 1;
        if (hp <= 0)
        {
            gameManager.ReduceZombieCount();
            m_animator.SetTrigger("Dead");
            isAlive = false;
            StartCoroutine("Died");
            zombieSoundController.PlaySound(ZombieSoundController.ZombieSounds.Death);
        }
        else
        {
            zombieSoundController.PlaySound(ZombieSoundController.ZombieSounds.Attacked);
        }
    }

    private IEnumerator Died()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}