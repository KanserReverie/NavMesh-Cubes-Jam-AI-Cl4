using UnityEngine;

namespace Steering
{
    [CreateAssetMenu(menuName = "Steering/Avoidance", fileName = "Avoidance")]
    public class AvoidanceBehaviour : SteeringBehaviour
    {
        [SerializeField] private float viewDistance = 1f;
        [SerializeField, Range(0.1f, 0.9f)] private float normalRatio = 0.35f;

        public override Vector3 Calculate(SteeringAgent _agent)
        {
            Vector3 force = _agent.CurrentForce;

            foreach (Vector3 dirction in SteeringAgentHelper.DirectionsInCone(_agent))
            {
                if(Physics.Raycast(_agent.Position, dirction, out RaycastHit hit, viewDistance))
                {
                    // Visualise the collision.
                    Debug.DrawLine(_agent.Position, hit.point, Color.red);

                    // Interpolate the normal by the forward over the normalRatio variable.
                    force += Vector3.Lerp(_agent.Forward, hit.normal, normalRatio);
                }
            }

            // Use the force luke.
            return force;
        }
    }
}