using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simulate a rope with verlet integration and no springs
public class RopeControllerRealisticNoSpring : MonoBehaviour
{
    public Transform startTransform;
    public Transform endTransform;
    public int numSegments = 10;
    public float ropeRadius = 0.1f;
    public Rigidbody connectedBody;

    private void Start()
    {
        GenerateRope();
    }

    private void GenerateRope()
    {
        Vector3 startPosition = startTransform.position;
        Vector3 endPosition = endTransform.position;
        Vector3 ropeDirection = (endPosition - startPosition).normalized;
        float ropeLength = Vector3.Distance(startPosition, endPosition);
        float segmentLength = ropeLength / numSegments;

        for (int i = 0; i < numSegments; i++)
        {
            Vector3 segmentPosition = startPosition + ropeDirection * (segmentLength * i);
            Quaternion segmentRotation = Quaternion.LookRotation(ropeDirection);

            GameObject ropeSegment = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            ropeSegment.transform.position = segmentPosition;
            ropeSegment.transform.rotation = segmentRotation;
            ropeSegment.transform.localScale = new Vector3(ropeRadius * 2, segmentLength / 2, ropeRadius * 2);
            ropeSegment.GetComponent<Collider>().enabled = false;

            Rigidbody segmentRigidbody = ropeSegment.AddComponent<Rigidbody>();
            segmentRigidbody.mass = 0.1f;
            segmentRigidbody.drag = 0.5f;
            segmentRigidbody.angularDrag = 0.5f;
            segmentRigidbody.useGravity = false;

            HingeJoint joint = ropeSegment.AddComponent<HingeJoint>();
            joint.connectedBody = connectedBody;
            joint.axis = Vector3.right;
            joint.anchor = Vector3.zero;
            joint.connectedAnchor = Vector3.zero;
        }
    }
}