using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0.5f;

    private float m_horizontal;
    private float m_vertical;
    private Rigidbody m_rigidbody;
    private bool isEnabled = true;
    private Animator m_animator;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (isEnabled)
        {
            m_horizontal = Input.GetAxis("Horizontal");
            m_vertical = Input.GetAxis("Vertical");
        }
        else
        {
            m_horizontal = 0;
            m_vertical = 0;
        }
        m_animator.SetFloat("movement", Mathf.Abs(m_horizontal) + Mathf.Abs(m_vertical));

        Vector3 lookDirection = new Vector3(m_vertical, 0, -m_horizontal) * 6 + transform.position;

        if(Mathf.Abs(m_horizontal) > 0.1f || Mathf.Abs(m_vertical) > 0.1f)
            transform.DOLookAt(lookDirection, 0.1f);

    }

    private void FixedUpdate()
    {
        //Continus Movement
        Vector3 movementVector = new Vector3(m_vertical * Time.fixedDeltaTime, 0, -m_horizontal * Time.fixedDeltaTime);
        movementVector.Normalize();
        movementVector *= movementSpeed;
        movementVector.y = m_rigidbody.velocity.y;
        m_rigidbody.velocity = movementVector;
        // if(0.1f < Mathf.Abs(m_horizontal) )
        //     m_rigidbody.velocity = new Vector3(m_horizontal * movementSpeed, 0, 0);

        // if (0.1f < Mathf.Abs(m_vertical))
        //     m_rigidbody.velocity = new Vector3(0, 0, m_vertical * movementSpeed);

        //Hop Movement
        // if (Mathf.Abs(m_horizontal) > 0.1f && !isHoping)
        // {
        //     RaycastHit hit;

        //     //Prevents from jumping into wall
        //     if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.3f))
        //     {
        //         //Jump in place
        //         isHoping = true;
        //         m_rigidbody.DOJump(transform.position, hopPower, 1, hopTime).OnComplete(FinishedHoping);
        //     }
        //     else
        //     {
        //         isHoping = true;
        //         Vector3 destination = transform.position + new Vector3(m_horizontal, 0, 0) * movementSpeed;
        //         m_rigidbody.DOJump(destination, hopPower, 1, hopTime).OnComplete(FinishedHoping);
        //     }
        // }

        // if (Mathf.Abs(m_vertical) > 0.1f && !isHoping)
        // {
        //     RaycastHit hit;

        //     if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.3f))
        //     {
        //         isHoping = true;
        //         m_rigidbody.DOJump(transform.position, hopPower, 1, hopTime).OnComplete(FinishedHoping);
        //     }
        //     else
        //     {
        //         isHoping = true;
        //         Vector3 destination = transform.position + new Vector3(0, 0, m_vertical) * movementSpeed;
        //         m_rigidbody.DOJump(destination, hopPower, 1, hopTime).OnComplete(FinishedHoping);
        //     }
        // }
    }
    public void InputEnable(bool p_enable)
    {
        isEnabled = p_enable;
    }
}
