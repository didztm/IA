using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerCamera : MonoBehaviour
{
    #region Public Members

    #endregion

    #region Public void
    #endregion

    #region System

    void Start ()
    {
        offset = transform.position - target.transform.position;
    }

    void Update ()
    {
        	
    }
    void LateUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = desiredPosition;
    }
    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members
    [SerializeField]
    private GameObject target;
    private Vector3 offset;
    #endregion
}
