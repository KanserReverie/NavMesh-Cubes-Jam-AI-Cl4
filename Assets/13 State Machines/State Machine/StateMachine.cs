using System.Collections.Generic;
using UnityEngine;

namespace StateMachines
{
    public enum States
    {
        Translate, 
        Rotate, 
        Scale
    }

    // The delegate that dictates what the fuctions for each state will look like.
    public delegate void StateDelegate();

    public class StateMachine : MonoBehaviour
    {
        private Dictionary<States, StateDelegate> states = new Dictionary<States, StateDelegate>();

        [SerializeField] private States currentState = StateMachines.States.Translate;
        [SerializeField] private Transform controlled; // The thing that will be affected by our statemachine.
        [SerializeField] private float speed = 1f; // This isn't really in a statemachine, this is just for testing.

        // This is used to change states from anywhere within the code that has reference
        // to the StateMachine
        public void ChangeState(States _newState)
        {
            if (_newState != currentState)
            {
                currentState = _newState;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // This is the same as checking if the variable is null, then setting it, otherwise retain the value.
            controlled ??= transform;


            states.Add(States.Translate, Translate);
            states.Add(States.Rotate, Rotate);
            states.Add(States.Scale, Scale);
        }

        // Update is called once per frame
        void Update()
        {
            // These two lines are what actually runs the state machine.
            // It works by attempting to retrieve the relevate function for the current state,
            // then tunning the function if it successfully fout it.
            if(states.TryGetValue(currentState, out StateDelegate state))
            {
                state.Invoke();
            }
            else
            {
                Debug.LogError($"No state fuction set for state {currentState}.");
            }
        }

        // The function that will run when we are in the Translate state.
        private void Translate() => controlled.position += transform.forward * Time.deltaTime * speed;
        private void Rotate() => controlled.Rotate(Vector3.up, speed * 0.5f);
        private void Scale() => controlled.localScale += Vector3.one * Time.deltaTime * speed;
    }
}