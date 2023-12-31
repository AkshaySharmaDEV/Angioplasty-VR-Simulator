using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    public float m_defaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;
    private LineRenderer m_LineRenderer = null;

    void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        UpdateLine();
    }

    void UpdateLine()
    {
        PointerEventData data = m_InputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ?
            0 : data.pointerCurrentRaycast.distance;
        RaycastHit hit = CreateRaycast(targetLength);
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if (hit.collider != null || (targetLength == 0))
        {
            endPosition = transform.position;
            m_Dot.SetActive(false);
            m_LineRenderer.SetPosition(0, transform.position);
            m_LineRenderer.SetPosition(1, transform.position);
        }
        else
        {
            m_Dot.SetActive(true);
            m_Dot.transform.position = endPosition;
            m_LineRenderer.SetPosition(0, transform.position);
            m_LineRenderer.SetPosition(1, endPosition);
        }
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_defaultLength);
        return hit;
    }
}
