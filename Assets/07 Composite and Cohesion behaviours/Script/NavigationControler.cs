using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationControler : MonoBehaviour
{

    [SerializeField]
    private List<NavMeshAgent> agents = new List<NavMeshAgent>();
    [SerializeField]
    private List<NavMeshModifierVolume> waterVolumes = new List<NavMeshModifierVolume>();
    [SerializeField]
    private NavMeshSurface robotSurface;

    private new Camera camera;

    // Start is called before the first frame update
    void Start() => camera = gameObject.GetComponent<Camera>();

    // Update is called once per frame
    void Update()
    {
        // if the left mouse button was pressed
        if(Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the world using the mouse position
            if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                // Make all agents move to this position
                agents.ForEach(agent => agent.SetDestination(hit.point));
            }
        }

        // If the space key was pressed yo
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Loop through all SurfaceModifiers and inver their active states
            waterVolumes.ForEach(volume => volume.enabled = !volume.enabled);
            // Rebuild the navmesh
            robotSurface.BuildNavMesh();
        }
    }
}
