using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour
{
    #region Public Members

    #endregion

    #region Public void

    #endregion

    #region System
    private void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
       Vector3 vo = new Vector3(m_transform.position.x+1.5f, m_transform.transform.position.y, m_transform.transform.position.z);
       Gizmos.DrawSphere(vo, 1f);

    }
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_transform = transform;
        m_col = GetComponent<Collider>();
        m_agent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        if (m_debug)
        {
            Debug.Log(e_ennemy_state);
        }
        StateAction(e_ennemy_state);
       
        
    }

    #endregion

    #region Tools Debug and Utility
    //detection du joueur
    
    //search
    //moving
    private void StateAction(e_state state)
    {
        switch (state)
        {
            case e_state.SEARCHING:
                e_ennemy_state = PlayerSearching(m_debug);
                break;
            case e_state.SPOTTED:
                PlayerSpotted();
                e_ennemy_state = e_state.SEARCHING;//
                break;
            case e_state.JUMPING:
                break;
            case e_state.MOVING:
                break;
            case e_state.RUNNING:
                break;

            default:
                break;
        }

    }
   
    private e_state PlayerSearching(bool _debug = false)
    {
        //m_col.bounds.extents -> donne la position de l'objet 

        
        Vector3 vo = new Vector3(m_transform.position.x, m_transform.transform.position.y, m_transform.transform.position.z);
        Vector3 vd = new Vector3(0.5f, 0f, 0f);
        Vector3 vdsphere = new Vector3(0.09f, 0f, 0f);
        Ray aim = new Ray(vo, vd);
        Ray aimSphere = new Ray(vo, vdsphere);
        if (_debug)
        {
            //Debug.DrawRay(vo, vd, Color.green);
           // Debug.Log("Vecteur origine ray:" + vo);
           // Debug.Log("Vecteur destination ray:" + vd);
            // Debug.Log(m_col.Raycast(aim, out hit)); //
            Debug.Log(Physics.SphereCast(aimSphere,1f, out hit));
            Debug.DrawRay(vo, vdsphere, Color.blue);
           // Debug.DrawRay(vo, vd, Color.green);
            
        }
        //Vue au alentour
        if (Physics.SphereCast(aimSphere, 10f, out hit)) {
            if (hit.collider.name == "Cube")
            {
              //  return e_state.SPOTTED;
            }
        }
        //vue de loin
        /*
        if (Physics.Raycast(vo, vd, out hit, 10f))
        {
            if (hit.collider.name == "Cube")
            {
                return e_state.SPOTTED;
            }
        }*/

        return e_state.SEARCHING;



    }

    private void PlayerSpotted()
    {
        PlayerFollow();
    }
    private void PlayerFollow() {
        Vector3 v = hit.transform.position;
        m_agent.destination = v;
    }
    private void Move(float speed, bool boolJump, bool boolCrounch)
    {


    }
    private void Crounch()
    {
        Vector3 v = new Vector3(1f, 0.5f, 1f);
        m_transform.localScale = v;
        Debug.Log("test");
    }
    private void Stand()
    {
        Vector3 v = new Vector3(1f, 1f, 1f);
        m_transform.localScale = v;
        m_transform.localScale.Set(1f, 1f, 1f);
    }
    private void Jump(float jumpForce)
    {
        if (m_rigidbody.position.y < 0.6)
        {
            m_rigidbody.AddForce(Vector3.up * 500f);
        }



    }
    #endregion

    #region Private and Protected Members
    private enum e_state
    {
        INVALID = -1,
        SEARCHING,
        SPOTTED,
        MOVING,
        JUMPING,
        CROUNCHING,
        STANDING,
        RUNNING,
        WALKING
    }
    private Transform m_transform;
    private Rigidbody m_rigidbody;
    private Collider m_col;
    private float jumpForce = 10.0f;
    private float m_speed = 5f;
    private float alertDist;
    private float attackDist;
    private float walkingDist;
    private bool m_debug = true;
    private e_state e_ennemy_state = e_state.SEARCHING;
    private RaycastHit hit = new RaycastHit();
    private RaycastHit sphereHit = new RaycastHit();
    private NavMeshAgent m_agent;
    #endregion
}
