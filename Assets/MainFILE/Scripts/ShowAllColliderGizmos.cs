using UnityEngine;

public class ShowAllColliderGizmos : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Collider[] colliders = FindObjectsOfType<Collider>();

        foreach (Collider collider in colliders)
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = collider.transform.localToWorldMatrix;

            if (collider is BoxCollider boxCollider)
            {
                Gizmos.DrawWireCube(boxCollider.center, boxCollider.size);
            }
            else if (collider is SphereCollider sphereCollider)
            {
                Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius);
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                Vector3 center = capsuleCollider.center;
                Vector3 point1 = center + Vector3.up * (capsuleCollider.height * 0.5f - capsuleCollider.radius);
                Vector3 point2 = center - Vector3.up * (capsuleCollider.height * 0.5f - capsuleCollider.radius);

                Gizmos.DrawWireSphere(point1, capsuleCollider.radius);
                Gizmos.DrawWireSphere(point2, capsuleCollider.radius);
                Gizmos.DrawLine(point1, point2);
            }
            else if (collider is MeshCollider meshCollider)
            {
                Gizmos.DrawWireMesh(meshCollider.sharedMesh);
            }
        }
    }
}
