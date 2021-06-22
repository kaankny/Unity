using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public GameObject image;
    public float Distance;
    void Update()
    {
        Vector3 ileri = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, ileri, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            if (hit.distance <= Distance && hit.collider.gameObject.tag == "Takeable")
            {
                image.SetActive(true);
            }
            else
            {
                image.SetActive(false);
            }
        }
    }
}
