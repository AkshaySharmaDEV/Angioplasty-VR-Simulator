using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class LaserInput : MonoBehaviour
{
    public static GameObject currentObject;
    int currentID;

    void Start()
    {
        currentObject = null;
        currentID = 0;
    }

    void Update()
    {
        // Sends out a Raycast and returns an array filled with everything it hit
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);

        // Goes through all the hits and checks if any of them were our button
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            // I use the object's ID to determine if I have already run the code for this object
            int id = hit.collider.gameObject.GetInstanceID();

            // If the ID is different, then I will run it again, but if I have, it is unnecessary to keep running it
            if (currentID != id)
            {
                currentID = id;
                currentObject = hit.collider.gameObject;

                // Checks based on the name
                string name = currentObject.name;
                if (name == "Next")
                {
                    UnityEngine.Debug.Log("HIT NEXT");
                }

                // Checks based on the tag
                string tag = currentObject.tag;
                if (tag == "Button")
                {
                    UnityEngine.Debug.Log("HIT BUTTON");
                }
            }
        }
    }
}
