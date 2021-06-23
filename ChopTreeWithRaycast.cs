using UnityEngine;
using UnityEngine.UI;

public class ChopTreeWithRaycast : MonoBehaviour
{
    [Header("GameObjects")]
    public Image crosshair; //this image will turn red when u can chop tree.

    [Header("Floats")]
    public float Distance; //specify how far to hit the tree (make 1.5f)
    public float FallingSpeed; //the speed of falling (make 25)
    public float DestroyTime; //how many seconds will tree destroy (make 4)

    [Header("Varible")]
    public string TreeTag; //which tag tree contains 


    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        crosshair.color = Color.white;
        if (Physics.Raycast(transform.position, forward, out hit))
        {
            if (hit.distance <= Distance && hit.collider.gameObject.tag == TreeTag)
            {
                crosshair.color = Color.red;
                if (Input.GetMouseButtonDown(0))
                {
                    Rigidbody TreeRB;
                    TreeRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    TreeRB.isKinematic = false; //isKinematic need to be true in tree
                    TreeRB.AddForce(transform.forward * FallingSpeed);
                    DestroyTree(hit.collider.gameObject, DestroyTime);
                }
            }
        }
    }

    private void PlusTreeCount()
    {
        //write the script what u want when u chop the tree
    }

    private void DestroyTree(GameObject gb,float time)
    {
        PlusTreeCount();
        Destroy(gb, time);
    }
}
