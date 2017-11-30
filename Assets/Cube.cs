using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum e_state
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
public class Cube : MonoBehaviour
{
    #region Public Members

    #endregion

    #region Public void
    e_state e_cube_state = e_state.STANDING;
    #endregion

    #region System

    void Awake()
    {
        

    }
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_transform = transform;
       // Debug.Log(m_transform.position);
    }
    void Update()
    {
        Debug.Log(e_cube_state);
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
            
        //if (Input.GetKeyUp(KeyCode.RightControl)) m_speed = 5f;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            e_cube_state = e_state.JUMPING;

        }

        StateAction(e_cube_state);

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
               // Debug.Log("Je saute");
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
    private void Move(float speed,bool boolJump,bool boolCrounch)
    {

        Vector3 v = Vector3.zero;
        v.x = m_transform.position.x+ Input.GetAxisRaw("Vertical") * Time.deltaTime*speed;
        v.z = m_transform.position.z+-(Input.GetAxisRaw("Horizontal") * Time.deltaTime*speed);
        v.y = m_transform.position.y;
        Debug.Log(speed);
        if (boolJump) {
            Jump(jumpForce);
        }
        if (boolCrounch)
        {
            Crounch() ;
        }
        m_transform.position=v;

    }
    private void Crounch() {
        Vector3 v = new Vector3(1f, 0.5f, 1f);
        m_transform.localScale=v;
        Debug.Log("test");
    }
    private void Stand() {
        Vector3 v = new Vector3(1f, 1f, 1f);
        m_transform.localScale = v;
        m_transform.localScale.Set(1f, 1f, 1f);
    }
    private void Jump(float jumpForce) {
        if (m_rigidbody.position.y < 0.6)
        {
            m_rigidbody.AddForce(Vector3.up * 500f);
        }
      
        
        
    }

    private void Gravity() {

        if (m_transform.position.y > 1f)
        {
            mass += 1f ;
            m_rigidbody.mass = mass;
            
        }
        //Debug.Log(m_rigidbody.position.y);
        if (m_rigidbody.position.y<=0.5 && mass>1f) {
            Debug.Log(mass);
            
            m_rigidbody.AddForce(-Vector3.up * 10f, ForceMode.Force);
            mass = 1f;
            m_rigidbody.position.Set(m_rigidbody.position.x, 0.5f, m_rigidbody.position.z);
            m_rigidbody.mass = mass;
        }/*
        if
        {
            mass = 1f;
            // m_rigidbody.AddForce(-Vector3.up * jumpForce, ForceMode.Force);
            m_rigidbody.mass = mass;
            m_rigidbody.position.Set(m_rigidbody.position.x, 0.5f, m_rigidbody.position.z);

        }*/
    }
   
    #endregion

    #region Private and Protected Members
    private Transform m_transform;
    private BoxCollider col;
    private float gravity = 14.0f;
    private float jumpForce =10.0f;
    private float m_speed=5f;
    private float verticalVelocity;
    private Rigidbody m_rigidbody;
    private float mass;
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