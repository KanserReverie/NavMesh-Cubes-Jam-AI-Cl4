using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering.Testing
{
    public class Wanderer : MonoBehaviour
    {
        [SerializeField, Range(0.01f, 0.1f)] private float jitter = 0.05f;
        [SerializeField, Min(1f)] private float speed = 1f;
        [SerializeField] private float smoothing = 0.1f;

        private Vector3 currentForce = Vector3.zero;
        private Vector3 velocity = Vector3.zero;
        

        // Update is called once per frame
        void Update()
        {
            Vector3 movement = (transform.forward + (CalculateForce() * speed)) * Time.deltaTime;

            Vector3 position = Vector3.SmoothDamp(
                transform.position,
                transform.position + movement,
                ref velocity,
                smoothing);

            Quaternion rotation = Quaternion.Slerp(
                transform.localRotation,
                Quaternion.LookRotation(currentForce),
                Time.deltaTime);

            transform.localPosition = position;
            transform.localRotation = rotation;

        }

        private Vector3 CalculateForce()
        {
            // Swe start by copying the currentForce
            Vector3 force = currentForce;

            //Next we calculate the random offset
            Vector2 offset = new Vector2(Random.Range(-jitter, jitter), Random.Range(-jitter, jitter));

            // Add the offset to the horrizontal and vertical.
            // axis of the transform.
            force += transform.right * offset.x;
            force += transform.up * offset.y;

            // Make sure the force is normalized because it is a direction.
            force.Normalize();

            // Update the current force with the calculated one and return it.
            currentForce = force;
            return force;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position, currentForce);
            Gizmos.DrawWireSphere(transform.position + currentForce, 0.1f);
        }
    }
}