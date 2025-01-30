using System;



//Generic Class that takes in a type of Enum
public class StateMachine<TState> where TState : Enum
{

    TState currState; //The Current State
    //=====| Constructor |====
    public StateMachine(TState initState) {
        currState = initState; //Sets a State type as the initial state for the machine
    }

    //Changed the state
    public void ChangeState(TState newState) { 
        currState = newState;
    }

    //Gets the current state of the machine
    public TState GetState() { return currState; }
}
