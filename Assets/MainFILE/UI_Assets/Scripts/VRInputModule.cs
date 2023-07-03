using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    public Camera m_Camera;
    public SteamVR_Input_Sources m_targetSource;
    public SteamVR_Action_Boolean m_ClickAction;

    private GameObject m_CurrentObject = null;
    private PointerEventData m_data = null;


    protected override void Awake()
    {
        base.Awake();

        m_data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        m_data.Reset();
        m_data.position = new Vector2(m_Camera.pixelWidth/2, m_Camera.pixelHeight/2);
        
        eventSystem.RaycastAll(m_data, m_RaycastResultCache);
        // Debug.Log(m_RaycastResultCache.Count);
        m_data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_data.pointerCurrentRaycast.gameObject;

        m_RaycastResultCache.Clear();
        HandlePointerExitAndEnter(m_data, m_CurrentObject);

        if(m_ClickAction.GetStateDown(m_targetSource)) ProcessPress(m_data);
        if(m_ClickAction.GetStateUp(m_targetSource)) ProcessRelease(m_data);
    }

    public PointerEventData GetData()
    {
        return m_data;
    }

    private void ProcessPress(PointerEventData a_data)
    {
        a_data.pointerPressRaycast = a_data.pointerCurrentRaycast;

        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, a_data,
            ExecuteEvents.pointerDownHandler);

        if(newPointerPress == null) 
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        a_data.pressPosition = a_data.position;
        a_data.pointerPress = newPointerPress;
        a_data.rawPointerPress = m_CurrentObject;
    }

    private void ProcessRelease(PointerEventData a_data)
    {
        ExecuteEvents.Execute(a_data.pointerPress, a_data, ExecuteEvents.pointerUpHandler);
        GameObject pointerUpHandler = ExecuteEvents.
            GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        if(a_data.pointerPress = pointerUpHandler)
        {
            ExecuteEvents.Execute(a_data.pointerPress, a_data, ExecuteEvents.pointerClickHandler);
        }

        eventSystem.SetSelectedGameObject(null);
        
        a_data.pressPosition = Vector2.zero;
        a_data.pointerPress = null;
        a_data.rawPointerPress = null;
    }
}
