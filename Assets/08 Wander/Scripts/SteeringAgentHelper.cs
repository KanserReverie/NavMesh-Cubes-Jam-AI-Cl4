using UnityEngine;

// for lists
using System.Collections.Generic;


namespace Steering
{
    public static class SteeringAgentHelper
    {
        // can never be changed
        const int viewDirections = 250;

        public static readonly Vector3[] directions;
        private static Vector3[] coneDirections = null;

        // Default parameters are parameters that don't need to specifically be passed in,
        // if they aren't the set value will be, used, otherwise the one passed in will be.
        // Default parameters also MUST be at the end of the parameter list.
        public static Vector3 [] DirectionsInCone(SteeringAgent _agent, bool _forceRecalulate = false)
        {
            // Determine if this function hasn't been run before 
            if (coneDirections == null || _forceRecalulate)
            {
                List<Vector3> newDirections = new List<Vector3>();

                // Look through every direction that has already been calculated in the sphere
                foreach (Vector3 direction in directions)
                {
                    // Calculate the angle between the forward of the agent
                    // and this direction ... if it is less than the view angle, we can add
                    // it to the list.
                    if(Vector3.Angle(direction, _agent.Forward) <_agent.ViewAngle)
                    {
                        newDirections.Add(direction);
                    }
                }

                // Copy the directions found into the coneDirections array.
                coneDirections = newDirections.ToArray();
            }
            return coneDirections;
        }


        static SteeringAgentHelper()
        {
            directions = new Vector3[viewDirections];

            float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
            float angleIncrement = Mathf.PI * 2 * goldenRatio;

            for (int i = 0; i < viewDirections; i++)
            {
                float t = (float)i / viewDirections;
                float inclination = Mathf.Acos(1 - 2 * t);
                float azimuth = angleIncrement * i;

                float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
                float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
                float z = Mathf.Cos(inclination);

                directions[i] = new Vector3(x, y, z);
            }
        }
    }
}