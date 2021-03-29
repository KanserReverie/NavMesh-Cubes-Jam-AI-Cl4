using UnityEngine;

// Means it will require these and make them if they don
[RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(MeshRenderer))]

public class Node : MonoBehaviour
{
    [SerializeField] private Color safe = Color.green;

    [SerializeField] private Color ignored = Color.red;

    // The new means it wont 'collide' with e.g. "rigidbody" is in a class wayyyy above like in object or something
    private new Rigidbody rigidbody;
    private new BoxCollider collider;
    private new MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        // gets rigid body and freezes it
        // we need a rigid body to do the colliding
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        collider = gameObject.GetComponent<BoxCollider>();
        collider.isTrigger = true;

        renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material.color = safe;

    }

    private void OnTriggerEnter(Collider _other)
    {
        TryChangeColor(_other, ignored);
    }

    private void OnTriggerStay(Collider _other)
    {
        TryChangeColor(_other, ignored);
    }

    private void OnTriggerExit(Collider _other)
    {
        TryChangeColor(_other, safe);
    }



    // Put underscores in paramaters
    private void TryChangeColor(Collider _other, Color _color)
    {
        if(_other.GetComponent<Node>() || !_other.CompareTag("PathfindingObstacle"))
        {
            return;
        }

        if(renderer == null)
        {
            renderer = gameObject.GetComponent<MeshRenderer>();
        }

        renderer.material.color = _color;
    }
}