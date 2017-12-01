using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Public Members

    #endregion

    #region Public void
    e_state e_cube_state = e_state.STANDING;
    #endregion

    #region System

    void Awake()
    {

        m_rigidbody = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();
    }
    private void Start()
    {
        
    }
    void Update()
    {
        if (debug)
        {
            Debug.Log(e_cube_state);
        }
        ManageInput();
        StateAction(e_cube_state);
        Debug.Log("Update_end():" + m_transform.position);
    }

    #endregion

    #region Tools Debug and Utility
    private void StateAction(e_state state)
    {
        switch (state)
        {
            case e_state.STANDING:
                //Debug.Log("Je suis debout");
                break;
            case e_state.DEAD:
                //Debug.Log("Je suis mort");
                break;
            case e_state.CROUNCHING:
                // Debug.Log("Je m'abaisse");
                Move(m_speed, false, true);
                break;
            case e_state.JUMPING:
               if(debug) Debug.Log("Je saute");
                Move(m_speed, true, false);
                e_cube_state = e_state.MOVING;
                break;
            case e_state.MOVING:
                //Debug.Log("Je bouge");
                m_speed = 5f;
                Move(m_speed, false, false);
                break;
            case e_state.RUNNING:
                //Debug.Log("Je cours");
                m_speed = 50f;
                Move(m_speed,false,false);
                break;

            default:
                break;
        }

    }
    private void ManageInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            e_cube_state = e_state.JUMPING;
        }
        if (Input.GetKeyUp(KeyCode.RightControl))
            e_cube_state = e_state.MOVING;
        if (Input.GetKey(KeyCode.RightControl) && e_cube_state == e_state.MOVING)
            e_cube_state = e_state.RUNNING;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            e_cube_state = e_state.MOVING;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            e_cube_state = e_state.MOVING;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            e_cube_state = e_state.MOVING;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            e_cube_state = e_state.MOVING;
        if (Input.GetKeyDown(KeyCode.RightShift))
            e_cube_state = e_state.CROUNCHING;
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            Stand();
            e_cube_state = e_state.MOVING;
        }
    }
    private void Move(float speed,bool boolJump,bool boolCrounch)
    {
        m_t_position = m_transform.position;
        Vector3 v = Vector3.zero;
        v.x = m_t_position.x+ Input.GetAxisRaw("Vertical") * Time.deltaTime*speed;
        v.z = m_t_position.z+-(Input.GetAxisRaw("Horizontal") * Time.deltaTime*speed);
        v.y = m_t_position.y;
        if (debug)Debug.Log(boolJump);
        if (boolJump) {
            Jump(jumpForce);
        }
        if (boolCrounch)
        {
            Crounch() ;
        }
        m_transform.position=v;

    }
    private void Crounch(bool _debug=false) {
        Vector3 v = new Vector3(1f, 0.5f, 1f);
        m_transform.localScale=v;
    }
    private void Stand(bool _debug = false) {
        Vector3 v = new Vector3(1f, 1f, 1f);
        m_transform.localScale = v;
        m_transform.localScale.Set(1f, 1f, 1f);
    }
    private void Jump(float jumpForce) {
        
        if (Isgrounded())
        {
            if (debug) Debug.Log("Jump2");
            m_rigidbody.AddForce(Vector3.up * 500000f);
        } 
    }
    private bool Isgrounded() {
        Vector3 vo = transform.position;
        Vector3 vd = new Vector3(0f, -0.1f, 0f);
        RaycastHit hit;
        if(debug)Debug.DrawRay(vo,vd,Color.red);
        if (Physics.Raycast(vo, vd, out hit, 0.6f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }

    #endregion

    #region Private and Protected Members
    private enum e_state
    {
        INVALID = -1,
        JUMPING,
        ROTATING,
        CROUNCHING,
        DEAD,
        STANDING,
        RUNNING,
        MOVING
    }
    private Transform m_transform;
    private BoxCollider col;
    private float gravity = 14.0f;
    private float jumpForce =10.0f;
    private float m_speed=5f;
    private float verticalVelocity;
    private Rigidbody m_rigidbody;
    private float mass;
    private bool debug=true;
    private Vector3 m_t_position;
    #endregion
}
/*
  Movement axe
  ------------
     Vertical
         X U
         1     
 L -1    0   1 R   Horizontal
 Z       -1
         D

    Gravity
    -------
gravity = (0.0,-9.8,0.0)
Position= (1.0,0.5,0.0)



     */